import { Avatar, Typography } from "@mui/material";
import { User } from "../../app/model/user";

interface Props {
  creator: User;
}

export default function Instructor({ creator }: Props) {
  return (
    <>
      <Typography variant="h5" sx={{ fontWeight: 900 }}>
        Instructor
      </Typography>
      <Typography>{creator.fullName}</Typography>
      <Avatar src={creator.avatar} />
      <p>{creator.instruction}</p>
    </>
  );
}
