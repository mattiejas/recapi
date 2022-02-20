import 'reflect-metadata'
import './controllers'

import bodyParser from 'koa-bodyparser'
import { exceptions } from './middleware/exceptions'
import getRecipesRouter from './routes/recipes'
import { app } from './app'
import { env } from 'process'

app.use(bodyParser())
app.use(exceptions())
app.use(getRecipesRouter())

const port = env.PORT || 3000
app.listen(port)

console.info(`Server running on port ${port}.`)
console.info('Press Ctrl+C to quit.')
