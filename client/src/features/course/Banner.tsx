import { Box, Container, Typography, Button } from "@mui/material";
import { Course } from "../../app/model/course";

interface Props {
  course: Course;
}

export default function Banner({ course }: Props) {
  return (
    <Box sx={{ backgroundColor: "#1c1d1f", color: "white", paddingY: 10 }}>
      <Container
        maxWidth="xl"
        sx={{ display: "flex", justifyContent: "space-around" }}
      >
        <Box>
          <Typography variant="h2">{course.name}</Typography>
          <p>Introduction: {course.introduction}</p>
          <p>
            Created by <b>{course.creator.fullName}</b>
          </p>
          <p>Last updated {course.updateTime.toString()}</p>
          <p>{course.numberOfStudent} students enrolled</p>
        </Box>
        <Box
          sx={{
            width: "25%",
            backgroundColor: "white",
            border: "1px solid white",
          }}
        >
          <img
            src={course.imageCourse}
            style={{
              width: "100%",
              height: "190px",
              objectFit: "contain",
              border: "1px solid white",
            }}
          />

          <Button
            color="secondary"
            sx={{ width: "80%", border: "1px solid #2D2F31" }}
          >
            Add to cart
          </Button>
        </Box>
      </Container>
    </Box>
  );
}
