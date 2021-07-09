import classes from "./BlueBtn.module.css";

const BlueBtn = (props) => {
  return (
    <div className={classes.btn} style={{ width: props.width }}>
      <button onClick={props.onClick}>{props.children}</button>
    </div>
  );
};

export default BlueBtn;
