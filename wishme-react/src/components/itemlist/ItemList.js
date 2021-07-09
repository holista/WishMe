import { useEffect, useRef, useState } from "react";
import { useHistory } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";

import classes from "./ItemList.module.css";
import Carousel from "../carousel/Carousel";
import useApi from "../../hooks/use-api";
import Spinner from "../ui/Spinner";
import { eventActions } from "../../store/event-slice";
import Card from "../ui/Card";
import Modal from "../ui/Modal";
import EditBar from "../ui/editBar/EditBar";
import NewItem from "../item/newItem/NewItem";
import GreyBtn from "../ui/buttons/GreyBtn";

const ItemList = (props) => {
  const dispatch = useDispatch();
  const history = useHistory();

  const token = useSelector((state) => state.auth.token);
  const eventId = props.eventId;
  const listId = props.wishlistId;

  const items = useSelector((state) => state.event.items[listId]) ?? [];

  const { isLoading, error, sendRequest } = useApi();

  const [isRemoving, setIsRemoving] = useState(false);
  const [isAddingItem, setIsAddingItem] = useState(false);
  const [itemWasAdded, setItemWasAdded] = useState(false);
  const [editModeIsActive, setEditModeIsActive] = useState(false);

  const nameInputRef = useRef();
  const descriptionInputRef = useRef();

  const openNewItemHandler = () => {
    setIsAddingItem(true);
    history.push(`/udalost/${eventId}/seznam/${listId}/nova-polozka`);
  };

  const closeNewItemHandler = () => {
    setIsAddingItem(false);
    setItemWasAdded(false);
    history.goBack();
  };

  const openItemHandler = (itemId) => {
    history.push(`/udalost/${eventId}/seznam/${listId}/polozka/${itemId}`);
  };

  const itemWasAddedHandler = () => {
    setItemWasAdded(true);
  };

  useEffect(() => {
    sendRequest(
      {
        url: `items?offset=0&limit=100&wishlistId=${listId}`,
        headers: { Authorization: `Bearer ${token}` },
      },
      (responseData) => {
        console.log("Itemlist: seting items of the list");
        dispatch(
          eventActions.setItems({
            id: listId,
            items: responseData.models.map((item) => {
              return {
                id: item.id,
                key: item.id,
                name: item.name,
                price: item.price,
                description: item.description,
                imageUrl: item.imageUrl,
                claimed: item.claimed,
              };
            }),
          })
        );
      }
    );
  }, []);

  const removeListHandler = () => {
    sendRequest({
      url: `wishlists/${listId}`,
      method: "DELETE",
      headers: { Authorization: `Bearer ${token}` },
    });
    setIsRemoving(false);
  };

  const saveListHandler = () => {
    sendRequest(
      {
        url: `wishlists/${listId}`,
        method: "PUT",
        body: JSON.stringify(),
        headers: { Authorization: `Bearer ${token}` },
      },
      setEditModeIsActive(false)
    );
  };

  return (
    <>
      {isLoading && <Spinner />}
      {!isLoading && (
        <Card>
          <EditBar
            onRemove={() => setIsRemoving(true)}
            editing={!editModeIsActive}
            onEdit={() => setEditModeIsActive(true)}
            onSave={saveListHandler}
          />
          <section className={classes.listName}>
            <div className={classes.control}>
              {!editModeIsActive ? (
                <h1>{props.name}</h1>
              ) : (
                <input
                  type="text"
                  id="title"
                  defaultValue={props.name}
                  ref={nameInputRef}
                />
              )}
            </div>
            <div className={classes.control}>
              {!editModeIsActive ? (
                <p>{props.description}</p>
              ) : (
                <textarea
                  type="text"
                  id="description"
                  rows="3"
                  defaultValue={props.description}
                  ref={descriptionInputRef}
                />
              )}
            </div>
          </section>
          <Carousel
            defaultTitle="Přidejte nový předmět"
            data={items}
            onNewData={openNewItemHandler}
            onData={openItemHandler}
          />
        </Card>
      )}

      <Modal
        modalIsOpen={isAddingItem}
        onClose={closeNewItemHandler}
        header={!itemWasAdded ? "Přidejte nový předmět" : "Předmět byl přidán"}
      >
        <NewItem eventId={eventId} itemAdded={itemWasAddedHandler} />
      </Modal>

      <Modal
        modalIsOpen={isRemoving}
        onClose={() => setIsRemoving(false)}
        header="Vážně chcete seznam smazat?"
      >
        <GreyBtn onClick={removeListHandler}>Smazat</GreyBtn>
      </Modal>
    </>
  );
};

export default ItemList;
