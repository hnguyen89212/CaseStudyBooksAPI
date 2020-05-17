<template>
  <v-app>
    <v-content>
      <v-toolbar color="primary" class="white--text">
        <v-toolbar-title>
          <span>Info3067Exercises</span>
        </v-toolbar-title>
        <v-spacer></v-spacer>
      </v-toolbar>
      <BrandList />
      <v-footer absolute class="headline" color="warning">
        <v-col class="text-center" cols="12">
          &copy;{{ new Date().getFullYear() }} — INFO3067
        </v-col>
      </v-footer>
    </v-content>
  </v-app>
</template>

<script>
import BrandList from "./components/BrandList";

export default {
  name: "App",

  components: {
    BrandList,
  },

  data: () => ({
    //
  }),
};
</script>
