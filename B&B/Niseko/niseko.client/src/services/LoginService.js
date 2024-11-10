//@/services/LoginService.js
//不使用 pinia 做狀態管理，改儲存在 localstorage

import axiosInstance from '@/constants/axios';
import Cookies from 'js-cookie';
import { jwtDecode } from 'jwt-decode';

class LoginService {
    constructor() {
        this.formData = {
            FAccount: '',
            FPassword: '',
            FLoginTimezone: '',
        };
        this.loading = false;
        this.error = null;
    }
    //for this fn use
    setTimezone(timezone) {
        this.formData.FLoginTimezone = timezone;
    }
    updateField(field, value) {
        this.formData[field] = value;
    }
    setIsLoggedIn(status) {
        localStorage.setItem('isLoggedIn', JSON.stringify(status));
    }
    setMemberData(data) {
        localStorage.setItem('memberData', JSON.stringify(data));
        localStorage.setItem('memberName', data.FMemberName);
    }
    setLoginRecordId(id) {
        localStorage.setItem('loginRecordId', id);
    }

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
            const loginRecordId = response.data.loginRecordId;

            if (token) {
                Cookies.set('token', token, { expires: 1, path: '/' });
                this.setLoginRecordId(loginRecordId);
                const tokenCheck = Cookies.get('token');

                if (tokenCheck) {
                    await this.fetchMemberData();
                    window.location.href = '/';
                }
            }
        } catch (err) {
            if (err.response) {
                this.error = err.response.data.message || err.response.statusText;
            } else {
                this.error = err.message;
            }
        } finally {
            this.loading = false;
        }
    }

    async fetchMemberData() {
        try {
            const token = Cookies.get('token');
            if (token) {
                const decodedToken = jwtDecode(token);
                const memberId = decodedToken.FMemberID;
                const response = await axiosInstance.get(`/api/Members/${memberId}`, {
                    headers: { Authorization: `Bearer ${token}` }
                });

                this.setIsLoggedIn(true);
                this.setMemberData(response.data);
                console.log('Member ID:', memberId);
                console.log('Response data:', response.data);
            }
        } catch (err) {
            this.error = err.message;
        }
    }

    async logout() {
        try {
            const loginRecordId = localStorage.getItem('loginRecordId');
            if (loginRecordId) {
                await axiosInstance.post('/api/MemberAuthWebsite/website/logout', { FLoginRecordID: loginRecordId });
            } else {
                console.log('loginRecordId is null');
            }
        } catch (err) {
            console.error('Error during logout:', err);
        } finally {
            Cookies.remove('token');
            this.setIsLoggedIn(false);
            localStorage.removeItem('memberData');
            localStorage.removeItem('memberName');
            localStorage.removeItem('loginRecordId');
            window.location.href = '/login';
        }
    }
    
    //for App.vue get
    isLoggedIn() {
        return JSON.parse(localStorage.getItem('isLoggedIn')) || false;
    }
    getMemberName() {
        return localStorage.getItem('memberName') || '';
    }
}

export default new LoginService();
