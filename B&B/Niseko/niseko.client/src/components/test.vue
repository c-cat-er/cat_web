<!--@/components/DateTimePickerTest.vue-->
<template>
    <v-container>
        <h2>测试 DateTimePicker 组件</h2>

        <v-menu v-model="pickerModel"
                :close-on-content-click="false"
                transition="scale-transition"
                offset-y
                min-width="290px">
            <template v-slot:activator="{ on, attrs }">
                <v-text-field v-model="formattedDatetime"
                              label="选择日期时间"
                              readonly
                              v-bind="attrs"
                              @focus="pickerModel = true"
                              @click="pickerModel = true"></v-text-field>
            </template>
            <v-date-picker v-model="selectedDatetime"
                           color="primary"
                           type="date"
                           @change="onDateChange"
                           locale="zh-CN"
                           ></v-date-picker>
        </v-menu>
    </v-container>
</template>

<script setup>
    import { ref } from 'vue';

    // 初始值为 Date 对象
    const selectedDatetime = ref(new Date());
    // 格式化后的日期字符串，用于显示在输入框中
    const formattedDatetime = ref(selectedDatetime.value.toISOString().slice(0, 10)); // 默认为 YYYY-MM-DD

    const pickerModel = ref(false); // 初始不显示选择器

    // 监听日期选择器的变化，并格式化为字符串
    const onDateChange = (value) => {
        if (value instanceof Date) {
            selectedDatetime.value = value;
            formattedDatetime.value = selectedDatetime.value.toISOString().slice(0, 10); // 更新为 YYYY-MM-DD 格式
        }
        pickerModel.value = false; // 选择完日期后关闭选择器
    };
</script>
