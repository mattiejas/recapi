import Router from "koa-router"
import { getHelloWorld } from "../controllers/hello-world"

export default function getHelloWorldRouter() {
  const router = new Router()
  router.get('/hello-world', getHelloWorld)
  return router.routes()
}
