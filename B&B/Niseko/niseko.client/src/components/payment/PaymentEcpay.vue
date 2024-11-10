<!--@/components/payment/PaymentEcpay.vue -->
<template>
    <v-container>
        <h3>綠界金流支付</h3>
        <v-btn @click="processEcpayPayment" :loading="loading">綠界支付</v-btn>
        <v-alert v-if="error" type="error">{{ error }}</v-alert>
    </v-container>
</template>
<script setup>
    import { ref } from 'vue';
    import { storeToRefs } from 'pinia';
    import { memberDataStore } from '@/stores/MemberDataStore';
    import { paymentStore } from '@/stores/PaymentStore';
    const memberDataStoreInstance = memberDataStore();
    const paymentStoreInstance = paymentStore();
    const { loading, error } = storeToRefs(paymentStoreInstance);

    // 初始化 paymentRequest 对象
    const paymentRequest = ref({
        //db欄位
        FMemberID: '', //from MemberData.vue get
        FInitialAmount: '', //from CartSummary.vue get
        FFinalAmount: '', //from CartSummary.vue get
        FProductType: 'H',
        FProductID: '', //from SelectedItems.vue get，可有多個
        FOrderDetailAmount: '', //from SelectedItems.vue get，可有多個
        FRemarks: '', //from SelectedItems.vue get，可有多個
        FPickupLocationID: '',
        FDropoffLocationID: '',
        FStartDatetime: '', //from SelectedItems.vue get，可有多個
        FEndDatetime: '', //from SelectedItems.vue get，可有多個
        FPaymentMethodID: 6,
        FPaymentDatetime: new Date(), //用戶實際支付日期時間
        FPaymentAmount: '', //用戶實際支付金額
        //綠界參數
        MerchantID: '你的商店代号',
        TradeInfo: '',  // 由后端生成
        TradeSha: '',   // 由后端生成
        Version: '1.0',
        RespondType: 'JSON',
        TimeStamp: new Date().getTime().toString()
    });

    // 处理支付请求
    const processEcpayPayment = async () => {
        try {
            await store.processEcpayPayment();
            // 假设 API 返回支付链接，你可以重定向到该链接
            window.location.href = store.paymentUrl;
        } catch (err) {
            console.error('ECPay payment failed:', err);
        }
    };
</script>
