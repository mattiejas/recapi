import Koa from 'koa'
import { Method, Route, Controller } from '../decorations'

@Controller('/hello')
export default class HelloWorldController {
  @Route(Method.GET, '/world')
  test(ctx: Koa.Context) {
    ctx.body = { hello: 'world' }
  }

  @Route(Method.GET, '/')
  test2(ctx: Koa.Context) {
    ctx.body = { hello: null }
  }
}
