-- Your SQL goes here
CREATE TABLE recipes
(
    id          SERIAL PRIMARY KEY,
    name        TEXT NOT NULL,
    description TEXT NOT NULL
);

CREATE TABLE ingredients
(
    id   SERIAL PRIMARY KEY,
    name TEXT NOT NULL
);

CREATE TABLE recipe_ingredients
(
    id            SERIAL PRIMARY KEY,
    recipe_id     INTEGER REFERENCES recipes (id) ON DELETE CASCADE NOT NULL,
    ingredient_id INTEGER REFERENCES ingredients (id) ON DELETE CASCADE NOT NULL,
    quantity      INTEGER NOT NULL,
    unit          TEXT    NOT NULL
);