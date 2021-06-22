import { useHistory } from "react-router-dom";
import { useDispatch } from "react-redux";

import { useSelector } from "react-redux";

import classes from "./ItemList.module.css";
import { uiActions } from "../../store/ui-slice";
import Carousel from "../carousel/Carousel";
import useApi from "../../hooks/use-api";

const ItemList = (props) => {
  const DUMMY_DATA = [
    { name: "1" },
    { name: "2" },
    { name: "3" },
    { name: "4" },
  ];

  const dispatch = useDispatch();
  const history = useHistory();

  const { token } = useSelector((state) => state.auth.token);

  const { isLoading, error, sendRequest } = useApi();

  const addListHandler = () => {
    sendRequest({
      url: `events/${props.eventId}/wishlists`,
      method: "POST",
      headers: { Authorization: `Bearer ${token}` },
    });
  };

  const openNewItemHandler = () => {
    dispatch(uiActions.openModal());
    history.push("/event/new-item");
  };

  return (
    <div className={classes.list}>
      <div className={classes.listName}>
        <button onClick={addListHandler}>Přidat seznam přání</button>
        <h1>Zadejte jméno seznamu</h1>
      </div>
      <Carousel
        defaultTitle="Přidejte nový předmět"
        data={DUMMY_DATA}
        onNewData={openNewItemHandler}
      />
    </div>
  );
};

export default ItemList;
