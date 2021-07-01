import { useHistory } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";

import classes from "./ItemList.module.css";
import { uiActions } from "../../store/ui-slice";
import Carousel from "../carousel/Carousel";
import useApi from "../../hooks/use-api";
import Spinner from "../ui/Spinner";
import { useEffect } from "react";
import { eventActions } from "../../store/event-slice";

const ItemList = (props) => {
  const dispatch = useDispatch();
  const history = useHistory();

  const token = useSelector((state) => state.auth.token);
  const eventId = props.eventId;
  const listId = props.wishlistId;
  const items = useSelector((state) => state.event.items);

  const { isLoading, error, sendRequest } = useApi();

  const openNewItemHandler = () => {
    dispatch(uiActions.openModal());
    history.push(`/event/${eventId}/wishlist/${listId}/new-item`);
  };

  const openItemHandler = () => {};

  useEffect(() => {
    sendRequest(
      {
        url: `items?offset=0&limit=100&wishlistId=${listId}`,
        headers: { Authorization: `Bearer ${token}` },
      },
      (responseData) => {
        console.log("Itemlist: seting items of the list");
        return dispatch(
          eventActions.setItems(
            responseData.models.map((item) => ({
              itemId: item.id,
              key: item.id,
              name: item.name,
              price: item.price,
              description: item.description,
              imageUrl: item.imageUrl,
              claimed: item.claimed,
            }))
          )
        );
      }
    );
  }, [listId, token, sendRequest, dispatch]);

  return (
    <div className={classes.list}>
      {isLoading && <Spinner />}
      {!isLoading && (
        <>
          <section className={classes.listName}>
            <div className={classes.control}>
              <h3>{props.name}</h3>
            </div>
            <div className={classes.control}>
              <h3>{props.description}</h3>
            </div>
          </section>
          <Carousel
            defaultTitle="Přidejte nový předmět"
            data={items}
            onNewData={openNewItemHandler}
            onData={openItemHandler}
          />
        </>
      )}
    </div>
  );
};

export default ItemList;
