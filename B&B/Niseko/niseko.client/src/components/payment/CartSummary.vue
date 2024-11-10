<!--@/components/payment/CartSummary.vue -->
<template>
    <v-container>
        <v-row>
            <v-col>
                <div>初始總金額: {{ FInitialAmount }} 元</div>
                <!--<div>折扣: { discount  元</div>  暫不考慮折扣，故 FInitialAmount = FFinalAmount -->
                <div>最終應支付金額: {{ FFinalAmount }} 元</div>
            </v-col>
        </v-row>
    </v-container>
</template>
<script setup>
    import { computed, watch } from 'vue';
    import { storeToRefs } from 'pinia';
    import { shoppingCartStore } from '@/stores/ShoppingCartItemsStore';
    import { paymentStore } from '@/stores/PaymentStore';
    const store = shoppingCartStore();
    const { FInitialAmount } = storeToRefs(store); //discount
    //暫時不考慮折扣，直接在 CartSummary.vue 中设置 FFinalAmount 与 FInitialAmount 相同
    const FFinalAmount = computed(() => FInitialAmount.value);
    const payment = paymentStore();

    //每次计算出最终金额时，更新 PaymentStore
    watch(FFinalAmount, (newVal) => {
        payment.setPaymentData({ FFinalAmount: newVal });
    });
</script>
