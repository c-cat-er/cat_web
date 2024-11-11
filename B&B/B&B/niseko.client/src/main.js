//5. @main.js import router
import './assets/main.css'
import { createApp } from 'vue'
import App from './App.vue'
import routers from '@/constants/routers';
import pinia from '@/constants/pinia';
import vuetify from '@/constants/vuetify';
import { BButton, BAlert, createBootstrap } from 'bootstrap-vue-next';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue-next/dist/bootstrap-vue-next.css';

createApp(App)
    .use(routers)
    .use(pinia)
    .use(vuetify)
    .use(createBootstrap)
    .component('BButton', BButton)
    .component('BAlert', BAlert)
    .mount('#app');
