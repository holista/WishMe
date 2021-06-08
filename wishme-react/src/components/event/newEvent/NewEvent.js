import classes from "./NewEvent.module.css";
import Modal from "../../ui/Modal";
import { useState } from "react";

const NewEvent = (props) => {
  return (
    <Modal header="Vytvořte novou událost">
      <section className={classes.section}>
        <div>
          <h2>Zadejte základní informace o Vaší události.</h2>
        </div>
        <form className={classes.form}>
          <div className={classes.control}>
            <label htmlFor="title">Název události</label>
            <input type="text" id="title" />
          </div>
          <div className={classes.time}>
            <div className={classes.control}>
              <label htmlFor="date">Datum</label>
              <input type="date" id="date" />
            </div>
            <div className={classes.control}>
              <label htmlFor="time">Čas</label>
              <input type="time" id="time" />
            </div>
          </div>

          <div className={classes.control}>
            <label htmlFor="description">Popis</label>
            <textarea type="text" id="description" rows="5" />
          </div>
        </form>
      </section>

      <section className={classes.section}>
        <div>
          <h1>Co si přejete?</h1>
          <h2>Zadejte url předmětu nebo začněte vyhledávat v řádku.</h2>
        </div>
        <div className={classes.control}>
          <label htmlFor="description">Předmět</label>
          <input type="text" id="description" rows="5" />
        </div>
      </section>

      <section>
        <h3>vygenerovany odkaz</h3>
        <button>Vygenerovat odkaz</button>
      </section>
    </Modal>
  );
};

export default NewEvent;
