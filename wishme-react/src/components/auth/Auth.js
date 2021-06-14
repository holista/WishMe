import { useRef, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router-dom";
import { authActions } from "../../store/auth-slice";
import classes from "./Auth.module.css";

const Auth = (props) => {
  const usernameInputRef = useRef();
  const passwordInputRef = useRef();
  const passwordRepeatInputRef = useRef();
  const dispatch = useDispatch();
  const { isRegistered, isAuthenticated, isOrganizer, token, organizerId } =
    useSelector((state) => state.auth);
  const history = useHistory();

  const authHandler = (event) => {
    event.preventDefault();
    dispatch(authActions.login());
    history.push("/mainpage");
  };

  const loginHandler = (event) => {
    event.preventDefault();
    dispatch(authActions.login(fetchLogin()));
    history.push("/mainpage");
  };

  const registerHandler = (event) => {
    event.preventDefault();
    dispatch(authActions.register(fetchRegistration()));
  };

  const toggleHandler = () => {
    dispatch(authActions.toggle());
  };

  const fetchRegistration = () => {
    const dataReg = {
      username: usernameInputRef.current.value,
      password: passwordInputRef.current.value,
    };

    fetch("http://localhost:8085/api/v1/users/register/organizer", {
      method: "POST",
      body: JSON.stringify(dataReg),
    })
      .then((response) => response.json())
      .then((responseData) => {
        return {
          organizerId: responseData.organizerId,
          token: responseData.token,
        };
      });
  };

  const fetchLogin = () => {
    const dataReg = {
      username: usernameInputRef.current.value,
      password: passwordInputRef.current.value,
    };

    fetch("http://localhost:8085/api/v1/users/login/organizer", {
      method: "POST",
      body: JSON.stringify(dataReg),
    })
      .then((response) => response.json())
      .then((responseData) => {
        return {
          organizerId: responseData.organizerId,
          token: responseData.token,
        };
      });
  };

  return (
    <>
      <div className={classes.formWrap}>
        <form
          onSubmit={!isRegistered ? registerHandler : loginHandler}
          className={classes.form}
        >
          <div className={classes.header}>
            <h1>WishMe</h1>
          </div>

          <div>
            <div className={classes.control}>
              <input placeholder="Uživatelské jméno" ref={usernameInputRef} />
            </div>

            <div className={classes.control}>
              <input placeholder="Heslo" ref={passwordInputRef} />
            </div>
          </div>

          {!isRegistered && (
            <div className={classes.control}>
              <input
                placeholder="Potvrdit heslo"
                ref={passwordRepeatInputRef}
              />
            </div>
          )}

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
