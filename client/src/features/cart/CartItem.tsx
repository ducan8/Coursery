import { Paper } from "@mui/material";
import { useEffect, useState } from "react";
import agent from "../../app/api/agen";
import { Course } from "../../app/model/course";

export default function CartItem(id: string) {
  const [course, setCourse] = useState<Course>();

  useEffect(() => {
    agent.Catalog.details(id).then((res) => setCourse(res.data));
  });

  return <Paper variant="elevation">{course!.name}</Paper>;
}
