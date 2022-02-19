import Koa from "koa";
const app = new Koa();

app.use(async (ctx) => {
  ctx.body = "Hello, world!";
});

const port = 3000;
app.listen(port);
console.info(`Server running on port ${port}`);
