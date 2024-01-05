use axum::routing::get;
use log::{error, info};
use recapi::{AppState, get_connection_pool};


#[tokio::main]
async fn main() {
    tracing_subscriber::fmt::init();
    info!("Starting recapi...");

    let pool = match get_connection_pool() {
        Ok(pool) => pool,
        Err(e) => {
            error!("Failed to connect to database: {}", e);
            std::process::exit(1);
        }
    };

    let state = AppState { pool };

    let app = axum::Router::new()
        .route("/", get(|| async { "OK" }))
        .merge(recapi::routes::recipes::router())
        .with_state(state);

    let listener = tokio::net::TcpListener::bind("0.0.0.0:3000").await.unwrap();

    info!("listening on :{}", listener.local_addr().unwrap().port());
    axum::serve(listener, app).await.unwrap();
}
