// A mixin to fetch data from REST server.
export default {
  data() {
    return {
      serverBaseURL: "https://localhost:44308/api/",
    };
  },
  methods: {
    fetchData: async function(apiCall) {
      let payload = {};
      try {
        let response = await fetch(`${this.serverBaseURL}${apiCall}`);
        payload = await response.json();
      } catch (err) {
        console.log(err);
        payload = err;
      }
      return payload;
    },
  },
};
