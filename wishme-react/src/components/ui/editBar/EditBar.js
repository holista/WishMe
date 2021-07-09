import {
  FaPencilAlt,
  FaTrashAlt,
  FaArrowLeft,
  FaSave,
} from "react-icons/fa/index";
import classes from "./EditBar.module.css";

const EditBar = (props) => {
  return (
    <div className={classes.edit}>
      <div>
        {props.arrowBack && (
          <button onClick={props.goTo}>
            <FaArrowLeft />
          </button>
        )}
      </div>
      <div>
        <button onClick={props.onRemove}>
          <FaTrashAlt />
        </button>
        {props.editing ? (
          <button onClick={props.onEdit}>
            <FaPencilAlt />
          </button>
        ) : (
          <button onClick={props.onSave}>
            <FaSave />
          </button>
        )}
      </div>
    </div>
  );
};

export default EditBar;
