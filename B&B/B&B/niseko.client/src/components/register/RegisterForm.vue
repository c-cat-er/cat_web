<!-- @/components/register/RegisterForm.vue -->
<template>
    <v-container>
        <v-form @submit.prevent="submitForm">
            <v-text-field v-model="formData.FMemberName" label="名稱*" required
                          :error-messages="error?.FMemberName && error.FMemberName.length ? error.FMemberName : ''"></v-text-field>
            <!--k-p,
            item-text 屬性用於顯示給用戶看的文本。
            item-value 屬性用於存儲在 formData 中的實際值。
    -->
            <!--<v-select v-model="formData.FSkiLevelID" :items="['1(初)', '2(中)', '3(高)']" label="滑雪級別*"></v-select>
    <v-select v-model="formData.FGender" :items="['男(M)', '女(F)', '其他(O)']" label="性別*"></v-select>-->
            <v-select v-model="formData.FSkiLevelID" :items="skiLevelItems" item-title="text" item-value="value" label="滑雪級別*"
                      :error-messages="error?.FSkiLevelID && error.FSkiLevelID.length ? error.FSkiLevelID : ''"></v-select>
            <v-select v-model="formData.FGender" :items="genderItems" item-title="text" item-value="value" label="性別*"
                      :error-messages="error?.FGender && error.FGender.length ? error.FGender : ''"></v-select>
            <v-text-field v-model="formData.FBirthdate" label="生日"></v-text-field>
            <v-text-field v-model="formData.FCountryCode" label="國屬編碼"></v-text-field>
            <v-text-field v-model="formData.FPhone" label="手機"></v-text-field>
            <v-text-field v-model="formData.FEmail" label="信箱"></v-text-field>
            <v-text-field v-model="formData.FAccount" label="帳號*" required :error-messages="error?.FAccount && error.FAccount.length ? error.FAccount : ''"></v-text-field>
            <v-text-field v-model="formData.FPassword"
                          :type="showPassword ? 'text' : 'password'" label="密碼*" required
                          :error-messages="error?.FPassword && error.FPassword.length ? error.FPassword : ''"
                          append-icon="mdi-eye" @click:append="togglePasswordVisibility">
            </v-text-field>
            <v-btn type="submit" :loading="loading">註冊</v-btn>
            <!--<v-alert v-if="error" type="error">{{ error }}</v-alert>--> <!--顯示詳細錯誤信息-->
            <v-alert v-if="error && !loading" type="error">資料未填寫完或錯誤</v-alert>
        </v-form>
    </v-container>
</template>
<script setup>
    // @ts-nocheck 禁用 typescript 檢查
    import { ref, onMounted } from 'vue';
    //wm1. use localstorage，尚未轉為註冊版
    //import LoginService from '@/services/LoginService';
    //const formData = reactive(LoginService.formData);
    //const loading = ref(LoginService.loading);
    //const error = ref(LoginService.error);

    //wm2. use pinia and pinia-plugin-persistedstate
    import { storeToRefs } from 'pinia';
    import { registerFormStore } from '@/stores/RegisterFormStore'; //k-p, use store.js storage state
    const registerStore = registerFormStore();
    const { formData, loading, error } = storeToRefs(registerStore); //use 使用解構賦值來提取需要的狀態，use storeToRefs let state 保持響應式
    const { submitForm, setTimezone } = registerStore; //方法不需要轉換為 ref, only state need

    //k-p, template item
    //const skiLevelItems = [
    //    { text: '初(1)', value: 1 },
    //    { text: '中(2)', value: 2 },
    //    { text: '高(3)', value: 3 }
    //];
    const skiLevelItems = ['初(1)', '中(2)', '高(3)'].map((level, index) => ({
        text: level,
        value: index + 1
    }));
    const genderItems = [
        { text: '男', value: 'M' },
        { text: '女', value: 'F' },
        { text: '其他', value: 'O' }
    ];

    //k-p, use type 切換密碼顯示
    const showPassword = ref(false);
    const togglePasswordVisibility = () => {
        showPassword.value = !showPassword.value;
    };

    //k-p, client 端 use Vue Mounted get 時區
    function getClientTimezone() {
        return Intl.DateTimeFormat().resolvedOptions().timeZone;
    }
    onMounted(() => {
        registerStore.resetFormData();
        setTimezone(getClientTimezone());
    });

    //const submitForm = async () => {
    //    //wm1. use localstorage, add other content
    //    //await LoginService.submitForm();
    //    //loading.value = LoginService.loading;
    //    //error.value = LoginService.error;

    //    //wm2. use pinia and pinia-plugin-persistedstate
    //    await registerStore.submitForm();
    //};
</script>
