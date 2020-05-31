// A mixin to fetch data from REST server.
export default {
  data() {
    return {
      serverBaseURL: "https://localhost:44308/api/",
    };
  },
  methods: {
    $_getdata: async function(apiCall) {
      let payload = {};
      // Since app is secure, headers for authorization is a must.
      let headers = this.buildHeaders();
      try {
        let response = await fetch(`${this.serverBaseURL}${apiCall}`, {
          method: "GET",
          headers: headers,
        });
        payload = await response.json();
      } catch (err) {
        console.log(err);
        payload = err;
      }
      return payload;
    },
    $_postdata: async function(apiCall, data) {
      let payload = JSON.stringify(data);
      let headers = {};
      if (apiCall === "register" || apiCall == "login") {
        headers = { "Content-Type": "application/json; charset=utf-8" };
      } else {
        headers = this.buildHeaders();
      }
      try {
        let response = await fetch(`${this.serverBaseURL}${apiCall}`, {
          method: "POST",
          headers: headers,
          body: payload,
        });
        payload = await response.json();
      } catch (error) {
        payload = error;
      }
      return payload;
    },
    // We append the user's token to make further API requests.
    buildHeaders: function() {
      let headers = new Headers();
      const user = JSON.parse(sessionStorage.getItem("user"));
      headers.append("Content-Type", "application/json");
      headers.append("Authorization", "Bearer " + user.token);
      return headers;
    },
  },
};
