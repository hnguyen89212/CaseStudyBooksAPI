import React from "react";
import auth from "./auth";
import { Redirect, Link } from "react-router-dom";

class Register extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      firstName: "",
      lastName: "",
      email: "",
      password: "",
      loginFailed: false,
    };
    this.handleInputChange = this.handleInputChange.bind(this);
    this.handleControlledInputChange = this.handleControlledInputChange.bind(
      this
    );
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleInputChange(e) {
    const target = e.target;
    const value = target.value;
    const name = target.name;
    this.setState({
      [name]: value,
    });
  }

  handleControlledInputChange(e) {
    this.setState({ 
      [e.target.name]: e.target.value
    });
  }

  async handleSubmit(e) {
    e.preventDefault();

    let payload = {
      email: this.state.email,
      password: this.state.password,
      firstName: this.state.firstName,
      lastName: this.state.lastName,
    };

    await auth.register(
      payload,
      () => {
        this.props.history.push("/brandlist");
      },
      () => {
        this.setState({
          loginFailed: true,
        });
      }
    );
  }

  render() {
    if (auth.isAuthenticated()) {
      return <Redirect to="/brandlist" />;
    }
    return (
      <div className="container my-3">
        <h1 className="large text-primary">Register Today</h1>
        <p className="lead">
          <i className="fas fa-user"></i> Easily Sign Up
        </p>
        {this.state.loginFailed && (
          <p className="alert alert-danger">Please try again</p>
        )}
        <form onSubmit={this.handleSubmit} className="form">
          <div className="form-group">
            <label htmlFor="email">First name:</label>
            <input
              required
              className="form-control"
              type="text"
              name="firstName"
              value={this.state.firstName}
              onChange={this.handleControlledInputChange}
            ></input>
          </div>
          <div className="form-group">
            <label htmlFor="email">Last name:</label>
            <input
              required
              className="form-control"
              type="text"
              name="lastName"
              value={this.state.lastName}
              onChange={this.handleControlledInputChange}
            ></input>
          </div>
          <div className="form-group">
            <label htmlFor="email">Email:</label>
            <input
              required
              className="form-control"
              type="email"
              name="email"
              value={this.state.email}
              onChange={this.handleControlledInputChange}
            ></input>
          </div>
          <div className="form-group">
            <label htmlFor="password">Password:</label>
            <input
              required
              className="form-control"
              minLength="6"
              type="password"
              name="password"
              value={this.state.password}
              onChange={this.handleInputChange}
            ></input>
          </div>
          <input
            className="btn btn-primary"
            type="submit"
            value="Sign Up"
          ></input>
        </form>
        <p className="my-3">
          Already have an account? <Link to="/login">Log In</Link>
        </p>
      </div>
    );
  }
}

export default Register;
