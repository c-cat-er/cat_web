<!--@/pages/LodgingBookingPage.vue -->
<template>
    <v-container>
        <!-- 添加頂部空白區域 -->
        <v-row>
            <v-col>
                <v-spacer class="my-5"></v-spacer>
            </v-col>
        </v-row>
        <v-row>
            <!-- 當 currentRoom 存在時才渲染以下組件 -->
            <v-col v-if="currentRoom" cols="12" md="8">
                <LodgingBookingIntro :room="currentRoom" />
                <!--k-p, :room="currentRoom" 是 Vue.js 的語法，用於將父組件中的數據或變量（currentRoom）作為 prop 傳遞給子組件 (LodgingBookingIntro)。-->
            </v-col>
            <v-col v-if="currentRoom" cols="12" md="4">
                <LodgingBookingForm :room="currentRoom" />
            </v-col>
            <!-- 若 currentRoom 尚未加載，顯示加載中的指示器 -->
            <v-col v-else class="text-center">
                <v-progress-circular indeterminate color="primary"></v-progress-circular>
            </v-col>
        </v-row>
        <!-- 其他推薦民宿等組件 -->
    </v-container>
</template>
<script setup>
    import { computed, onMounted } from 'vue';
    import { useRoute } from 'vue-router';
    import { useLodgingStore } from '@/stores/LodgingStore';
    import LodgingBookingIntro from '@/components/lodgingBooking/LodgingBookingIntro.vue';
    import LodgingBookingForm from '@/components/lodgingBooking/LodgingBookingForm.vue';
    const route = useRoute();
    const lodgingStore = useLodgingStore();

    //根據路由參數獲取 roomID
    const roomIDParam = route.params.roomID;
    const roomID = Array.isArray(roomIDParam) ? parseInt(roomIDParam[0], 10) : parseInt(roomIDParam, 10);

    //currentRoom 會根據房間 ID 找尋對應的房間資料
    const currentRoom = computed(() => lodgingStore.rooms.find(room => room.FHomestayRoomID === roomID));

    //onMounted 時檢查是否已經有房間數據
    onMounted(async () => {
        // 如果在 store 中找不到對應房間數據，則從服務器加載單個房間數據
        if (!currentRoom.value) {
            await lodgingStore.fetchSingleRoom(roomID);  // 定義一個新的 action 來加載單個房間數據
        }
    });
</script>

<!--帶優化
在 LodgingBookingPage.vue 中，如果引入的組件不需要做其他事情（例如，僅用於顯示），可以考慮使用 defineAsyncComponent 動態加載它們，以減少初始頁面的加載時間。-->
