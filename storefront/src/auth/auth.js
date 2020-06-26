import {
  SERVER_BASE_URL,
  CREDENTIAL_INVALID,
  REGISTER_SUCCESS,
} from "../config/Config";

class Auth {
  constructor() {
    this.authenticated = true;
    if (
      sessionStorage.getItem("token") ===
        "Credentials invalid - login failed" ||
      sessionStorage.getItem("token") == null
    ) {
      this.authenticated = false;
    }
  }

  async register(payload, success, failure) {
    try {
      let response = await fetch(`${SERVER_BASE_URL}/register`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(payload),
      });
      payload = await response.json();
    } catch (error) {
      payload = error;
    }
    if (payload.token == REGISTER_SUCCESS) {
      this.authenticated = true;
      await sessionStorage.setItem("token", payload.token);
      await sessionStorage.setItem("user", payload.email);
      await sessionStorage.setItem("cart", JSON.stringify([]));
      success();
    } else {
      failure();
    }
  }
  /*
  {
    "firstName": "Tony",
    "lastName": "Ioannou",
    "email": "tony@gmail.ca",
    "password": "",
    "token": "User registration success."
  }
  */

  async login(payload, success, failure) {
    try {
      let response = await fetch(`${SERVER_BASE_URL}/login`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(payload),
      });
      payload = await response.json();
    } catch (error) {
      payload = error;
    }
    if (payload.token !== CREDENTIAL_INVALID) {
      this.authenticated = true;
      await sessionStorage.setItem("token", payload.token);
      await sessionStorage.setItem("user", payload.email);
      await sessionStorage.setItem("cart", JSON.stringify([]));
      success();
    } else {
      failure();
    }
  }

  async logout(cb) {
    this.authenticated = false;
    await sessionStorage.removeItem("token");
    await sessionStorage.removeItem("user");
    cb();
  }

  isAuthenticated() {
    return this.authenticated;
  }
}

export default new Auth();
