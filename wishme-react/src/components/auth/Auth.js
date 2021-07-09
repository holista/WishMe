import { useRef, useState } from "react";
import { useDispatch } from "react-redux";
import { useHistory } from "react-router-dom";
import { FaEyeSlash, FaEye } from "react-icons/fa/index";

import { authActions } from "../../store/auth-slice";
import classes from "./Auth.module.css";
import useApi from "../../hooks/use-api";
import Spinner from "../ui/Spinner";
import BlueBtn from "../ui/buttons/BlueBtn";

const Auth = (props) => {
  const history = useHistory();
  const dispatch = useDispatch();

  const [isRegistrating, setIsRegistrating] = useState(true);
  const [passwordIsVisible, setPasswordIsVisible] = useState(false);

  const usernameInputRef = useRef();
  const passwordInputRef = useRef();
  const passwordRepeatInputRef = useRef();

  const togglePasswordVisibility = () => {
    setPasswordIsVisible((prevState) => !prevState);
  };

  const toggleHandler = () => {
    setIsRegistrating((prevState) => !prevState);
  };

  const { isLoading, error, sendRequest } = useApi();

  const registerHandler = (event) => {
    event.preventDefault();
    const dataReg = {
      username: usernameInputRef.current.value,
      password: passwordInputRef.current.value,
    };

    sendRequest(
      {
        url: "users/register/organizer",
        method: "POST",
        body: JSON.stringify(dataReg),
      },
      (responseData) => {
        dispatch(
          authActions.register({
            organizerId: responseData.organizerId,
            token: responseData.token,
          })
        );
      }
    );
  };

  const loginHandler = (event) => {
    event.preventDefault();
    const dataReg = {
      username: usernameInputRef.current.value,
      password: passwordInputRef.current.value,
    };

    sendRequest(
      {
        url: "users/login/organizer",
        method: "POST",
        body: JSON.stringify(dataReg),
      },
      (responseData) => {
        dispatch(
          authActions.login({
            organizerId: responseData.organizerId,
            token: responseData.token,
          })
        );
        history.push("/moje-udalosti");
      }
    );
  };

  const eye = passwordIsVisible ? (
    <FaEyeSlash onClick={togglePasswordVisibility} className={classes.eye} />
  ) : (
    <FaEye onClick={togglePasswordVisibility} className={classes.eye} />
  );

  return (
    <>
      <div className={classes.formWrap}>
        <div className={classes.header}>
          <h1>WishMe</h1>
        </div>
        <form
          onSubmit={!isRegistrating ? registerHandler : loginHandler}
          className={classes.form}
        >
          <div className={classes.control}>
            <input
              placeholder="Uživatelské jméno"
              type="text"
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
            />
            <span>{eye}</span>
          </div>

          {!isRegistrating && (
            <div className={classes.control}>
              <input
                placeholder="Potvrdit heslo"
                type={passwordIsVisible ? "text" : "password"}
                ref={passwordRepeatInputRef}
                required
              />
              <span>{eye}</span>
            </div>
          )}

          <div className={classes.btn}>
            {isLoading && <Spinner />}
            {!isLoading && (
              <BlueBtn width="62%">
                {isRegistrating ? "Přihlásit" : "Registrovat"}
              </BlueBtn>
            )}
          </div>
        </form>
        <div className={classes.switching}>
          <h3>
            {!isRegistrating ? "Máte účet? " : "Nemáte účet? "}
            <span onClick={toggleHandler}>
              {!isRegistrating ? "Přihlaste se." : "Zaregistrujte se."}
            </span>
          </h3>
        </div>
      </div>
    </>
  );
};

export default Auth;
