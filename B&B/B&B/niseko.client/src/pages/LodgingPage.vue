<!--@/pages/LodgingPage.vue 所有民宿主頁-->
<template>
    <v-container>
        <!-- 添加頂部空白區域 -->
        <v-row>
            <v-col>
                <v-spacer class="my-5"></v-spacer>
            </v-col>
        </v-row>
        <v-row>
            <v-col cols="12" md="4">
                <LodgingFilter />
            </v-col>
            <v-col cols="12" md="8">
                <LodgingSelect />
            </v-col>
        </v-row>
    </v-container>
</template>
<script setup>
    import LodgingFilter from '@/components/lodging/LodgingFilter.vue'
    import LodgingSelect from '@/components/lodging/LodgingSelect.vue'
</script>
