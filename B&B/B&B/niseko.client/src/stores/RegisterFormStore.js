// @/stores/RegisterFormStore.js
import { defineStore } from 'pinia'; //main.js 已設置 pinia 路徑，此處可直接引入
import axiosInstance from '@/constants/axios';
import Cookies from 'js-cookie'; //storage token in cookie
import { jwtDecode } from 'jwt-decode'; //use cookie's token get data

export const registerFormStore = defineStore('register', { //register 自定義辨識用
    state: () => ({
        formData: { //k-p, 皆須與 DTO 同名
            //FMemberCode: '', //generateMemberCode(),
            FMemberName: '',
            FSkiLevelID: 0, //須為數字類型
            FGender: '',
            FBirthdate: null,
            FCountryCode: null,
            FPhone: null,
            FEmail: null,
            FAccount: '',
            FPassword: '',
            //FLoginTypeID: 1,
            FLoginTimezone: '',
            //FLoginIPAddress: '',
            //FLoginDatetime: '', //new Date().toISOString()
        },
        loading: false,
        error: null,
        isRegisterStoreLoggedIn: false,
        //原fetch 改從 cookie get
        //registerMemberData: null, //存儲會員資料
        //registerMemberName: '',
        registerMemberId: null,
        registerMemberName: null,
        registerLoginRecordId: null
    }),
    actions: {
        setTimezone(timezone) {
            this.formData.FLoginTimezone = timezone;
        },
        updateField(field, value) {
            this.formData[field] = value;
        },
        setIsLoggedIn(status) {
            this.isRegisterStoreLoggedIn = status;
        },
        //setMemberData(data) {
        //    this.registerMemberData = data;
        //    this.registerMemberName = data.FMemberName;
        //},
        //setMemberId(id) {
        //    this.memberId = id;
        //},
        setLoginRecordId(id) {
            this.registerLoginRecordId = id;
        },
        resetFormData() {
            this.formData = {
                FMemberName: '',
                FSkiLevelID: 0,
                FGender: '',
                FBirthdate: null,
                FCountryCode: null,
                FPhone: null,
                FEmail: null,
                FAccount: '',
                FPassword: '',
                FLoginTimezone: '',
            };
            this.loading = false;
            this.error = null;
        },
        async submitForm() {
            this.loading = true;
            this.error = null;
            try {
                //console.log('Submitting form data:', JSON.stringify(this.formData));
                const response = await axiosInstance.post('/api/MemberAuthWebsite/website/register', this.formData, {
                    headers: { //k-p, 將時區儲存在標頭傳遞至 server
                        'Timezone': this.formData.FLoginTimezone,
                        //'Content-Type': 'application/json' //明確設置 Content-Type，可省
                    }
                });
                const token = response.data.token;
                const registerLoginRecordId = response.data.loginRecordId;
                if (token) {
                    //改在前端設置 token 到 Cookies 中，因為後端設置此處無法取得
                    //document.cookie = `token=${token}; max-age=${60 * 60 * 24}; path=/`; //有效期1天
                    Cookies.set('token', token, { expires: 1, path: '/' });
                    this.setLoginRecordId(registerLoginRecordId);
                    //const tokenCheck = Cookies.get('token');

                    //確認瀏覽器中的 Cookies
                    //document.cookie.split(';').forEach(cookie => {
                    //    console.log('Browser cookie:', cookie);
                    //});

                    //if (tokenCheck) { //用於登入後直接 get 會員資料
                        //this.setIsLoggedIn(true);
                    //}

                    //k-p, 解碼 token 取得會員資料
                    this.setIsLoggedIn(true); //設為登入狀態
                    const decodedToken = jwtDecode(token);
                    this.registerMemberId = decodedToken.FMemberID;
                    this.registerMemberName = decodedToken.FMemberName;
                    this.resetFormData(); //清除表单数据
                    window.location.href = '/'; //回首頁
                    //else {
                    //    this.error = '無法設置 Cookies';
                    //}
                    //} else {
                    //    this.error = 'Token 未找到';
                }
            } catch (err) {
                //this.error = err.response?.data?.message || err.message; //只顯示狀態碼不顯示文字信息
                if (err.response) { //顯示文字信息
                    this.error = err.response.data.errors; //顯示詳細錯誤信息，這不能移除否則無法顯示 RegisterForm.vue 的各欄位錯誤信息
                    //console.log('Detailed errors:', this.error);
                } else {
                    this.error = err.message;
                }
            } finally {
                this.loading = false;
            }
        },
        async logout() {
            try {
                if (this.loginLoginRecordId) {
                    const response = await axiosInstance.post('/api/MemberAuthWebsite/website/logout', { FLoginRecordID: this.loginLoginRecordId });
                } else {
                    console.log('FLoginRecordID is null');
                }
            } catch (err) {
                console.error('Error during logout:', err);
            } finally {
                Cookies.remove('token');
                this.setIsLoggedIn(false);
                this.setLoginRecordId(null);
                this.registerMemberId = null;
                this.registerMemberName = null;
                window.location.href = '/login';
            }
        }
    },
    persist: { //k-p,將 Pinia 狀態持久化到本地存儲
        enabled: true,
        strategies: [
            {
                key: 'register',
                storage: localStorage
            }
        ]
    }
});
//expires: 1：设置 cookie 的过期时间为 1 天。这个值可以是一个数字（代表天数），也可以是一个 Date 对象，表示具体的过期日期。
//secure: true：指定 cookie 仅通过 HTTPS 协议发送，增加安全性。只有在使用 HTTPS 连接时，cookie 才会被浏览器发送。
//sameSite: 'Strict'：设置 cookie 的 SameSite 属性为 Strict，防止跨站请求伪造（CSRF）攻击。Strict 模式下，cookie 只会在同一站点请求中发送，跨站请求不会携带此 cookie。
