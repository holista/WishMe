import classes from "./Modal.module.css";

const Modal = (props) => {
  return (
    <>
      {props.modalIsOpen && (
        <div className={classes.overlay}>
          <div className={classes.backdrop} onClick={props.onClose}></div>
          <div className={classes.modal}>
            <div className={classes.close}>
              <button onClick={props.onClose}>x</button>
            </div>
            {props.header && (
              <div className={classes.header}>
                <h1>{props.header}</h1>
              </div>
            )}
            {props.children}
          </div>
        </div>
      )}
    </>
  );
};

export default Modal;
