import classes from "./NewEventItem.module.css";
import Card from "../ui/Card";

const NewEventItem = (props) => {
  return (
    <Card className={classes.newEventItem}>
      <div onClick={props.onClick}>
        <h1 className={classes.title}>Vytvořte událost</h1>
        <span className={classes.plus}>+</span>
      </div>
    </Card>
  );
};

export default NewEventItem;
