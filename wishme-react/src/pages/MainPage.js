import MainCarousel from "../components/carousel/MainCarousel";
import EventItem from "../components/event/EventItem";

const MainPage = (props) => {
  return (
    <>
      <MainCarousel eItem={EventItem} />
    </>
  );
};

export default MainPage;
