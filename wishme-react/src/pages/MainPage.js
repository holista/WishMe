import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router";

import Carousel from "../components/carousel/Carousel";
import NewEvent from "../components/event/newEvent/NewEvent";
import { uiActions } from "../store/ui-slice";

const MainPage = (props) => {
  const DUMMY_DATA = [
    { title: "1" },
    { title: "2" },
    { title: "3" },
    { title: "4" },
  ];

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

  const openEventHandler = () => {
    history.push("/event");
  };

  return (
    <>
      <Carousel
        onNewData={openNewEventHandler}
        onData={openEventHandler}
        data={DUMMY_DATA}
        defaultTitle="Vytvořte novou událost"
        centerPosition={true}
      />
      {modalIsOpen && <NewEvent />}
    </>
  );
};

export default MainPage;
