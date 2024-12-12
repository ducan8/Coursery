import { Box, Skeleton } from "@mui/material";
import Grid from "@mui/material/Grid2";

export default function Loading() {
  return (
    <Grid container>
      <Box sx={{ width: "100%", pb: 5 }}>
        <Skeleton sx={{ height: 400 }} animation="wave" variant="rectangular" />
        <Skeleton animation="wave" sx={{ pt: 3 }} />
        <Skeleton animation="wave" />
        <Skeleton animation="wave" />
        <Skeleton sx={{ height: 400 }} animation="wave" variant="rectangular" />
      </Box>
    </Grid>
  );
}
