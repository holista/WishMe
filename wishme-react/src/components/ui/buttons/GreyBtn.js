import classes from "./GreyBtn.module.css";

const GreyBtn = (props) => {
  return (
    <div className={classes.btn} style={{ width: props.width }}>
      <button onClick={props.onClick}>{props.children}</button>
    </div>
  );
};

export default GreyBtn;
