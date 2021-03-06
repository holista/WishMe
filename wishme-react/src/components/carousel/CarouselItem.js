import classes from "./CarouselItem.module.css";
import Image from "../ui/Image";

const CarouselItem = (props) => {
  let content = <h1>{props.title}</h1>;

  const bigger = props.bigger && classes.bigger;

  if (props.image) {
    content = (
      <div className={classes.imageWrap}>
        <Image src={props.image} className={classes.image} />
      </div>
    );
  }

  return (
    <div className={`${classes.item} + ${bigger}`} onClick={props.onClick}>
      {content}
    </div>
  );
};

export default CarouselItem;
