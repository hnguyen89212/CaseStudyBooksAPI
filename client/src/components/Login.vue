<template>
  <v-container>
    <v-row flex align-center>
      <v-col xs12 sm4 elevation-6>
        <v-card style="height:75vh;">
          <!-- title -->
          <v-card-title class="justify-center display-1 purple--text"
            >Login</v-card-title
          >
          <v-card-text class="pt-4">
            <div>
              <v-form v-model="valid" ref="form">
                <!-- email address -->
                <v-text-field
                  color="primary"
                  label="Email"
                  v-model="email"
                  :rules="[rules.required]"
                  required
                >
                </v-text-field>
                <!-- password -->
                <v-text-field
                  color="primary"
                  label="Password"
                  v-model="password"
                  min="4"
                  required
                  :append-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'"
                  :rules="[rules.required, rules.minLength]"
                  :type="show1 ? 'text' : 'password'"
                  @click:append="show1 = !show1"
                >
                </v-text-field>
                <!-- login button -->
                <v-row justify="center">
                  <v-btn
                    @click="login"
                    :class="{
                      'purple darken-4 white--text': valid,
                      disabled: !valid,
                    }"
                    :disabled="valid == false"
                  >
                    Login
                  </v-btn>
                </v-row>
              </v-form>
            </div>
            <!-- status -->
            <div class="text-center" style="margin-top:5vh;">
              {{ status }}
            </div>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import fetcher from "../mixins/fetcher";

export default {
  name: "Login",
  data() {
    return {
      email: "",
      password: "",
      show1: false,
      valid: false,
      status: "",
      rules: {
        required: function(value) {
          return !!value || "This field is required.";
        },
        minLength: function(value) {
          return (
            value.length >= 4 || "This field must be at least 4 characters."
          );
        },
      },
    };
  },
  methods: {
    login: async function() {
      await sessionStorage.removeItem("user");
      try {
        this.status = "Loggin in, please wait...";
        let userHelper = {
          firstname: "",
          lastname: "",
          email: this.email,
          password: this.password,
        };
        let payload = await this.$_postdata("login", userHelper);
        if (payload.token.indexOf("login failed") > 0) {
          this.status = payload.token;
        } else {
          await sessionStorage.setItem("user", JSON.stringify(payload));
          this.$route.params.nextUrl
            ? this.$router.push({ name: this.$route.params.nextUrl })
            : this.$router.push({ name: "home" });
        }
      } catch (error) {
        console.log("here " + error);
        this.status = `An error has occured: ${error}`;
      }
    },
  },
  mixins: [fetcher],
};
</script>
