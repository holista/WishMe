import { useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router-dom";
import { authActions } from "../../store/auth-slice";
import classes from "./Auth.module.css";

const Auth = (props) => {
  const dispatch = useDispatch();
  const isAuthenticated = useSelector((state) => state.auth.isAuthenticated);
  const [isRegistered, setIsRegistered] = useState(true);
  const history = useHistory();

  const authHandler = (event) => {
    event.preventDefault();
    dispatch(authActions.login());
    history.push("/mainpage");
  };

  const toggleHandler = () => {
    setIsRegistered((prevState) => !prevState);
  };

  const email = (
    <div className={classes.control}>
      <input placeholder="Email" />
    </div>
  );

  return (
    <>
      <div className={classes.formWrap}>
        <form onSubmit={authHandler} className={classes.form}>
          <ul className={classes.formActions}>
            <li>
              <a>{isRegistered ? "Přihlášení" : "Registrace"}</a>
            </li>
          </ul>

          <div>
            <div className={classes.control}>
              <input placeholder="Username" />
            </div>

            {!isRegistered && email}

            <div className={classes.control}>
              <input placeholder="Heslo" />
            </div>
          </div>

          <div className={classes.switching}>
            <h3>
              {!isRegistered ? "Už zde máte účet? " : "Ještě zde nemáte účet? "}
              <span onClick={toggleHandler}>
                {!isRegistered ? "Přihlaste se." : "Registrujte se."}
              </span>
            </h3>
          </div>

          <button>{isRegistered ? "Přihlásit" : "Registrovat"}</button>
        </form>
      </div>
    </>
  );
};

export default Auth;
