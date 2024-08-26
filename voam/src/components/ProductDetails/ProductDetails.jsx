import { useEffect, useState, useMemo, useContext } from "react";
import { useNavigate, useParams, Link } from "react-router-dom";
import useForm from "../../hooks/useForm";
import Swal from "sweetalert2";

import Figure from "react-bootstrap/Figure";
import Carousel from "react-bootstrap/Carousel";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";

import { useCart } from "../../context/cartContext";
import * as productService from "../../services/productService";
import Path from "../../utils/paths";
import styles from "./ProductDetails.module.css";
import { OrderFormKeys } from "../../utils/constants";

export default function ProductDetails() {
  //Separate form in new component
  const { id } = useParams();
  const [product, setProduct] = useState({});
  const [errors, setErrors] = useState("");
  const navigate = useNavigate();

  const { addToCart } = useCart();

  const errorMessages = {
    invalidSize: "Select product size",
    notEnoughQuantity: "We don't have that many products",
    zeroOrEmptyInput: "Can't do this",
  };

  useEffect(() => {
    productService
      .getOne(id)
      .then(setProduct)
      .catch((err) => {
        console.log(err);
        navigate(Path.Items);
      });
  }, [id]);

  const submitHandler = () => {
    const selectedSizeId = values.size;
    const selectedSize = product.sizes.find(
      (size) => size.name == selectedSizeId
    );
    if (!selectedSize) {
      setErrors(errorMessages.invalidSize);
      return;
    }

    if (parseInt(values.amount) <= 0 || values.amount == "") {
      setErrors(errorMessages.zeroOrEmptyInput);
      return;
    }
    setErrors("");

    const data = {
      productId: id,
      size: values.size,
      quantity: values.amount,
    };

    addToCart(id, values.size, values.amount);

    Swal.fire({
      timer: 4000,
      title: "Added to Cart!",
      text: "Your item has been successfully added to the shopping cart. Ready to check out or keep shopping?",
      icon: "success",
    });
  };

  const initialValues = useMemo(
    () => ({
      amount: 1,
      size: 0,
    }),
    []
  );

  const { values, onChange, onSubmit } = useForm(
    submitHandler,
    initialValues,
    product
  );

  return (
    <div className={styles.container}>
      <div className={styles.content}>
        <Carousel fade className={styles.carousel} data-bs-theme="dark">
          {Object.keys(product).length !== 0 &&
            product.images.map((image, index) => {
              return (
                <Carousel.Item key={index}>
                  <Figure.Image
                    alt="product-img"
                    src={image.filePath}
                    className={styles.productImg}
                  />
                </Carousel.Item>
              );
            })}
        </Carousel>
        <div className={styles.productDetails}>
          <h1 className={styles.productName}>{product.name}</h1>
          <div className={styles.productInfo}>
            <p className={styles.price}>
              <b>Price</b>: {product.price} lv.
            </p>
            <p className={styles.description}>
              <b>Description</b>: {product.description}
            </p>
            <Form className={styles.formAddToCart} onSubmit={onSubmit}>
              {(errors == errorMessages.invalidSize ||
                errors === errorMessages.notEnoughQuantity ||
                errors === errorMessages.zeroOrEmptyInput) && (
                <p className={styles.invalid}>{errors}</p>
              )}
              <div className={styles.sizeAndQuantityContainer}>
                <Form.Select
                  name={OrderFormKeys.Size}
                  onChange={onChange}
                  value={values.size}
                  className={styles.sizeSelector}
                >
                  <option value="">Select size</option>
                  {Object.keys(product).length !== 0 &&
                    product.sizes.map((size) => {
                      return (
                        <option
                          value={size.name}
                          key={size.name}
                          name={size.name}
                        >
                          {size.name}
                        </option>
                      );
                    })}
                </Form.Select>
                <Form.Control
                  type="number"
                  id={OrderFormKeys.Amount}
                  name={OrderFormKeys.Amount}
                  onChange={onChange}
                  value={values.amount}
                  className={styles.amountSelector}
                />
              </div>
              <Button
                className={styles.submitButton}
                type="submit"
                variant="success"
              >
                Add to cart
              </Button>
            </Form>
          </div>
        </div>
      </div>
    </div>
  );
}
