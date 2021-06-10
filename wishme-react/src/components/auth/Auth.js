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

  return (
    <>
      <div className={classes.formWrap}>
        <form onSubmit={authHandler} className={classes.form}>
          <div className={classes.header}>
            <h1>WishMe</h1>
          </div>

          <div>
            <div className={classes.control}>
              <input placeholder="Username" />
            </div>

            {!isRegistered && (
              <div className={classes.control}>
                <input placeholder="Email" />
              </div>
            )}

            <div className={classes.control}>
              <input placeholder="Heslo" />
            </div>
          </div>

          <div className={classes.btn}>
            <button>{isRegistered ? "Přihlásit" : "Registrovat"}</button>
          </div>

          <div className={classes.switching}>
            <h3>
              {!isRegistered ? "Máte účet? " : "Nemáte účet? "}
              <span onClick={toggleHandler}>
                {!isRegistered ? "Přihlaste se." : "Zaregistrujte se."}
              </span>
            </h3>
          </div>
        </form>
      </div>
    </>
  );
};

export default Auth;
