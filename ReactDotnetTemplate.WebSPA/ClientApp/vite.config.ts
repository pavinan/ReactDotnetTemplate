import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react-swc'

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [react()],
    resolve: {
        alias:{
            "path":"path-browserify"
        }
    },
    server: {
        port: 5067,
        strictPort: true
    },
    build: {
        outDir: '../wwwroot',
    }
})
