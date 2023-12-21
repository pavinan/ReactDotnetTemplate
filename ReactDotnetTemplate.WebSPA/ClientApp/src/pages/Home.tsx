import { useState } from 'react'
import reactLogo from '../assets/react.svg'
import viteLogo from '/vite.svg'

function Home() {
    const [count, setCount] = useState(0)

    return (
        <div className='max-w-3xl mx-auto pt-10 xl:max-w-none xl:ml-0 xl:mr-[15.5rem] xl:pr-16'>
            <div className='flex'>
                <a href="https://vitejs.dev" target="_blank">
                    <img src={viteLogo} className="size-32" alt="Vite logo" />
                </a>
                <a href="https://react.dev" target="_blank">
                    <img src={reactLogo} className="size-32" alt="React logo" />
                </a>
            </div>
            <h1 className='text-2xl ml-12 mt-2 mb-2'>Vite + React</h1>
            <div className="">
                <button onClick={() => setCount((count) => count + 1)}>
                    count is {count}
                </button>
                <p>
                    o Edit <code>src/App.tsx</code> and save to test HMR..
                </p>
            </div>
            <p>
                Click on the Vite and React logos to learn more
            </p>
        </div>
    )
}

export default Home
