import axios from "axios";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Course } from "../../app/model/course";
import Banner from "./Banner";
import Content from "./Content";

export default function CourseDetail() {
  const { id } = useParams<{ id: string }>();
  const [course, setCourse] = useState<Course | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    axios
      .get(`http://localhost:5000/api/course/getCourseWithAllSubject/${id}`)
      .then((res) => {
        setCourse(res.data.data);
        console.log(res);
      })
      .finally(() => setLoading(false));
  }, [id]);

  if (loading) return "loading...";
  if (!course) return "404 not found...";

  return (
    <>
      <Banner course={course} />
      <Content course={course} />
    </>
  );
}
