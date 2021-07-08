import { useParams } from "react-router-dom";

import Event from "../components/event/Event";
import ItemLists from "../components/itemlist/ItemLists";
import NewItemList from "../components/itemlist/newItemList/NewItemList";

const EventPage = (props) => {
  const { evId } = useParams();

  return (
    <>
      <Event eventId={evId} />
      <div>
        <h1>Vyberte svému blízkému dárek dle jeho představ</h1>
      </div>
      <ItemLists eventId={evId} />
      <NewItemList eventId={evId} />
    </>
  );
};

export default EventPage;
