import { useState } from "react";
import { FaChevronRight, FaChevronLeft } from "react-icons/fa/index";

import classes from "./Carousel.module.css";
import CarouselItem from "./CarouselItem";

const Carousel = (props) => {
  const [curSlide, setCurSlide] = useState(0);
  const data = props.data;
  const slidesLength = data.length;

  const nextSlideHandler = () => {
    setCurSlide(curSlide === slidesLength ? -1 : curSlide + 1);
  };

  const prevSlideHandler = () => {
    setCurSlide(curSlide === -1 ? slidesLength - 1 : curSlide - 1);
  };

  const getSlide = (data, index) => {
    if (data[index] !== undefined) {
      return (
        <CarouselItem
          title={data[index].name}
          onClick={() => props.onData(data[index].id)}
          key={data[index].id}
        />
      );
    } else {
      return (
        <CarouselItem title={props.defaultTitle} onClick={props.onNewData} />
      );
    }
  };

  const slides = [];

  slides.push(getSlide(data, curSlide - 1));
  slides.push(getSlide(data, curSlide));
  slides.push(getSlide(data, curSlide + 1));

  const arrowRight = (
    <FaChevronRight className={classes.arrowRight} onClick={nextSlideHandler} />
  );
  const arrowLeft = (
    <FaChevronLeft className={classes.arrowLeft} onClick={prevSlideHandler} />
  );

  const specialClasses = props.centerPosition && classes.center;

  return (
    <div className={`${classes.carousel} + ${specialClasses}`}>
      {arrowLeft}
      <div className={classes.slides}>{slides}</div>
      {arrowRight}
    </div>
  );
};

export default Carousel;
