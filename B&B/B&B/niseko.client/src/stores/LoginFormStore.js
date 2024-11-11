//@/stores/LoginFormStore.vue
import { defineStore } from 'pinia';
import axiosInstance from '@/constants/axios';
import Cookies from 'js-cookie';
import { jwtDecode } from 'jwt-decode';

export const loginFormStore = defineStore('login', {
    state: () => ({
        formData: {
            FAccount: '',
            FPassword: '',
            FLoginTimezone: '',
        },
        loading: false,
        error: null,
        isLoginStoreLoggedIn: false,
        loginMemberId: null,
        loginMemberName: '',
        loginLoginRecordId: null
    }),
    actions: {
        setTimezone(timezone) {
            this.formData.FLoginTimezone = timezone;
        },
        updateField(field, value) {
            this.formData[field] = value;
        },
        setIsLoggedIn(status) {
            this.isLoginStoreLoggedIn = status;
        },
        setLoginRecordId(id) {
            this.loginLoginRecordId = id;
        },
        resetFormData() {
            this.formData = {
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
                console.log('Submitting form data:', JSON.stringify(this.formData));
                const response = await axiosInstance.post('/api/MemberAuthWebsite/website/login', this.formData, {
                    headers: {
                        'Timezone': this.formData.FLoginTimezone,
                    }
                });
                const token = response.data.token;
                const loginLoginRecordId = response.data.loginRecordId;
                if (token) {
                    Cookies.set('token', token, { expires: 1, path: '/' });
                    this.setLoginRecordId(loginLoginRecordId);
                    //const tokenCheck = Cookies.get('token');
                    //if (tokenCheck) {
                    //}
                    this.setIsLoggedIn(true);
                    const decodedToken = jwtDecode(token);
                    this.loginMemberId = decodedToken.FMemberID;
                    this.loginMemberName = decodedToken.FMemberName;
                    this.resetFormData(); //清除表单数据
                    window.location.href = '/';

                    //else {
                    //    this.error = '無法設置 Cookies';
                    //}
                //} else {
                //    this.error = 'Token 未找到';
                }
            } catch (err) {
                if (err.response) {
                    this.error = err.response.data.errors;
                } else {
                    this.error = err.message;
                }
            } finally {
                this.loading = false;
            }
        },
        //async fetchMemberData() {
        //    try {
        //        const token = Cookies.get('token');
        //        if (token) {
        //            const decodedToken = jwtDecode(token);
        //            const memberId = decodedToken.FMemberID;
        //            const response = await axiosInstance.get(`/api/Members/${memberId}`, {
        //                headers: { Authorization: `Bearer ${token}` }
        //            });
                    
        //            this.setMemberData(response.data);
        //            console.log('Member ID:', memberId);
        //            console.log('Response data:', response.data);
        //        }
        //    } catch (err) {
        //        this.error = err.message;
        //    }
        //},
        async logout() {
            try {
                if (this.loginLoginRecordId) {
                    const response = await axiosInstance.post('/api/MemberAuthWebsite/website/logout', { FLoginRecordID : this.loginLoginRecordId });
                    //console.log('Logout response:', response.data);
                } else {
                    console.log('FLoginRecordID is null');
                }
            } catch (err) {
                console.error('Error during logout:', err);
            } finally {
                Cookies.remove('token');
                this.setIsLoggedIn(false);
                this.setLoginRecordId(null);
                this.loginMemberId = null;
                this.loginMemberName = null;
                window.location.href = '/login';
            }
        }
    },
    persist: {
        enabled: true,
        strategies: [
            {
                key: 'login',
                storage: localStorage
            }
        ]
    }
});
