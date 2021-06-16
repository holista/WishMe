import classes from "./NewEvent.module.css";
import Modal from "../../ui/Modal";
import { useRef } from "react";

const NewEvent = (props) => {
  const titleInputRef = useRef();
  const dateInputRef = useRef();
  const timeInputRef = useRef();
  const descriptionInputRef = useRef();

  const sendData = () => {
    const eventData = {};
    fetch("", { method: "POST", body: JSON.stringify(eventData) });
  };

  const submitHandler = (event) => {
    event.preventDefault();
  };

  return (
    <Modal header="Vytvořte novou událost">
      <section className={classes.section}>
        <div>
          <h2>Zadejte základní informace o Vaší události.</h2>
        </div>
        <form className={classes.form} onSubmit={submitHandler}>
          <div className={classes.control}>
            <label htmlFor="title">Název události</label>
            <input type="text" id="title" ref={titleInputRef} />
          </div>
          <div className={classes.time}>
            <div className={classes.control}>
              <label htmlFor="date">Datum</label>
              <input type="date" id="date" ref={dateInputRef} />
            </div>
            <div className={classes.control}>
              <label htmlFor="time">Čas</label>
              <input type="time" id="time" ref={timeInputRef} />
            </div>
          </div>

          <div className={classes.control}>
            <label htmlFor="description">Popis</label>
            <textarea
              type="text"
              id="description"
              rows="5"
              ref={descriptionInputRef}
            />
          </div>

          <div className={classes.btn}>
            <button>Přidat událost</button>
          </div>
        </form>
      </section>
    </Modal>
  );
};

export default NewEvent;
