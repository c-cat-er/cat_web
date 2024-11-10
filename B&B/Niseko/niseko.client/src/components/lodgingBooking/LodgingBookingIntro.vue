<!--@/components/lodging/LodgingBookingIntro.vue 當前民宿介紹-->
<!--在刷新時重新加載單個房間的數據。-->
<template>
    <!-- 加載成功 -->
    <v-container v-if="!loading && room">
        <h3>民宿房間ID：{{ room.FHomestayRoomID }}</h3>
        <h3>民宿房間名：{{ room.FHomestayName }} - {{ room.FRoomCode }}</h3>
        <p>描述：{{ room.FDescription }}</p>
        <p>地點：{{ room.FAddressName }}</p>
        <p>人數：{{ room.FMaxCapacity }} 人</p>
        <p>價格：{{ room.FStayPrice }} 元 / {{ room.FStayDays }} 日</p>
        <!-- 顯示所有圖片 -->
        <div v-if="Array.isArray(room.FRoomImages?.$values) && room.FRoomImages.$values.length > 0">
            <v-row>
                <v-col v-for="(image, index) in room.FRoomImages.$values" :key="index" cols="12" md="4">
                    <v-img :src="`/src/assets/images/h/${image}`"
                           lazy-src="/src/assets/images/placeholder.jpg"
                           aspect-ratio="1.5"></v-img>
                </v-col>
            </v-row>
        </div>
        <p v-else>房間信息未找到</p>
    </v-container>
    <v-container v-else>
        <v-progress-circular indeterminate color="primary"></v-progress-circular>
    </v-container>
</template>
<script setup>
    import { defineProps, computed } from 'vue'; //k-p,
    import { useLodgingStore } from '@/stores/LodgingStore';
    const lodgingStore = useLodgingStore();

    const props = defineProps({
        //defineProps 是 Vue 3 Composition API 的一部分，用於在 setup 函數中定義並接收從父組件傳遞過來的 props。
        //defineProps：這個函數會返回包含所有從父組件傳遞過來的 props 的對象。這樣你就可以在 setup 函數中使用 props.room 來訪問傳遞的 room 對象。
        room: {
            type: Object, //當前對象
            required: true,
            //validator: (value) => 'FHomestayRoomID' in value && 'FStayPrice' in value //此验证器用于检查 props 传递的对象是否包含指定的必要属性。
            //validator 主要用于开发模式下的 props 验证。当你在开发过程中编写和调试代码时，Vue 会根据 validator 的规则来检查传递给组件的 props 是否符合预期，并在控制台中显示警告。如果 props 不符合验证规则，Vue 会在开发环境中发出警告。
            //在生产环境中，Vue 默认情况下不会执行 props 验证，以减少性能开销。这意味着在生产环境中，即使 props 验证器中的规则未通过，Vue 也不会发出警告或错误。
        }
    });

    //加載狀態
    const loading = computed(() => lodgingStore.loading);
</script>
