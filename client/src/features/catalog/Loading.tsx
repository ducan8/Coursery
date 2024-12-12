import { Box, Container, Skeleton } from "@mui/material";
import Grid from "@mui/material/Grid2";

export default function Loading() {
  return (
    <Grid container>
      <Box sx={{ width: "100%", pb: 5 }}>
        <Skeleton sx={{ height: 400 }} animation="wave" variant="rectangular" />
        <Skeleton animation="wave" sx={{ pt: 3 }} />
        <Skeleton animation="wave" />
        <Skeleton animation="wave" />
      </Box>
      <Container maxWidth="xl">
        <Grid
          container
          spacing={{ xs: 2, md: 3 }}
          columns={{ xs: 4, sm: 8, md: 12 }}
        >
          <Grid size={3} sx={{ pt: 1 }}>
            <Skeleton variant="rectangular" sx={{ height: 170, width: 300 }} />
            <Skeleton width="90%" />
            <Skeleton width="90%" />
            <Skeleton width="80%" />
          </Grid>
          <Grid size={3} sx={{ pt: 1 }}>
            <Skeleton variant="rectangular" sx={{ height: 170, width: 300 }} />
            <Skeleton width="90%" />
            <Skeleton width="90%" />
            <Skeleton width="80%" />
          </Grid>
          <Grid size={3} sx={{ pt: 1 }}>
            <Skeleton variant="rectangular" sx={{ height: 170, width: 300 }} />
            <Skeleton width="90%" />
            <Skeleton width="90%" />
            <Skeleton width="80%" />
          </Grid>
          <Grid size={3} sx={{ pt: 1 }}>
            <Skeleton variant="rectangular" sx={{ height: 170, width: 300 }} />
            <Skeleton width="90%" />
            <Skeleton width="90%" />
            <Skeleton width="80%" />
          </Grid>
        </Grid>
      </Container>
    </Grid>
  );
}
