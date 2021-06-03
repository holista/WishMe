import { useState } from "react";
import { NavLink, useHistory } from "react-router-dom";
import classes from "./Auth.module.css";

const Auth = (props) => {
  const [isRegistered, setIsRegistered] = useState(true);
  const [isLogin, setIsLogin] = useState(false);
  const history = useHistory();

  const authHandler = (event) => {
    event.preventDefault();
    setIsLogin(true);
    history.push("/mainpage");
  };

  const loginHandler = () => {
    setIsRegistered(true);
  };

  const registrationHandler = () => {
    setIsRegistered(false);
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
            <li onClick={loginHandler}>
              <a>Přihlásit se</a>
            </li>
            <li onClick={registrationHandler}>
              <a>Registrovat se</a>
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

          <div className={classes.submitBtn}>
            <button>Přihlásit</button>
          </div>
        </form>
      </div>
    </>
  );
};

export default Auth;
