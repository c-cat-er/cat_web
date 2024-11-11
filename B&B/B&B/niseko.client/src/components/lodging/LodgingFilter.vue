<!--@/components/lodging/LodgingFilter.vue 民宿過濾 -->
<template>
    <v-row>
        <v-col cols="12">
            <v-range-slider v-model="lodgingStore.priceRange"
                            :min="lodgingStore.minPrice"
                            :max="lodgingStore.maxPrice"
                            :step="10"
                            label="價格範圍"
                            append-icon="mdi-currency-usd"
                            prepend-icon="mdi-currency-usd"
                            class="mt-3">
                <template #append>
                    <span>{{ lodgingStore.priceRange[1] }} 元</span>
                </template>
                <template #prepend>
                    <span>{{ lodgingStore.priceRange[0] }} 元</span>
                </template>
            </v-range-slider>
        </v-col>
    </v-row>
    <v-row>
        <v-col cols="12" v-for="location in lodgingStore.locations" :key="location.value">
            <v-checkbox v-model="lodgingStore.selectedLocations"
                        :label="location.text"
                        :value="location.value"></v-checkbox>
        </v-col>
    </v-row>
</template>
<script setup>
    import { onMounted } from 'vue';
    import { useLodgingStore } from '@/stores/LodgingStore';
    const lodgingStore = useLodgingStore();
    //價格計算改寫再 LodgingStore.js
    //const minPrice = computed(() => Math.min(...lodgingStore.rooms.map(room => room.FStayPrice)));
    //const maxPrice = computed(() => Math.max(...lodgingStore.rooms.map(room => room.FStayPrice)));

    onMounted(() => {
        lodgingStore.fetchLocations();
    });
</script>
