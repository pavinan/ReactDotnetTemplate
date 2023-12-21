import { useGetTodosQuery, Todo } from '../../services/todoservice';

function TodoItem({ todo }: { todo: Todo }) {
    return (
        <div className='peer group flex-auto'>
            <h3 className='mb-2 font-semibold text-slate-900 dark:text-slate-200'>{todo.title}</h3>
            <p className='prose prose-slate prose-sm text-slate-600 dark:prose-dark'>{todo.description}</p>
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