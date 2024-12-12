import { CssBaseline } from "@mui/material";
import Header from "./Header";
import { Outlet } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

function App() {
  return (
    <>
      <ToastContainer position="top-center" theme="dark" />
      <CssBaseline />
      <Header />
      <Outlet />
    </>
  );
}

export default App;
