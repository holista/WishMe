import { useHistory } from "react-router-dom";
import { useSelector } from "react-redux";

import Event from "../components/event/Event";
import NewItem from "../components/item/newItem/NewItem";
import ItemList from "../components/item/ItemList";

const EventPage = (props) => {
  const history = useHistory();

  const id = history.location.pathname.substring(
    history.location.pathname.lastIndexOf("/") + 1
  );

  const modalIsOpen = useSelector((state) => state.ui.modalIsOpen);
  if (!modalIsOpen) {
    history.replace(`/event/${id}`);
  }

  return (
    <>
      <Event eventId={id} />
      <ItemList />
      {modalIsOpen && <NewItem />}
    </>
  );
};

export default EventPage;
