import { useSelector } from "react-redux";
import { useState, useRef } from "react";

import classes from "./NewItemList.module.css";
import useApi from "../../../hooks/use-api";
import Card from "../../ui/Card";
import BlueBtn from "../../ui/buttons/BlueBtn";

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
    <Card>
      <form className={classes.form}>
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
        </section>

        <BlueBtn onClick={addListHandler} width="25%">
          Přidat seznam přání
        </BlueBtn>
      </form>
    </Card>
  );
};

export default NewItemList;
