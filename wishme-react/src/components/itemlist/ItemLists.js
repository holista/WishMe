import { useSelector, useDispatch } from "react-redux";
import { useEffect } from "react";

import ItemList from "./ItemList";
import useApi from "../../hooks/use-api";
import Spinner from "../ui/Spinner";
import { eventActions } from "../../store/event-slice";

const ItemLists = (props) => {
  const dispatch = useDispatch();

  const token = useSelector((state) => state.auth.token);
  const eventId = props.eventId;
  const lists = useSelector((state) => state.event.lists);
  const { isLoading, error, sendRequest } = useApi();

  useEffect(() => {
    sendRequest(
      {
        url: `wishlists?offset=0&limit=100&eventId=${eventId}`,
        headers: { Authorization: `Bearer ${token}` },
      },
      (responseData) => {
        dispatch(
          eventActions.setLists(
            responseData.models.map((list) => ({
              eventId: eventId,
              key: list.id,
              wishlistId: list.id,
              name: list.name,
              description: list.description,
            }))
          )
        );
        console.log("ItemLists: seting all lists");
      }
    );
  }, [token, sendRequest, eventId, dispatch]);

  return (
    <>
      {isLoading && <Spinner />}
      {!isLoading &&
        lists.map((list) => (
          <ItemList
            key={list.wishlistId}
            eventId={list.eventId}
            wishlistId={list.wishlistId}
            name={list.name}
            description={list.description}
          />
        ))}
    </>
  );
};

export default ItemLists;
