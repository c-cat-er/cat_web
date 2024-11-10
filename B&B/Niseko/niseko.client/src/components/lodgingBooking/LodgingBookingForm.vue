<!--@/components/lodging/LodgingBookingForm.vue 當前民宿預定-->
<template>
    <v-container>
        <v-form @submit.prevent="handleFormSubmit">
            <!--自動填充會員ID，v-show="false" 隱藏-->
            <v-text-field v-model="formData.FMemberID" label="會員ID" required readonly ></v-text-field>
            <!--當前選擇的產品ID-->
            <v-text-field v-model="formData.FProductID" label="產品ID" required readonly ></v-text-field>

            <!-- 使用 Flatpickr -->
            <!--<Flatpickr v-model="formData.FStartDatetime" label="选择起始日期时间" :disableDates="holidays" />
            <Flatpickr v-model="formData.FEndDatetime" label="选择结束日期时间" :disableDates="holidays" />-->
            <!--k-p, 因此需要兩個 v-model，故改手動绑定事件-->
            <Flatpickr ref="flatpickrRef"
                       :modelValue1="formData.FStartDatetime" :modelValue2="formData.FEndDatetime"
                       @update:modelValue1="formData.FStartDatetime = $event"
                       @update:modelValue2="formData.FEndDatetime = $event"
                       labelStart="選擇起始日期時間" labelEnd="選擇結束日期時間" :disableDates="holidays" :autoAddOneDay="true" />
            <!--<pre>FStartDatetime: {{ formData.FStartDatetime }}</pre>
            <pre>FEndDatetime: {{ formData.FEndDatetime }}</pre>-->
            <!--價格依據天數自動計算-->
            <v-text-field v-model="formData.FPrice" label="價格" readonly></v-text-field>
            <v-text-field v-model="formData.FRemark" label="備註(50字以內)"></v-text-field>
            <v-btn @click="handleReset" :loading="loading" class="mx-2">清空重填</v-btn>
            <v-btn type="submit" :loading="loading" class="mx-2">加到購物車</v-btn>
            <v-btn type="submit" :loading="loading" class="mx-2">直接付款</v-btn>
            <!-- 當 error 狀態不為 null 或 undefined（即有錯誤信息）時，v-alert 元素會被渲染出來，顯示錯誤信息。否則，這個 v-alert 元素將不會顯示。 -->
            <!--<v-alert v-if="error" type="error">{{ error }}</v-alert>-->
            <v-alert v-if="error" type="error">請先登入</v-alert>
            <v-alert v-if="success" type="success">{{ success }}</v-alert>
        </v-form>
    </v-container>
</template>
<script setup>
    import { ref, onMounted, watch } from 'vue'
    import { storeToRefs } from 'pinia';
    import { lodgingFormStore } from '@/stores/LodgingBookingFormStore';
    import { registerFormStore } from '@/stores/RegisterFormStore'; //k-p, 讀取此檔的 memberId 自動填充表單
    import { loginFormStore } from '@/stores/LoginFormStore'; //k-p, 讀取此檔的 memberId 自動填充表單
    import Flatpickr from '@/components/share/Flatpickr.vue';
    const formStore = lodgingFormStore();
    const registerStore = registerFormStore();
    const loginStore = loginFormStore();
    const { formData, loading, error, success } = storeToRefs(formStore);
    const { registerMemberId } = storeToRefs(registerStore);
    const { loginMemberId } = storeToRefs(loginStore);
    const { submitForm } = formStore;
    const flatpickrRef = ref(null);

    // 国定假日
    const holidays = [
        "2024-01-01", // 元旦
        "2024-02-10", // 春节
        "2024-04-04", // 清明节
        "2024-05-01", // 劳动节
    ];

    const props = defineProps({
        room: {
            type: Object,
            required: true,
            //validator: (value) => 'FHomestayRoomID' in value && 'FStayPrice' in value
        }
    });

    //k-p, 初始化表單，並填充會員ID。优先检查 loginMemberId 是否存在，然后再检查 registerMemberId
    onMounted(() => {
        formData.value.FMemberID = loginMemberId.value || registerMemberId.value || '';
        formData.value.FProductID = props.room.FHomestayRoomID;

        // 每次進入頁面時，重新計算價格
        calculatePrice();
    });

    //監聽日期變化並計算價格 ??
    watch(
        [() => formData.value.FStartDatetime, () => formData.value.FEndDatetime],
        calculatePrice
    );

    function calculatePrice() {
        const startDate = new Date(formData.value.FStartDatetime);
        const endDate = new Date(formData.value.FEndDatetime);

        if (!isNaN(startDate.getTime()) && !isNaN(endDate.getTime())) { // 确保日期是有效的
            const days = (endDate - startDate) / (1000 * 60 * 60 * 24); // 计算天数差
            formData.value.FPrice = days > 0 ? (days * props.room.FStayPrice).toString() : '0';  // 计算价格并转换为字符串
        } else {
            formData.value.FPrice = '0'; // 如果选择无效的日期范围，设置价格为 0
        }
    }

    //清空表格資料重填
    const handleReset = () => {
        formStore.resetFormData();
        if (flatpickrRef.value) {
            flatpickrRef.value.clearDates(); //清除 Flatpickr 日期
        }
    };

    //提交表單後清除 Flatpickr 日期值
    const handleFormSubmit = async () => {
        try {
            await submitForm(); //提交表单数据
            handleReset();
        } catch (err) {
            console.error('Form submission failed:', err);
        }
    };
</script>

<!-- 待優化
在提交表單之前才動態添加 FProductID 或 FMemberID 等信息，避免每次頁面加載時都需要初始化所有狀態。-->
<!--您想了解如何處理表單提交成功後的響應並顯示成功消息嗎？
b. 您需要進一步了解如何在表單提交前進行數據驗證嗎？-->
