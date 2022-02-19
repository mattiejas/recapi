import Koa from 'koa'
import Router from '@koa/router'

const app = new Koa()
const router = new Router()

router.get('/', async (ctx) => {
  ctx.body = { hello: 'world' }
})

app.use(router.routes())

const port = 3000
app.listen(port)
console.info(`Server running on port ${port}`)
