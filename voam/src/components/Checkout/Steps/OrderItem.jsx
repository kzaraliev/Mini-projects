import { useEffect, useState } from "react";

import * as productService from "../../../services/productService";
import defaultImg from "../../../assets/hoodie_icon.png";
import styles from "./StepTwo.module.css";

export default function OrderItem({ productId, quantity, size, hasBorder }) {
  const [product, setProduct] = useState();

  useEffect(() => {
    productService.getOne(productId).then((res) => setProduct(res));
  }, []);

  const imgSrc = product?.images?.[0] ? product.images[0].filePath : defaultImg;

  return (
    <>
      {product === undefined ? (
        <p>Loading...</p>
      ) : (
        <li
          className={`${styles.cartItem} ${
            hasBorder ? styles.borderItems : ""
          }`}
        >
          <div className={styles.productImgSection}>
            <img
              src={imgSrc}
              alt="Cart item image"
              className={styles.imgCartItem}
            />
          </div>
          <div className={styles.productData}>
            <p className={styles.productName}>{product.name}</p>
            <p>Size {size}</p>
            <p>Price: {product.price * quantity} lv.</p>
            <p>Quantity: {quantity}</p>
          </div>
        </li>
      )}
    </>
  );
}
