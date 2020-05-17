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
          style="height:12vh;width:12vh;"
          aspect-ratio="1"
        />
      </v-col>
    </v-row>
    <!-- cart status -->
    <v-row style="margin:2vw;">{{ status }}</v-row>
    <!-- container to house all 3 tables -->
    <div v-if="this.status !== 'Cart flushed.'">
      <!-- 1st table -->
      <v-simple-table>
        <thead>
          <tr>
            <!-- <th class="text-left">Book</th> -->
            <th class="text-left">Qty</th>
            <th class="text-left">Product</th>
            <!-- ext = price * qty -->
            <!-- <th class="text-left">Ext.</th> -->
          </tr>
        </thead>
      </v-simple-table>
      <!-- 2nd table: scrollbale simple list -->
      <v-simple-table
        style="max-height:12vh;margin-bottom:2vh;"
        class="overflow-y-auto"
      >
        <tbody>
          <tr v-for="item in cart" :key="item.id">
            <td>{{ item.qty }}</td>
            <td>{{ item.item.productName }}</td>
          </tr>
        </tbody>
      </v-simple-table>
      <!-- 3rd table -->
      <v-simple-table
        style="max-height:40vh;margin-bottom:2vh;border:solid;border-width:thin;"
        class="overflow-y-auto"
      >
        <thead>
          <tr>
            <th colspan="4" class="text-left title">Cart Summary</th>
          </tr>
          <tr>
            <th class="text-left">Book</th>
            <th class="text-center">Cost Price</th>
            <th class="text-center">Qty</th>
            <!-- ext = price * qty -->
            <th class="text-center">Extended Price</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(each, i) in cart" v-bind:key="i">
            <td>{{ each.item.productName }}</td>
            <td>{{ each.item.costPrice | currency }}</td>
            <td>x{{ each.qty }}</td>
            <td>{{ (each.item.costPrice * each.qty) | currency }}</td>
          </tr>
          <tr>
            <td></td>
            <td></td>
            <td>
              Subtotal:
            </td>
            <td>{{ subtotal | currency }}</td>
          </tr>
          <tr>
            <td></td>
            <td></td>
            <td>
              HST:
            </td>
            <td>{{ hst | currency }}</td>
          </tr>
          <tr>
            <td></td>
            <td></td>
            <td>
              Cart total:
            </td>
            <td>{{ cartTotal | currency }}</td>
          </tr>
          <tr>
            <td colspan="12">
              <v-btn primary rounded outlined color="info" @click="flushCart"
              >Flush cart
            </v-btn>
            </td>
          </tr>
        </tbody>
      </v-simple-table>
    </div>
  </v-container>
</template>

<script>
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
  methods: {
    flushCart: function() {
      sessionStorage.removeItem("cart");
      this.cart = [];
      this.status = "Cart flushed.";
    },
  },
};
/*
this.cart = [
  {
    item: {
      brand:null
      brandId:1
      costPrice:122.72
      description:"CSS: The Definitive Guide: ztgtftnqlradmuruhrfjxqzbtiszbcntdeeebnqvxkryflfbkyooujpvvrgtdftxkmspvjdntjnjffitmfzivkvuilamjgekvcyi"
      graphicName:"css_the_definitive_guide.jpg"
      msrp:122.98
      productName:"CSS: The Definitive Guide"
      qtyOnBackOrder:3
      qtyOnHand:89
      releasedYear:2017
      timer:"AAAAAAAACAk="
    },
    productName: "",
    qty: "1"
  },
]
*/
</script>
