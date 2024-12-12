import { useState, useEffect } from "react";
import { Course } from "../../app/model/course";
import CourseList from "./CourseList";
import Slideshow from "./Slideshow";
import { Box, Container, Typography } from "@mui/material";
import "./index.css";
import agent from "../../app/api/agen";
import Loading from "./Loading";

export default function catalog() {
  const [courses, setCourses] = useState<Course[] | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    agent.Catalog.list()
      .then((courses) => setCourses(courses.data))
      .finally(() => setLoading(false));
  }, []);

  if (loading) return <Loading />;

  return (
    <div>
      <Slideshow />

      <Container maxWidth="xl" sx={{ marginY: 5 }}>
        <Box marginY={5}>
          <Typography variant="h4" component="h4" color="textPrimary">
            All the skills you need in one place
          </Typography>
          <Typography variant="h6" color="secondary">
            From critical skills to technical topics, Coursery supports your
            professional development.
          </Typography>
        </Box>

        <Typography variant="h4" component="h4" color="textPrimary">
          Learners are viewing
        </Typography>

        <CourseList courses={courses} />
      </Container>
    </div>
  );
}
