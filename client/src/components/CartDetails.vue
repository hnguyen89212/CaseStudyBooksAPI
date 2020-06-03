<template>
  <v-container>
    <!-- cart title -->
    <!-- <v-row justify="center">
      <v-col class="display-1 text-center">Your current cart</v-col>
    </v-row> -->
    <!-- cart image -->
    <v-row>
      <v-col justify="center" align="center">
        <strong>Shopping Cart:</strong>
        <v-img
          :src="require('../assets/cart.png')"
          style="height:9vh;width:12vh;"
          aspect-ratio="1"
        />
      </v-col>
    </v-row>
    <!-- cart status -->
    <v-row style="margin:2vw;">{{ status }}</v-row>

    <!-- container to house all tables -->
    <div v-if="this.status !== 'Cart flushed.'">

      <v-simple-table>
        <thead >
          <tr>
            <th class="title" colspan="3">Cart Summary</th>
          </tr>
          <tr>
            <th class="text-center">Book</th>
            <th class="text-center">Cost Price</th>
          </tr>
        </thead>
      </v-simple-table>

      <v-simple-table
        style="max-height:45vh;margin-bottom:2vh;"
        class="overflow-y-auto"
      >
        <tbody v-for="(each, i) in cart" v-bind:key="i" class="text-center">
          <tr colspan="4">
            <td>{{ each.item.productName }}</td>
            <td >${{ each.item.costPrice.toFixed(2) }} x {{ each.qty }} = <span class="font-weight-bold">${{ subtotal.toFixed(2) }}</span></td>
          </tr>
        </tbody>
        <tbody class="text-center" >
          <tr>
            <td>
              Subtotal:
            </td>
            <td>${{ subtotal.toFixed(2) }}</td>
          </tr>
          <tr>
            <td>
              HST:
            </td>
            <td>${{ hst.toFixed(2) }}</td>
          </tr>
          <tr>
            <td>
              Cart total:
            </td>
            <td>${{ cartTotal.toFixed(2) }}</td>
          </tr>
          <tr colspan="12">
            <td>
              <v-btn medium rounded outlined color="default" @click="flushCart"
                >Flush cart
              </v-btn>
            </td>
            <td>
              <v-btn medium rounded outlined color="success" @click="checkout">
                Check out
              </v-btn>
            </td>
            <td></td>
          </tr>
        </tbody>
      </v-simple-table>
    </div>
  </v-container>
</template>

<script>
import fetcher from "../mixins/fetcher";

export default {
  name: "CartDetails",
  data() {
    return {
      subtotal: 0, // sum of extended prices
      hst: 0,
      cartTotal: 0,
      cart: [],
      status: "",
    };
  },
  beforeMount: function() {
    if (sessionStorage.getItem("cart")) {
      this.cart = JSON.parse(sessionStorage.getItem("cart"));
      this.cart.map((each) => {
        this.subtotal += each.item.costPrice * each.qty;
      });
      this.hst = this.subtotal * 0.13;
      this.cartTotal = this.subtotal + this.hst;
    } else {
      this.cart = [];
    }
  },
  mixins: [fetcher],
  methods: {
    flushCart: function() {
      sessionStorage.removeItem("cart");
      this.cart = [];
      this.status = "Cart flushed.";
    },
    checkout: async function() {
      let user = JSON.parse(sessionStorage.getItem("user"));
      let cart = JSON.parse(sessionStorage.getItem("cart"));
      try {
        this.status = "Checking out, saving your cart...";
        let orderHelper = {
          email: user.email,
          selections: cart,
        };
        // Makes a HttpPost request to the OrderController.
        let payload = await this.$_postdata("order", orderHelper);
        payload = JSON.stringify(payload);
        if (payload.indexOf("not") > 0 || payload.indexOf("problem") > 0) {
          this.status = payload;
        } else {
          this.flushCart();
          this.status = payload;
        }
      } catch (err) {
        this.status = `Error checking out: ${err}`;
      }
    },
  },
};
/*
cart (in back-end OrderHelper.Selections) = [
  {
    item: {
      brand: null
      brandId: 4
      costPrice: 136.37
      description: "Critical Thinking Skills For Dummies: sdoalxxhtiguhegntzeshtrkgneiyopozgduvwtyxvkjogghfheldowjswziesrbdxbcckqeskmhbkhwpjxhltkhchetwczwjkuc"
      graphicName: "ciritical_thinking.jpg"
      msrp: 137.6
      productName: "Critical Thinking Skills For Dummies"
      qtyOnBackOrder: 5
      qtyOnHand: 74
      releasedYear: 1994
      timer: "AAAAAAAAB/4="
    }
    productName: "Critical Thinking Skills For Dummies"
    qty: "1"
  },
  {
    item: {
      brand: null
      brandId: 4
      costPrice: 29.18
      description: "Financial Modeling in Excel For Dummies: bsbphaqjxptwqlxznipcihaljmqbxyuxjwgzhocfkacnrqgohbrlxgbolqqowjlsytecgtiuamneibzvdfejcwfpsijojhonicug"
      graphicName: "financial_modeling.jpg"
      msrp: 32.67
      productName: "Financial Modeling in Excel For Dummies"
      qtyOnBackOrder: 3
      qtyOnHand: 67
      releasedYear: 2019
      timer: "AAAAAAAACAI="
    },
    productName: "Financial Modeling in Excel For Dummies"
    qty: "2"
  }
];

user = {
  email: "h_nguyen89212@fanshaweonline.ca"
  firstName: ""
  lastName: ""
  password: ""
  token: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6Imhfbmd1eWVuODkyMTJAZmFuc2hhd2VvbmxpbmUuY2EiLCJuYmYiOjE1OTA4ODI4NDMsImV4cCI6MTU5MTQ4NzY0MywiaWF0IjoxNTkwODgyODQzfQ.jA0vkEFMAo6bkcCyUqRJOzatUGg7siwAaUjCTSWGuDM"
};
*/
</script>
