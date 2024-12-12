import { Box, TextField, Typography } from "@mui/material";
// import { DatePicker, LocalizationProvider } from "@mui/x-date-pickers";
// import { AdapterDateFns } from "@mui/x-date-pickers/AdapterDateFns";

export default function Signup() {
  return (
    <>
      <Box sx={{ width: "30%" }}>
        <Typography variant={"h4"} fontWeight={800}>
          Sign up and start learning
        </Typography>
        <TextField
          placeholder="Email"
          size="medium"
          sx={{
            width: "80%",
            "& .MuiOutlinedInput-root": {
              backgroundColor: "#F7F9FA",
            },
          }}
        ></TextField>
        <TextField
          placeholder="Username"
          size="medium"
          sx={{
            width: "80%",
            "& .MuiOutlinedInput-root": {
              backgroundColor: "#F7F9FA",
            },
          }}
        ></TextField>
        <TextField
          placeholder="Fullname"
          size="medium"
          sx={{
            width: "80%",
            "& .MuiOutlinedInput-root": {
              backgroundColor: "#F7F9FA",
            },
          }}
        ></TextField>
        <TextField
          placeholder="Password"
          size="medium"
          sx={{
            width: "80%",
            "& .MuiOutlinedInput-root": {
              backgroundColor: "#F7F9FA",
            },
          }}
        ></TextField>
        <TextField
          placeholder="Phonenumber"
          size="medium"
          sx={{
            width: "80%",
            "& .MuiOutlinedInput-root": {
              backgroundColor: "#F7F9FA",
            },
          }}
        ></TextField>

        {/* <LocalizationProvider dateAdapter={AdapterDateFns}>
          <DatePicker
            label="Select Date"
            value={selectedDate}
            onChange={handleDateChange}
            renderInput={(params) => <TextField {...params} />}
          />
          <div>
            Selected Date: {selectedDate ? selectedDate.toString() : "None"}
          </div>
        </LocalizationProvider> */}
      </Box>
    </>
  );
}
