# recapi

## Prerequisites

- Rust (https://rustup.rs/)
- PostgreSQL (https://www.postgresql.org/download/)
- Diesel CLI (https://diesel.rs/guides/getting-started/)

## Setup

1. Clone the repository
2. Create a `.env` file in the root directory of the project and add the following environment variables:
```
DATABASE_URL=postgres://<username>:<password>@localhost/recapi
```

3. Run `diesel setup` to create the database
4. Run `diesel migration run` to run the migrations

## Running
1. Run `cargo run --bin api` to start the server
2. Run `cargo run --bin scrape` to start the scraper
