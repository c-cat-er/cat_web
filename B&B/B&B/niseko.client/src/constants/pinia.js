//2. @/constants/pinia.js
//set pinia (just for temporary storage 僅暫存) and pinia-plugin-persistedstate (for 儲存狀態持久化)
import { createPinia } from 'pinia';
import piniaPluginPersistedstate from 'pinia-plugin-persistedstate';
const pinia = createPinia();
pinia.use(piniaPluginPersistedstate);
export default pinia;

//如果 piniaPluginPersistedstate 插件僅用於臨時存儲（如備份表單數據），可以將其應用範圍限制在特定的 Store 中，而不是全局使用。這樣可以避免不必要的性能開銷。
