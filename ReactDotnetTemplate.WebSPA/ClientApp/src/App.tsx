import { Outlet, NavLink } from "react-router-dom";

function AppLink({ to, name }: { to: string, name: string }) {
    return (
        <li>
            <NavLink
                className={({ isActive }) => {
                    let defaultClass = ""

                    if (isActive) {
                        defaultClass += "block border-l pl-4 -ml-px text-sky-500 border-current font-semibold dark:text-sky-400";
                    } else {
                        defaultClass += "block border-l pl-4 -ml-px border-transparent hover:border-slate-400 dark:hover:border-slate-500 text-slate-700 hover:text-slate-900 dark:text-slate-400 dark:hover:text-slate-300";
                    }

                    return defaultClass;
                }}
                to={to}>{name}</NavLink>
        </li>
    );
}

export default function App() {
    return (
        <div className="antialiased text-slate-500 dark:text-slate-400 bg-white dark:bg-slate-900 h-full">
            <div className="max-w-8xl mx-auto px-4 sm:px-6 md:px-8">
                <div className="hidden lg:block fixed z-20 inset-0 top-[3.8125rem] left-[max(0px,calc(50%-49rem))] right-auto w-[10rem] pb-10 pl-8 pr-6 overflow-y-auto">
                    <nav className="lg:text-sm lg:leading-6 relative">
                        <ul className="space-y-6 lg:space-y-2 border-l border-slate-100 dark:border-slate-800">
                            <AppLink to="/" name="Home" />
                            <AppLink to="/todos" name="Todos" />
                        </ul>
                    </nav>
                </div>
                <div className="lg:pl-[19.5rem]" >
                    <Outlet />
                </div>
            </div>
        </div>
    );
}
