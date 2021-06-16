import { useState } from "react";
import { FaChevronRight, FaChevronLeft } from "react-icons/fa/index";

import EventItem from "../event/EventItem";
import NewEventItem from "../event/NewEventItem";
import classes from "./MainCarousel.module.css";

const MainCarousel = (props) => {
  const DUMMY_DATA = [
    { title: "1" },
    { title: "2" },
    { title: "3" },
    { title: "4" },
  ];

  const [curSlide, setCurSlide] = useState(0);
  const slidesLength = DUMMY_DATA.length;

  const nextSlideHandler = () => {
    setCurSlide(curSlide === slidesLength ? -1 : curSlide + 1);
  };

  const prevSlideHandler = () => {
    setCurSlide(curSlide === -1 ? slidesLength - 1 : curSlide - 1);
  };

  const getSlide = (data, index) => {
    if (data[index] !== undefined) {
      return <EventItem title={data[index].title} onClick={props.onEvent} />;
    } else {
      return <NewEventItem onClick={props.onNewEvent} />;
    }
  };

  const slides = [];

  slides.push(getSlide(DUMMY_DATA, curSlide - 1));
  slides.push(getSlide(DUMMY_DATA, curSlide));
  slides.push(getSlide(DUMMY_DATA, curSlide + 1));

  return (
    <div className={classes.slider}>
      <FaChevronLeft className={classes.leftArrow} onClick={prevSlideHandler} />
      <div className={classes.slides}>{slides}</div>
      <FaChevronRight
        className={classes.rightArrow}
        onClick={nextSlideHandler}
      />
    </div>
  );
};

export default MainCarousel;
