//@/stores/PaymentStore.js
import { defineStore } from 'pinia';
import axiosInstance from '@/constants/axios';

export const paymentStore = defineStore('payment', {
    state: () => ({
        //db需要
        FMemberID: '',
        FInitialAmount: '',
        FFinalAmount: '',
        FProductType: '',
        FProductID: '',
        FOrderDetailAmount: '',
        FPickupLocationID: '',
        FDropoffLocationID: '',
        FStartDatetime: '',
        FEndDatetime: '',
        FPaymentMethodID: '',
        FPaymentDatetime: '',
        FPaymentAmount: '',
        loading: false,
        error: null,
        paymentUrl: null, //用于存储返回的跳轉支付链接
    }),
    actions: {
        setPaymentData(data) {
            //设置支付所需的各种信息
            /* 09/95 update
            this.FMemberID = data.FMemberID || this.FMemberID;
            this.FInitialAmount = data.FInitialAmount || this.FInitialAmount;
            this.FFinalAmount = data.FFinalAmount || this.FFinalAmount;
            this.FProductType = data.FProductType || this.FProductType;
            this.FProductID = data.FProductID || this.FProductID;
            this.FOrderDetailAmount = data.FOrderDetailAmount || this.FOrderDetailAmount;
            this.FPickupLocationID = data.FPickupLocationID || this.FPickupLocationID;
            this.FDropoffLocationID = data.FDropoffLocationID || this.FDropoffLocationID;
            this.FStartDatetime = data.FStartDatetime || this.FStartDatetime;
            this.FEndDatetime = data.FEndDatetime || this.FEndDatetime; */

            Object.assign(this, data); //use Vue 的響應式狀態自動更新數據
        },
        async processPayment(apiEndpoint) {
            this.loading = true;
            this.error = null;
            try {
                console.log("Payment Request Data:", paymentRequest); // 打印请求数据
                const response = await axiosInstance.post(apiEndpoint, {
                    FMemberID: this.FMemberID,
                    FFinalAmount: this.FFinalAmount,
                    // 其他必要參數
                });
                this.paymentUrl = response.data.paymentUrl;
            } catch (err) {
                this.error = err.message || '支付失敗';
                throw err;
            } finally {
                this.loading = false;
            }
        },
        async processEcpayPayment() {
            return this.processPayment('/api/MemberOrders/ProcessEcpayPayment');
        },
        async processBlueStarPayment() {
            return this.processPayment('/api/MemberOrders/ProcessBlueStarPayment');
        }
    }
});

//支付選擇
//Visa, MasterCard, JCB: 这些是日本非常常见的信用卡品牌，也是全球广泛接受的支付方式。几乎所有日本用户都习惯使用信用卡进行在线支付。
//AMEX: 美国运通卡（AMEX）在日本的使用率不如前三者高，但也有一定市场份额，特别是在国际旅客中。

//PayPay: 作为日本本土最大、最流行的移动支付方式之一，PayPay 的用户群体不断增长，特别是在年轻人和频繁使用智能手机的用户中。
//LINE Pay: 另一个流行的移动支付方式，特别是在使用LINE作为通信工具的用户中非常受欢迎。
//Apple Pay / Google Pay: 这些支付方式在日本的使用率也逐渐上升，特别是在习惯使用智能手机的用户中。

//银行转账（Furikomi）: 传统的银行转账依然是日本用户常用的支付方式，尤其是在较大金额的支付中。许多日本的在线购物和预订网站都支持通过银行转账付款。
//Konbini 支付: 许多日本人习惯通过便利店（如 7 - Eleven, FamilyMart, Lawson 等）支付在线订单。用户可以在下单后获得付款代码，然后到便利店付款。这种支付方式在无信用卡用户或不愿意在线输入信用卡信息的用户中很受欢迎。
