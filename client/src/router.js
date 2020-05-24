import Vue from "vue";
import Router from "vue-router";
import Home from "./components/Home.vue";
import BrandList from "./components/BrandList.vue";
import CartDetails from "./components/CartDetails.vue";
import Register from "./components/Register.vue";
import Login from "./components/Login.vue";
import Logout from "./components/Logout.vue";

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
    {
      path: "/cart",
      name: "cart",
      component: CartDetails,
    },
    {
      path: "/register",
      name: "register",
      component: Register,
    },
    {
      path: "/login",
      name: "login",
      component: Login,
    },
    {
      path: "/logout",
      name: "logout",
      component: Logout,
    },
  ],
});

// Secures the application. User ought to login/register first.
router.beforeEach((to, from, next) => {
  const publicPages = ["/login", "/register", "/logout"];
  const securePages = !publicPages.includes(to.path);
  if (securePages && !sessionStorage.getItem("user")) {
    next({
      name: "login",
      params: { nextUrl: to.name },
    });
  } else {
    next();
  }
});
