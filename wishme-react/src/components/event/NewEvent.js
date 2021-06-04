import classes from "./NewEvent.module.css";
import Card from "../ui/Card";

const NewEvent = (props) => {
  const nextStepHandler = () => {};

  return (
    <Card className={classes.newEvent}>
      <div className={classes.steps}>
        <span>1</span>
        <span>2</span>
        <span>3</span>
      </div>
      <form>
        <section>
          <div>
            <label htmlFor="title">Název události</label>
            <input type="text" id="title" />
          </div>
          <div>
            <label htmlFor="date">Datum</label>
            <input type="date" id="date" />
          </div>
          <div>
            <label htmlFor="description">Popis</label>
            <input type="textarea" id="description" />
          </div>
        </section>
        <button type="button" onClick={nextStepHandler}>
          Další krok
        </button>
      </form>
    </Card>
  );
};

export default NewEvent;
