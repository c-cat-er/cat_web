<!--@/components/cart/ShoppingCartItems.vue -->
<template>
    <v-container>
        <!--若購物車是空的，显示一条提示信息或空购物车状态，而不是直接渲染数据-->
        <template v-if="cartItems.length === 0">
            <v-row justify="center" class="my-5">
                <v-col cols="12" class="text-center">
                    <v-alert type="info" dismissible>您的購物車目前是空的。</v-alert>
                </v-col>
            </v-row>
        </template>
        <!--若非空-->
        <template v-else>
            <!--动态 headers-->
            <v-data-table :items="cartItems" :headers="headers" item-value="FShoppingCartID" class="elevation-1">
                <template v-slot:top>
                    <v-toolbar flat>
                        <v-toolbar-title>購物車</v-toolbar-title>
                    </v-toolbar>
                </template>
                <!--複選框-->
                <template v-slot:item.checkbox="{ item }">
                    <v-checkbox v-model="item.selected" @change="() => selectItem(item)" />
                </template>
                <!-- 刪除按鈕 -->
                <template v-slot:item.actions="{ item }">
                    <v-btn @click="openDeleteDialog(item)" icon>
                        <v-icon>mdi-delete</v-icon>
                    </v-btn>
                </template>
            </v-data-table>
        </template>

        <!-- 刪除確認對話框 -->
        <v-dialog v-model="deleteDialogVisible" max-width="400">
            <v-card>
                <v-card-title class="headline">確認刪除</v-card-title>
                <v-card-text>您確定要刪除此商品嗎？</v-card-text>
                <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn color="green darken-1" text @click="deleteDialogVisible = false">取消</v-btn>
                    <v-btn color="red darken-1" text @click="confirmDelete">刪除</v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
    </v-container>
</template>
<script setup>
    import { ref, computed, onMounted } from 'vue';
    import { storeToRefs } from 'pinia';
    import { shoppingCartStore } from '@/stores/ShoppingCartItemsStore';
    const store = shoppingCartStore();
    const { cartItems } = storeToRefs(store);
    const { removeFromCart, selectItem } = store;

    // 定義變量，用來控制刪除確認對話框的顯示
    const deleteDialogVisible = ref(false);
    const itemToDelete = ref(null); // 用來存儲要刪除的項目

    // 打開刪除對話框並記錄要刪除的項目
    const openDeleteDialog = (item) => {
        itemToDelete.value = item;
        deleteDialogVisible.value = true;
    };

    // 確認刪除項目
    const confirmDelete = () => {
        if (itemToDelete.value) {
            removeFromCart(itemToDelete.value.FShoppingCartID);
        }
        deleteDialogVisible.value = false;
        itemToDelete.value = null;
    };

    //k-p, 根據產品類型動態生成 headers
    //const headers = computed(() => { //k-p, 要帶入的表單資料
    //    const baseHeaders = [
    //        { title: '選擇', value: 'checkbox', sortable: false }, //不排序
    //        { title: '產品類型', value: 'FProductType' },
    //        { title: '產品ID', value: 'FProductID' }
    //    ];
    //    const endingHeaders = [
    //        { title: '價格', value: 'FPrice' },
    //        { title: '備註', value: 'FRemark', sortable: false },
    //        { title: '刪除', key: 'actions', sortable: false },
    //    ];

    //    //確認產品類型來決定顯示哪些欄位
    //    const productType = cartItems.value.length > 0 ? cartItems.value[0].FProductType : null;

    //    if (productType === 'H') {
    //        return [
    //            ...baseHeaders,
    //            { title: '民宿名', value: 'FHomestayName' },
    //            { title: '民宿房間名', value: 'FRoomCode' },
    //            { title: '起始時間', value: 'FStartDatetime' },
    //            { title: '結束時間', value: 'FEndDatetime' },
    //            ...endingHeaders
    //        ];
    //    } else if (productType === 'C') {
    //        return [
    //            ...baseHeaders,
    //            { title: '上車ID', value: 'FPickupLocationID' },
    //            { title: '下車ID', value: 'FDropoffLocationID' },
    //            { title: '起始時間', value: 'FStartDatetime' },
    //            { title: '結束時間', value: 'FEndDatetime' },
    //            ...endingHeaders
    //        ];
    //    } else if (productType === 'S' || productType === 'E' || productType === 'T') {
    //        return [
    //            ...baseHeaders,
    //            { title: '地點ID', value: 'FLocationID' },
    //            { title: '天數', value: 'FDays' },
    //            { title: '人數', value: 'FPeopleCount' },
    //            ...endingHeaders
    //        ];
    //    }

    //    return [...baseHeaders, ...endingHeaders];
    //});

    //k-p, 上面簡寫後
    const generateHeaders = (additionalHeaders = []) => [
        { title: '選擇', value: 'checkbox', sortable: false },
        { title: '產品類型', value: 'FProductType' },
        { title: '產品ID', value: 'FProductID' },
        ...additionalHeaders,
        { title: '價格', value: 'FPrice' },
        { title: '備註', value: 'FRemark', sortable: false },
        { title: '刪除', key: 'actions', sortable: false }
    ];
    const headers = computed(() => {
        const productType = cartItems.value[0]?.FProductType;
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

    onMounted(() => { // 頁面加載時獲取購物車項目
        store.fetchCart();
    });
</script>

<!-- k-p, <v-data-table> 是 Vuetify 的一個組件，用於顯示表格數據。
:items="cartItems" 表示將 cartItems 資料綁定到表格。
:headers="headers" 表示將 js 的表頭資訊綁定到表格。-->
<!-- k-p, v-slot: 用來指示這是一個插槽
    item.actions 是插槽的名稱，用來定義在 actions 列中要顯示的內容
    "{ item }" 是插槽作用域 (scope)，它將每一行的數據對象 (item) 傳遞給插槽內部。-->
