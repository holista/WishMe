import { useRef, useEffect, useState } from "react";
import { useSelector } from "react-redux";

import classes from "./NewItem.module.css";
import Modal from "../../ui/Modal";
import Image from "../../ui/Image";

const NewItem = (props) => {
  const urlInputRef = useRef();

  const token = useSelector((state) => state.auth.token);
  const [name, setName] = useState("");
  const [price, setPrice] = useState(null);
  const [imageUrl, setImageUrl] = useState("");
  const [dataIsVisible, setDataIsVisible] = useState(false);

  const getItem = (url) => {
    fetch(
      `http://localhost:8085/api/v1/items/suggestions/heureka/detail?url=${url}`,
      {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          accept: "application/json",
          Authorization: `Bearer ${token}`,
        },
      }
    )
      .then((response) => response.json())
      .then((responseData) => {
        setName(responseData.name);
        setPrice(responseData.price);
        setImageUrl(responseData.imageUrl);
        setDataIsVisible(true);
      });
  };

  const changeUrlHandler = (event) => {
    event.preventDefault();
    const url = urlInputRef.current.value;
    const parsedUrl = encodeURIComponent(url);
    getItem(parsedUrl);
  };

  const submitHandler = (event) => {
    event.preventDefault();
  };

  return (
    <Modal header="Přidejte nový předmět">
      <section className={classes.section}>
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

          {dataIsVisible && (
            <div className={classes.control}>
              <label htmlFor="title">Název</label>
              <input type="text" id="title" value={name} />
            </div>
          )}

          {dataIsVisible && (
            <div className={classes.control}>
              <label htmlFor="price">Cena</label>
              <input type="text" id="price" value={price} />
            </div>
          )}

          {dataIsVisible && (
            <div className={classes.control}>
              <label htmlFor="price">Popis</label>
              <textarea type="text" id="price" rows="5" />
            </div>
          )}

          {dataIsVisible && (
            <div className={classes.imageWrap}>
              <Image src={imageUrl} alt={name} className={classes.image} />
            </div>
          )}

          <div className={classes.btn}>
            <button>Přidat předmět</button>
          </div>
        </form>
      </section>
    </Modal>
  );
};

export default NewItem;
