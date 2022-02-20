import Koa from 'koa'
import bodyParser from 'koa-bodyparser'
import { exceptions } from './middleware'

const app = new Koa()
app.use(bodyParser())
app.use(exceptions())

export { app }
