<!--@/pages/ShoppingCartPage.vue -->
<template>
    <v-container>
        <ShoppingCartItems />
        <v-row justify="end">
            <v-col cols="auto">
                <v-alert v-model="showAlert" type="error" dismissible>{{ alertMessage }}</v-alert> <!-- 用于显示错误消息 -->
                <v-alert :value="true" type="info">總價格: {{ FInitialAmount }} 元</v-alert>
                <!--:value="true"-->
            </v-col>
            <v-col cols="auto">
                <v-btn @click="payment">購買</v-btn>
            </v-col>
        </v-row>
    </v-container>
</template>
<script setup>
    import { ref } from 'vue';
    import { useRouter } from 'vue-router'; 
    import { storeToRefs } from 'pinia';
    import ShoppingCartItems from '@/components/cart/ShoppingCartItems.vue';
    import { shoppingCartStore } from '@/stores/ShoppingCartItemsStore';
    const router = useRouter();
    const store = shoppingCartStore();
    const { FInitialAmount, selectedItems } = storeToRefs(store);

    //定義控制 v-alert 的狀態變量
    const showAlert = ref(false);
    const alertMessage = ref('');

    //付款邏輯
    const payment = () => {
        if (selectedItems.value.length > 0) {
            router.push({ name: 'Payment' }); // 有商品時跳轉到付款頁面
        } else {
            // 無商品選擇時顯示提示
            alertMessage.value = '請選擇至少一個商品進行購買';
            showAlert.value = true;
        }
    };
</script>
