<!--@/components/login/LoginForm.vue -->
<template>
    <v-container>
        <v-form @submit.prevent="submitForm">
            <v-text-field v-model="formData.FAccount" label="帳號" required
                          :error-messages="error?.FAccount && error.FAccount.length ? error.FAccount : ''"></v-text-field>
            <v-text-field v-model="formData.FPassword" :type="showPassword ? 'text' : 'password'" label="密碼*" required
                          :error-messages="error?.FPassword && error.FPassword.length ? error.FPassword : ''"
                          append-icon="mdi-eye" @click:append="togglePasswordVisibility">
            </v-text-field>
            <v-btn type="submit" :loading="loading">登錄</v-btn>
            <!--<v-alert v-if="error" type="error">{{ error }}</v-alert>--> <!--顯示詳細錯誤信息-->
            <v-alert v-if="error && !loading" type="error">資料未填寫完或錯誤</v-alert>
        </v-form>
    </v-container>
</template>
<script setup>
    // @ts-nocheck 禁用 typescript 檢查
    import { ref, onMounted } from 'vue';
    import { storeToRefs } from 'pinia';
    import { loginFormStore } from '@/stores/LoginFormStore';
    const loginStore = loginFormStore();
    const { formData, loading, error } = storeToRefs(loginStore);
    const { submitForm, setTimezone } = loginStore;

    const showPassword = ref(false);
    const togglePasswordVisibility = () => {
        showPassword.value = !showPassword.value;
    };

    function getClientTimezone() {
        return Intl.DateTimeFormat().resolvedOptions().timeZone;
    }
    onMounted(() => {
        loginStore.resetFormData();
        setTimezone(getClientTimezone());
    });
</script>
