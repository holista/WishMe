import classes from "./Event.module.css";
import Card from "../ui/Card";
import { useSelector } from "react-redux";
import { useState } from "react";
import moment from "moment";

const Event = (props) => {
  const token = useSelector((state) => state.auth.token);

  const [title, setTitle] = useState();
  const [date, setDate] = useState();
  const [time, setTime] = useState();
  const [description, setDescription] = useState();

  const getEvent = () => {
    fetch(`http://localhost:8085/api/v1/events/${props.eventId}`, {
      method: "GET",
      headers: {
        accept: "application/json",
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
    })
      .then((response) => response.json())
      .then((responseData) => {
        const dateData = moment(responseData.dateTimeUtc).toDate();
        const dayData = `${dateData.getDay()}.${dateData.getMonth()}.${dateData.getFullYear()}`;
        const timeData = `${dateData.getHours()}:${dateData.getMinutes()}`;

        setTitle(responseData.name);
        setDate(dayData);
        setTime(timeData);
        setDescription(responseData.description);
      });
  };

  getEvent();

  return (
    <Card className={classes.event}>
      <div className={classes.title}>
        <h1>{title}</h1>
      </div>
      <div className={classes.dateTime}>
        <div>
          <h1>{date}</h1>
        </div>
        <div>
          <h1>{time}</h1>
        </div>
      </div>

      <div>
        <h1>{description}</h1>
      </div>
    </Card>
  );
};

export default Event;
