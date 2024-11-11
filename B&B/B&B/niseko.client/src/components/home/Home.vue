<!--@/components/home/Home.vue -->
<template>
    <v-container fluid class="p-0">
        <div class="main-content">
            <div class="video-section">
                <video :src="home1.src" :alt="home1.alt" loop muted poster=""></video> <!--autoplay-->
            </div>
            <!-- 视频下方添加占位元素，避免视频与内容重叠 -->
            <div class="video-placeholder"></div>
            <div class="homestay-booking">
                <h2>民宿預定</h2>
                <!--Flatpickr.vue-->
                <!--<DatePicker />-->
            </div>
            <!-- 民宿介紹，使用 imageConfig 動態顯示圖片 -->
            <div class="homestay-intro">
                <h2>民宿介紹</h2>
                <div v-for="(imageKey, index) in imageKeys" :key="index" class="intro-item">
                    <v-row>
                        <v-col cols="12" md="4">
                            <h3>{{ imageConfig[imageKey].alt }}</h3>
                        </v-col>
                        <v-col cols="12" md="8">
                            <v-img :src="imageConfig[imageKey].src"
                                   :alt="imageConfig[imageKey].alt"
                                   :style="{ width: imageConfig[imageKey].width, height: imageConfig[imageKey].height }"
                                   lazy-src="/src/assets/images/placeholder.jpg"
                                   aspect-ratio="1.5">
                            </v-img>
                        </v-col>
                    </v-row>
                </div>
            </div>
            <div class="shuttle-intro">
                <h2>交通車租送</h2>
                <v-col cols="12" md="4">
                    <h3>{{ shuttle1.alt }}</h3>
                </v-col>
                <v-col cols="12" md="8">
                    <v-img :src="shuttle1.src"
                           :alt="shuttle1.alt"
                           :style="{ width: shuttle1.width, height: shuttle1.height }"
                           lazy-src="/src/assets/images/placeholder.jpg"
                           aspect-ratio="1.5">
                    </v-img>
                </v-col>
            </div>
        </div>
    </v-container>
</template>
<script setup>
    //import DatePicker from '@/components/share/Datepicker.vue';
    import { imageConfig } from '@/constants/imageConfig.js';

    // 單個主視頻
    const home1 = imageConfig.home1;
    // 需要循環顯示的 homestay 圖片 key
    const imageKeys = ['homestay1', 'homestay2', 'homestay3'];
    // 單獨顯示的 shuttle1 圖片
    const shuttle1 = imageConfig.shuttle1;
</script>
<style scoped>
    .main-content {
        display: flex;
        flex-direction: column;
    }
    .video-section {
        height: 90vh;
        background-color: white;
        position: absolute; /*使 video 從頂開始*/
        top: 0; /*使 video 從頂開始*/
        left: 0; /*使 video 從頂開始*/
        width: 100vw;
        overflow: hidden;
    }
    .video-section > video {
        width: 100vw;
        object-fit: cover; /* k-p, */
    }
    .video-placeholder { /* 占位元素，填充视频的高度 */
        height: 90vh;
        width: 100%;
    }

    .homestay-booking {
        background-color: #97CBFF; /*天空藍*/
    }
    .homestay-intro {
        display: flex;
        flex-direction: column;
        justify-content: space-between;
        align-items: center;
        background-color: #97CBFF; /*天空藍*/
        gap: 20px;
        padding: 20px;
    }
    .intro-item {
        width: 60%;
        background-color: #f5f5f5;
        padding: 15px;
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
    }

    .shuttle-intro {
        display: flex;
        flex-direction: column;
        justify-content: space-between;
        align-items: center;
        background-color: #97CBFF; /*天空藍*/
    }
</style>
