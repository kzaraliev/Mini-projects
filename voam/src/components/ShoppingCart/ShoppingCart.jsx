import { useState, useEffect, useContext } from "react";
import { Link } from "react-router-dom";

import { useCart } from "../../context/cartContext";
import styles from "./ShoppingCart.module.css";
import { TiShoppingCart } from "react-icons/ti";
import CartItem from "./CartItem.jsx";
import Path from "../../utils/paths.js";
import * as productService from "../../services/productService.js";

export default function ShoppingCart() {
  const [cartData, setCartData] = useState([]);
  const { cart, changeQuantity, removeFromCart } = useCart();

  useEffect(() => {
    async function loadCartData() {
      const products = await Promise.all(
        cart.map(async (item) => {
          const product = await productService.getOne(item.productId);
          return {
            productId: item.productId,
            name: product.name,
            size: item.size,
            price: product.price,
            quantity: item.quantity,
            image: product.images[0]?.filePath || null, 
          };
        })
      );
      setCartData(products);
    }
    loadCartData();
  }, [cart]);

  function handleItemDelete(itemId, size) {
    setCartData((currentData) =>
      currentData.filter(
        (item) => item.productId !== itemId || item.size !== size
      )
    );

    removeFromCart(itemId, size);
  }

  function handleQuantityUpdate(
    itemId,
    size,
    newQuantity,
    updatedTotalPriceForItem
  ) {
    setCartData((currentData) => {
      if (!Array.isArray(currentData)) return currentData;
      const updatedCartItems = currentData.map((item) => {
        if (item.productId === itemId) {
          return {
            ...item,
            quantity: newQuantity,
            totalPrice: updatedTotalPriceForItem,
          };
        }
        return item;
      });

      return updatedCartItems;
    });
    changeQuantity(itemId, size, newQuantity);
  }

  return (
    <div className={styles.shoppingCart}>
      <div className={styles.container}>
        <h1 className={styles.title}>Shopping Cart</h1>
        <ul className={styles.productsList}>
          {cartData === undefined ? (
            <p>Loading...</p>
          ) : cartData.length == 0 ? (
            <>
              <TiShoppingCart className={styles.emptyCartIcon} />
              <h2 className={styles.emptyCartTitle}>Your cart is empty</h2>
              <p className={styles.emptyCartText}>
                Looks like you haven't added anything to your cart.{" "}
                <Link to={Path.Items}> Go ahead & explore. </Link>
              </p>
            </>
          ) : (
            cartData.map((cartItem, index) => (
              <CartItem
                key={cartItem.productId + cartItem.size}
                id={cartItem.productId}
                productId={cartItem.productId}
                quantity={cartItem.quantity}
                size={cartItem.size}
                onDelete={handleItemDelete}
                hasBorder={index !== cartData.length - 1 && cartData.length > 1}
                onUpdate={handleQuantityUpdate}
              />
            ))
          )}
        </ul>
        {cartData === undefined ? (
          <p>Loading</p>
        ) : (
          cartData.length != 0 && (
            <Link to={Path.Checkout}>
              <button className={styles.submitButton} type="submit">
                Proceed to checkout (
                {cartData
                  .reduce(
                    (total, item) => total + item.price * item.quantity,
                    0
                  )
                  .toFixed(2)}{" "}
                lv.)
              </button>
            </Link>
          )
        )}
      </div>
    </div>
  );
}
