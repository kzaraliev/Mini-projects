import { useState, useEffect } from "react";

import styles from "./StepTwo.module.css";
import OrderItem from "./OrderItem.jsx";
import * as orderService from "../../../services/orderService.js";
import logoImg from "../../../assets/logo.png";
import { useCart } from "../../../context/cartContext";
import * as productService from "../../../services/productService.js";

export default function StepTwo({ changeActiveStep }) {
  const { cart } = useCart();
  const [billingDetails, setBillingDetails] = useState({});
  const [cartData, setCartData] = useState([]);
  const [totalPrice, setTotalPrice] = useState(0);

  useEffect(() => {
    const savedData = localStorage.getItem("checkout-data");
    if (savedData) {
      try {
        const checkoutData = JSON.parse(savedData);
        setBillingDetails(checkoutData);
      } catch (error) {
        console.error("Failed to parse checkout data:", error);
      }
    }
    async function fetchCartData() {
      const products = await Promise.all(
        cart.map(async (item) => {
          const product = await productService.getOne(item.productId);
          return {
            ...item,
            name: product.name,
            price: product.price,
            image: product.images[0]?.filePath || null,
          };
        })
      );

      const total = products.reduce(
        (sum, item) => sum + item.price * item.quantity,
        0
      );

      setCartData(products);
      setTotalPrice(total);
    }

    fetchCartData();
  }, [cart]);


  function submitOrder() {
    let products = [];

    Object.values(cartData).map((item) => {
      products.push(`${item.name} - ${item.size} x ${item.quantity}`);
    });

    const params = {
      sender: "Voam Clothing",
      name: billingDetails.fullName,
      econtOffice: billingDetails.econtOffice,
      town: billingDetails.city,
      phone: billingDetails.phone,
      to: billingDetails.email,
      orderNumber: Math.floor(Math.random() * 10000) + 1,
      date: new Date().toISOString().slice(0, 10),
      products: products.join("\r\n"),
      totalPrice: totalPrice.toFixed(2),
    };

    orderService.add(params);
  }

  return (
    <div className={styles.orderPreviewContainer}>
      <div className={styles.shoppingCart}>
        <div className={styles.container}>
          <h1 className={styles.title}>Order details</h1>
          <div className={styles.shippingInfoContainer}>
            <div>
              <h3>Billing info:</h3>
              <ul className={styles.shippingInfo}>
                <li>
                  <span className={styles.shippingLabel}>Name</span>:{" "}
                  {billingDetails.fullName}
                </li>
                <li>
                  <span className={styles.shippingLabel}>Email</span>:{" "}
                  {billingDetails.email}
                </li>
                <li>
                  <span className={styles.shippingLabel}>Phone</span>:{" "}
                  {billingDetails.phone}
                </li>
                <li>
                  <span className={styles.shippingLabel}>Econt Office</span>:{" "}
                  {billingDetails.econtOffice}
                </li>
                <li>
                  <span className={styles.shippingLabel}>Town/City</span>:{" "}
                  {billingDetails.city}
                </li>
                <li>
                  <span className={styles.shippingLabel}>Payment Method</span>:{" "}
                  {billingDetails.payment}
                </li>
              </ul>
            </div>
            <div className={styles.logoContainer}>
              <img
                className={styles.logo}
                src={logoImg}
                alt="Logo voam clothing"
              />
            </div>
          </div>
          <ul className={styles.productsList}>
            <h3 className={styles.productListTitle}>Your products:</h3>
            {cart === undefined ? (
              <p>Loading...</p>
            ) : (
              cart.map((cartItem, index) => (
                <OrderItem
                  key={cartItem.id + cartItem.size}
                  id={cartItem.productId}
                  productId={cartItem.productId}
                  quantity={cartItem.quantity}
                  size={cartItem.size}
                  hasBorder={index !== cart.length - 1 && cart.length > 1}
                />
              ))
            )}
          </ul>
          <button
            className={styles.submitButton}
            type="submit"
            onClick={() => {
              changeActiveStep(3);
              submitOrder();
            }}
          >
            Place order ({cart === undefined ? "" : totalPrice.toFixed(2)} lv.)
          </button>
        </div>
      </div>
    </div>
  );
}
