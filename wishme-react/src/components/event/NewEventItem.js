import classes from "./NewEventItem.module.css";
import Card from "../ui/Card";

const NewEventItem = (props) => {
  return (
    <Card className={classes.newEventItem}>
      <h1 className={classes.title}>Vytvořte událost</h1>
      <span className={classes.plus}>+</span>
    </Card>
  );
};

export default NewEventItem;
