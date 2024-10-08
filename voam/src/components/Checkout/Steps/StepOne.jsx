import Form from "react-bootstrap/Form";
import FloatingLabel from "react-bootstrap/FloatingLabel";
import { useFormik } from "formik";
import { useState } from "react";

import styles from "../Checkout.module.css";
import { OrderKeys } from "../../../utils/constants";
import orderValidation from "./orderValidation";

export default function StepOne({ changeActiveStep, onFormDataChange }) {

  const [user, setUser] = useState({
    [OrderKeys.Email]: "",
    [OrderKeys.FirstName]: "",
    [OrderKeys.LastName]: "",
    [OrderKeys.PhoneNumber]: "",
    [OrderKeys.EcontOffice]: "",
    [OrderKeys.City]: "",
    [OrderKeys.PaymentMethod]: "",
  });

  const {
    values,
    errors,
    touched,
    handleBlur,
    handleChange,
    handleSubmit,
    isSubmitting,
  } = useFormik({
    initialValues: {
      [OrderKeys.Email]: user[OrderKeys.Email] || "",
      [OrderKeys.FirstName]: user[OrderKeys.FirstName] || "",
      [OrderKeys.LastName]: user[OrderKeys.LastName] || "",
      [OrderKeys.PhoneNumber]: user[OrderKeys.PhoneNumber] || "",
      [OrderKeys.EcontOffice]: user[OrderKeys.EcontOffice] || "",
      [OrderKeys.City]: user[OrderKeys.City] || "",
      [OrderKeys.PaymentMethod]: "Upon delivery",
    },
    validationSchema: orderValidation,
    onSubmit,
    enableReinitialize: true,
  });

  async function onSubmit(values) {
    onFormDataChange(values);
    changeActiveStep(2);
  }

  return (
    <>
      <div className={styles.containerForm}>
        <Form className={styles.form} onSubmit={handleSubmit}>
          <h1 className={styles.title}>Billing Details</h1>

          <Form.Group className="mb-3">
            {errors[OrderKeys.FirstName] && touched[OrderKeys.FirstName] && (
              <p className={styles.invalid}>{errors[OrderKeys.FirstName]}</p>
            )}
            <FloatingLabel
              htmlFor={OrderKeys.FirstName}
              label="First name"
              className="mb-3"
            >
              <Form.Control
                type="text"
                id="firstName"
                name={OrderKeys.FirstName}
                placeholder="Enter First name"
                onChange={handleChange}
                onBlur={handleBlur}
                value={values[OrderKeys.FirstName]}
              />
            </FloatingLabel>
          </Form.Group>

          <Form.Group className="mb-3">
            {errors[OrderKeys.LastName] && touched[OrderKeys.LastName] && (
              <p className={styles.invalid}>{errors[OrderKeys.LastName]}</p>
            )}
            <FloatingLabel
              htmlFor={OrderKeys.LastName}
              label="Last name"
              className="mb-3"
            >
              <Form.Control
                type="text"
                id="lastName"
                name={OrderKeys.LastName}
                placeholder="Enter last name"
                onChange={handleChange}
                onBlur={handleBlur}
                value={values[OrderKeys.LastName]}
              />
            </FloatingLabel>
          </Form.Group>

          <Form.Group className="mb-3">
            {errors[OrderKeys.PhoneNumber] &&
              touched[OrderKeys.PhoneNumber] && (
                <p className={styles.invalid}>
                  {errors[OrderKeys.PhoneNumber]}
                </p>
              )}
            <FloatingLabel
              htmlFor={OrderKeys.PhoneNumber}
              label="Phone number"
              className="mb-3"
            >
              <Form.Control
                type="text"
                id="phoneNumber"
                name={OrderKeys.PhoneNumber}
                placeholder="Enter phone number"
                onChange={handleChange}
                onBlur={handleBlur}
                value={values[OrderKeys.PhoneNumber]}
              />
            </FloatingLabel>
          </Form.Group>

          <Form.Group className="mb-3">
            {errors[OrderKeys.Email] && touched[OrderKeys.Email] && (
              <p className={styles.invalid}>{errors[OrderKeys.Email]}</p>
            )}
            <FloatingLabel
              htmlFor={OrderKeys.Email}
              label="Email address"
              className="mb-3"
            >
              <Form.Control
                type="text"
                id="email"
                name={OrderKeys.Email}
                placeholder="Enter email"
                onChange={handleChange}
                onBlur={handleBlur}
                value={values[OrderKeys.Email]}
              />
            </FloatingLabel>
          </Form.Group>

          <Form.Group className="mb-3">
            {errors[OrderKeys.EcontOffice] &&
              touched[OrderKeys.EcontOffice] && (
                <p className={styles.invalid}>
                  {errors[OrderKeys.EcontOffice]}
                </p>
              )}
            <FloatingLabel
              htmlFor={OrderKeys.EcontOffice}
              label="Econt street name and number"
              className="mb-3"
            >
              <Form.Control
                type="text"
                id={OrderKeys.EcontOffice}
                name={OrderKeys.EcontOffice}
                placeholder="Enter EcontOffice"
                onChange={handleChange}
                onBlur={handleBlur}
                value={values[OrderKeys.EcontOffice]}
              />
            </FloatingLabel>
          </Form.Group>

          <Form.Group className="mb-3">
            {errors[OrderKeys.City] && touched[OrderKeys.City] && (
              <p className={styles.invalid}>{errors[OrderKeys.City]}</p>
            )}
            <FloatingLabel
              htmlFor={OrderKeys.City}
              label="Enter city"
              className="mb-3"
            >
              <Form.Control
                type="text"
                id={OrderKeys.City}
                name={OrderKeys.City}
                placeholder="Enter your City"
                onChange={handleChange}
                onBlur={handleBlur}
                value={values[OrderKeys.City]}
              />
            </FloatingLabel>
          </Form.Group>

          <Form.Group className="mb-3">
            <Form.Check
              className={styles.radioPayment}
              inline
              label="Pay upon delivery"
              name={[OrderKeys.PaymentMethod]}
              type="radio"
              id={[OrderKeys.PaymentMethod]}
              value={values[OrderKeys.PaymentMethod]}
              defaultChecked={true}
            />
          </Form.Group>

          <button
            className={styles.submitButton}
            type="submit"
            disabled={isSubmitting}
          >
            Next step
          </button>
        </Form>
      </div>
    </>
  );
}
