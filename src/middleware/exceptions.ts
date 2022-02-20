import Koa from 'koa'

export const exceptions = (): Koa.Middleware => async (ctx, next) => {
  try {
    await next()
  } catch (err: any) {
    ctx.status = 400
    ctx.body = {
      status: 400,
      message: err.message,
    }
    console.log('Error handler:', err.message)
  }
}
