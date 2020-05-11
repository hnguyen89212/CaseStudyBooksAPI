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
          item-value="id"
          @input="changeBrand"
          v-model="selectedId"
          style="max-height: 50%;"
        ></v-select>
      </v-col>
    </v-row>
    <!-- display the products of the selected brand here -->
    <div v-if="products.length > 0">
      <v-row justify="center" class="text-center headline"
        >Items in Category</v-row
      >
      <v-row justify="center" style="margin-top:1vh;">
        <v-col class="text-left display-2">
          <v-list style="max-height: 60vh;" class="overflow-y-auto">
            <v-list-item-group>
              <v-list-item
                v-for="(item, i) in products"
                :key="i"
                style="border:solid;"
              >
                <v-col style="width:25%;">
                  <v-img
                    :src="require(`../assets/${item.graphicName}`)"
                    class="my-3"
                    style="height:10vh;width:10vh;"
                    aspect-ratio="1"
                  />
                </v-col>
                <v-col style="width:75%;">
                  <v-list-item-content>
                    <v-list-item-title class="title" v-text="item.description"
                      >></v-list-item-title
                    >
                  </v-list-item-content>
                </v-col>
              </v-list-item>
            </v-list-item-group>
          </v-list>
        </v-col>
      </v-row>
    </div>
    <v-footer absolute class="headline">
      <v-col class="text-center" cols="12">
        &copy;{{ new Date().getFullYear() }} — INFO3067
      </v-col>
    </v-footer>
  </v-container>
</template>

<script>
import fetcher from "../mixins/fetcher";

export default {
  name: "BrandList",
  data() {
    return {
      selectedId: 0,
      brands: [],
      products: [],
      status: {},
    };
  },
  mixins: [fetcher],
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
  methods: {
    changeBrand: async function(brandId) {
      if (brandId > 0) {
        try {
          this.status = "fetching brands from server...";
          this.products = await this.fetchData(`product/${brandId}`);
          this.status = `Found ${this.products.length} products`;
        } catch (err) {
          console.log(err);
          this.status = `Error has occured: ${err.message}`;
        }
      }
    },
  },
};
</script>
