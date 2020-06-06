export default {
  methods: {
    roundToTwoDecimals: function(amount, decimals = 2) {
      return Number(Math.round(amount + "e" + decimals) + "e-" + decimals);
    },
  },
};
