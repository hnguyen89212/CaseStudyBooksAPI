import Vue from "vue";
import Router from "vue-router";
import Home from "./components/Home.vue";
import BrandList from "./components/BrandList.vue";

Vue.use(Router);

export const router = new Router({
  mode: "history",
  base: process.env.BASE_URL,
  routes: [
    {
      path: "/",
      name: "home",
      component: Home,
    },
    {
      path: "/brands",
      name: "Brands/Publishers",
      component: BrandList,
    },
  ],
});
