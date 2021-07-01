import { useHistory } from "react-router-dom";
import { useSelector } from "react-redux";

import Event from "../components/event/Event";
import ItemLists from "../components/itemlist/ItemLists";
import NewItemList from "../components/itemlist/newItemList/NewItemList";
import NewItem from "../components/item/newItem/NewItem";

const EventPage = (props) => {
  const history = useHistory();
  const id = history.location.pathname.substring(
    history.location.pathname.lastIndexOf("/") + 1
  );
  const modalIsOpen = useSelector((state) => state.ui.modalIsOpen);

  return (
    <>
      <Event eventId={id} />
      <div>
        <h1>Vyberte svému blízkému dárek dle jeho představ</h1>
      </div>
      <ItemLists eventId={id} />
      <NewItemList eventId={id} />
      {modalIsOpen && <NewItem eventId={id} />}
    </>
  );
};

export default EventPage;
