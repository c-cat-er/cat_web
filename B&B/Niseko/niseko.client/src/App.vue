﻿<!-- @/App.vue -->
<template>
    <v-app>
        <v-app-bar class="navbar">
            <v-container class="p-0">
                <v-row class="align-center">
                    <!-- Nav Items -->
                    <v-col cols="4" class="d-flex text-center justify-content-around">
                        <router-link class="nav-link" to="/lodging">民宿</router-link>
                        <router-link class="nav-link" to="/photography">攝影</router-link>
                    </v-col>

                    <!-- Logo -->
                    <v-col cols="4" class="align-center text-center justify-center">
                        <router-link class="navbar-brand" to="/" aria-label-en="Niseko Logo" aria-label-jp="??">
                            Logo
                        </router-link>
                    </v-col>

                    <!-- Nav Search -->
                    <v-col cols="2" class="text-center justify-center">
                        <v-icon>mdi-magnify</v-icon>
                        <!--<v-btn text class="ps-1">[Ctrl+K]</v-btn>-->
                    </v-col>

                    <!-- Nav Member -->
                    <v-col cols="2" class="d-flex text-center justify-content-around" aria-label-zh="會員" aria-label-en="navMember" aria-label-jp="??">
                        <template v-if="isLoggedIn">
                            <span>歡迎, {{ memberId }},{{ memberName }} </span>
                            <!--k-p, 會員中心下拉菜單-->
                            <!--offset-y 
                            open-on-hover 懸浮時展開-->
                            <v-menu offset-y class="my-menu" open-on-hover>
                                <!--激活按钮-->
                                <template v-slot:activator="{ props }">
                                    <!--v-bind="attrs" 屬性集合
                                    v-on="on" 事件集合-->
                                    <v-btn v-bind="props" color="primary" text>
                                        選單
                                        <v-icon right>mdi-menu-down</v-icon>
                                    </v-btn>
                                </template>
                                <v-list>
                                    <v-list-item @click="goToPage('memberCenter')">
                                        <v-list-item-title>會員中心</v-list-item-title>
                                    </v-list-item>
                                    <v-list-item @click="goToPage('shoppingCart')">
                                        <v-list-item-title>購物車</v-list-item-title>
                                    </v-list-item>
                                    <v-list-item @click="goToPage('test')">
                                        <v-list-item-title>付款</v-list-item-title>
                                    </v-list-item>
                                    <v-list-item @click="logout">
                                        <v-list-item-title>登出</v-list-item-title>
                                    </v-list-item>
                                </v-list>
                            </v-menu>
                        </template>
                        <template v-else>
                            <router-link class="nav-link" to="/register">註冊</router-link>
                            <router-link class="nav-link" to="/login">登入</router-link>
                        </template>
                    </v-col>
                </v-row>
            </v-container>
        </v-app-bar>

        <v-main>
            <router-view /> <!--k-p, 由 Vue Router 提供指定組件-->
        </v-main>

        <v-footer class="footer-section justify-center align-center text-center">
            <v-container class="p-0">
                <v-row class="justify-space-between">
                    <v-col cols="auto" class="footer-list">
                        <h5>關於我們</h5>
                        <ul>
                            <li>Niseko 民宿</li>
                            <li>電話 | 111111111 ??</li>
                            <li>電郵 | <a href="mailto:lesson@chaseforsnow.com">lesson@chaseforsnow.com ??</a></li>
                            <li>地址 | 日本縣 ??</li>
                            <li>服務時間週一至週五 09:00-18:00 ??</li>
                            <li><a href="#">常見問答 <span class="external-link">🔗</span></a></li>
                            <li><a href="#">聯絡我們 <span class="external-link">🔗</span></a></li>
                            <li><a href="#">特價訂閱 <span class="external-link">🔗</span></a></li>
                        </ul>
                    </v-col>
                    <v-col cols="auto" class="footer-list">
                        <h5>實用資訊</h5>
                        <ul>
                            <li><a href="#">日本天氣 <span class="external-link">🔗</span></a></li>
                            <li><a href="#">日本環境 <span class="external-link">🔗</span></a></li>
                            <li><a href="#">遊日攻略 <span class="external-link">🔗</span></a></li>
                        </ul>
                    </v-col>
                    <v-col cols="auto" class="footer-list">
                        <h5>合作夥伴</h5>
                        <ul>
                            <li><a href="#">夥伴中心 <span class="external-link">🔗</span></a></li>
                            <li><a href="#">支援中心 <span class="external-link">🔗</span></a></li>
                            <li><a href="#">爭議中心 <span class="external-link">🔗</span></a></li>
                        </ul>
                    </v-col>
                    <v-col cols="auto" class="footer-list">
                        <h5>隱私&政策</h5>
                        <ul>
                            <li><a href="#">著作權 <span class="external-link">🔗</span></a></li>
                            <li><a href="#">隱私權 <span class="external-link">🔗</span></a></li>
                            <li><a href="#">條款與細則 <span class="external-link">🔗</span></a></li>
                            <li><a href="#">Cookie 政策 <span class="external-link">🔗</span></a></li>
                            <li><a href="#">法律聲明 <span class="external-link">🔗</span></a></li>
                        </ul>
                    </v-col>
                    <v-col cols="auto" class="footer-list">
                        <h5>網站相關</h5>
                        <ul>
                            <li><a href="#">🌍語言切換(點跳視窗) <span class="external-link">🔗</span></a></li>
                            <li><a href="#">地圖導覽 <span class="external-link">🔗</span></a></li>
                            <li><a href="#">🎧使用幫助 <span class="external-link">🔗</span></a></li>
                            <li><a href="#">意見箱 <span class="external-link">🔗</span></a></li>
                            <li><a href="#">網站維護 <span class="external-link">🔗</span></a></li>
                            <li><a href="#">系統維護 <span class="external-link">🔗</span></a></li>
                        </ul>
                        <div aria-label-zh="下載App" aria-label-en="??" aria-label-jp="??">下載App</div>
                    </v-col>
                </v-row>
                <v-row class="text-center">
                    <v-col>
                        Copyright © 2024 by Niseko - <a href="#">條款及細則 Terms & Conditions</a>
                    </v-col>
                </v-row>
            </v-container>
        </v-footer>
    </v-app>
