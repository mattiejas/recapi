import { PrismaClient } from '@prisma/client'
import { IRouterContext } from 'koa-router'
import { Inject, Service } from 'typedi'
import { Method, Route, Controller } from '../decorations'
import { PRISMA_CLIENT } from '../app'

@Controller('/recipes')
@Service()
export default class RecipesController {
  constructor(@Inject(PRISMA_CLIENT) private readonly prisma: PrismaClient) {}

  @Route(Method.GET)
  async getRecipes(context: IRouterContext) {
    console.log(this, this.prisma)
    const recipes = await this.prisma.recipe.findMany()
    context.body = recipes
  }

  @Route(Method.POST)
  async createRecipe(context: IRouterContext) {
    const { title } = context.request.body
    const recipe = await this.prisma.recipe.create({
      data: {
        title,
      },
    })
    context.body = recipe
  }
}
