import { useHistory } from "react-router-dom";
import { useSelector } from "react-redux";

import Event from "../components/event/Event";
import NewItem from "../components/item/newItem/NewItem";
import ItemList from "../components/itemlist/ItemList";
import NewItemList from "../components/itemlist/newItemList/NewItemList";
import useApi from "../hooks/use-api";
import { useEffect, useState } from "react";
import Spinner from "../components/ui/Spinner";

const EventPage = (props) => {
  const history = useHistory();
  const [lists, setLists] = useState(null);

  const id = history.location.pathname.substring(
    history.location.pathname.lastIndexOf("/") + 1
  );
  const token = useSelector((state) => state.auth.token);

  const modalIsOpen = useSelector((state) => state.ui.modalIsOpen);

  const { isLoading, error, sendRequest } = useApi();

  useEffect(() => {
    sendRequest(
      {
        url: `wishlists?offset=0&limit=100&eventId=${id}`,
        headers: { Authorization: `Bearer ${token}` },
      },
      (responseData) => {
        setLists(
          responseData.models.map((list) => (
            <ItemList
              eventId={id}
              key={list.id}
              name={list.name}
              description={list.description}
            />
          ))
        );
      }
    );
  }, [id, token, sendRequest]);

  return (
    <>
      <Event eventId={id} />
      <div>
        <h1>Vyberte svému blízkému dárek dle jeho představ</h1>
      </div>
      {isLoading && <Spinner />}
      {lists}
      <NewItemList eventId={id} />
      {modalIsOpen && <NewItem />}
    </>
  );
};

export default EventPage;
