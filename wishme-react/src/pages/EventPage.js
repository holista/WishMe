import { useHistory } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";

import Carousel from "../components/carousel/Carousel";
import NewItem from "../components/item/newItem/NewItem";
import { uiActions } from "../store/ui-slice";

const EventPage = (props) => {
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
    history.push("/event");
  }

  const openNewItemHandler = () => {
    dispatch(uiActions.openModal());
    history.push("/event/new-item");
  };

  return (
    <>
      <div>
        <h1>Vyberte svému blízkému dárek dle jeho představ</h1>
      </div>
      <section>
        <div>
          <h1>Název seznamu</h1>
          <h2>Zadejte url předmětu nebo začněte vyhledávat v řádku.</h2>
        </div>
        <Carousel
          defaultTitle="Přidejte nový předmět"
          data={DUMMY_DATA}
          onNewData={openNewItemHandler}
        />
      </section>
      {modalIsOpen && <NewItem />}
    </>
  );
};

export default EventPage;
