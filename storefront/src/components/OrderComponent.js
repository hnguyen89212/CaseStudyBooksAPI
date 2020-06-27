import React from "react";
import { Link } from "react-router-dom";
import { SERVER_BASE_URL } from "../config/Config";
import { formatPrice } from "../utils/Utils";

class OrderComponent extends React.Component {
  // this.props.match.params.orderId
  constructor(props) {
    super(props);
    this.state = {
      orderDetails: [],
      subtotal: 0,
    };
  }

  async componentDidMount() {
    let payload = null;
    let total = 0;
    const email = await sessionStorage.getItem("user");
    const token = await sessionStorage.getItem("token");
    const url = `${SERVER_BASE_URL}/order/${this.props.match.params.orderId}/${email}`;
    // console.log(url);
    let headers = new Headers();
    headers.append("Content-Type", "application/json");
    headers.append("Authorization", "Bearer " + token);
    try {
      let response = await fetch(url, {
        method: "GET",
        headers: headers,
      });
      payload = await response.json();
      payload.forEach((each) => {
        total += each.qtyS * each.msrp;
      });
      this.setState({
        orderDetails: payload,
        subtotal: total,
      });
    } catch (error) {
      payload = error;
    }
  }

  render() {
    let hst = this.state.subtotal * 0.13;
    return (
      <div className="container my-3">
        <h3>Order # {this.props.match.params.orderId}</h3>
        <table className="table table-striped table-hover">
          <thead>
            <tr>
              <td>Title</td>
              <td>MSRP</td>
              <td>Ordered</td>
              <td>Available</td>
              <td>Backordered</td>
              <td>Extended Price</td>
            </tr>
          </thead>
          <tbody>
            {this.state.orderDetails.map((each) => (
              <tr key={each.productName}>
                <td>{each.productName}</td>
                <td>$ {each.msrp}</td>
                <td>{each.qtyO}</td>
                <td>{each.qtyS}</td>
                <td>{each.qtyB}</td>
                <td>$ {formatPrice(each.msrp * each.qtyS)}</td>
              </tr>
            ))}
            <tr>
              <td>Subtotal:</td>
              <td>$ {this.state.subtotal}</td>
              <td></td>
              <td></td>
              <td></td>
              <td></td>
            </tr>
            <tr>
              <td>HST:</td>
              <td>$ {formatPrice(hst)}</td>
              <td></td>
              <td></td>
              <td></td>
              <td></td>
            </tr>
            <tr>
              <td>Total:</td>
              <td>$ {formatPrice(this.state.subtotal + hst)}</td>
              <td></td>
              <td></td>
              <td></td>
              <td></td>
            </tr>
          </tbody>
        </table>
        <Link className="btn btn-primary my-3" to="/orderlist">
          Order History
        </Link>
      </div>
    );
  }
}

export default OrderComponent;
