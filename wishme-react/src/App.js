import { Switch, Route, Redirect } from "react-router-dom";
//import { useSelector } from "react-redux";

import Layout from "./components/layout/Layout";
import WelcomePage from "./pages/WelcomePage";
import MainPage from "./pages/MainPage";
import EventPage from "./pages/EventPage";
import ItemPage from "./pages/ItemPage";
import IntroductionPage from "./pages/IntroductionPage";

function App() {
  //const isAuthenticated = useSelector((state) => state.auth.isAuthenticated);

  return (
    <Layout>
      <Switch>
        <Route path="/" exact>
          <Redirect to="/vitejte" />
        </Route>

        <Route path="/vitejte">
          <WelcomePage />
        </Route>

        <Route path="/jak-to-funguje">
          <IntroductionPage />
        </Route>

        <Route path="/moje-udalosti">
          <MainPage />
        </Route>

        <Route path="/udalost/:evId/seznam/:listId/polozka/:id">
          <ItemPage />
        </Route>

        <Route path="/udalost/:evId">
          <EventPage />
        </Route>
      </Switch>
    </Layout>
  );
}

export default App;
