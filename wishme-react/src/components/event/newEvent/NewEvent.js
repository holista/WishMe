import { useRef, useState } from "react";
import { useSelector } from "react-redux";
import { useHistory } from "react-router";
import moment from "moment";

import classes from "./NewEvent.module.css";
import Spinner from "../../ui/Spinner";
import useApi from "../../../hooks/use-api";
import Image from "../../ui/Image";
import BlueBtn from "../../ui/buttons/BlueBtn";
import useImage from "../../../hooks/use-img";

const NewEvent = (props) => {
  const history = useHistory();
  const token = useSelector((state) => state.auth.token);

  const [thumb, setThumb] = useState();

  const titleInputRef = useRef();
  const dateInputRef = useRef();
  const timeInputRef = useRef();
  const descriptionInputRef = useRef();
  const imageInputRef = useRef();

  const { isLoading, error, sendRequest } = useApi();
  const { toBase64 } = useImage();

  const addEventHandler = async (event) => {
    event.preventDefault();

    const time = moment.duration(timeInputRef.current.value);
    const date = moment(dateInputRef.current.value);
    const dateTime = date.add(time).utc().format();

    const eventData = {
      name: titleInputRef.current.value,
      dateTimeUtc: dateTime,
      description: descriptionInputRef.current.value,
    };

    eventData.image = await toBase64(imageInputRef.current.files[0]);

    sendRequest(
      {
        url: `events`,
        method: "POST",
        body: JSON.stringify(eventData),
        headers: { Authorization: `Bearer ${token}` },
      },
      (responseData) => {
        history.replace(`/udalost/${responseData.id}`);
      }
    );
  };

  const changeImageHandler = async (e) => {
    e.preventDefault();
    const thumbnail = await toBase64(e.target.files[0]);
    setThumb(thumbnail);
  };

  return (
    <div className={classes.section}>
      <div className={classes.title}>
        <h2>Zadejte základní informace o Vaší události.</h2>
      </div>
      <form className={classes.form} onSubmit={addEventHandler}>
        <div className={classes.control}>
          <label htmlFor="title">Název události</label>
          <input type="text" id="title" ref={titleInputRef} />
        </div>
        <div className={classes.time}>
          <div className={classes.control}>
            <label htmlFor="date">Datum</label>
            <input type="date" id="date" ref={dateInputRef} />
          </div>
          <div className={classes.control}>
            <label htmlFor="time">Čas</label>
            <input type="time" id="time" ref={timeInputRef} />
          </div>
        </div>

        <div className={classes.control}>
          <label htmlFor="description">Popis</label>
          <textarea
            type="text"
            id="description"
            rows="5"
            ref={descriptionInputRef}
          />
        </div>

        <div className={classes.controlImg}>
          <label htmlFor="image">Obrázek</label>
          {thumb && (
            <div className={classes.thumbWrap}>
              <Image src={`data:image/jpeg;base64,${thumb}`} />
            </div>
          )}
          <input
            type="file"
            accept="image/*"
            multiple={false}
            id="image"
            onChange={changeImageHandler}
            ref={imageInputRef}
          />
        </div>

        <div className={classes.btn}>
          <BlueBtn width="25%">Přidat událost</BlueBtn>
        </div>
      </form>
    </div>
  );
};

export default NewEvent;
