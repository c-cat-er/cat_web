import { createRouter, createWebHistory } from 'vue-router'
import shop from '../views/Shop.vue'
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: shop
    },
    {
      path: '/shop',
      name: 'shop',
      component: shop
    },
    {
      path: '/cart',
      name: 'cart',
      component: () => import('../views/cart.vue')
    },
    {
      path: '/Profile',
      name: 'Profile',
      component: () => import('../views/Profile.vue')
    },
  ]
})

export default router
