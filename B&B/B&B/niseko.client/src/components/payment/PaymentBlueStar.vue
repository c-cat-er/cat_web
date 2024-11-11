<!--@/components/payment/PaymentBlueStar.vue -->
<template>
    <v-container>
        <h3>藍星金流支付</h3>
        <v-btn @click="processBlueStarPayment" :loading="loading">藍星支付</v-btn>
        <v-alert v-if="error" type="error">{{ error }}</v-alert>
    </v-container>
</template>
<script setup>
    import { storeToRefs } from 'pinia';
    import { paymentStore } from '@/stores/PaymentStore';
    const store = paymentStore();
    const { loading, error } = storeToRefs(store);

    const processBlueStarPayment = async () => {
        try {
            await store.processBlueStarPayment();
            // 假设 API 返回支付链接，你可以重定向到该链接
            window.location.href = store.paymentUrl;
        } catch (err) {
            console.error('BlueStar payment failed:', err);
        }
    };
</script>
