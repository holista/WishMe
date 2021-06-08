import { useState } from "react";
import MainCarousel from "../components/carousel/MainCarousel";
import NewEvent from "../components/event/newEvent/NewEvent";

const MainPage = (props) => {
  const [openEvent, setOpenEvent] = useState(false);

  const openEventHandler = () => {
    setOpenEvent(true);
  };

  return (
    <>
      {openEvent && <NewEvent />}
      <MainCarousel onOpenNewEvent={openEventHandler} />
    </>
  );
};

export default MainPage;
