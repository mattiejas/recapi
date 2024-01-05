use anyhow::anyhow;
use log::info;
use chromiumoxide::{Browser, BrowserConfig};
use tokio::task::JoinHandle;
use recapi::error::{AppError, AppResult};
use futures::StreamExt;

#[tokio::main]
async fn main() {
    tracing_subscriber::fmt::init();
    info!("Starting scraper...");

    let url = "https://www.jumbo.com/recepten/gebakken-aardappelen-met-appelmoes-en-kip-504543";

    // create a browser instance
    let (mut browser, handle) = create_browser().await.expect("failed to create browser");

    // parse the page
    let page = parse_page(&browser, url).await;

    if let Ok(page) = page {
        info!("Successfully parsed page: {:#?}", page);
    }

    // close the browser and wait for the handler to finish
    browser.close().await.expect("failed to close browser");
    handle.await.expect("failed to join handler");
}

async fn create_browser() -> AppResult<(Browser, JoinHandle<()>)> {
    let config = BrowserConfig::builder().with_head().build()
        .map_err(|_| AppError::AnyhowError(anyhow!("Failed to build browser config")))?;

    let (browser, mut handler) = Browser::launch(config).await
        .map_err(|_| AppError::AnyhowError(anyhow!("Failed to launch browser")))?;

    let handle = tokio::spawn(async move {
        loop {
            if handler.next().await.is_none() {
                break;
            }
        }
    });

    Ok((browser, handle))
}

#[derive(Debug)]
struct RecipePage {
    title: String,
    description: String,
}

async fn parse_page(browser: &Browser, url: &str) -> AppResult<RecipePage> {
    let page = browser.new_page(url).await
        .map_err(|_| AppError::AnyhowError(anyhow!("Failed to create new page")))?;

    let title = get_text_for_xpath(&page, r#"//*[@id="__nuxt"]/div/div[2]/div/article/div[1]/div/div[1]/h1/strong"#)
        .await
        .map_err(|_| AppError::AnyhowError(anyhow!("Failed to find text")))?;

    let description = get_text_for_xpath(&page, r#"//*[@id="__nuxt"]/div/div[2]/div/article/div[1]/div/p[2]"#)
        .await
        .map_err(|_| AppError::AnyhowError(anyhow!("Failed to find description")))?;

    Ok(RecipePage {
        title,
        description,
    })
}

async fn get_text_for_xpath(page: &chromiumoxide::Page, xpath: &str) -> AppResult<String> {
    let selector = page.find_xpath(xpath).await
        .map_err(|_| AppError::AnyhowError(anyhow!("Failed to find xpath")))?;

    let text = selector.inner_text().await
        .map_err(|_| AppError::AnyhowError(anyhow!("Failed to get inner text")))?;

    match text {
        Some(text) => Ok(text),
        None => Err(AppError::AnyhowError(anyhow!("Failed to get text"))),
    }
}