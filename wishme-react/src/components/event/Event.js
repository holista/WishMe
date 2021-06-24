import { useSelector } from "react-redux";
import { useEffect, useState } from "react";
import { FaCalendarAlt, FaClock } from "react-icons/fa/index";
import moment from "moment";

import classes from "./Event.module.css";
import Card from "../ui/Card";
import useApi from "../../hooks/use-api";
import Spinner from "../ui/Spinner";

const Event = (props) => {
  const token = useSelector((state) => state.auth.token);

  const [title, setTitle] = useState();
  const [date, setDate] = useState();
  const [time, setTime] = useState();
  const [description, setDescription] = useState();

  const { isLoading, error, sendRequest } = useApi();

  useEffect(() => {
    sendRequest(
      {
        url: `events/${props.eventId}`,
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
  }, []);

  const editHandler = () => {};

  const calendarIcon = <FaCalendarAlt />;
  const clockIcon = <FaClock />;

  return (
    <>
      {isLoading && <Spinner />}
      <Card className={classes.event}>
        <div className={classes.edit}>
          <button onClick={editHandler}>Upravit událost</button>
        </div>
        <div className={classes.title}>
          <h1>{title}</h1>
        </div>
        <div className={classes.dateTime}>
          <div className={classes.control}>
            <span className={classes.icon}>{calendarIcon}</span>
            <h3>{date}</h3>
          </div>
          <div className={classes.control}>
            <span className={classes.icon}>{clockIcon}</span>
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
