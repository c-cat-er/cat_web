<!--@/pages/PaymentPage.vue -->
<template>
    <v-container>
        <SelectedItems :items="selectedItems" />
        <MemberData />
        <!--<DiscountCode />-->
        <CartSummary />
        <v-row>
            <v-col cols="12" md="4">
                <PaymentEcpay />
            </v-col>
            <v-col cols="12" md="4">
                <PaymentBlueStar />
            </v-col>
            <v-col cols="12" md="4">
                <PayPalButton />
            </v-col>
        </v-row>
    </v-container>
</template>

<script setup>
    import { onMounted } from 'vue';
    import { useRouter } from 'vue-router';
    import { storeToRefs } from 'pinia';
    import SelectedItems from '@/components/payment/SelectedItems.vue';
    import MemberData from '@/components/payment/MemberData.vue';
    //import DiscountCode from '@/components/payment/DiscountCode.vue';
    import CartSummary from '@/components/payment/CartSummary.vue';
    import PaymentEcpay from '@/components/payment/PaymentEcpay.vue';
    import PaymentBlueStar from '@/components/payment/PaymentBlueStar.vue';
    import PayPalButton from '@/components/payment/PayPalButton.vue';
    import { shoppingCartStore } from '@/stores/ShoppingCartItemsStore';

    const router = useRouter();
    const store = shoppingCartStore();
    const { selectedItems } = storeToRefs(store);

    onMounted(() => {
        if (!selectedItems.value.length) { //若無商品項跳回到購物車頁
            router.push({ name: 'ShoppingCart' });
        }
    });
</script>
