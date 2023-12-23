import React, { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faPlus } from '@fortawesome/free-solid-svg-icons'

import { useAddTodoMutation, Todo } from '../../services/todoservice';

const AddTodo: React.FC = () => {
    const [title, setTitle] = useState('');
    const [description, setDescription] = useState('');
    const [addTodo, { error, isLoading }] = useAddTodoMutation();

    const handleTitleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setTitle(event.target.value);
    };

    const handleDescriptionChange = (event: React.ChangeEvent<HTMLTextAreaElement>) => {
        setDescription(event.target.value);
    };

    const handleSubmit = (event: React.FormEvent) => {
        event.preventDefault();

        if (!title || !description) {
            return;
        }
        else {
            addTodo({ title, description });
        }

        // Reset the input fields
        setTitle('');
        setDescription('');
    };

    return (
        <form onSubmit={handleSubmit} className="grid grid-cols-1 gap-6">
            <label className="block">
                <span className="text-gray-700 dark:text-white">
                    Title
                </span>
                <input type="text"
                    value={title}
                    onChange={handleTitleChange}
                    className="mt-1 block w-full" />
            </label>

            <label className="block">
                <span className="text-gray-700 dark:text-white">
                    Description
                </span>
                <textarea id="description" value={description} onChange={handleDescriptionChange} className="mt-1 block w-full" />
            </label>

            <button type="submit" disabled={isLoading} className="bg-blue-500 disabled:bg-blue-400 text-white py-2 px-4 rounded-md group">
                <FontAwesomeIcon className='group-hover:animate-bounce' icon={faPlus} /> Add new todo</button>
        </form>
    );
};

export default AddTodo;
