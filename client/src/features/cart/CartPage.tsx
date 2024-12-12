import { useEffect, useState } from "react";
import { Cart } from "../../app/model/cart";
import { Typography } from "@mui/material";
import ListItem from "./ListCartItem";

export default function CartPage() {
  const [cart, setCart] = useState<Cart | null>(null);
  // const [loading, setLoading] = useState(true);

  useEffect(() => {
    const cart = localStorage.getItem("cart");
    if (cart) {
      setCart(JSON.parse(cart));
    }
  }, []);

  function EmptyCart() {
    return (
      <>
        <Typography>0 Course in Cart</Typography>
        <p>Your cart is empty. Keep shopping to find a course</p>
      </>
    );
  }

  return (
    <div>
      <Typography variant="h4">Shopping Cart</Typography>
      {!cart ? EmptyCart() : <ListItem />}
    </div>
  );
}
