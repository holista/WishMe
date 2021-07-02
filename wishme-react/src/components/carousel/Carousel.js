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
          image={data[index].imageUrl}
          onClick={() => props.onData(data[index].id)}
          key={data[index].id}
        />
      );
    } else {
      return (
        <CarouselItem
          title={props.defaultTitle}
          onClick={props.onNewData}
          key={index}
        />
      );
    }
  };

  const centerMode = props.centerPosition && classes.center;

  return (
    <div className={`${classes.carousel} + ${centerMode}`}>
      <FaChevronLeft className={classes.arrow} onClick={prevSlideHandler} />
      <div className={classes.slides}>
        {getSlide(data, curSlide - 1)}
        {getSlide(data, curSlide)}
        {getSlide(data, curSlide + 1)}
      </div>
      <FaChevronRight className={classes.arrow} onClick={nextSlideHandler} />
    </div>
  );
};

export default Carousel;
