import { Box, Typography } from "@mui/material";

interface Props {
  requirement: string | null;
}

export default function Requirements({ requirement }: Props) {
  return (
    <Box sx={{ border: "1px solid grey" }}>
      <Typography variant="h5" sx={{ fontWeight: 900 }}>
        Requirements:{" "}
      </Typography>
      <p>{requirement}</p>
    </Box>
  );
}
