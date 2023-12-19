import { useGetTodosQuery, Todo } from '../../services/todoservice';

function TodoItem({ todo }: { todo: Todo }) {
    return (
        <div>
            <h3>{todo.title}</h3>
            <p>{todo.description}</p>
        </div>
    )
}


export default function TodoList() {
    
    const { data, error, isLoading } = useGetTodosQuery();    

    return (
        <div className="">
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