import { useRef, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router-dom";
import { FaEyeSlash, FaEye } from "react-icons/fa/index";

import { authActions } from "../../store/auth-slice";
import classes from "./Auth.module.css";

const Auth = (props) => {
  const [passwordIsVisible, setPasswordIsVisible] = useState(false);

  const usernameInputRef = useRef();
  const passwordInputRef = useRef();
  const passwordRepeatInputRef = useRef();
  const dispatch = useDispatch();
  const { isRegistered /*isAuthenticated, isOrganizer, token, organizerId*/ } =
    useSelector((state) => state.auth);
  const history = useHistory();

  const togglePasswordVisibility = () => {
    setPasswordIsVisible((prevState) => !prevState);
  };
  const eye = passwordIsVisible ? (
    <FaEye onClick={togglePasswordVisibility} className={classes.eye} />
  ) : (
    <FaEyeSlash onClick={togglePasswordVisibility} className={classes.eye} />
  );

  const toggleHandler = () => {
    dispatch(authActions.toggle());
  };

  const registerHandler = (event) => {
    event.preventDefault();
    const dataReg = {
      username: usernameInputRef.current.value,
      password: passwordInputRef.current.value,
    };

    fetch("http://localhost:8085/api/v1/users/register/organizer", {
      method: "POST",
      body: JSON.stringify(dataReg),
      headers: {
        "Content-Type": "application/json",
        accept: "application/json",
      },
    })
      .then((response) => response.json())
      .then((responseData) => {
        dispatch(
          authActions.register({
            organizerId: responseData.organizerId,
            token: responseData.token,
          })
        );
      });
  };

  const loginHandler = (event) => {
    event.preventDefault();
    const dataReg = {
      username: usernameInputRef.current.value,
      password: passwordInputRef.current.value,
    };

    fetch("http://localhost:8085/api/v1/users/login/organizer", {
      method: "POST",
      body: JSON.stringify(dataReg),
      headers: {
        "Content-Type": "application/json",
        accept: "application/json",
      },
    })
      .then((response) => {
        if (!response.ok) throw new Error();
        return response.json();
      })
      .then((responseData) => {
        dispatch(
          authActions.login({
            organizerId: responseData.organizerId,
            token: responseData.token,
          })
        );
        history.push("/mainpage");
      })
      .catch((err) => console.log(err));
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
              <input
                placeholder="Uživatelské jméno"
                type={passwordIsVisible ? "text" : "password"}
                ref={usernameInputRef}
                required
              />
            </div>

            <div className={classes.control}>
              <input
                placeholder="Heslo"
                type={passwordIsVisible ? "text" : "password"}
                ref={passwordInputRef}
                required
                eye
              />
              <i>{eye}</i>
            </div>
          </div>

          {!isRegistered && (
            <div className={classes.control}>
              <input
                placeholder="Potvrdit heslo"
                ref={passwordRepeatInputRef}
                required
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
