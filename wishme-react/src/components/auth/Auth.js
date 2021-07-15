import { useRef, useState } from "react";
import { useDispatch } from "react-redux";
import { useHistory } from "react-router-dom";
import { FaEyeSlash, FaEye } from "react-icons/fa/index";

import classes from "./Auth.module.css";
import { authActions } from "../../store/auth-slice";
import useApi from "../../hooks/use-api";
import Spinner from "../ui/Spinner";
import BlueBtn from "../ui/buttons/BlueBtn";
import Image from "../ui/Image";
import logo from "../../assets/logo.png";

const Auth = (props) => {
  const history = useHistory();
  const dispatch = useDispatch();

  const [isRegistrating, setIsRegistrating] = useState(true);
  const [passwordIsVisible, setPasswordIsVisible] = useState(false);
  const [inputError, setInputError] = useState(null);

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

    const username = usernameInputRef.current.value;
    const password = passwordInputRef.current.value;
    const passwordRepeat = passwordRepeatInputRef.current.value;

    if (password.length || passwordRepeat.length || username.length === 0) {
      setInputError("Vyplňte prosím prázdná pole!");
      return;
    } else if (password !== passwordRepeat) {
      setInputError("Zadaná hesla se neshodují!");
      return;
    } else if (username.length < 4) {
      setInputError("Uživatelské jméno musí obsahovat nejméně 4 znaky!");
      return;
    } else if (password.length < 5) {
      setInputError("Heslo musí obsahovat nejméně 5 znaků!");
      return;
    }

    setInputError(null);

    const dataReg = {
      username,
      password,
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
        setInputError(null);
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
      },
      (responseData) => {
        if (responseData.status === 401) {
          setInputError("Špatné uživatelské jméno a heslo!");
        }
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
        <div className={classes.logoWrap}>
          <Image src={logo} className={classes.logo} />
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
            />
          </div>

          <div className={classes.control}>
            <input
              placeholder="Heslo"
              type={passwordIsVisible ? "text" : "password"}
              ref={passwordInputRef}
            />
            <span>{eye}</span>
          </div>

          {!isRegistrating && (
            <div className={classes.control}>
              <input
                placeholder="Potvrdit heslo"
                type={passwordIsVisible ? "text" : "password"}
                ref={passwordRepeatInputRef}
              />
              <span>{eye}</span>
            </div>
          )}

          {inputError && <div className={classes.error}>{inputError}</div>}

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
