import Vue from "vue";
import Vuetify from "vuetify/lib";
import colors from "vuetify/lib/util/colors";

Vue.use(Vuetify);

export default new Vuetify({
  theme: {
    themes: {
      light: {
        primary: colors.deepPurple.darken4, // #E53935
        secondary: colors.red.lighten4, // #FFCDD2
        accent: colors.indigo.lighten5,
        error: "#FF5252",
        info: "#2196F3",
        success: "#4CAF50",
        warning: "#FFE082",
      },
    },
  },
});
