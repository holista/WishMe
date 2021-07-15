import { useDispatch, useSelector } from "react-redux";
import { NavLink, useHistory } from "react-router-dom";
import { authActions } from "../../store/auth-slice";

import classes from "./MainNavigation.module.css";
import Image from "../ui/Image";
import logo from "../../assets/logo.png";

const MainNavigation = (props) => {
  const isAuthenticated = useSelector((state) => state.auth.isAuthenticated);
  const dispatch = useDispatch();
  const history = useHistory();

  const logoutHandler = () => {
    dispatch(authActions.logout());
    history.push("/vitejte");
  };

  const logoHandler = () => {
    if (isAuthenticated) {
      history.push("/moje-udalosti");
    } else {
      history.push("/vitejte");
    }
  };

  return (
    <header className={classes.header}>
      <div className={classes.logoWrap} onClick={logoHandler}>
        <Image src={logo} className={classes.logo} />
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
