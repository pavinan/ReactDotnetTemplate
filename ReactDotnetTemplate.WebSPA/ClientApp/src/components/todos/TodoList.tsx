import { useGetTodosQuery, useDeleteTodoMutation, Todo } from '../../services/todoservice';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faTrash } from '@fortawesome/free-solid-svg-icons'

function TodoItem({ todo }: { todo: Todo }) {

    const [deleteTodo, { isLoading }] = useDeleteTodoMutation();

    return (
        <div className='relative flex flex-col bg-slate-50 rounded-lg p-6 dark:bg-slate-800 dark:highlight-white/5 my-2 w-[30rem]'>
            <h3 className='text-base text-slate-900 font-semibold dark:text-slate-300'>{todo.title}</h3>
            <p className='mt-2 text-slate-700 dark:text-slate-300'>{todo.description}</p>
            <div className='absolute bottom-4 right-4'>
                <button disabled={isLoading}
                    onClick={() => deleteTodo({ id: todo.id })}
                    className='group px-4 py-2 bg-red-500 text-white rounded-md ml-2 disabled:bg-red-400'>
                    <FontAwesomeIcon className='group-hover:animate-bounce' icon={faTrash} /> Delete</button>
            </div>
        </div>
    )
}


export default function TodoList() {

    const { data, error, isLoading } = useGetTodosQuery();
    return (
        <div className="max-w-3xl mx-auto relative z-20 pt-10 xl:max-w-none ">
            {error ? (
                <>Oh no, there was an error</>
            ) : isLoading ? (
                <>Loading...</>
            ) : data ? (
                <>
                    {
                        data.map(t => <TodoItem key={t.id} todo={t} />)
                    }

                </>
            ) : null}
        </div>
    )
}