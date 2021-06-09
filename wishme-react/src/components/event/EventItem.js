import classes from "./EventItem.module.css";
import Card from "../ui/Card";

const EventItem = (props) => {
  return (
    <Card className={classes.eventItem}>
      <div onClick={props.onClick}>
        <h1>{props.title}</h1>
      </div>
    </Card>
  );
};

export default EventItem;
