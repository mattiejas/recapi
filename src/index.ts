import Koa from 'koa'
import Router from 'koa-router'
import { PrismaClient } from '@prisma/client'
import bodyParser from 'koa-bodyparser'
import { exceptions } from './middleware/exceptions'

const app = new Koa()
const router = new Router()
const prisma = new PrismaClient()

app.use(bodyParser()).use(exceptions())

// hello world endpoint
router.get('/', async (ctx) => {
  ctx.body = { hello: 'world' }
})

// fetch all recipes
router.get('/recipes', async (ctx) => {
  const recipes = await prisma.recipe.findMany()
  ctx.body = recipes
})

// create a new recipe
router.post('/recipes', async (ctx) => {
  const { title } = ctx.request.body
  const recipe = await prisma.recipe.create({
    data: {
      title,
    },
  })
  ctx.body = recipe
})

const port = 3000
app.use(router.routes()).listen(port)
console.info(`Server running on port ${port}`)
