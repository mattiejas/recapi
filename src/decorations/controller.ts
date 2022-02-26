import Router from 'koa-router'
import Container from 'typedi'
import { Method } from '.'
import { app } from '../app'
import { removeLeadingAndTrailingSlashes } from '../utils'

type Methods = 'get' | 'post' | 'put' | 'delete' | 'patch'

export function Controller(path = '') {
  return function _Controller<T extends { new (...args: any[]): any }>(
    constr: T
  ) {
    const instance = Container.get(constr)
    const router = new Router()
    const base = removeLeadingAndTrailingSlashes(path)
    for (const key of Object.getOwnPropertyNames(constr.prototype)) {
      if (key !== 'constructor') {
        const routeHandler = constr.prototype[key]
        const path = Reflect.getMetadata('path', constr.prototype, key)
        const method = Reflect.getMetadata(
          'method',
          constr.prototype,
          key
        ) as Method

        const m = method.toLowerCase() as Methods

        // register with and without trailing slash
        router[m](
          `/${base}/${path}`,
          async (ctx) => await routeHandler.bind(instance)(ctx)
        )
        router[m](
          `/${base}/${path}/`,
          async (ctx) => await routeHandler.bind(instance)(ctx)
        )
      }
    }
    app.use(router.routes())
    return constr
  }
}
