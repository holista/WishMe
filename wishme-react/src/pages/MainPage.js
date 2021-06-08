import { useState } from "react";
import { useHistory } from "react-router";
import MainCarousel from "../components/carousel/MainCarousel";
import NewEvent from "../components/event/newEvent/NewEvent";

const MainPage = (props) => {
  const [openEvent, setOpenEvent] = useState(false);
  const history = useHistory();

  const openEventHandler = () => {
    setOpenEvent(true);
    history.push("/mainpage/new-event");
  };

  return (
    <>
      {openEvent && <NewEvent />}
      <MainCarousel onOpenNewEvent={openEventHandler} />
    </>
  );
};

export default MainPage;
