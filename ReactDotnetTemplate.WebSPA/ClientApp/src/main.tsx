import React from 'react'
import ReactDOM from 'react-dom/client'
import { Provider } from 'react-redux'
import {
    createBrowserRouter,
    RouterProvider,
} from "react-router-dom";
import App from './App'
import './index.css'

import { store } from './store';
import Home from './pages/Home';
import Todos from './pages/Todos';


const router = createBrowserRouter([
    {
        path: "/",
        element: <App />,
        children: [
            {
                path: "/",
                element: <Home />,
            },
            {
                path: "/todos",
                element: <Todos />,
            },
        ],
    },
]);

ReactDOM.createRoot(document.getElementById('root')!).render(
    <React.StrictMode>
        <Provider store={store}>
            <RouterProvider router={router} />
        </Provider>
    </React.StrictMode>,
)
