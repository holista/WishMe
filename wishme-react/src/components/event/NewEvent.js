import classes from "./NewEvent.module.css";
import Card from "../ui/Card";

const NewEvent = (props) => {
  return (
    <Card className={classes.newEvent}>
      <h1 className={classes.title}>Vytvořte událost</h1>
      <span className={classes.plus}>+</span>
    </Card>
  );
};

export default NewEvent;
