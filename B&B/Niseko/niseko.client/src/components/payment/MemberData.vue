<!--@/components/payment/MemberData.vue -->
<template>
    <v-container>
        <!--不用會員ID 編號-->
        <h2>購買人信息</h2>
        <div>ID: {{ FMemberID }}</div>
        <div>編號: {{ FMemberCode }}</div>
        <div>姓名: {{ FMemberName }}</div>
        <div>電話: {{ FPhone }}</div>
        <div>信箱: {{ FEmail }}</div>
    </v-container>
</template>

<script setup>
    import { storeToRefs } from 'pinia';
    import { memberDataStore } from '@/stores/MemberDataStore';
    import { paymentStore } from '@/stores/PaymentStore';
    const store = memberDataStore();
    //store.fetchMemberData(); //k-p, 
    store.initializeMemberData(); //k-p, call memberDataStore fn, from cookie get member data and init
    const { FMemberID, FMemberCode, FMemberName, FPhone, FEmail } = storeToRefs(store);
    const payment = paymentStore();

    //k-p, 存储会员信息到支付状态
    payment.setPaymentData({
        FMemberID: FMemberID.value,
        FMemberName: FMemberName.value,
        FPhone: FPhone.value,
        FEmail: FEmail.value
    });
</script>
