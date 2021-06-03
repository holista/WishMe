import { useState } from "react";
import { FaChevronRight, FaChevronLeft } from "react-icons/fa/index";

import classes from "./MainCarousel.module.css";

const MainCarousel = (props) => {
  const ImageData = [
    {
      image:
        "https://images.unsplash.com/photo-1546190255-451a91afc548?ixlib=rb-1.2.1",
    },
    {
      image:
        "https://images.unsplash.com/photo-1591348122449-02525d70379b?ixlib=rb-1.2.1",
    },
    {
      image:
        "https://images.unsplash.com/photo-1548802673-380ab8ebc7b7?ixlib=rb-1.2.1",
    },
    {
      image:
        "https://images.unsplash.com/photo-1577023311546-cdc07a8454d9?ixlib=rb-1.2.1",
    },
  ];

  const slides = ImageData;

  const [curSlide, setCurSlide] = useState(0);
  const length = slides.length;

  const nextSlideHandler = () => {
    setCurSlide(curSlide === length - 1 ? 0 : curSlide + 1);
  };

  const prevSlideHandler = () => {
    setCurSlide(curSlide === 0 ? length - 1 : curSlide - 1);
  };

  const mappedSlides = slides.map((slide, index) => {
    return (
      <div key={index}>
        {index === curSlide && (
          <img className={classes.slide} src={slide.image} alt="" />
        )}
      </div>
    );
  });

  return (
    <div className={classes.slider}>
      <FaChevronLeft className={classes.leftArrow} onClick={prevSlideHandler} />
      {mappedSlides}
      <FaChevronRight
        className={classes.rightArrow}
        onClick={nextSlideHandler}
      />
    </div>
  );
};

export default MainCarousel;
