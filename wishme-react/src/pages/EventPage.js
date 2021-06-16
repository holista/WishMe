import { useHistory } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";

import Carousel from "../components/carousel/Carousel";
import NewItem from "../components/item/newItem/NewItem";
import { uiActions } from "../store/ui-slice";
import ItemList from "../components/item/ItemList";

const EventPage = (props) => {
  const history = useHistory();

  const modalIsOpen = useSelector((state) => state.ui.modalIsOpen);
  if (!modalIsOpen) {
    history.replace("/event");
  }

  return (
    <>
      <ItemList />
      {modalIsOpen && <NewItem />}
    </>
  );
};

export default EventPage;