</template>
<script setup>
    import { ref, computed, onMounted } from 'vue';
    import { useRouter } from 'vue-router';
    //wm1. with LoginService.js
    //import LoginService from '@/services/LoginService';
    //const isLoggedIn = ref(LoginService.isLoggedIn());
    //const memberName = ref(LoginService.getMemberName());

    //wm2. with LoginFormStore.js
    import { storeToRefs } from 'pinia';
    import { registerFormStore } from '@/stores/RegisterFormStore';
    import { loginFormStore } from '@/stores/LoginFormStore';
    import './assets/base.css';  // 引入全局样式
    const router = useRouter();
    const registerStore = registerFormStore();
    const loginStore = loginFormStore();
    const menu = ref(false);

    //使用不同的變量名稱來避免重複聲明
    //const { isLoggedIn, memberName, loginRecordId } = storeToRefs(registerStore);
    //const { isLoggedIn, memberName, loginRecordId } = storeToRefs(loginStore);
    const { isRegisterStoreLoggedIn, registerMemberId, registerMemberName, registerLoginRecordId } = storeToRefs(registerStore);
    const { isLoginStoreLoggedIn, loginMemberId, loginMemberName, loginLoginRecordId } = storeToRefs(loginStore);
    //合併狀態，優先使用登錄狀態
    const isLoggedIn = computed(() => isLoginStoreLoggedIn.value || isRegisterStoreLoggedIn.value);
    const memberId = computed(() => loginMemberId.value || registerMemberId.value);
    const memberName = computed(() => loginMemberName.value || registerMemberName.value);
    const loginRecordId = computed(() => loginLoginRecordId.value || registerLoginRecordId.value);
    //console.log(isLoggedIn.value, memberName.value, loginRecordId.value);

    const goToPage = (page) => {
        switch (page) {
            case 'memberCenter':
                router.push('/memberCenter');
                break;
            case 'shoppingCart':
                router.push('/shoppingCart');
                break;
            case 'test':
                router.push('/test');
                break;
        }
    };

    //wm1. logout 靜態寫法
    //const logout = loginStore.logout;

    //wm2. logout 動態寫法
    const logout = async () => {
        if (isLoginStoreLoggedIn.value) {
            await loginStore.logout();
        } else if (isRegisterStoreLoggedIn.value) {
            await registerStore.logout();
        }
    };

    //onMounted(async () => { //用於刷新此 page 動作
    //    //wm1. with LoginService.js
    //    //if (LoginService.isLoggedIn()) {
    //    //    await LoginService.fetchMemberData();
    //    //    isLoggedIn.value = true;
    //    //    memberName.value = LoginService.getMemberName();
    //    //}

    //    //wm2. with LoginFormStore.js
    //    if (isLoggedIn.value) {
    //        await loginStore.fetchMemberData();
    //    }
    //});
</script>
<style scoped>
    /*k-p, 在 Vue 中，當你在 <template> 部分中使用組件時，組件名會自動轉換為小寫的 HTML 標籤，故 CSS 用小寫。*/
    .navbar {
        /* position: sticky;*/
        top: 0;
        z-index: 10;
        background-color: transparent !important;
        color: white !important;
    }
    main {
        height: auto;
        flex: 1;
        background-color: var(--primary-color); /*待改 漸層色!!??*/
        padding-bottom: 10px;
    }
    .my-menu {
    }
    .my-menu > .v-list {
        background-color: white; /* 颜色对比 */
    }
    footer {
        background-color: cornflowerblue; /**/
        font-size: 12px;
    }
    footer > v-container > v-row {

    }
    .footer-list > ul > li {
        list-style: none;
    }
</style>
