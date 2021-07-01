import { useSelector } from "react-redux";
import { useState, useRef } from "react";

import classes from "./NewItemList.module.css";
import useApi from "../../../hooks/use-api";

const NewItemList = (props) => {
  const [listIsCreated, setListIsCreated] = useState(false);
  const token = useSelector((state) => state.auth.token);

  const { isLoading, error, sendRequest } = useApi();

  const nameInputRef = useRef();
  const descriptionInputRef = useRef();

  const addListHandler = () => {
    const dataList = {
      name: nameInputRef.current.value,
      description: descriptionInputRef.current.value,
    };

    sendRequest(
      {
        url: `events/${props.eventId}/wishlists`,
        body: JSON.stringify(dataList),
        method: "POST",
        headers: { Authorization: `Bearer ${token}` },
      },
      () => {
        setListIsCreated(true);
      }
    );
  };

  return (
    <div className={classes.list}>
      <section className={classes.listName}>
        <div className={classes.control}>
          <input
            type="text"
            placeholder="Zadejte jméno seznamu"
            ref={nameInputRef}
            required
          />
        </div>
        <div className={classes.control}>
          <input
            type="text"
            rows="2"
            placeholder="Popište nový seznam"
            ref={descriptionInputRef}
          />
        </div>
        <div className={classes.control}>
          <button onClick={addListHandler}>Přidat seznam přání</button>
        </div>
      </section>
    </div>
  );
};

export default NewItemList;
