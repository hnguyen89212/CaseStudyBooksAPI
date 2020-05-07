<template>
  <v-app>
    <v-content>
      <BrandList />
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
