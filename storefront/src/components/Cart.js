import React, { Fragment } from "react";
import { Link } from "react-router-dom";
import { SERVER_BASE_URL } from "../config/Config";

class Cart extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      cart: [],
      isOrderPlaced: false,
      totalCount: 0,
      subtotal: 0,
    };
    this.placeOrder = this.placeOrder.bind(this);
    this.flushCart = this.flushCart.bind(this);
  }

  async componentDidMount() {
    let cart = await sessionStorage.getItem("cart");
    if (!cart) {
      cart = [];
    } else {
      cart = JSON.parse(cart);
      let count = 0;
      let total = 0;
      cart.map((each) => {
        count += each.qty;
        total += each.qty * each.item.costPrice;
      });
      this.setState({
        totalCount: count,
        subtotal: total,
      });
    }
    this.setState({ cart: cart });
  }

  async placeOrder(e) {
    e.preventDefault();
    let email = await sessionStorage.getItem("user");
    let token = await sessionStorage.getItem("token");
    let orderHelper = {
      email: email,
      selections: this.state.cart,
    };
    let payload = JSON.stringify(orderHelper);
    let headers = new Headers();
    headers.append("Content-Type", "application/json");
    headers.append("Authorization", "Bearer " + token);
    try {
      let response = await fetch(`${SERVER_BASE_URL}/order`, {
        method: "POST",
        headers: headers,
        body: payload,
      });
      payload = await response.json();
    } catch (error) {
      payload = error;
    }
    if (payload.includes("is successfully saved")) {
      this.setState({
        isOrderPlaced: true,
      });
      // Clears cart
      sessionStorage.removeItem("cart");
    }
  }

  flushCart() {
    sessionStorage.removeItem("cart");
    this.setState({
      cart: [],
    });
  }

  render() {
    if (this.state.isOrderPlaced) {
      return (
        <div className="container my-3">
          <h3>Order is successfully placed.</h3>
          <Link className="btn btn-primary my-3" to="/brandlist">
            Back Home
          </Link>
        </div>
      );
    }
    if (this.state.cart.length === 0) {
      return (
        <div className="container my-3">
          <h3>Your Cart Is Empty.</h3>
          <Link className="btn btn-primary my-3" to="/brandlist">
            Back Home
          </Link>
        </div>
      );
    }
    let hst = "" + this.state.subtotal * 0.13;
    const decimalPointIndex = hst.indexOf(".");
    hst = Number.parseFloat(hst.substr(0, decimalPointIndex + 2));
    return (
      <div className="container my-3">
        <table className="table table-hover table-striped">
          <thead>
            <tr>
              <th>Name</th>
              <th>Sales Price</th>
              <th>Quantity (items)</th>
            </tr>
          </thead>
          <tbody>
            {this.state.cart.map((each) => (
              <tr key={each.productName}>
                <td>{each.productName}</td>
                <td>$ {each.item.costPrice}</td>
                <td>x {each.qty}</td>
              </tr>
            ))}
            <tr>
              <td>Subtotal:</td>
              <td>$ {this.state.subtotal}</td>
              <td>{this.state.totalCount} items</td>
            </tr>
            <tr>
              <td>HST:</td>
              <td>$ {hst}</td>
              <td>13%</td>
            </tr>
            <tr>
              <td>Total:</td>
              <td>$ {this.state.subtotal + hst}</td>
              <td></td>
            </tr>
          </tbody>
        </table>
        <form onSubmit={this.placeOrder}>
          <button className="btn btn-primary">Place Order</button>
        </form>
        <button className="btn btn-danger my-3" onClick={this.flushCart}>
          Flush Cart
        </button>
      </div>
    );
  }
}

export default Cart;
