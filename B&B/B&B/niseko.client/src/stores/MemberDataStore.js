//@/stores/MemberDataStore.js
import { defineStore } from 'pinia';
import Cookies from 'js-cookie';
import { jwtDecode } from 'jwt-decode';
export const memberDataStore = defineStore('member', {
    state: () => ({
        FMemberID: '',
        FMemberCode: '',
        FMemberName: '',
        FPhone: '',
        FEmail: '',
    }),
    actions: {
        initializeMemberData() {
            const token = Cookies.get('token');
            const decodedToken = jwtDecode(token);
            if (token) {
                const decodedToken = jwtDecode(token);
                this.FMemberID = decodedToken.FMemberID || '';
                this.FMemberCode = decodedToken.FMemberCode || '';
                this.FMemberName = decodedToken.FMemberName || '';
                this.FPhone = decodedToken.FPhone || '';
                this.FEmail = decodedToken.FEmail || '';
            }
        }
    }
});
