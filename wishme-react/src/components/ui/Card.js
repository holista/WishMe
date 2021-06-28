import classes from "./Card.module.css";

const Card = (props) => {
  return (
    <div className={props.className} onClick={props.onClick} key={props.key}>
      {props.children}
    </div>
  );
};

export default Card;
