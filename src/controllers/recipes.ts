import { PrismaClient } from '@prisma/client'
import { IRouterContext } from 'koa-router'

const prisma = new PrismaClient()

async function getRecipes(context: IRouterContext) {
  const recipes = await prisma.recipe.findMany()
  context.body = recipes
}

async function createRecipe(context: IRouterContext) {
  const { title } = context.request.body
  const recipe = await prisma.recipe.create({
    data: {
      title,
    },
  })
  context.body = recipe
}

export { getRecipes, createRecipe }
