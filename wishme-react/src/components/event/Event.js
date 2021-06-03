import classes from "./Event.module.css";
import Card from "../ui/Card";

const Event = (props) => {
  return (
    <Card className={classes.event}>
      <h1>{props.title}</h1>
    </Card>
  );
};

export default Event;
