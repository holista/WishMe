import { useState } from "react";
import { useParams } from "react-router-dom";

import Event from "../components/event/Event";
import ItemLists from "../components/itemlist/ItemLists";
import NewItemList from "../components/itemlist/newItemList/NewItemList";
import Spinner from "../components/ui/Spinner";

const EventPage = (props) => {
  const { evId } = useParams();

  const [evLoaded, setEvLoaded] = useState(false);
  const [listsLoaded, setListsLoaded] = useState(false);

  let visible = evLoaded && listsLoaded;

  return (
    <>
      {!visible && <Spinner />}

      <Event
        eventId={evId}
        visible={visible}
        isLoaded={() => setEvLoaded(true)}
      />
      <div>
        <h1>Vyberte svému blízkému dárek dle jeho představ</h1>
      </div>
      <ItemLists
        eventId={evId}
        visible={visible}
        isLoaded={() => setListsLoaded(true)}
      />
      <NewItemList eventId={evId} visible={visible} />
    </>
  );
};

export default EventPage;
