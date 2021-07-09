import { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { useHistory } from "react-router";

import Carousel from "../components/carousel/Carousel";
import Modal from "../components/ui/Modal";
import NewEvent from "../components/event/newEvent/NewEvent";
import Spinner from "../components/ui/Spinner";
import useApi from "../hooks/use-api";

const MainPage = (props) => {
  const history = useHistory();

  const { token, organizerId } = useSelector((state) => state.auth);
  const [events, setEvents] = useState([]);
  const [newEventModalIsOpen, setNewEventModalIsOpen] = useState(false);

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
        console.log("Mainpage: seting events");
      }
    );
  }, [organizerId, token, sendRequest]);

  const openNewEventHandler = () => {
    setNewEventModalIsOpen(true);
    history.push("/moje-udalosti/nova-udalost");
  };

  const closeNewEventHandler = () => {
    setNewEventModalIsOpen(false);
    history.push("/moje-udalosti");
  };

  const openEventHandler = (id) => {
    history.push(`/udalost/${id}`);
  };

  return (
    <>
      {isLoading && <Spinner />}
      {!isLoading && (
        <Carousel
          onNewData={openNewEventHandler}
          onData={openEventHandler}
          data={events}
          defaultTitle="Vytvořte novou událost"
          centerPosition={true}
        />
      )}

      <Modal
        modalIsOpen={newEventModalIsOpen}
        onClose={closeNewEventHandler}
        header="Vytvořte novou událost"
      >
        <NewEvent />
      </Modal>
    </>
  );
};

export default MainPage;
