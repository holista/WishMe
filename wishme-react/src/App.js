import { Switch, Route, Redirect } from "react-router-dom";
import { useSelector } from "react-redux";

import Layout from "./components/layout/Layout";
import WelcomePage from "./pages/WelcomePage";
import MainPage from "./pages/MainPage";
import EventPage from "./pages/EventPage";

function App() {
  const isAuthenticated = useSelector((state) => state.auth.isAuthenticated);

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
        <Route path="/event">
          <EventPage />
        </Route>
      </Switch>
    </Layout>
  );
}

export default App;
