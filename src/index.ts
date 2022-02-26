import 'reflect-metadata'

import { app } from './app'
import { env } from 'process'

import './controllers'

const port = env.PORT || 3000
app.listen(port)

console.info(`Server running on port ${port}.`)
console.info('Press Ctrl+C to quit.')
