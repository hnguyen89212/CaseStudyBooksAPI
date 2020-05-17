<template>
  <v-app>
    <v-content>
      <Navbar />
      <router-view></router-view>
      <v-footer absolute class="headline" color="warning">
        <v-col class="text-center" cols="12">
          &copy;{{ new Date().getFullYear() }} — INFO3067
        </v-col>
      </v-footer>
    </v-content>
  </v-app>
</template>

<script>
import Navbar from "./components/Navbar.vue";

export default {
  name: "App",
  components: {
    Navbar,
  },
  data: () => ({
    // empty
  }),
};
</script>
