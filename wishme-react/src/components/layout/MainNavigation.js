import { useDispatch, useSelector } from "react-redux";
import { NavLink, useHistory } from "react-router-dom";
import { authActions } from "../../store/auth-slice";

import classes from "./MainNavigation.module.css";

const MainNavigation = (props) => {
  const isAuthenticated = useSelector((state) => state.auth.isAuthenticated);
  const dispatch = useDispatch();
  const history = useHistory();

  const logoutHandler = () => {
    dispatch(authActions.logout());
  };

  const logoHandler = () => {
    if (isAuthenticated) {
      history.push("/moje-udalosti");
    } else {
      history.push("/welcome");
    }
  };

  return (
    <header className={classes.header}>
      <div className={classes.logo} onClick={logoHandler}>
        WishMe
      </div>
      <nav className={classes.nav}>
        <ul>
          <li>
            <NavLink to="/jak-to-funguje">Jak to funguje</NavLink>
          </li>
          {isAuthenticated && (
            <li>
              <NavLink to="/moje-udalosti">Moje události</NavLink>
            </li>
          )}
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
