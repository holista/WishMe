import { useDispatch, useSelector } from "react-redux";
import { NavLink } from "react-router-dom";
import { authActions } from "../../store/auth-slice";

import classes from "./MainNavigation.module.css";

const MainNavigation = (props) => {
  const isAuthenticated = useSelector((state) => state.auth.isAuthenticated);
  const dispatch = useDispatch();

  const logoutHandler = () => {
    dispatch(authActions.logout());
  };

  return (
    <header className={classes.header}>
      <div className={classes.logo}>WishMe</div>
      <nav className={classes.nav}>
        <ul>
          {isAuthenticated && (
            <li>
              <NavLink to="/" onClick={logoutHandler}>
                Odhlásit
              </NavLink>
            </li>
          )}
        </ul>
      </nav>
    </header>
  );
};

export default MainNavigation;
