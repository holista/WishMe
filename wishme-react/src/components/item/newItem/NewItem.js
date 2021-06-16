import classes from "./NewItem.module.css";
import Modal from "../../ui/Modal";

const NewItem = (props) => {
  return (
    <Modal header="Přidejte nový předmět">
      <section className={classes.section}>
        <div>
          <h2>Zadejte informace o novém předmětu.</h2>
        </div>
        <form className={classes.form}>
          <div className={classes.control}>
            <label htmlFor="url">Zadejte url předmětu</label>
            <input type="url" id="url" />
          </div>

          <div className={classes.control}>
            <label htmlFor="searching">Začněte vyhledávat</label>
            <input type="text" id="searching" />
          </div>

          <div className={classes.control}>
            <label htmlFor="title">Název</label>
            <input type="text" id="title" />
          </div>

          <div className={classes.control}>
            <label htmlFor="price">Cena</label>
            <input type="text" id="price" />
          </div>

          <div className={classes.control}>
            <label htmlFor="price">Popis</label>
            <textarea type="text" id="price" rows="5" />
          </div>

          <div className={classes.control}>
            <label htmlFor="image">Obrázek</label>
            <input type="text" id="image" />
          </div>
        </form>
      </section>
    </Modal>
  );
};

export default NewItem;
