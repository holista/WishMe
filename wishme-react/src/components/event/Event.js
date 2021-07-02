import { useSelector } from "react-redux";
import { useEffect, useState } from "react";
import {
  FaCalendarAlt,
  FaClock,
  FaPencilAlt,
  FaTrashAlt,
  FaArrowLeft,
} from "react-icons/fa/index";

import moment from "moment";

import classes from "./Event.module.css";
import Card from "../ui/Card";
import useApi from "../../hooks/use-api";
import Spinner from "../ui/Spinner";
import { useHistory } from "react-router";

const Event = (props) => {
  const history = useHistory();
  const token = useSelector((state) => state.auth.token);
  const eventId = props.eventId;

  const [title, setTitle] = useState();
  const [date, setDate] = useState();
  const [time, setTime] = useState();
  const [description, setDescription] = useState();

  const { isLoading, error, sendRequest } = useApi();

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
    sendRequest({
      url: `events/${eventId}`,
      method: "DELETE",
      headers: { Authorization: `Bearer ${token}` },
    });

    history.replace("/mainpage");
  };

  const editEventHandler = () => {};

  return (
    <>
      {isLoading && <Spinner />}
      <Card className={classes.event}>
        <div className={classes.edit}>
          <div>
            <button onClick={() => history.goBack()}>
              <FaArrowLeft />
            </button>
          </div>
          <div>
            <button onClick={removeEventHandler}>
              <FaTrashAlt />
            </button>
            <button onClick={editEventHandler}>
              <FaPencilAlt />
            </button>
          </div>
        </div>
        <div className={classes.title}>
          <h1>{title}</h1>
        </div>
        <div className={classes.dateTime}>
          <div className={classes.control}>
            <span className={classes.icon}>
              <FaCalendarAlt />
            </span>
            <h3>{date}</h3>
          </div>
          <div className={classes.control}>
            <span className={classes.icon}>
              <FaClock />
            </span>
            <h3>{time}</h3>
          </div>
        </div>

        <div>
          <p>{description}</p>
        </div>
      </Card>
    </>
  );
};

export default Event;
