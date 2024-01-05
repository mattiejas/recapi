use axum::http::StatusCode;
use axum::{debug_handler, Json, Router};
use axum::extract::State;
use axum::routing::{get, post};
use diesel::RunQueryDsl;
use diesel::associations::HasTable;
use log::{error, info};
use crate::{AppState, schema};
use crate::error::AppError;
use crate::models::{NewRecipe, Recipe};
use crate::schema::recipes::dsl::recipes;

pub fn router() -> Router<AppState> {
    Router::new()
        .route("/recipes", get(get_recipes))
        .route("/recipes", post(create_recipe))
}

async fn get_recipes(State(state): State<AppState>) -> Result<Json<Vec<Recipe>>, StatusCode> {
    let pool = state.pool.clone();
    let mut conn = pool.get()
        .or_else(|_| Err(StatusCode::INTERNAL_SERVER_ERROR))?;

    let entities = recipes::table().load::<Recipe>(&mut conn)
        .or_else(|_| Err(StatusCode::INTERNAL_SERVER_ERROR))?;

    Ok(Json(entities))
}

#[debug_handler]
async fn create_recipe(
    State(state): State<AppState>,
    Json(payload): Json<NewRecipe>,
) -> Result<Json<Recipe>, AppError> {
    let pool = state.pool.clone();
    let mut conn = pool.get()
        .map_err(|e| {
            error!("Failed to get connection from pool: {}", e);
            AppError::DatabaseConnectionError(e)
        })?;

    info!("Creating recipe: {:#?}", payload);

    let entity = diesel::insert_into(schema::recipes::table)
        .values(&payload)
        .get_result::<Recipe>(&mut conn)
        .map_err(|e| {
            error!("Failed to create recipe: {}", e);
            AppError::DatabaseError(e)
        })?;

    return Ok(Json(entity));
}