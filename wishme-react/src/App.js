import { Switch, Route, Redirect } from "react-router-dom";

import Layout from "./components/layout/Layout";
import WelcomePage from "./pages/WelcomePage";
import MainPage from "./pages/MainPage";
import EventPage from "./pages/EventPage";

function App() {
  return (
    <Layout>
      <Switch>
        <Route path="/" exact>
          <Redirect to="/welcome" />
        </Route>
        <Route path="/welcome">
          <WelcomePage />
        </Route>
        <Route path="/mainpage">
          <MainPage />
        </Route>
        <Route path="/event">
          <EventPage />
        </Route>
      </Switch>
    </Layout>
  );
}

export default App;
