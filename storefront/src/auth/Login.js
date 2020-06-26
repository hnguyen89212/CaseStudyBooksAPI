import React from "react";
import auth from "./auth";
import { Redirect, Link } from "react-router-dom";

class Login extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
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
    this.setState({ email: e.target.value });
  }

  async handleSubmit(e) {
    e.preventDefault();

    let payload = {
      email: this.state.email,
      password: this.state.password,
    };

    await auth.login(
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
        <h1 className="large text-primary">Log In</h1>
        <p className="lead">
          <i className="fas fa-user"></i> Sign into Your Account
        </p>
        {this.state.loginFailed && (
          <p className="alert alert-danger">Invalid Credentials</p>
        )}
        <form onSubmit={this.handleSubmit} className="form">
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
            value="Log In"
          ></input>
        </form>
        <p className="my-3">
          Don't have an account? <Link to="/register">Sign Up</Link>
        </p>
      </div>
    );
  }
}

export default Login;
