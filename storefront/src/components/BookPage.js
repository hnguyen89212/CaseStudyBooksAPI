import React from "react";
import { Redirect, Link } from "react-router-dom";
import auth from "../auth/auth";

class BookPage extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      quantity: "0",
      isBookAdded: false,
      isQtyInvalid: false,
    };
    this.addToCart = this.addToCart.bind(this);
    this.handleControlledInputChange = this.handleControlledInputChange.bind(
      this
    );
  }

  async addToCart(e) {
    e.preventDefault();
    const qty = Number.parseInt(this.state.quantity);
    if (qty > 0) {
      let item = {
        productName: this.props.location.book.productName,
        qty: qty,
        item: this.props.location.book,
      };
      let cart = await sessionStorage.getItem("cart");
      cart = !cart ? [] : JSON.parse(cart);
      cart.push(item);
      this.setState({
        isBookAdded: true,
        isQtyInvalid: false,
      });
      await sessionStorage.setItem("cart", JSON.stringify(cart));
    } else {
      this.setState({
        isQtyInvalid: true,
      });
    }
  }

  handleControlledInputChange(e) {
    this.setState({ quantity: e.target.value });
  }

  render() {
    if (!auth.isAuthenticated() || !this.props.location.book) {
      return <Redirect to={"/brandlist"} />;
    } else {
      return (
        this.props.location.book && (
          <div className="container my-5">
            <form className="form">
              <div>
                <img
                  className="thumbnail shadow p-3 mb-5 bg-white rounded form-group my-auto"
                  alt="placeholder"
                  src={require(`../assets/${this.props.location.book.graphicName}`)}
                />
                <div className="text-justify my-3">
                  Class aptent taciti sociosqu ad litora torquent per conubia
                  nostra, per inceptos himenaeos. Nunc ornare aliquam quam, ac
                  lobortis neque consectetur eget. Nunc mollis risus nec feugiat
                  dapibus. Pellentesque habitant morbi tristique senectus et
                  netus et malesuada fames ac turpis egestas. Mauris in quam nec
                  sapien aliquet viverra eu quis tellus. Cras magna ante,
                  hendrerit non nibh sed, lacinia gravida ligula. Curabitur
                  gravida scelerisque est eu eleifend. Mauris porta auctor
                  sapien, ac laoreet magna mollis vel. Vestibulum feugiat congue
                  nibh. Fusce at dolor sed massa fringilla feugiat a at mi.
                  Vivamus hendrerit, tellus eget tincidunt dapibus, arcu erat
                  auctor quam, vel fringilla justo diam non odio. Nam id
                  vehicula leo, ac vestibulum ipsum. Etiam libero lacus, laoreet
                  a sem eget, porta aliquet ipsum.
                </div>
              </div>

              {/* <div>{this.props.location.book.description}</div> */}
              <div className="form-group my-3">
                <label htmlFor="quantity">Quantity:</label>
                <input
                  className="form-control"
                  type="text"
                  name="quantity"
                  id="quantity"
                  value={this.state.quantity}
                  onChange={this.handleControlledInputChange}
                  required
                />
              </div>
              {this.state.isQtyInvalid && (
                <p className="alert alert-warning">
                  Quantity must be at least 1.
                </p>
              )}
              {!this.state.isBookAdded ? (
                <button
                  className="btn btn-primary"
                  onClick={(e) => this.addToCart(e)}
                >
                  Add to Cart
                </button>
              ) : (
                <Link to="/cart" className="btn btn-info my-3">
                  Thank you. Go to Cart.
                </Link>
              )}
            </form>
            <div>
              <Link to="/brandlist" className="btn btn-warning my-3">
                Back Home
              </Link>
            </div>
          </div>
        )
      );
    }
  }
}

export default BookPage;

/*
var item = {
  productName: "",
  qty: 1,
  item: selectedProduct ({  await this.$_getdata(`product/${brandId}`) @index })
}

var cart = [item, etc.]

var orderHelper = {
  email: "",
  selections: cart
}

auth.login(() => {
  this.props.history.push("/app");
});
*/
