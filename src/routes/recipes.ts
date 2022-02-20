import Router from "koa-router"
import { createRecipe, getRecipes } from "../controllers/recipes"

export default function getRecipesRouter() {
  const router = new Router()
  router.get('/recipes', getRecipes)
  router.post('/recipes', createRecipe)
  return router.routes()
}
