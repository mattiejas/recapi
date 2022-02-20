import { PrismaClient } from '@prisma/client'
import { IRouterContext } from 'koa-router'
import { Method, Route, Controller } from '../decorations'

const prisma = new PrismaClient()

@Controller('/recipes')
export default class RecipesController {
  @Route(Method.GET)
  async getRecipes(context: IRouterContext) {
    const recipes = await prisma.recipe.findMany()
    context.body = recipes
  }

  @Route(Method.POST)
  async createRecipe(context: IRouterContext) {
    const { title } = context.request.body
    const recipe = await prisma.recipe.create({
      data: {
        title,
      },
    })
    context.body = recipe
  }
}
