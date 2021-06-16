import { useHistory } from "react-router-dom";
import { useDispatch } from "react-redux";

import classes from "./ItemList.module.css";
import { uiActions } from "../../store/ui-slice";
import Carousel from "../carousel/Carousel";

const ItemList = (props) => {
  const DUMMY_DATA = [
    { title: "1" },
    { title: "2" },
    { title: "3" },
    { title: "4" },
  ];

  const dispatch = useDispatch();
  const history = useHistory();

  const openNewItemHandler = () => {
    dispatch(uiActions.openModal());
    history.push("/event/new-item");
  };

  return (
    <>
      <div>
        <h1>Vyberte svému blízkému dárek dle jeho představ</h1>
      </div>
      <div className={classes.list}>
        <div className={classes.listName}>
          <h1>Zadejte jméno seznamu</h1>
        </div>
        <Carousel
          defaultTitle="Přidejte nový předmět"
          data={DUMMY_DATA}
          onNewData={openNewItemHandler}
        />
      </div>
    </>
  );
};

export default ItemList;
