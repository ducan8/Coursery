import {
  Card,
  CardActionArea,
  CardMedia,
  CardContent,
  Typography,
  CardActions,
  Box,
  Button,
} from "@mui/material";
import { Course } from "../../app/model/course";
import { Link } from "react-router-dom";
import { toast } from "react-toastify";
import { useEffect, useState } from "react";
import { LoadingButton } from "@mui/lab";
import { Cart } from "../../app/model/cart";

interface Props {
  course: Course;
}

const styleButton = {
  transition: "all 0.3s ease-in-out",
  opacity: 0,
  transform: "translateX(-50%)",
};

const styleFadeIn = {
  opacity: 1,
  transform: "translateX(0)",
};

export default function CourseCard({ course }: Props) {
  const [loading, setLoading] = useState(false);
  const [cart, setCart] = useState<Cart>();

  useEffect(() => {
    const cart = localStorage.getItem("cart");
    if (cart) {
      setCart(JSON.parse(cart));
    }
  }, []);

  async function addToCart(id: string) {
    setLoading(true);
    if (!cart) {
      const newCart: Cart = {
        userId: "ahihi",
        courseIds: [id],
      };
      setCart(newCart);
      await localStorage.setItem("cart", JSON.stringify(newCart));
    } else {
      if (!cart.courseIds.includes(id)) {
        const newCart: Cart = {
          userId: cart.userId,
          courseIds: [...cart.courseIds, id],
        };
        setCart(newCart);
        await localStorage.setItem("cart", JSON.stringify(newCart));
      } else {
        toast.warn("khóa học đã tồn tại trong giỏ hàng");
      }
    }
    setLoading(false);
    console.log(cart);
  }

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
            {course.introduction}
          </Typography>
        </CardContent>
      </CardActionArea>
      <CardActions>
        <Typography gutterBottom variant="h6" component="b">
          {"$" + (course.price / 100).toFixed(2)}
        </Typography>
        {cart?.courseIds.includes(course.id) ? (
          <Button
            component={Link}
            to="/cart"
            variant="outlined"
            sx={{ ...styleButton, ...styleFadeIn }}
          >
            Go to Cart
          </Button>
        ) : (
          <LoadingButton
            sx={{ ...styleButton, ...styleFadeIn }}
            loading={loading}
            variant="contained"
            onClick={() => addToCart(course.id)}
          >
            Add to cart
          </LoadingButton>
        )}
      </CardActions>
    </Card>
  );
}
