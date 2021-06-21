import classes from "./Item.module.css";
import Card from "../ui/Card";

const Item = (props) => {
  return (
    <Card className={classes.item}>
      <div onClick={props.onClick}>
        <h1>{props.title}</h1>
      </div>
    </Card>
  );
};

export default Item;
