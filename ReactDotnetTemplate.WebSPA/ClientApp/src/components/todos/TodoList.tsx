import { useState } from 'react'
import { useGetTodosQuery } from '../../services/todoservice';

export default function TodoList() {
    // Using a query hook automatically fetches data and returns query values
    const { data, error, isLoading } = useGetTodosQuery();
    // Individual hooks are also accessible under the generated endpoints:
    // const { data, error, isLoading } = pokemonApi.endpoints.getPokemonByName.useQuery('bulbasaur')

    return (
        <div className="">
            {error ? (
                <>Oh no, there was an error</>
            ) : isLoading ? (
                <>Loading...</>
            ) : data ? (
                <>
                    {
                        data.map(t => (
                            <div key={t.id}>
                                <h3>{t.title}</h3>
                                <p>{t.description}</p>
                            </div>))
                    }

                </>
            ) : null}
        </div>
    )
}