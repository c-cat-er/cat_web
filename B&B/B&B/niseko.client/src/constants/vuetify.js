//4. @/constants/vuetify.js Vue UI 組件庫
import 'vuetify/styles'; //Vuetify 樣式
import { createVuetify } from 'vuetify';
import { aliases, mdi } from 'vuetify/lib/iconsets/mdi'; //引入 MDI 圖標集import * as components from 'vuetify/components'
import * as components from 'vuetify/components';
import * as directives from 'vuetify/directives';
import '@mdi/font/css/materialdesignicons.css'; //引入 Material Design Icons 字體
//import zhHans from 'vuetify/lib/locale/zh-Hans.mjs'; // 引入简体中文语言包
//import zhHant from 'vuetify/lib/locale/zh-Hant.mjs'; // 引入繁体中文语言包

const vuetify = createVuetify({
    components,
    directives,
    theme: {
        defaultTheme: 'light',
        themes: {
            light: {
                primary: '#1976D2', //修改默认的主颜色
            },
        },
    },
    icons: {
        defaultSet: 'mdi',
        aliases,
        sets: {
            mdi,
        },
    },
  //other Vuetify 配置
  //Vuetify 允許你自定義應用的主題，包括顏色、字體、圖標等。這有助於你創建符合品牌風格的統一設計。
  //設置全局的應用設置，如 RTL（從右到左）支持、斷點、自定義 CSS 等。
  //自定義這些組件的默認屬性
  //Vuetify 支持多種插件和擴展，如表單驗證、國際化支持等，可以輕鬆集成到你的應用中。
  //添加多語言支持
});
export default vuetify;
