import React, { Fragment } from "react";
import BrandComponent from "./BrandComponent";
import BookList from "./BookList";
import auth from "../auth/auth";
import { Link } from "react-router-dom";
import { SERVER_BASE_URL } from "../config/Config";

class BrandList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      error: null,
      isLoaded: false,
      brands: [],
      books: [],
    };
    this.handleClick = this.handleClick.bind(this);
    this.logout = this.logout.bind(this);
  }

  componentDidMount() {
    const url = `${SERVER_BASE_URL}/brand`;
    const token = sessionStorage.getItem("token");
    let headers = new Headers();
    headers.append("Content-Type", "application/json");
    headers.append("Authorization", `Bearer ${token}`);
    fetch(url, {
      method: "GET",
      headers: headers,
    })
      .then((res) => res.json())
      .then(
        (result) => {
          this.setState({
            isLoaded: true,
            brands: result,
          });
        },
        (error) => {
          this.setState({
            isLoaded: true,
            error,
          });
        }
      );
  }

  handleClick(publisher) {
    let id = publisher.id;
    let url = `${SERVER_BASE_URL}/product/${id}`;
    fetch(url)
      .then((response) => response.json())
      .then(
        (result) => {
          this.setState({
            books: result,
            isLoaded: true,
          });
        },
        (error) => {
          this.setState({
            isLoaded: true,
            error,
          });
        }
      );
  }

  async logout() {
    await auth.logout(() => {
      this.props.history.push("/login");
    });
  }

  render() {
    return (
      <div className="container my-3">
        <div>
          <button
            className="btn btn-outline-warning mx-3"
            onClick={() => this.logout()}
          >
            Log out
          </button>
          <Link className="btn btn-outline-info" to="/cart">
            Your Cart
          </Link>
        </div>
        <h3 className="my-5">Publishers</h3>
        <table className="table table-hover">
          <thead>
            <tr>
              <th>ID</th>
              <th>Publisher</th>
            </tr>
          </thead>
          <tbody>
            {this.state.brands.map((eachBrand) => (
              <tr
                key={eachBrand.id}
                onClick={() => this.handleClick(eachBrand)}
                data-toggle="tooltip"
                data-placement="top"
                title="CLick for inventory"
              >
                <td># {eachBrand.id}</td>
                <td>
                  <BrandComponent id={eachBrand.id} name={eachBrand.name} />
                </td>
              </tr>
            ))}
          </tbody>
        </table>
        <h3 className="my-5">Publisher's Inventory</h3>
        <BookList books={this.state.books} />
      </div>
    );
  }
}

export default BrandList;
