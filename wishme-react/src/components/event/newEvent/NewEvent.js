import { useRef } from "react";
import { useSelector } from "react-redux";
import { useHistory } from "react-router";
import moment from "moment";

import classes from "./NewEvent.module.css";
import Modal from "../../ui/Modal";
import useApi from "../../../hooks/use-api";

const NewEvent = (props) => {
  const history = useHistory();
  const token = useSelector((state) => state.auth.token);

  const titleInputRef = useRef();
  const dateInputRef = useRef();
  const timeInputRef = useRef();
  const descriptionInputRef = useRef();
  const imageInputRef = useRef();

  const { isLoading, error, sendRequest } = useApi();

  const toBase64 = async (file) => {
    try {
      const imageStr = await toBase64Convertor(file);
      const index = imageStr.indexOf("base64,");
      return imageStr.substring(index + 7);
    } catch (e) {
      return "";
    }
  };

  const toBase64Convertor = (file) => {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => resolve(reader.result.toString());
      reader.onerror = (error) => reject(error);
    });
  };

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
        history.replace(`/event/${responseData.id}`);
      }
    );
  };

  return (
    <Modal header="Vytvořte novou událost">
      <section className={classes.section}>
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

          <div className={classes.control}>
            <label htmlFor="image">Obrázek</label>
            <input
              type="file"
              accept="image/*"
              multiple={false}
              id="image"
              ref={imageInputRef}
            />
          </div>

          <div className={classes.btn}>
            <button>Přidat událost</button>
          </div>
        </form>
      </section>
    </Modal>
  );
};

export default NewEvent;
