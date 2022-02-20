import Router from 'koa-router'
import { app } from '../app'
import { removeLeadingAndTrailingSlashes } from '../utils'

export function Controller(path = '') {
  return function _Controller<T extends { new (...args: any[]): any }>(
    constr: T
  ) {
    const router = new Router()
    const base = removeLeadingAndTrailingSlashes(path)
    for (const key of Object.getOwnPropertyNames(constr.prototype)) {
      if (key !== 'constructor') {
        const routeHandler = constr.prototype[key]
        const path = Reflect.getMetadata('path', constr.prototype, key)
        router.get(`/${base}/${path}`, async (ctx) => await routeHandler(ctx))
      }
    }
    app.use(router.routes())
    return constr
  }
}
