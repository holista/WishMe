import { useState } from "react";

import classes from "./Modal.module.css";

const Modal = (props) => {
  const [openModal, setOpenModal] = useState(true);

  if (openModal) {
    document.body.style.overflow = "hidden";
  }

  const closeModalHandler = () => {
    setOpenModal(false);
  };

  return (
    <>
      {openModal && (
        <div className={classes.backdrop} onClick={closeModalHandler}></div>
      )}
      {openModal && (
        <div className={classes.modal}>
          <div className={classes.modalHeader}>
            <h1>{props.header}</h1>
            <button onClick={closeModalHandler}>x</button>
          </div>
          {props.children}
        </div>
      )}
    </>
  );
};

export default Modal;
