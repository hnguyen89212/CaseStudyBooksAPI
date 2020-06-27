import React from "react";
import { Link } from "react-router-dom";
import { SERVER_BASE_URL } from "../config/Config";

class OrderHistory extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      orders: [],
      selectedOrderId: 0,
    };
    this.seeOrder = this.seeOrder.bind(this);
  }

  async componentDidMount() {
    const email = await sessionStorage.getItem("user");
    const token = await sessionStorage.getItem("token");
    const url = `${SERVER_BASE_URL}/order/${email}`;
    let headers = new Headers();
    headers.append("Content-Type", "application/json");
    headers.append("Authorization", "Bearer " + token);
    try {
      let response = await fetch(url, {
        method: "GET",
        headers: headers,
      });
      let payload = await response.json();
      this.setState({
        orders: payload,
      });
    } catch (error) {}
  }

  seeOrder(orderId) {
    this.props.history.push(`/order/${orderId}`);
  }

  render() {
    return (
      <div className="container my-3">
        <div>
          <Link className="btn btn-outline-info mx-3" to="/Home">
            Home
          </Link>
          <Link className="btn btn-outline-info mx-3" to="/cart">
            Your Cart
          </Link>
        </div>
        <table className="table table-striped table-hover my-3">
          <thead>
            <tr>
              <th>Order ID</th>
              <th>Placement Date</th>
              <th>Value</th>
            </tr>
          </thead>
          <tbody>
            {this.state.orders.map((each) => (
              <tr key={each.id} onClick={() => this.seeOrder(each.id)}>
                <td>{each.id}</td>
                <td>{each.orderDate}</td>
                <td>$ {each.orderAmount}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    );
  }
}

export default OrderHistory;
