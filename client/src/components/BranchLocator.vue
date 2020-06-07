<template>
  <v-container>
    <v-row align="center" justify="center">
      <v-col class="title text-center">Hai Nguyen Case Study</v-col>
    </v-row>
    <v-row align="center" justify="center">
      <v-col class="title text-center">Find Some Stores Near You</v-col>
    </v-row>
    <v-row justify="center">
      <v-col>
        <v-text-field
          color="primary"
          label="Enter address"
          v-model="address"
          required
        ></v-text-field>
      </v-col>
    </v-row>
    <v-row justify="center">
      <v-btn
        @click="getLatAndLng()"
        :class="{ 'purple darken-4 white--text': valid, disabled: !valid }"
        >Locate Branches</v-btn
      >
    </v-row>
    <!-- <v-row justify="center">
      <div
        id="map"
        ref="map"
        class="googlemap"
        style="margin-top:2vh;margin-bottom:5vh;height:250px;width:300px;min-height:300px;"
      ></div>
    </v-row> -->
    <v-row
      class="display-1"
      justify="center"
      align="center"
      style="margin-top:2vh;color:purple"
      >{{ status }}</v-row
    >

    <!-- dialog -->
    <v-dialog v-model="dialog" justify="center" align="center">
      <v-card style="height:450px;width:350px;">
        <v-row>
          <v-spacer></v-spacer>
          <v-btn
            text
            @click="dialog = false"
            style="font-size:XX-large;margin:1vw;"
            >X</v-btn
          >
        </v-row>
        <v-row justify="center">
          <div
            id="map"
            ref="map"
            class="googlemap"
            style="margin-top:2vh;margin-bottom:5vh;height:380px;width:320px;min-height:300px;"
          ></div>
        </v-row>
      </v-card>
    </v-dialog>
  </v-container>
</template>

<script>
import fetcher from "../mixins/fetcher";

export default {
  data() {
    return {
      status: "",
      address: "N5Y-5R6",
      valid: true,
      map: null,
      dialog: false,
      details: [],
    };
  },
  mixins: [fetcher],
  methods: {
    getLatAndLng: function() {
      // If dialog = false, the whole v-dialog is not created, hence there is NOT div#map element created.
      // Hence, the geo-related code may throw exception.
      this.dialog = true;
      try {
        // A service for converting between an address to LatLng
        let geocoder = new window.google.maps.Geocoder();
        geocoder.geocode({ address: this.address }, async (results, status) => {
          if (status === window.google.maps.GeocoderStatus.OK) {
            // only if google gives us the OK
            // Gets the lattitude and longitude
            this.lat = results[0].geometry.location.lat();
            this.lng = results[0].geometry.location.lng();
            // the destination
            let myLatLng = new window.google.maps.LatLng(this.lat, this.lng);

            let options = {
              zoom: 10,
              center: myLatLng,
              mapTypeId: window.google.maps.MapTypeId.ROADMAP,
            };
            // targets the div#map element, sets the center to the destination.
            this.map = new window.google.maps.Map(this.$refs["map"], options);
            let center = this.map.getCenter();

            // just an info window, decoupled from all variables.
            let infowindow = new window.google.maps.InfoWindow({ content: "" });

            // queries to the DB to get the array of Store object.
            this.details = await this.$_getdata(
              `branch/${this.lat}/${this.lng}`
            );

            console.log(this.details);

            // Iterates the list and make marker for each of them.
            this.details.map((detail, count) => {
              let img = `/img/marker${count + 1}.png`;
              let marker = new window.google.maps.Marker({
                position: new window.google.maps.LatLng(
                  detail.latitude,
                  detail.longitude
                ),
                animation: window.google.maps.Animation.DROP,
                icon: img,
                title: `Store#${detail.id} ${detail.street}, ${detail.city},${detail.region}`,
                html: `<div>Store#${detail.id}<br/>${detail.street}, ${
                  detail.city
                }<br/>${detail.distance.toFixed(2)} km</div>`,
                visible: true,
              });

              window.google.maps.event.addListener(marker, "click", () => {
                infowindow.setContent(marker.html); // added .html to the marker object.
                infowindow.close();
                infowindow.open(this.map, marker);
              });
              marker.setMap(this.map);
            }); // end map

            this.map.setCenter(center);
            window.google.maps.event.trigger(this.map, "resize");
          }
        });
      } catch (err) {
        console.log(err);
      }
    },
  },
};

/*
this.details = [
  {
    city: "London"
    distance: 1.3159183403884895
    id: 2468
    latitude: 43.00372
    longitude: -81.225
    region: "ON"
    street: "Fanshawe College"
  },
  {}
];
 */
</script>
