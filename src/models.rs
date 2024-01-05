use diesel::{Insertable, Queryable, Selectable};
use serde::{Deserialize, Serialize};

#[derive(Queryable, Selectable, Serialize)]
#[diesel(table_name = crate::schema::recipes)]
pub struct Recipe {
    pub id: i32,
    pub name: String,
    pub description: String,
}

#[derive(Insertable, Deserialize, Debug)]
#[diesel(table_name = crate::schema::recipes)]
pub struct NewRecipe {
    pub name: String,
    pub description: String,
}

#[derive(Queryable, Selectable)]
#[diesel(table_name = crate::schema::ingredients)]
pub struct Ingredient {
    pub id: i32,
    pub name: String,
}

#[derive(Queryable, Selectable)]
#[diesel(table_name = crate::schema::recipe_ingredients)]
pub struct RecipeIngredient {
    pub id: i32,
    pub recipe_id: i32,
    pub ingredient_id: i32,
    pub quantity: f32,
    pub unit: String,
}