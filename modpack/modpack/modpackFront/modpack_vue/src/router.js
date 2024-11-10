import { createWebHistory, createRouter } from "vue-router";
import Cart from "./components/Cart.vue";
import Checkout from "./components/Checkout.vue";
import Profile_order from "./components/Profile_order.vue";
import Product_listing from "./components/Product_listing.vue";
import Custom_listing from "./components/Custom_listing.vue";
const routes = [
  { path: "/cart", component: Cart },
  { path: "/checkout", component: Checkout },
  { path: "/profile_order", component: Profile_order },
  { path: "/product_listing/", component: Product_listing },
  { path: "/product_listing/:memberId", component: Product_listing },
  { path: "/custom_listing", component: Custom_listing },
  { path: "/custom_listing/:memberId", component: Custom_listing },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach((to, from, next) => {
  // 获取 memberId 参数
  const memberId = to.params.memberId;

  // 检查 memberId 是否存在
  if (memberId) {
    // 将 memberId 存储到 cookie 或 session 中
    // 请根据您的需求选择使用 cookie 或 session
    // 这里使用 document.cookie 存储到 cookie 中的示例
    document.cookie = `memberId=${memberId}; path=/;`;
  }
  // 继续导航
  next();
});
export default router;
