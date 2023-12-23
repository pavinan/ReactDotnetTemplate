import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'

export interface Todo {
    id: string
    title: string
    description: string
}


export const todosAPI = createApi({
    baseQuery: fetchBaseQuery({ baseUrl: 'api' }),
    tagTypes: ['todos'],
    endpoints: (builder) => ({
        getTodos: builder.query<Todo[], void>({
            query: () => `todos`,
            providesTags: ['todos']
        }),
        addTodo: builder.mutation<Todo, Partial<Todo>>({
            query: (body) => ({
                url: `todos`,
                method: 'POST',
                body,
            }),
            invalidatesTags: ['todos']
        }),
        deleteTodo: builder.mutation<{ id: string }, { id: string }>({
            query: ({ id }) => ({
                url: `todos/${id}`,
                method: 'DELETE',
            }),
            invalidatesTags: ['todos']
        }),
    }),
})

// Export hooks for usage in functional components
export const { useGetTodosQuery, useAddTodoMutation, useDeleteTodoMutation } = todosAPI
