//@/stores/LodgingBookingFormStore.js
import { defineStore } from 'pinia'
import axiosInstance from '@/constants/axios'
//import { watch } from 'vue';
export const lodgingFormStore = defineStore('lodgingForm', {
    state: () => ({
        formData: {
            FShoppingCartID: null,
            FMemberID: '',
            FProductType: 'H',
            FProductID: '',
            FPickupLocationID: null,
            FDropoffLocationID: null,
            FStartDatetime: '',
            FEndDatetime: '',
            FPrice: '',
            FRemark: null,
        },
        loading: false,
        error: null,
        success: null,
    }),
    actions: {
        resetFormData() {
            this.formData = {
                ...this.formData, //保留原始 formData 中不可改變的字段
                //日期歸零統一在 Flatpickr.vue 執行
                FPrice: '',
                FRemark: null,
            };
        },
        async submitForm() {
            this.loading = true
            this.error = null
            this.success = null
            try {
                //console.log('FormData before formatting:', JSON.stringify(this.formData));
                //UI顯示當地時間，db儲存國際時間。

                //改在後端處理
                //接收從 form 至 Flatpickr.vue 至 store 的 UTC 日期時間，移除完後轉為本地時間
                //const formatDatetime = (datetime) => {
                //    if (!datetime) return '';
                //    //将 UTC 时间字符串直接转换为 Date 对象
                //    const utcDate = new Date(datetime);
                //    //将 UTC 转换为本地时间
                //    const localDate = new Date(utcDate.getTime() - (utcDate.getTimezoneOffset() * 60000));
                //    //解析，只保留年、月、日、时、分
                //    return localDate.toISOString().slice(0, 16).replace('T', '').replace(/[-:]/g, '');
                //};

                //const formattedFormData = { //k-p, 轉換資料
                //    ...this.formData, //
                //    FStartDatetime: formatDatetime(this.formData.FStartDatetime),
                //    FEndDatetime: formatDatetime(this.formData.FEndDatetime),
                //};

                //console.log('Submitting form data:', JSON.stringify(this.formData));
                const response = await axiosInstance.post('/api/MemberShoppingCart/homestay', this.formData, {
                    headers: { //k-p, 將時區儲存在標頭傳遞至 server                        
                        'Content-Type': 'application/json' //明確設置 Content-Type，可省
                    }
                });
                this.success = "已成功添加到購物車！";
                this.resetFormData(); // 成功提交後重置表單
                //return response; //返回响应结果
            } catch (err) {
                this.error = err.message
            } finally {
                this.loading = false
            }
        }
    }
});

//帶優化
//你可以考慮使用 Vue 的 v - model 和 reset 方法來自動處理表單重置，而不是手動清空每個字段。

//确保 modelValue1 和 modelValue2 的变化会触发 Form 重新渲染
//export const useLodgingFormWatcher = (store) => {
//    watch(
//        () => store.formData.FStartDatetime,
//        (newValue) => {
//            console.log('FStartDatetime changed:', newValue);
//        }
//    );

//    watch(
//        () => store.formData.FEndDatetime,
//        (newValue) => {
//            console.log('FEndDatetime changed:', newValue);
//        }
//    );
//};
