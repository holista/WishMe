import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router";

import MainCarousel from "../components/carousel/MainCarousel";
import NewEvent from "../components/event/newEvent/NewEvent";
import { uiActions } from "../store/ui-slice";

const MainPage = (props) => {
  const modalIsOpen = useSelector((state) => state.ui.modalIsOpen);
  const dispatch = useDispatch();
  const history = useHistory();

  if (!modalIsOpen) {
    history.push("/mainpage");
  }

  const openEventHandler = () => {
    dispatch(uiActions.openModal());
    history.push("/mainpage/new-event");
  };

  return (
    <>
      <MainCarousel onClick={openEventHandler} />
      {modalIsOpen && <NewEvent />}
    </>
  );
};

export default MainPage;
