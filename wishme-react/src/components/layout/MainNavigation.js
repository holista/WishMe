import { NavLink } from "react-router-dom";

import classes from "./MainNavigation.module.css";

const MainNavigation = (props) => {
  return (
    <header className={classes.header}>
      <div className={classes.logo}>WishMe</div>
      <nav className={classes.nav}>
        <ul>
          <li>
            <NavLink to="">Vytvorit udalost</NavLink>
          </li>
          <li>
            <NavLink to="/">Odhlasit</NavLink>
          </li>
        </ul>
      </nav>
    </header>
  );
};

/*
<li>
            <NavLink>Vytvorit udalost</NavLink>
          </li>
          <li>
            <NavLink to="/welcome">Odhlasit</NavLink>
          </li>
          */

export default MainNavigation;
