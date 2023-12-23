import AddTodo from "../components/todos/AddTodo";
import TodoList from "../components/todos/TodoList";


export default function Todos() {
    return <div>
        <div className="w-[30rem]">
            <AddTodo />
        </div>
        <TodoList />
    </div>
}