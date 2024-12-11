import Grid from "@mui/material/Grid2";
import { Course } from "../../app/model/course";
import CourseCard from "./CourseCard";

interface Props {
  courses: Course[] | null;
}

export default function CourseList({ courses }: Props) {
  return (
    <Grid
      container
      spacing={{ xs: 2, md: 3 }}
      columns={{ xs: 4, sm: 8, md: 12 }}
    >
      {courses?.map((course, index) => (
        <Grid key={index} size={{ xs: 2, sm: 4, md: 3 }}>
          <CourseCard course={course} />
        </Grid>
      ))}
    </Grid>
  );
}
