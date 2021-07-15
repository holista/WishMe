import Loader from "react-loader-spinner";
import "react-loader-spinner/dist/loader/css/react-spinner-loader.css";

import classes from "./Spinner.module.css";

const Spinner = (props) => {
  return (
    <div className={classes.spinner + props.style}>
      <Loader
        type="TailSpin"
        color="#421daa"
        height={props.height ?? 80}
        width={props.width ?? 80}
        timeout={3000} //3 secs
      />
    </div>
  );
};

export default Spinner;
