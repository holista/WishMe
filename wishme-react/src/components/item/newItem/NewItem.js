import { useRef, useState } from "react";
import { useHistory } from "react-router";
import { useSelector } from "react-redux";
import { FaCheckCircle } from "react-icons/fa/index";

import classes from "./NewItem.module.css";
import Image from "../../ui/Image";
import useApi from "../../../hooks/use-api";
import Spinner from "../../ui/Spinner";
import BlueBtn from "../../ui/buttons/BlueBtn";

const NewItem = (props) => {
  const history = useHistory();
  const urlInputRef = useRef();

  const token = useSelector((state) => state.auth.token);

  const [itemIsSent, setItemIsSent] = useState(false);
  const [name, setName] = useState("");
  const [price, setPrice] = useState(null);
  const [description, setDescription] = useState("");
  const [imageUrl, setImageUrl] = useState("");
  const [dataIsVisible, setDataIsVisible] = useState(false);

  const { isLoading, error, sendRequest } = useApi();

  const curUrl = history.location.pathname;
  const listUrl = curUrl.substring(0, curUrl.lastIndexOf("/"));
  const listId = listUrl.substring(listUrl.lastIndexOf("/") + 1);

  const getItem = (url) => {
    sendRequest(
      {
        url: `items/suggestions/heureka/detail?url=${url}`,
        headers: { Authorization: `Bearer ${token}` },
      },
      (responseData) => {
        setName(responseData.name);
        setPrice(responseData.price);
        setDescription(responseData.description);
        setImageUrl(responseData.imageUrl);
        setDataIsVisible(true);
      }
    );
  };

  const changeUrlHandler = (event) => {
    event.preventDefault();
    const url = urlInputRef.current.value;
    const parsedUrl = encodeURIComponent(url);
    getItem(parsedUrl);
  };

  const submitHandler = (event) => {
    event.preventDefault();
    setItemIsSent(false);

    sendRequest(
      {
        url: `wishlists/${listId}/items`,
        method: "POST",
        body: JSON.stringify({
          name,
          description,
          price,
          imageUrl,
          url: urlInputRef.current.value,
        }),
        headers: { Authorization: `Bearer ${token}` },
      },
      () => {
        setItemIsSent(true);
        props.itemAdded();
      }
    );
  };

  return (
    <>
      {!itemIsSent && (
        <div className={classes.section}>
          <div>
            <h2>Zadejte informace o novém předmětu.</h2>
          </div>
          <form className={classes.form} onSubmit={submitHandler}>
            <div className={classes.control}>
              <label htmlFor="searching">Začněte vyhledávat</label>
              <input type="text" id="searching" />
            </div>

            <div className={classes.control}>
              <label htmlFor="url">Zadejte url předmětu</label>
              <input
                type="url"
                id="url"
                ref={urlInputRef}
                onChange={changeUrlHandler}
              />
            </div>

            <div>{isLoading && <Spinner />}</div>

            {dataIsVisible && (
              <div className={classes.control}>
                <label htmlFor="title">Název</label>
                <input type="text" id="title" defaultValue={name} />
              </div>
            )}

            {dataIsVisible && (
              <div className={classes.control}>
                <label htmlFor="price">Cena</label>
                <input type="text" id="price" defaultValue={price} />
              </div>
            )}

            {dataIsVisible && (
              <div className={classes.control}>
                <label htmlFor="description">Popis</label>
                <textarea
                  type="text"
                  id="description"
                  rows="5"
                  defaultValue={description}
                />
              </div>
            )}

            {dataIsVisible && (
              <div className={classes.imageWrap}>
                <Image src={imageUrl} alt={name} className={classes.image} />
              </div>
            )}
            <div className={classes.btn}>
              <BlueBtn width="25%">Přidat předmět</BlueBtn>
            </div>
          </form>
        </div>
      )}
      {itemIsSent && (
        <div className={classes.check}>
          <FaCheckCircle />
        </div>
      )}
    </>
  );
};

export default NewItem;
