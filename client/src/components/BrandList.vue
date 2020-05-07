<template>
  <v-container fluid>
    <v-row
      justify="center"
      class="text-center display-2"
      style="margin-top:.5em;"
      >Brands/Publishers</v-row
    >
    <v-row justify="center">
      <v-col class="title text-center" style="color:red">{{ status }}</v-col>
    </v-row>
    <v-row justify="center">
      <v-col class="text-left display-1">
        <v-select
          :items="brands"
          item-text="name"
          style="max-height: 50%;"
        ></v-select>
      </v-col>
    </v-row>
    <v-footer absolute class="headline">
      <v-col class="text-center" cols="12">
        &copy;{{ new Date().getFullYear() }} — INFO3067
      </v-col>
    </v-footer>
  </v-container>
</template>

<script>
export default {
  name: "BrandList",
  data() {
    return {
      brands: [],
      status: {},
    };
  },
  mounted: async function() {
    // don't use arrow function here
    try {
      this.status = "fetching brands from server...";
      let response = await fetch(`https://localhost:44308/api/brand`);
      this.brands = await response.json();
      this.status = `loaded ${this.brands.length + 1} brands`;
    } catch (err) {
      console.log(err);
      this.status = `Error has occured: ${err.message}`;
    }
  },
};
</script>