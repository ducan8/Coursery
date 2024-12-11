import {
  Card,
  CardActionArea,
  CardMedia,
  CardContent,
  Typography,
  CardActions,
  Button,
  Box,
} from "@mui/material";
import { Course } from "../../app/model/course";
import { Link } from "react-router-dom";

interface Props {
  course: Course;
}

export default function CourseCard({ course }: Props) {
  return (
    <Card>
      <CardActionArea>
        <Box component={Link} to={`/course/${course.id}`}>
          <CardMedia component="img" height="170" image={course.imageCourse} />
        </Box>
        <CardContent>
          <Typography gutterBottom variant="h5" component="div">
            {course.name}
          </Typography>
          <Typography variant="body2" sx={{ color: "text.secondary" }}>
            {course.introduce}
          </Typography>
        </CardContent>
      </CardActionArea>
      <CardActions>
        <Typography gutterBottom variant="h6" component="b">
          {"$" + (course.price / 100).toFixed(2)}
        </Typography>
        <Button size="small" color="primary">
          Add to cart
        </Button>
      </CardActions>
    </Card>
  );
}
