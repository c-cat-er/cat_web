//1. @/constants/routers.js set router
import { createRouter, createWebHistory } from 'vue-router';
import RegisterPage from '@/pages/RegisterPage.vue';
import LoginPage from '@/pages/LoginPage.vue';
import HomePage from '@/pages/HomePage.vue';
import AboutPage from '@/pages/AboutPage.vue';
import LodgingPage from '@/pages/LodgingPage.vue';
import LodgingBookingPage from '@/pages/LodgingBookingPage.vue';
import StoragePage from '@/pages/StoragePage.vue';
import ShuttlePage from '@/pages/ShuttlePage.vue';
import CoursePage from '@/pages/CoursePage.vue';
import PhotographyPage from '@/pages/PhotographyPage.vue';
import ShoppingCartPage from '@/pages/ShoppingCartPage.vue';
import PaymentPage from '@/pages/PaymentPage.vue';
import NotFoundPage from '@/pages/NotFoundPage.vue';
import test from '@/components/test.vue';

const routes = [
    { path: '/', name: 'Home', component: HomePage },
    { path: '/register', name: 'Register', component: RegisterPage },
    { path: '/login', name: 'Login', component: LoginPage },
    { path: '/about', name: 'About', component: AboutPage },
    { path: '/lodging', name: 'Lodging', component: LodgingPage },
    { path: '/lodging/:roomID', name: 'LodgingBooking', component: LodgingBookingPage },
    { path: '/storage', name: 'Storage', component: StoragePage },
    { path: '/shuttle', name: 'Shuttle', component: ShuttlePage },
    { path: '/course', name: 'Course', component: CoursePage },
    { path: '/photography', name: 'Photography', component: PhotographyPage },
    { path: '/shoppingCart', name: 'ShoppingCart', component: ShoppingCartPage },
    //{ path: '/buy', name: 'Buy', component: BuyPage },
    { path: '/payment', name: 'Payment', component: PaymentPage },
    { path: '/:pathMatch(.*)*', name: 'NotFound', component: NotFoundPage },
    { path: '/test', name: 'test', component: test },
];

const router = createRouter({
    history: createWebHistory('/'), //import.meta.env.BASE_URL
    routes,
});
export default router;
