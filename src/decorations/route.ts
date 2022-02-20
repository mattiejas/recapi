import { removeLeadingAndTrailingSlashes } from '../utils'

export enum Method {
  GET = 'GET',
  POST = 'POST',
  PUT = 'PUT',
  DELETE = 'DELETE',
  PATCH = 'PATCH',
}

export function Route(method: Method, path = ''): MethodDecorator {
  return (target, key, descriptor: PropertyDescriptor) => {
    Reflect.defineMetadata(
      'path',
      removeLeadingAndTrailingSlashes(path),
      target,
      key
    )
    Reflect.defineMetadata('method', method, target, key)
  }
}
