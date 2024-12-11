import { PlayCircle } from "@mui/icons-material";
import { Container, Box, Typography } from "@mui/material";
import { SimpleTreeView, TreeItem } from "@mui/x-tree-view";
import { Course } from "../../app/model/course";
import Requirements from "./Requirements";
import Instructor from "./Instructor";

interface Props {
  course: Course;
}

export default function Content({ course }: Props) {
  return (
    <Container
      maxWidth="lg"
      sx={{ display: "flex", justifyContent: "space-between" }}
    >
      {/* course content */}
      <Box>
        <Instructor creator={course.creator} />

        <Typography variant="h5" sx={{ fontWeight: 900 }}>
          Course content
        </Typography>
        <Box sx={{ minHeight: 352, minWidth: 250 }}>
          <SimpleTreeView sx={{ border: "1px solid grey" }}>
            {course.subjects.length > 0
              ? course.subjects.map((subject: any) => {
                  return (
                    <TreeItem
                      itemId={subject.id}
                      sx={{
                        border: "inherit",
                        "& .MuiCollapse-root": {
                          paddingLeft: 0,
                        },
                        // "& .MuiTreeItem-root": {
                        //   paddingLeft: 0,
                        // },
                        "& .MuiTreeItem-label": {
                          backgroundColor: "#F7F9A",
                          py: 1.5,
                        },
                      }}
                      label={<Typography>{subject.name}</Typography>}
                    >
                      <TreeItem
                        sx={{ border: "inherit", padding: 0 }}
                        itemId={Math.random().toString()}
                        label={
                          <div
                            style={{
                              display: "flex",
                            }}
                          >
                            <PlayCircle
                              sx={{ color: "grey", marginRight: 1 }}
                            />
                            <b>basic co ban</b>
                          </div>
                        }
                      />

                      {/* {subject.subjectDetails.map((subjectDetail: any) => {
                      return (
                        <TreeItem
                          itemId={subjectDetail.id}
                          label={subjectDetail.name}
                        />
                      );
                    })} */}
                    </TreeItem>
                  );
                })
              : "No content"}
          </SimpleTreeView>
        </Box>
      </Box>

      <Requirements requirement={course.requirement} />
    </Container>
  );
}
