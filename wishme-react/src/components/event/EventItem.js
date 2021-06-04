import classes from "./EventItem.module.css";
import Card from "../ui/Card";

const EventItem = (props) => {
  return (
    <Card className={classes.eventItem}>
      <h1>{props.title}</h1>
    </Card>
  );
};

export default EventItem;
