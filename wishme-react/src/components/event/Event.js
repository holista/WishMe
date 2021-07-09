import { useSelector } from "react-redux";
import { useEffect, useState, useRef } from "react";
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
import Image from "../ui/Image";
import useImage from "../../hooks/use-img";

const Event = (props) => {
  const history = useHistory();
  const token = useSelector((state) => state.auth.token);
  const eventId = props.eventId;

  const [title, setTitle] = useState();
  const [date, setDate] = useState();
  const [time, setTime] = useState();
  const [dateTime, setDateTime] = useState();
  const [description, setDescription] = useState();
  const [image, setImage] = useState();
  const [thumb, setThumb] = useState();
  const [isRemoving, setIsRemoving] = useState(false);
  const [editModeIsActive, setEditModeIsActive] = useState(false);

  const { isLoading, error, sendRequest } = useApi();
  const { toBase64 } = useImage();

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
        setDateTime(dateData);
        setDescription(responseData.description);
        setImage(responseData.image);
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
      history.replace("/moje-udalosti")
    );
  };

  const editEventHandler = () => {
    setEditModeIsActive(true);
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
        body: JSON.stringify(eventData),
        headers: { Authorization: `Bearer ${token}` },
      },
      setEditModeIsActive(false)
    );
  };

  const changeImageHandler = async (e) => {
    e.preventDefault();
    const thumbnail = await toBase64(e.target.files[0]);
    setThumb(thumbnail);
  };

  return (
    <>
      {isLoading && <Spinner />}
      {!isLoading && (
        <Card>
          <EditBar
            arrowBack
            goTo={() => history.replace("/moje-udalosti")}
            onRemove={() => setIsRemoving(true)}
            editing={!editModeIsActive}
            onEdit={editEventHandler}
            onSave={saveEventHandler}
          />

          <div className={classes.title}>
            {!editModeIsActive ? (
              <h1>{title}</h1>
            ) : (
              <input
                type="text"
                id="title"
                defaultValue={title}
                ref={titleInputRef}
              />
            )}
          </div>

          <div className={classes.details}>
            <section className={classes.section}>
              <div className={classes.dateTime}>
                <div className={classes.control}>
                  {!editModeIsActive ? (
                    <>
                      <span className={classes.icon}>
                        <FaCalendarAlt />
                      </span>
                      <h3>{date}</h3>
                    </>
                  ) : (
                    <input
                      type="date"
                      id="date"
                      defaultValue={dateTime.toISOString().substring(0, 10)}
                      ref={dateInputRef}
                    />
                  )}
                </div>
                <div className={classes.control}>
                  {!editModeIsActive ? (
                    <>
                      <span className={classes.icon}>
                        <FaClock />
                      </span>
                      <h3>{time}</h3>
                    </>
                  ) : (
                    <input
                      type="time"
                      id="time"
                      defaultValue={time}
                      ref={timeInputRef}
                    />
                  )}
                </div>
              </div>

              <div className={classes.description}>
                {!editModeIsActive ? (
                  <p>{description}</p>
                ) : (
                  <textarea
                    type="text"
                    id="description"
                    rows="5"
                    defaultValue={description}
                    ref={descriptionInputRef}
                  />
                )}
              </div>
            </section>

            {!editModeIsActive ? (
              <div className={classes.imageWrap}>
                <Image src={`data:image/jpeg;base64,${image}`} />
              </div>
            ) : (
              <div className={classes.controlImg}>
                <div className={classes.thumbWrap}>
                  <Image
                    src={
                      thumb
                        ? `data:image/jpeg;base64,${thumb}`
                        : `data:image/jpeg;base64,${image}`
                    }
                  />
                </div>

                <label htmlFor="image">Obrázek události</label>
                <input
                  type="file"
                  accept="image/*"
                  multiple={false}
                  id="image"
                  ref={imageInputRef}
                  onChange={changeImageHandler}
                />
              </div>
            )}
          </div>
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
