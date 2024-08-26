import { useEffect, useState } from "react";
import { HiMiniXMark } from "react-icons/hi2";
import {
  IoArrowForwardCircleOutline,
  IoArrowBackCircleOutline,
} from "react-icons/io5";

import * as productService from "../../services/productService";
import defaultImg from "../../assets/hoodie_icon.png";
import styles from "./ShoppingCart.module.css";

export default function CartItem({
  id,
  productId,
  quantity,
  size,
  onDelete,
  hasBorder,
  onUpdate,
}) {
  const [product, setProduct] = useState();

  useEffect(() => {
    async function fetchProduct() {
      const productData = await productService.getOne(productId);
      setProduct(productData);
    }
    fetchProduct();
  }, [productId]);

  const imgSrc = product?.images?.[0] ? product.images[0].filePath : defaultImg;

  function handleDelete() {
    onDelete(id, size);
  }

  function handleUpdateQuantity(change) {
    let newQuantity = quantity;
    if (change === "increase") {
      newQuantity = quantity + 1;
    } else if (change === "decrease" && quantity > 1) {
      newQuantity = quantity - 1;
    }

    if (newQuantity !== quantity) {
      const updatedTotalPriceForItem = newQuantity * product.price;
      onUpdate(id, size, newQuantity, updatedTotalPriceForItem);
    }
  }

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
            <HiMiniXMark className={styles.removeMark} onClick={handleDelete} />
            <img
              src={imgSrc}
              alt="Cart item image"
              className={styles.imgCartItem}
            />
          </div>
          <div className={styles.productData}>
            <p className={styles.productName}>{product.name}</p>
            <p className={styles.productSize}>Size: {size}</p>
            <p className={styles.productPrice}>
              Price: {product.price * quantity} lv.
            </p>
            <div>
              <p className={styles.productQuantity}>
                Quantity:
                <span>
                  <IoArrowBackCircleOutline
                    onClick={() => handleUpdateQuantity("decrease")}
                  />
                </span>
                {quantity}
                <span>
                  <IoArrowForwardCircleOutline
                    onClick={() => handleUpdateQuantity("increase")}
                  />
                </span>
              </p>
            </div>
          </div>
        </li>
      )}
    </>
  );
}
