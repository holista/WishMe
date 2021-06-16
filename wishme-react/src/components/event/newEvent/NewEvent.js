import classes from "./NewEvent.module.css";
import Modal from "../../ui/Modal";

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

          <section>
            <div className={classes.control}>
              {/*<label htmlFor="title">Vygenerovaný odkaz</label>*/}
              <input type="text" id="title" value="Vygenerovaný odkaz" />
            </div>
            <div className={classes.btn}>
              <button>Přidat událost</button>
            </div>
          </section>
        </form>
      </section>
    </Modal>
  );
};

export default NewEvent;
