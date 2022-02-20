
import { IRouterContext } from 'koa-router'

async function getHelloWorld(context: IRouterContext) {
  context.body = { hello: 'world' }
}

export { getHelloWorld }
