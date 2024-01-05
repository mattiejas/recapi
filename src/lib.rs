pub mod schema;
pub mod models;
pub mod routes;
pub mod error;

use std::env;
use diesel::{PgConnection};
use diesel::r2d2::{ConnectionManager, Pool};
use dotenvy::dotenv;
use anyhow::Result;

#[derive(Clone)]
pub struct AppState {
    pub pool: Pool<ConnectionManager<PgConnection>>,
}

pub fn get_connection_pool() -> Result<Pool<ConnectionManager<PgConnection>>> {
    dotenv().ok();

    let database_url = env::var("DATABASE_URL")?;

    let config = ConnectionManager::<PgConnection>::new(database_url);
    let pool = Pool::builder().build(config)?;

    Ok(pool)
}