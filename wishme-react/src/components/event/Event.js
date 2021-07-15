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
  const loaded = props.isLoaded;

  const [event, setEvent] = useState(null);

  const [isRemoving, setIsRemoving] = useState(false);
  const [editModeIsActive, setEditModeIsActive] = useState(false);

  const { isLoading, error, sendRequest } = useApi();
  const { imgIsLoading, toBase64 } = useImage();

  const titleInputRef = useRef();
  const dateInputRef = useRef();
  const timeInputRef = useRef();
  const descriptionInputRef = useRef();
  const imageInputRef = useRef();

  useEffect(() => {
    if (!editModeIsActive || history.location === `udalost/${eventId}`) {
      sendRequest(
        {
          url: `events/${eventId}`,
          headers: { Authorization: `Bearer ${token}` },
        },
        (responseData) => {
          const dateData = moment(responseData.dateTimeUtc).toDate();
          const dayData = `${dateData.getDate()}.${
            dateData.getMonth() + 1
          }.${dateData.getFullYear()}`;
          const timeData = `${dateData.getHours()}:${dateData.getMinutes()}`;

          setEvent({
            title: responseData.name,
            date: dayData,
            time: timeData,
            dateTime: dateData,
            description: responseData.description,
            image: responseData.image,
            thumb: responseData.image,
          });

          loaded();
        }
      );
    }
  }, [sendRequest, token, eventId, editModeIsActive, loaded, history.location]);

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
    history.push(`/udalost/${eventId}/upravit`);
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

    eventData.image = imageInputRef.current.files[0]
      ? await toBase64(imageInputRef.current.files[0])
      : event.image;

    await sendRequest({
      url: `events/${eventId}`,
      method: "PUT",
      body: JSON.stringify(eventData),
      headers: { Authorization: `Bearer ${token}` },
    });
    setEditModeIsActive(false);
    history.push(`/udalost/${eventId}`);
  };

  const changeImageHandler = async (e) => {
    e.preventDefault();
    const thumbnail = await toBase64(e.target.files[0]);
    setEvent((prevState) => {
      return { ...prevState, thumb: thumbnail };
    });
  };

  return (
    <>
      {props.visible && (
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
              <h1>{event.title}</h1>
            ) : (
              <input
                type="text"
                id="title"
                defaultValue={event.title}
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
                      <h3>{event.date}</h3>
                    </>
                  ) : (
                    <input
                      type="date"
                      id="date"
                      defaultValue={event.dateTime
                        .toISOString()
                        .substring(0, 10)}
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
                      <h3>{event.time}</h3>
                    </>
                  ) : (
                    <input
                      type="time"
                      id="time"
                      defaultValue={event.time}
                      ref={timeInputRef}
                    />
                  )}
                </div>
              </div>

              <div className={classes.description}>
                {!editModeIsActive ? (
                  <p>{event.description}</p>
                ) : (
                  <textarea
                    type="text"
                    id="description"
                    rows="5"
                    defaultValue={event.description}
                    ref={descriptionInputRef}
                  />
                )}
              </div>
            </section>

            {!editModeIsActive ? (
              <div className={classes.imageWrap}>
                <Image src={`data:image/jpeg;base64,${event.image}`} />
              </div>
            ) : (
              <div className={classes.controlImg}>
                <div className={classes.thumbWrap}>
                  {imgIsLoading ? (
                    <Spinner />
                  ) : (
                    <Image
                      src={`data:image/jpeg;base64,${
                        event.thumb ?? event.image
                      }`}
                    />
                  )}
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
