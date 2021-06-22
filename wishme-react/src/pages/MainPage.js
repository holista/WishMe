import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router";

import Carousel from "../components/carousel/Carousel";
import NewEvent from "../components/event/newEvent/NewEvent";
import useApi from "../hooks/use-api";
import { uiActions } from "../store/ui-slice";

const MainPage = (props) => {
  const { token, organizerId } = useSelector((state) => state.auth);
  const [events, setEvents] = useState([]);

  const { isLoading, error, sendRequest } = useApi();

  useEffect(() => {
    sendRequest(
      {
        url: `events?offset=0&limit=100&organizerId=${organizerId}`,
        headers: {
          Authorization: `Bearer ${token}`,
        },
      },
      (responseData) => {
        setEvents(responseData.models);
      }
    );
  }, [organizerId, token, sendRequest]);

  const modalIsOpen = useSelector((state) => state.ui.modalIsOpen);
  const dispatch = useDispatch();
  const history = useHistory();

  if (!modalIsOpen) {
    history.push("/mainpage");
  }

  const openNewEventHandler = () => {
    dispatch(uiActions.openModal());
    history.push("/mainpage/new-event");
  };

  const openEventHandler = (id) => {
    history.push(`/event/${id}`);
  };

  return (
    <>
      <Carousel
        onNewData={openNewEventHandler}
        onData={openEventHandler}
        data={events}
        defaultTitle="Vytvořte novou událost"
        centerPosition={true}
      />
      {modalIsOpen && <NewEvent />}
    </>
  );
};

export default MainPage;
