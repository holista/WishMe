import classes from "./CarouselItem.module.css";
import Card from "../ui/Card";

const CarouselItem = (props) => {
  return (
    <Card className={classes.eventItem}>
      <div onClick={props.onClick} id={props.id}>
        <h1>{props.title}</h1>
      </div>
    </Card>
  );
};

export default CarouselItem;
