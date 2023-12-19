import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'

export interface Todo {
    id: string
    title: string
    description: string
}


export const todosAPI = createApi({
    baseQuery: fetchBaseQuery({ baseUrl: 'api' }),
    tagTypes: [],
    endpoints: (builder) => ({
        getTodos: builder.query<Todo[], void>({
            query: () => `todos`,
        }),
    }),
})

// Export hooks for usage in functional components
export const { useGetTodosQuery } = todosAPI
