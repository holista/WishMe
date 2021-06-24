import { useHistory } from "react-router-dom";
import { useDispatch } from "react-redux";

import classes from "./ItemList.module.css";
import { uiActions } from "../../store/ui-slice";
import Carousel from "../carousel/Carousel";

const ItemList = (props) => {
  const DUMMY_DATA = [
    { name: "1" },
    { name: "2" },
    { name: "3" },
    { name: "4" },
  ];

  const dispatch = useDispatch();
  const history = useHistory();

  const openNewItemHandler = () => {
    dispatch(uiActions.openModal());
    history.push(`/event/${props.eventId}/new-item`);
  };

  const openItemHandler = () => {};

  return (
    <div className={classes.list}>
      <section className={classes.listName}>
        <div className={classes.control}>
          <h3>{props.name}</h3>
        </div>
        <div className={classes.control}>
          <h3>{props.description}</h3>
        </div>
      </section>

      <Carousel
        defaultTitle="Přidejte nový předmět"
        data={DUMMY_DATA}
        onNewData={openNewItemHandler}
        onData={openItemHandler}
      />
    </div>
  );
};

export default ItemList;
