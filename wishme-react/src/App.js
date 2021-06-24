import { Switch, Route, Redirect, useHistory } from "react-router-dom";
import { useSelector } from "react-redux";

import Layout from "./components/layout/Layout";
import WelcomePage from "./pages/WelcomePage";
import MainPage from "./pages/MainPage";
import EventPage from "./pages/EventPage";

function App() {
  const isAuthenticated = useSelector((state) => state.auth.isAuthenticated);
  /*
  if (!isAuthenticated) {
    return <Redirect to="/welcome" />;
  }
*/
  return (
    <Layout>
      <Switch>
        <Route path="/" exact>
          <Redirect to="/welcome" />
        </Route>
        <Route path="/welcome">
          <WelcomePage />
        </Route>
        {isAuthenticated ? (
          <Route path="/mainpage">
            <MainPage />
          </Route>
        ) : (
          <Route path="/welcome">
            <WelcomePage />
          </Route>
        )}
        <Route path="/event/:Id">
          <EventPage />
        </Route>
      </Switch>
    </Layout>
  );
}

export default App;
