import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router";
import { uiActions } from "../../store/ui-slice";

import classes from "./Modal.module.css";

const Modal = (props) => {
  const modalIsOpen = useSelector((state) => state.ui.modalIsOpen);
  const dispatch = useDispatch();
  const history = useHistory();

  if (modalIsOpen) {
    document.body.style.overflow = "hidden";
  }

  const closeModalHandler = () => {
    dispatch(uiActions.closeModal());
    history.goBack();
  };

  return (
    <>
      {modalIsOpen && (
        <div className={classes.overlay}>
          <div className={classes.backdrop} onClick={closeModalHandler}></div>
          <div className={classes.modal}>
            <div className={classes.close}>
              <button onClick={closeModalHandler}>x</button>
            </div>
            <div className={classes.modalHeader}>
              <h1>{props.header}</h1>
            </div>
            {props.children}
          </div>
        </div>
      )}
    </>
  );
};

export default Modal;
