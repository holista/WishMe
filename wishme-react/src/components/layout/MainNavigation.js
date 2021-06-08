import { NavLink } from "react-router-dom";

import classes from "./MainNavigation.module.css";

const MainNavigation = (props) => {
  return (
    <header className={classes.header}>
      <div className={classes.logo}>WishMe</div>
      <nav className={classes.nav}>
        <ul>
          <li>
            <NavLink to="/mainpage/new-event">Vytvořit událost</NavLink>
          </li>
          <li>
            <NavLink to="/">Odhlásit</NavLink>
          </li>
        </ul>
      </nav>
    </header>
  );
};

export default MainNavigation;
