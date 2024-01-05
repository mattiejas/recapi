use axum::http::StatusCode;
use axum::Json;
use axum::response::{IntoResponse, Response};

#[derive(Debug)]
pub enum AppError {
    DatabaseConnectionError(diesel::r2d2::PoolError),
    DatabaseError(diesel::result::Error),
    NotFound,
    AnyhowError(anyhow::Error),
}

impl IntoResponse for AppError {
    fn into_response(self) -> Response {
        #[derive(serde::Serialize)]
        struct ErrorMessage {
            message: String,
            error_type: String,
            status_code: u16,
        }

        let error_type = match self {
            AppError::DatabaseConnectionError(_) => "DatabaseConnectionError",
            AppError::DatabaseError(_) => "DatabaseError",
            AppError::NotFound => "NotFound",
            AppError::AnyhowError(_) => "InternalError",
        };

        let status_code = match self {
            AppError::NotFound => StatusCode::NOT_FOUND,
            _ => StatusCode::INTERNAL_SERVER_ERROR,
        };

        let message = match self {
            AppError::NotFound => "Not Found".to_string(),
            e => format!("{:?}", e),
        };

        let json = Json(ErrorMessage {
            message,
            error_type: error_type.to_string(),
            status_code: status_code.as_u16(),
        });

        (status_code, json).into_response()
    }
}