<!--@/components/lodging/LodgingSelect.vue 所有民宿房間資料 -->
<template>
    <v-row>
        <v-col v-for="room in lodgingStore.filteredRooms" :key="room.FHomestayRoomID" cols="12" sm="4">
            <!--sm="6"：在小屏幕（small）設備上，每行顯示兩個元素（每個元素佔用 6/12 = 50% 的寬度）。
                md="4"：在中等屏幕（medium）及以上設備上，每行顯示三個元素（每個元素佔用 4/12 = 33.33% 的寬度）。-->
            <v-card @click="goToBookingPage(room.FHomestayRoomID)">
                <!--<v-img :src="require(`@/assets/images/h1/${room.FImage}`)" aspect-ratio="1.5"></v-img>-->  <!--若用webpack-->
                <!--<v-img :src="`/src/assets/images/h1/${room.FImage}`" aspect-ratio="1.5"></v-img>-->
                <!--使用 loading加載，顯示多張圖片，加入判斷 RoomImages 和 $values 是否存在-->
                <v-img :src="`/src/assets/images/h/${room.RoomImages}`" lazy-src="/src/assets/images/placeholder.jpg" aspect-ratio="1.5"></v-img>
                <v-card-title>{{ room.FHomestayName }} - {{ room.FRoomCode }}</v-card-title>
                <v-card-text>地點ID：{{ room.FAddressName }}</v-card-text>
                <v-card-subtitle>人數：{{ room.FMaxCapacity }} 人</v-card-subtitle>
                <v-card-text>價格：{{ room.FStayPrice }} 元 / {{ room.FStayDays }} 日</v-card-text>
            </v-card>
        </v-col>
        <!--加載狀態-->
        <v-col cols="12" class="text-center">
            <v-progress-circular v-if="lodgingStore.loading" indeterminate color="primary"></v-progress-circular>
        </v-col>
    </v-row>
</template>
<script setup>
    import { onMounted, onBeforeUnmount } from 'vue';
    import { useLodgingStore } from '@/stores/LodgingStore';
    import { useRouter } from 'vue-router';
    const lodgingStore = useLodgingStore();
    const router = useRouter();

    //加載更多房間
    let isFetching = false;
    const loadMoreRooms = () => {
        if (!lodgingStore.loading && lodgingStore.hasMoreRooms && !isFetching) {
            isFetching = true;
            lodgingStore.fetchRooms().finally(() => {
                isFetching = false;
            });
        }
    };

    const onScroll = () => {
        // 判斷是否滾動到底部
        if ((window.innerHeight + window.scrollY) >= document.body.offsetHeight - 10) {
            loadMoreRooms();
        }
    };

    onMounted(() => {
        lodgingStore.fetchRooms();
        window.addEventListener('scroll', onScroll); // 監聽滾動事件
    });

    onBeforeUnmount(() => {
        window.removeEventListener('scroll', onScroll); // 組件卸載時移除滾動事件
    });

    const goToBookingPage = (roomID) => { //k-p, use router 將当前點選的roomID传递给 LodgingBookingPage.vue 组件
        router.push({ name: 'LodgingBooking', params: { roomID } });
    };
</script>
