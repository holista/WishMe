import classes from "./Event.module.css";
import Card from "../ui/Card";

const Event = (props) => {
  return (
    <Card className={classes.eventItem}>
      <div onClick={props.onClick}>
        <h1>{props.title}</h1>
      </div>
    </Card>
  );
};

export default Event;
