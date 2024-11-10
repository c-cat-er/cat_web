<!--@/components/payment/SelectedItems.vue 付款項目-->
<template>
    <v-container>
        <v-data-table :items="selectedItems" :headers="headers" class="elevation-1"></v-data-table>
    </v-container>
</template>
<script setup>
    import { computed } from 'vue';
    import { storeToRefs } from 'pinia';
    import { shoppingCartStore } from '@/stores/ShoppingCartItemsStore';
    import { paymentStore } from '@/stores/PaymentStore';
    const store = shoppingCartStore();
    const { selectedItems } = storeToRefs(store);

    const generateHeaders = (additionalHeaders = []) => [
        { title: '產品類型', value: 'FProductType' },
        { title: '產品ID', value: 'FProductID' },
        ...additionalHeaders,
        { title: '價格', value: 'FPrice' },
        { title: '備註', value: 'FRemark', sortable: false },
    ];
    const headers = computed(() => {
        const productType = selectedItems.value[0]?.FProductType;
        if (productType === 'H') {
            return generateHeaders([
                { title: '民宿名', value: 'FHomestayName' },
                { title: '民宿房間名', value: 'FRoomCode' },
                { title: '起始時間', value: 'FStartDatetime' },
                { title: '結束時間', value: 'FEndDatetime' }
            ]);
        } else if (productType === 'C') {
            return generateHeaders([
                { title: '上車ID', value: 'FPickupLocationID' },
                { title: '下車ID', value: 'FDropoffLocationID' },
                { title: '起始時間', value: 'FStartDatetime' },
                { title: '結束時間', value: 'FEndDatetime' }
            ]);
        } else if (productType === 'S' || productType === 'E' || productType === 'T') {
            return generateHeaders([
                { title: '地點ID', value: 'FLocationID' },
                { title: '天數', value: 'FDays' },
                { title: '人數', value: 'FPeopleCount' }
            ]);
        }
    });

    const payment = paymentStore();
    //将选中商品信息存入 PaymentStore
    selectedItems.value.forEach(item => {
        payment.setPaymentData({
            FProductID: item.FProductID,
            FProductType: item.FProductType,
            FOrderDetailAmount: item.FPrice
        });
    });
</script>
