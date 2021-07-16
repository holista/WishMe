import { useRef } from "react";
import LazyLoad from "react-lazyload";

import MainNavigation from "./MainNavigation";
import classes from "./Layout.module.css";
import backgroundVideo from "../../assets/backgroundVideo.mp4";
import background from "../../assets/background.jpg";

const Layout = (props) => {
  const videoRef = useRef();
  const setPlayBack = () => {
    videoRef.current.playbackRate = 0.4;
  };

  return (
    <>
      <MainNavigation />

      <main className={classes.main}>
        <LazyLoad style={{ width: "100%" }}>
          <video
            className={classes.video}
            ref={videoRef}
            onCanPlay={() => setPlayBack()}
            muted
            autoPlay
            poster={background}
          >
            <source src={backgroundVideo} typeof="video/mp4" />
          </video>
        </LazyLoad>

        <div className={classes.space}></div>
        {props.children}
      </main>
    </>
  );
};

export default Layout;
