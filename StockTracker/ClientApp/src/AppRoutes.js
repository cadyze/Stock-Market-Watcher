import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import { CurrentStockData } from "./components/CurrentStockData";

const AppRoutes = [
    //{
    //    index: true,
    //    element: <Home />
    //},
    //{
    //    path: '/counter',
    //    element: <Counter />
    //},
    //{
    //    path: '/fetch-data',
    //    element: <FetchData />
    //},
    {
        index: true,
        element: <CurrentStockData />
    }

];

export default AppRoutes;
