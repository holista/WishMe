import { useHistory, useParams } from "react-router-dom";
import { useSelector } from "react-redux";

import Event from "../components/event/Event";
import ItemLists from "../components/itemlist/ItemLists";
import NewItemList from "../components/itemlist/newItemList/NewItemList";
import NewItem from "../components/item/newItem/NewItem";

const EventPage = (props) => {
  const { evId } = useParams();

  const modalIsOpen = useSelector((state) => state.ui.modalIsOpen);

  return (
    <>
      <Event eventId={evId} />
      <div>
        <h1>Vyberte svému blízkému dárek dle jeho představ</h1>
      </div>
      <ItemLists eventId={evId} />
      <NewItemList eventId={evId} />
      {modalIsOpen && <NewItem eventId={evId} />}
    </>
  );
};

export default EventPage;
