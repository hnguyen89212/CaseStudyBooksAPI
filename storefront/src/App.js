import React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import BrandList from "./components/BrandList";
import BookPage from "./components/BookPage";
import PrivateRoute from "./routing/PrivateRoute";
import Login from "./auth/Login";
import Register from "./auth/Register";
import Cart from "./components/Cart";
import OrderHistory from "./components/OrderHistory";
import OrderComponent from "./components/OrderComponent";

function App() {
  return (
    <Router>
      <Switch>
        <Route exact path="/login" component={Login}></Route>
        <Route exact path="/register" component={Register}></Route>
        <PrivateRoute
          exact
          path="/bookpage"
          component={BookPage}
        ></PrivateRoute>
        <PrivateRoute
          exact
          path="/brandlist"
          component={BrandList}
        ></PrivateRoute>
        <PrivateRoute exact path="/order/:orderId" component={OrderComponent} />
        <PrivateRoute exact path="/orderlist" component={OrderHistory} />
        <Route path="/cart" component={Cart} />
        <Route path="*" component={Login} />
      </Switch>
    </Router>
  );
}

export default App;
