// @generated automatically by Diesel CLI.

diesel::table! {
    ingredients (id) {
        id -> Int4,
        name -> Text,
    }
}

diesel::table! {
    recipe_ingredients (id) {
        id -> Int4,
        recipe_id -> Int4,
        ingredient_id -> Int4,
        quantity -> Int4,
        unit -> Text,
    }
}

diesel::table! {
    recipes (id) {
        id -> Int4,
        name -> Text,
        description -> Text,
    }
}

diesel::joinable!(recipe_ingredients -> ingredients (ingredient_id));
diesel::joinable!(recipe_ingredients -> recipes (recipe_id));

diesel::allow_tables_to_appear_in_same_query!(
    ingredients,
    recipe_ingredients,
    recipes,
);
