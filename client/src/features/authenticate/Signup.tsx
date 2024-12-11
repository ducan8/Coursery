import { TextField, Typography } from "@mui/material";

export default function Signup() {
  return (
    <>
      <Typography component={"h1"}>
        Log in to continue your learning journey
      </Typography>
      <TextField
        placeholder="Search..."
        size="medium"
        sx={{
          width: "60%",
          "& .MuiOutlinedInput-root": {
            borderRadius: "30px", // Áp dụng cho phần input
            backgroundColor: "#F7F9FA",
          },
        }}
      ></TextField>
    </>
  );
}
