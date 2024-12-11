import { createBrowserRouter } from "react-router-dom";
import App from "../layout/App";
import CourseDetail from "../../features/course/CourseDetail";
import Signup from "../../features/authenticate/Signup";
import Catalog from "../../features/catalog/Catalog";
import Login from "../../features/authenticate/Login";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      { path: "", element: <Catalog /> },
      { path: "/course/:id", element: <CourseDetail /> },
      { path: "/sign_up", element: <Signup /> },
      { path: "/log_in", element: <Login /> },
    ],
  },
]);
