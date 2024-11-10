<!--@/components/payment/PayPalButton.vue -->
<template>
    <v-container>
        <h3>信用卡支付 (透過 PayPal)</h3>
        <div id="paypal-button-container"></div>
        <v-alert v-if="error" type="error">{{ error }}</v-alert>
    </v-container>
</template>
<script setup>
    // @ts-nocheck 禁用 typescript 檢查
    import { ref, onMounted } from 'vue';
    import { storeToRefs } from 'pinia';
    import { loadScript } from '@paypal/paypal-js';
    import { paymentStore } from '@/stores/PaymentStore';
    const error = ref(null);
    const store = paymentStore();
    const { FFinalAmount } = storeToRefs(store); // 获取最终金额

    onMounted(async () => {
        try {
            // 加载 PayPal SDK 并设置支付按钮
            const paypal = await loadScript({
                clientId: 'AfSKnmqCK8P21S0geLpbr8lojchVbiXE4pjopdsIujQiIu5fAlKQ8tO98VVUzEy4nuaZjHNPEbJJ_gHX', // 你的 PayPal 客户端 ID
                // disableFunding: ['credit', 'card'], // 禁用不需要的支付方式
                currency: 'TWD' // 设置货币
            });

            // 渲染 PayPal 按鈕
            if (paypal) {
                paypal.Buttons({
                    createOrder: function (data, actions) {
                        return actions.order.create({
                            purchase_units: [{
                                amount: {
                                    currency_code: 'TWD',
                                    value: FFinalAmount.value.toString() //使用存储的最终金额
                                }
                            }]
                        });
                    },
                    onApprove: function (data, actions) {
                        return actions.order.capture().then(function (details) {
                            const payerName = details.purchase_units[0].shipping?.name?.full_name;
                            if (payerName) {
                                alert('交易成功，感谢 ' + payerName);
                            } else {
                                alert('交易成功，但无法获取付款人信息。');
                            }
                        });
                    },
                    onError: function (err) {
                        console.error('PayPal 支付错误', err);
                        error.value = '支付过程中出现错误，请稍后重试。';
                    }
                }).render('#paypal-button-container');
            } else {
                error.value = '无法加载 PayPal SDK，请检查设置。';
            }
        } catch (err) {
            error.value = '无法加载 PayPal SDK，请检查设置。';
        }
    });
</script>
