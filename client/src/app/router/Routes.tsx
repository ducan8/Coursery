import { createBrowserRouter, Navigate } from "react-router-dom";
import App from "../layout/App";
import CourseDetail from "../../features/course/CourseDetail";
import Signup from "../../features/authenticate/Signup";
import Catalog from "../../features/catalog/Catalog";
import Login from "../../features/authenticate/Login";
import ServerError from "../../features/errors/ServerError";
import NotFound from "../../features/errors/NotFound";
import CartPage from "../../features/cart/CartPage";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      { path: "", element: <Catalog /> },
      { path: "/course/:id", element: <CourseDetail /> },
      { path: "/sign-up", element: <Signup /> },
      { path: "/log-in", element: <Login /> },
      { path: "/cart", element: <CartPage /> },
      { path: "/server-error", element: <ServerError /> },
      { path: "/not-found", element: <NotFound /> },
      { path: "*", element: <Navigate replace to="not-found" /> },
    ],
  },
]);
