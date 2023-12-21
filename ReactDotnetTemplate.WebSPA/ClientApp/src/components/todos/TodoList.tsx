import { useGetTodosQuery, Todo } from '../../services/todoservice';

function TodoItem({ todo }: { todo: Todo }) {
    return (
        <div className='relative flex flex-col bg-slate-50 rounded-lg p-6 dark:bg-slate-800 dark:highlight-white/5 my-2 w-[30rem]'>
            <h3 className='text-base text-slate-900 font-semibold dark:text-slate-300'>{todo.title}</h3>
            <p className='mt-2 text-slate-700 dark:text-slate-300'>{todo.description}</p>
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