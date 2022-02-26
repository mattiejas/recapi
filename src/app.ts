import { PrismaClient } from '@prisma/client'
import Koa from 'koa'
import bodyParser from 'koa-bodyparser'
import Container from 'typedi'
import { exceptions } from './middleware'

// inject prisma client
export const PRISMA_CLIENT = 'PRISMA_CLIENT'
Container.set(PRISMA_CLIENT, new PrismaClient())

const app = new Koa()
app.use(bodyParser())
app.use(exceptions())

export { app }
