import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Course } from "../../app/model/course";
import Banner from "./Banner";
import Content from "./Content";
import agent from "../../app/api/agen";
import Loading from "./Loading";

export default function CourseDetail() {
  const { id } = useParams<{ id: string }>();
  const [course, setCourse] = useState<Course | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    id &&
      agent.Catalog.details(id)
        .then((courseDetail) => {
          setCourse(courseDetail.data);
        })
        .finally(() => setLoading(false));
  }, [id]);

  if (loading) return <Loading />;

  if (!course) return "404 not found...";

  return (
    <>
      <Banner course={course} />
      <Content course={course} />
    </>
  );
}
