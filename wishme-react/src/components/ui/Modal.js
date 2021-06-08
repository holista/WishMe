import { useDispatch, useSelector } from "react-redux";
import { uiActions } from "../../store/ui-slice";

import classes from "./Modal.module.css";

const Modal = (props) => {
  const modalIsOpen = useSelector((state) => state.ui.modalIsOpen);
  const dispatch = useDispatch();

  if (modalIsOpen) {
    document.body.style.overflow = "hidden";
  }

  const closeModalHandler = () => {
    dispatch(uiActions.closeModal());
  };

  return (
    <>
      {modalIsOpen && (
        <div className={classes.backdrop} onClick={closeModalHandler}></div>
      )}
      {modalIsOpen && (
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
