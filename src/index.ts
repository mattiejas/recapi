import Koa from 'koa'
import bodyParser from 'koa-bodyparser'
import { exceptions } from './middleware/exceptions'
import getHelloWorldRouter from './routes/hello-world'
import getRecipesRouter from './routes/recipes'

const app = new Koa()

app.use(bodyParser())
app.use(exceptions())
app.use(getHelloWorldRouter())
app.use(getRecipesRouter())

const port = 3000
app.listen(port)
console.info(`Server running on port ${port}`)
