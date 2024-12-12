import { Search, ShoppingCart } from "@mui/icons-material";
import {
  AppBar,
  Toolbar,
  Typography,
  Button,
  Box,
  TextField,
  IconButton,
  Badge,
} from "@mui/material";
import { Link, NavLink } from "react-router-dom";

export default function Header() {
  return (
    <AppBar
      position="static"
      sx={{ backgroundColor: "white", paddingY: 0.3, zIndex: 99 }}
    >
      <Toolbar>
        <Typography
          component={NavLink}
          to="/"
          variant="h5"
          fontFamily={"Open sans"}
          color="textPrimary"
          sx={{ flexGrow: 0, textDecoration: "none" }}
        >
          <Box
            display={"flex"}
            justifyContent={"center"}
            alignItems={"center"}
            sx={{ marginLeft: 2 }}
          >
            <b>Course</b>ry
            <img
              src="/logofavicon.png"
              style={{
                width: 35,
                height: 35,
                marginLeft: 5,
              }}
            />
          </Box>
        </Typography>

        {/* search button */}
        <Box
          sx={{
            flexGrow: 2,
            marginLeft: 20,
          }}
        >
          <TextField
            placeholder="Search..."
            size="medium"
            sx={{
              width: "80%",
              "& .MuiOutlinedInput-root": {
                borderRadius: "30px",
                backgroundColor: "#F7F9FA",
              },
            }}
          >
            <Search color="primary" />
          </TextField>
        </Box>

        {/* group button */}
        <IconButton component={Link} to="/cart" size="large" sx={{ mr: 2 }}>
          <Badge badgeContent="4" color="secondary">
            <ShoppingCart sx={{ color: "#2D2F31" }} />
          </Badge>
        </IconButton>
        <Button
          variant="outlined"
          component={Link}
          to="/log-in"
          sx={{ border: "1px solid #2D2F31", color: "#2D2F31", marginLeft: 2 }}
        >
          Log in
        </Button>
        <Button
          component={Link}
          to="/sign-up"
          variant="contained"
          sx={{ backgroundColor: "#2D2F31", color: "white", marginLeft: 2 }}
        >
          Sign up
        </Button>
      </Toolbar>
    </AppBar>
  );
}
