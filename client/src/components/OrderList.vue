<template>
  <v-container fluid>
    <v-row
      justify="center"
      class="text-center headline indigo--text"
      style="margin-top:2vh;"
      >Saved Orders</v-row
    >
    <v-row justify="center">
      <v-col class="title text-center" style="color:red">{{ status }}</v-col>
    </v-row>
    <div v-if="orders.length > 0">
      <v-row justify="center" style="background-color:silver;margin:3vw;">
        <v-col class="text-center headline" cols="4">Id</v-col>
        <v-col cols="8" class="text-center headline">Date</v-col>
      </v-row>
      <v-row>
        <v-col>
          <v-list
            style="max-height: 50vh;margin-top:-3vh;"
            class="overflow-y-auto"
          >
            <v-list-item-group>
              <v-list-item
                v-for="(order, i) in orders"
                :key="i"
                style="border:solid;border-color:purple;margin-left:3vw;margin-right:3vw;"
                @click="popUpDialog(order)"
              >
                <v-col cols="4" class="text-center">
                  <v-list-item-content>
                    <v-list-item-title
                      class="subtitle-2"
                      v-text="order.id"
                    ></v-list-item-title>
                  </v-list-item-content>
                </v-col>
                <v-col cols="8" class="text-center">
                  <v-list-item-content>
                    <v-list-item-title
                      class="sub-title2"
                      v-text="formatDate(order.orderDate)"
                    ></v-list-item-title>
                  </v-list-item-content>
                </v-col>
              </v-list-item>
            </v-list-item-group>
          </v-list>
        </v-col>
      </v-row>

      <!-- popped up dialog -->
      <v-dialog
        v-model="dialog"
        v-if="selectedOrder"
        justify="center"
        align="center"
      >
        <v-card>
          <!-- exit button -->
          <v-row>
            <v-spacer></v-spacer>
            <v-btn
              text
              @click="dialog = false"
              style="font-size:XX-large;margin:2vw;"
              >X</v-btn
            >
          </v-row>
          <!-- title -->
          <v-row
            class="_headline"
            justify="center"
            align="center"
            style="margin-left:3vw;margin-right:3vw;color:purple"
          >
            Order# {{ selectedOrder.id }} @
            {{ formatDate(selectedOrder.orderDate) }}
          </v-row>
          <!-- Image -->
          <v-row>
            <v-col justify="center" align="center">
              <v-img
                :src="require('../assets/cart.png')"
                style="height:7vh;width:10vh;padding:0;"
                aspect-ratio="1"
              />
            </v-col>
          </v-row>
          <!-- heading -->
          <div
            v-for="(detail, i) in details"
            :key="i"
            style="margin-bottom:0;margin-top:-2vh;padding-right:3vw;"
          >
            <v-row
              bold
              style="margin-bottom:0;margin-top:-2vh;padding-right:3vw;"
            >
              <v-col cols="12" class="text-center">{{
                detail.productName
              }}</v-col>
            </v-row>
            <v-row style="margin-bottom:0;margin-top:-2vh;padding-left:3vw;">
              <v-col cols="3">MSRP</v-col>
              <v-col cols="2">S</v-col>
              <v-col cols="2">O</v-col>
              <v-col cols="2">B</v-col>
              <v-col cols="3">Ext.</v-col>
            </v-row>
            <v-row style="margin-bottom:0;margin-top:-2vh;padding-left:3vw;">
              <v-col cols="3">{{ detail.msrp }}</v-col>
              <v-col cols="2">{{ detail.qtyS }}</v-col>
              <v-col cols="2">{{ detail.qtyO }}</v-col>
              <v-col cols="2">{{ detail.qtyB }}</v-col>
              <v-col cols="3">{{
                roundToTwoDecimals(detail.msrp * detail.qtyS)
              }}</v-col>
            </v-row>
            <hr />
          </div>
          <div style="margin-bottom:0;margin-top:-2vh;padding-left:3vw;">
            <!-- summary -->
            <v-row>
              <v-col cols="4">Subtotal:</v-col>
              <v-col cols="8">{{ this.subTotal }}</v-col>
            </v-row>
            <v-row>
              <v-col cols="4">Subtotal:</v-col>
              <v-col cols="8">{{ this.tax }}</v-col>
            </v-row>
            <v-row>
              <v-col cols="4">Subtotal:</v-col>
              <v-col cols="8">{{ this.orderTotal }}</v-col>
            </v-row>
          </div>
        </v-card>
      </v-dialog>
    </div>
  </v-container>
</template>

<script>
import fetcher from "../mixins/fetcher";
import dateformatter from "../mixins/dateformatter";
import currencyformatter from "../mixins/currencyformatter";

export default {
  name: "OrderList",
  data() {
    return {
      orders: [],
      status: {},
      details: [],
      selectedOrder: {},
      dialog: false,
      dialogStatus: {},
      subTotal: 0,
      tax: 0,
      orderTotal: 0,
    };
  },
  mixins: [fetcher, dateformatter, currencyformatter],
  beforeMount: async function() {
    try {
      let user = JSON.parse(sessionStorage.getItem("user"));
      this.status = "fetching orders from server...";
      let route = this.$_buildRouteWithParams("order", user.email.trimEnd());
      this.orders = await this.$_getdata(route.slice(0, -1)); // in mixin
      this.status = `loaded ${this.orders.length} orders`;
    } catch (err) {
      console.log(err);
      this.status = `Error has occured: ${err.message}`;
    }
  },
  methods: {
    selectOrder: async function(orderId) {
      if (orderId > 0) {
        // don't use arrow function here
        try {
          let user = JSON.parse(sessionStorage.getItem("user"));
          this.status = `fetching details for order ${orderId}...`;
          let route = this.$_buildRouteWithParams(
            "order",
            orderId,
            user.email.trimEnd()
          );
          this.details = await this.$_getdata(route.slice(0, -1)); // remove end /
          this.status = `found order ${orderId} details`;
          await this.summarizeOrder();
        } catch (err) {
          console.log(err);
          this.status = `Error has occured: ${err.message}`;
        }
      }
    },
    popUpDialog: async function(order) {
      this.dialogStatus = "";
      this.dialog = !this.dialog;
      this.selectedOrder = order;
      await this.selectOrder(order.id); //orderId
    },
    summarizeOrder: async function() {
      this.subTotal = 0;
      this.details.forEach((detail) => {
        this.subTotal += detail.msrp * detail.qtyS;
      });
      this.subTotal = this.roundToTwoDecimals(this.subTotal);

      this.tax = this.roundToTwoDecimals(0.13 * this.subTotal);

      this.orderTotal = this.roundToTwoDecimals(this.subTotal + this.tax);
    },
  },
};
</script>
