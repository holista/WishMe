import { useState } from "react";
import { Switch, Route, Redirect } from "react-router-dom";

import "./App.css";
import Layout from "./components/layout/Layout";
import WelcomePage from "./pages/WelcomePage";
import MainPage from "./pages/MainPage";

function App() {
  return (
    <Layout>
      <Route path="/" exact>
        <Redirect to="/welcome" />
      </Route>
      <Route path="/welcome">
        <WelcomePage />
      </Route>
      <Route path="/mainpage">
        <MainPage />
      </Route>
    </Layout>
  );
}

export default App;
