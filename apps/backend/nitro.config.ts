import { defineConfig } from "nitro";

const compatibilityDate = '2026-03-23';

export default defineConfig({
  serverDir: './server',
  buildDir: '../../dist/backend/.nitro',
  output: {
    dir: '../../dist/backend/.output'
  },
  compatibilityDate,
  runtimeConfig: {
    databaseUrl: ''
  }
});
