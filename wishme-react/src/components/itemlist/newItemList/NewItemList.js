import { useSelector } from "react-redux";
import { useState, useRef } from "react";

import classes from "./NewItemList.module.css";
import useApi from "../../../hooks/use-api";
import Card from "../../ui/Card";
import BlueBtn from "../../ui/buttons/BlueBtn";

const NewItemList = (props) => {
  const token = useSelector((state) => state.auth.token);

  const [inputError, setInputError] = useState(null);

  const { isLoading, error, sendRequest } = useApi();

  const nameInputRef = useRef();
  const descriptionInputRef = useRef();

  const addListHandler = (e) => {
    e.preventDefault();
    const name = nameInputRef.current.value;
    const description = descriptionInputRef.current.value;

    if (name.length === 0) {
      setInputError("Vyplňte prosím název seznamu!");
      return;
    }
    setInputError(null);

    const dataList = {
      name,
      description,
    };

    sendRequest({
      url: `events/${props.eventId}/wishlists`,
      body: JSON.stringify(dataList),
      method: "POST",
      headers: { Authorization: `Bearer ${token}` },
    });
  };

  return (
    <>
      {props.visible && (
        <Card>
          <form className={classes.form} onSubmit={addListHandler}>
            <section className={classes.listName}>
              <div className={classes.control}>
                <input
                  type="text"
                  placeholder="Zadejte název seznamu"
                  ref={nameInputRef}
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

            <BlueBtn width="25%">Přidat seznam přání</BlueBtn>
          </form>
          {inputError && <div className={classes.error}>{inputError}</div>}
        </Card>
      )}
    </>
  );
};

export default NewItemList;
