<!--@/components/home/Navbar.vue-->
<template>
    <v-app-bar app>
        <v-container class="justify-content-center align-items-center">
            <v-row class="align-items-center">
                <!-- Nav Items -->
                <v-col cols="4" class="d-flex align-items-center">
                    <v-row class="justify-content-around">
                        <v-col>
                            <router-link class="nav-link" to="/lodging">民宿</router-link>
                        </v-col>
                        <v-col>
                            <router-link class="nav-link" to="/photography">攝影</router-link>
                        </v-col>
                    </v-row>
                </v-col>

                <!-- Logo -->
                <v-col cols="4" class="d-flex align-items-center justify-content-center">
                    <router-link class="navbar-brand" to="/" aria-label-en="Niseko Logo" aria-label-jp="??">
                        Logo
                    </router-link>
                </v-col>

                <!-- Nav Search -->
                <v-col cols="2" class="d-flex nav-search align-items-center justify-content-center">
                    <v-icon>mdi-magnify</v-icon>
                    <v-btn text class="ps-1">[Ctrl+K]</v-btn>
                </v-col>

                <!-- Nav Member -->
                <v-col cols="2" class="d-flex align-items-center justify-content-center" aria-label-zh="會員" aria-label-en="navMember" aria-label-jp="??">
                    <router-link class="nav-link" to="/register">註冊</router-link>
                    <router-link class="nav-link" to="/login">登入</router-link>
                </v-col>
            </v-row>
        </v-container>
    </v-app-bar>
</template>

<style scoped>
/*    .navbar {
        position: sticky;
        top: 0;
        z-index: 10;
        width: 100%;
        background-color: transparent !important;
    }

    .navbar-nav {
        flex-direction: row;
    }*/
</style>
