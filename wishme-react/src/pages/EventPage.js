import { useHistory } from "react-router-dom";
import { useSelector } from "react-redux";

import NewItem from "../components/item/newItem/NewItem";
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
