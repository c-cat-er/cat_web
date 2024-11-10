import { createApp } from "vue";
import { createPinia } from "pinia";
import mitt from "mitt";
import App from "./App.vue";
import router from "./router";

const eventbus = mitt();
const app = createApp(App);
const pinia = createPinia();
app.config.globalProperties.eventbus = eventbus;
app.use(pinia);
app.use(router);
app.mount("#app");
