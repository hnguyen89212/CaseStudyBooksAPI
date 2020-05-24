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
                @click="popUpDialog(item)"
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
    <!-- dialog, rendered if the selectedProduct is non-empty only. -->
    <v-dialog
      v-model="dialog"
      v-if="Object.keys(selectedProduct).length"
      justify="center"
      align="center"
    >
      <v-card>
        <v-row>
          <v-spacer></v-spacer>
          <!-- a X button to close the dialog -->
          <v-btn
            text
            @click="dialog = false"
            style="font-size:XX-large;margin:1vw;"
            >X</v-btn
          >
        </v-row>
        <v-row style="margin-left:3vw;margin-right:3vw;">
          <v-col class="font-weight-medium, title" style="width:50%;"
            >{{ selectedProduct.productName }} (published
            {{ selectedProduct.releasedYear }})</v-col
          >
          <v-col style="width:50%;">
            <v-img :src="require(`../assets/${selectedProduct.graphicName}`)" />
          </v-col>
        </v-row>
        <v-row class="title" justify="center" align="center">
          MSRP: {{ selectedProduct.msrp | currency }}
        </v-row>
        <v-row class="subtitle-2" justify="center" align="center">
          Items in stock: {{ selectedProduct.qtyOnHand }}
        </v-row>
        <v-row
          justify="center"
          align="center"
          style="margin-left:3vw;margin-right:3vw;"
        >
          <v-col>
            <strong>Description</strong>: {{ selectedProduct.description }}
          </v-col>
        </v-row>
        <v-row style="margin-left:3vw;">
          <v-col>Qty:</v-col>
          <v-col>
            <input
              type="number"
              maxlength="3"
              placeholder="enter qty"
              size="3"
              style="width: 15vw;border-bottom:solid;text-align:right"
              v-model="qty"
            />
          </v-col>
          <v-col cols="7"></v-col>
        </v-row>
        <v-row
          justify="center"
          align="center"
          style="margin-bottom:2vh;margin-left:3vw;"
        >
          <v-col>
            <v-btn medium outlined color="default" @click="addToCart"
              >Add To cart</v-btn
            >
          </v-col>
          <v-col>
            <v-btn medium outlined color="default" @click="viewCart"
              >View cart</v-btn
            >
          </v-col>
        </v-row>
        <v-row justify="center" align="center" style="padding-bottom:5vh;">{{
          this.dialogStatus
        }}</v-row>
      </v-card>
    </v-dialog>
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
      // a switch to toggle the dialog
      dialog: false,
      selectedProduct: {},
      dialogStatus: "",
      qty: 0,
      cart: [],
    };
  },
  mixins: [fetcher],
  mounted: async function() {
    // don't use arrow function here
    try {
      this.status = "fetching brands from server...";
      let response = await this.$_getdata("brand");
      this.brands = response;
      this.status = `loaded ${this.brands.length + 1} brands`;
    } catch (err) {
      console.log(err);
      this.status = `Error has occured: ${err.message}`;
    }
    if (sessionStorage.getItem("cart")) {
      this.cart = await JSON.parse(sessionStorage.getItem("cart"));
    }
  },
  methods: {
    changeBrand: async function(brandId) {
      if (brandId > 0) {
        try {
          this.status = "fetching brands from server...";
          this.products = await this.$_getdata(`product/${brandId}`);
          this.status = `Found ${this.products.length} products`;
        } catch (err) {
          console.log(err);
          this.status = `Error has occured: ${err.message}`;
        }
      }
    },
    popUpDialog: function(product) {
      this.dialog = !this.dialog;
      this.selectedProduct = product;
      this.dialogStatus = "";
    },
    addToCart: function() {
      const index = this.cart.findIndex(
        // is item already on the cart
        // (item) => item.id === this.selectedProduct.id
        (item) => item.productName === this.selectedProduct.productName
      );
      if (parseInt(this.qty) > 0) {
        index === -1
          ? this.cart.push({
              // item is not yet in cart
              productName: this.selectedProduct.productName,
              qty: this.qty,
              item: this.selectedProduct,
            })
          : (this.cart[index] = {
              // the item is already on cart, update the quantity
              productName: this.selectedProduct.productName,
              qty: this.qty,
              item: this.selectedProduct,
            });
        this.dialogStatus = `${this.qty} item(s) added`;
      } else if (this.qty === "0") {
        // if quantity = 0 and index is valid, remove the item at that index.
        index === -1 ? null : this.cart.splice(index, 1); // remove
        this.dialogStatus = `item(s) removed`;
      }
      sessionStorage.setItem("cart", JSON.stringify(this.cart));
      console.table(this.cart);
    },
    viewCart: function() {
      // whenever user presses View Cart, the "/cart" route is activated.
      this.$router.push({
        name: "cart",
      });
    },
  },
};
/**
brand: (...)
brandId: (...)
costPrice: (...)
description: (...)
graphicName: (...)
msrp: (...)
productName: (...)
qtyOnBackOrder: (...)
qtyOnHand: (...)
releasedYear: (...)
timer: (...)
 */
</script>
