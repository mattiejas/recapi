import Koa from 'koa'
import { Method, Route, Controller } from '../decorations'

@Controller('/hello')
export default class HelloWorldController {
  @Route(Method.GET, '/world')
  helloWorld(ctx: Koa.Context) {
    ctx.body = { hello: 'world' }
  }

  @Route(Method.GET, '/')
  hello(ctx: Koa.Context) {
    ctx.body = { hello: null }
  }
}
