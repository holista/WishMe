import { useSelector } from "react-redux";
import { useEffect, useState } from "react";
import { FaCalendarAlt, FaClock } from "react-icons/fa/index";
import { useHistory } from "react-router";
import moment from "moment";

import classes from "./Event.module.css";
import Card from "../ui/Card";
import useApi from "../../hooks/use-api";
import Spinner from "../ui/Spinner";
import EditBar from "../ui/editBar/EditBar";
import Modal from "../ui/Modal";
import GreyBtn from "../ui/buttons/GreyBtn";
import { useRef } from "react";

const Event = (props) => {
  const history = useHistory();
  const token = useSelector((state) => state.auth.token);
  const eventId = props.eventId;

  const [title, setTitle] = useState();
  const [date, setDate] = useState();
  const [time, setTime] = useState();
  const [description, setDescription] = useState();
  const [isRemoving, setIsRemoving] = useState(false);
  const [editModeIsActive, setEditModeIsActive] = useState(false);

  const { isLoading, error, sendRequest } = useApi();

  const titleInputRef = useRef();
  const dateInputRef = useRef();
  const timeInputRef = useRef();
  const descriptionInputRef = useRef();
  const imageInputRef = useRef();

  useEffect(() => {
    sendRequest(
      {
        url: `events/${eventId}`,
        headers: { Authorization: `Bearer ${token}` },
      },
      (responseData) => {
        const dateData = moment(responseData.dateTimeUtc).toDate();
        const dayData = `${dateData.getDay()}.${dateData.getMonth()}.${dateData.getFullYear()}`;
        const timeData = `${dateData.getHours()}:${dateData.getMinutes()}`;

        setTitle(responseData.name);
        setDate(dayData);
        setTime(timeData);
        setDescription(responseData.description);
      }
    );
  }, [eventId, token, sendRequest]);

  const removeEventHandler = () => {
    sendRequest(
      {
        url: `events/${eventId}`,
        method: "DELETE",
        headers: { Authorization: `Bearer ${token}` },
      },
      history.replace("/mainpage")
    );
  };

  const editEventHandler = () => {
    setEditModeIsActive(true);
  };

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

  const saveEventHandler = async () => {
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
        url: `events/${eventId}`,
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
            arrowBack
            goTo={() => history.replace("/mainpage")}
            onRemove={() => setIsRemoving(true)}
            editing={!editModeIsActive}
            onEdit={editEventHandler}
            onSave={saveEventHandler}
          />

          <div className={classes.title}>
            {!editModeIsActive ? (
              <h1>{title}</h1>
            ) : (
              <input type="text" id="title" value={title} ref={titleInputRef} />
            )}
          </div>
          <div className={classes.dateTime}>
            <div className={classes.control}>
              <span className={classes.icon}>
                <FaCalendarAlt />
              </span>
              {!editModeIsActive ? (
                <h3>{date}</h3>
              ) : (
                <input type="date" id="date" value={date} ref={dateInputRef} />
              )}
            </div>
            <div className={classes.control}>
              <span className={classes.icon}>
                <FaClock />
              </span>
              {!editModeIsActive ? (
                <h3>{time}</h3>
              ) : (
                <input type="time" id="time" value={time} ref={timeInputRef} />
              )}
            </div>
          </div>

          <div className={classes.control}>
            {!editModeIsActive ? (
              <p>{description}</p>
            ) : (
              <textarea
                type="text"
                id="description"
                rows="5"
                value={description}
                ref={descriptionInputRef}
              />
            )}
          </div>

          {editModeIsActive && (
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
          )}
        </Card>
      )}
      <Modal
        modalIsOpen={isRemoving}
        onClose={() => setIsRemoving(false)}
        header="Chcete událost smazat?"
      >
        <GreyBtn onClick={removeEventHandler}>Smazat</GreyBtn>
      </Modal>
    </>
  );
};

export default Event;
