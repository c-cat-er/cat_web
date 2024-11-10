<template>
  <!-- breadcrumbs -->
  <section class="breadcrumbs separator-bottom">
    <div class="container">
      <div class="row">
        <div class="col">
          <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
              <li class="breadcrumb-item"><a :href="`${MVC_URL}`">首頁</a></li>
              <li class="breadcrumb-item">
                <router-link to="/cart">購物車</router-link>
              </li>
              <li class="breadcrumb-item active" aria-current="page">
                下單頁面
              </li>
            </ol>
          </nav>
        </div>
      </div>
    </div>
  </section>

  <!-- hero -->
  <section>
    <div class="container">
      <div class="row">
        <div class="col text-center">
          <h1>下單頁面</h1>
        </div>
      </div>
    </div>
  </section>

  <section class="no-overflow pt-0">
    <div class="container">
      <div class="row gutter-4 justify-content-between">
        <div class="col-lg-8">
          <!-- 會員資訊 -->
          <div class="row align-items-end mb-2">
            <div class="col-md-6">
              <h2 class="h3 mb-0">
                <span class="text-muted">01.</span> 會員資訊
              </h2>
            </div>
          </div>
          <div class="row gutter-1 mb-6">
            <div class="form-group col-md-12">
              <label for="memberName">會員姓名</label>
              <input
                type="text"
                class="form-control"
                id="memberName"
                readonly
                :value="storeNav.memberInfo.name"
              />
            </div>
            <div class="form-group col-md-12">
              <label for="memberPhone">會員電話</label>
              <input
                type="text"
                class="form-control"
                id="memberPhone"
                readonly
                :value="storeNav.memberInfo.phone"
              />
            </div>
            <div class="form-group col-md-12">
              <label for="memberEmail">會員信箱</label>
              <input
                type="text"
                class="form-control"
                id="memberEmail"
                readonly
                :value="storeNav.memberInfo.email"
              />
            </div>
            <div class="form-group col-md-12">
              <label for="memberAddress">會員信箱</label>
              <input
                type="text"
                class="form-control"
                id="memberAddress"
                readonly
                :value="storeNav.memberInfo.address"
              />
            </div>
          </div>

          <!-- delivery -->
          <div class="row align-items-end mb-2">
            <div class="col-md-6">
              <h2 class="h3 mb-0">
                <span class="text-muted">02.</span> 寄送資訊<span
                  style="color: red"
                  >*</span
                >
              </h2>
            </div>
          </div>
          <div class="row gutter-1 mb-6">
            <div class="form-group col-md-12">
              <label for="RecipientName">收件人姓名</label>
              <input
                v-model="recipientName"
                type="text"
                class="form-control"
                id="RecipientName"
                placeholder="請輸入姓名"
              />
            </div>

            <div class="form-group col-md-12">
              <label for="RecipientAddress">收件人地址</label>
              <input
                v-model="recipientAddress"
                type="text"
                class="form-control"
                id="RecipientAddress"
                placeholder="請輸入地址"
              />
            </div>

            <!-- 帳單收件人 -->
            <div class="form-group col-md-12">
              <label for="BillRecipientName">帳單收件人姓名</label>
              <input
                v-model="billRecipientName"
                type="text"
                class="form-control"
                id="BillRecipientName"
                placeholder="請輸入姓名"
              />
            </div>

            <div class="form-group col-md-12">
              <label for="BillRecipientAddress">帳單收件人地址</label>
              <input
                v-model="billRecipientAddress"
                type="text"
                class="form-control"
                id="BillRecipientAddress"
                placeholder="請輸入地址"
              />
            </div>
            <div class="form-group col-md-12">
              <div class="form-check">
                <input
                  class="form-check-input"
                  type="checkbox"
                  id="useMemberInfoCheckbox"
                  v-model="useMemberInfo"
                />
                <label class="form-check-label" for="useMemberInfoCheckbox">
                  同會員資料
                </label>
              </div>
            </div>
          </div>

          <!-- 寄送方式 -->
          <div class="row align-items-end mb-2">
            <div class="col-md-6">
              <h2 class="h3 mb-0">
                <span class="text-muted">03.</span> 寄送方式<span
                  style="color: red"
                  >*</span
                >
              </h2>
            </div>
          </div>
          <div class="row gutter-1">
            <div
              class="col-md-6"
              v-for="shipping in shippingMethods"
              :key="shipping.shippingId"
            >
              <div class="custom-control custom-choice">
                <input
                  type="radio"
                  :id="'choice-shipping-' + shipping.shippingId"
                  :name="'choice-shipping-' + shipping.shippingId"
                  :value="shipping.shippingId"
                  v-model="selectedShippingMethod"
                  class="custom-control-input"
                />
                <label
                  class="custom-control-label text-dark"
                  :for="'choice-shipping-' + shipping.shippingId"
                >
                  <span class="d-flex justify-content-between mb-1 eyebrow"
                    >{{ shipping.description }}
                    <span class="text-muted"
                      >${{ shipping.deliveryCost }}</span
                    ></span
                  >
                  寄送描述
                </label>
                <span class="choice-indicator"></span>
              </div>
            </div>
          </div>
        </div>
        <aside class="col-lg-4">
          <div class="row">
            <!-- order preview -->
            <div class="col-12">
              <div class="card card-data bg-light">
                <div class="card-header py-2 px-3">
                  <div class="row align-items-center">
                    <div class="col">
                      <h3 class="fs-18 mb-0">購物車</h3>
                    </div>
                  </div>
                </div>
                <div class="card-body">
                  <ul class="list-group list-group-line">
                    <li
                      v-for="item in storeCart.selectedItems"
                      :key="item.cartId"
                      class="list-group-item d-flex justify-content text-dark align-items-center"
                    >
                      <img
                        style="width: 70px"
                        :src="`${IMG_URL}/${item.imageFile}/${item.imageFileName}`"
                        alt=""
                        class="mr-3"
                      />
                      {{ item.name }}
                      <br />
                      單價：{{ item.price }} 元
                      <br />
                      數量：{{ item.quantity }}
                      <br />
                      小計：{{ item.price * item.quantity }} 元
                    </li>
                  </ul>
                </div>
              </div>
            </div>

            <!-- order summary -->
            <div class="col-12 mt-1">
              <div class="card card-data bg-light">
                <div class="card-header py-2 px-3">
                  <div class="row align-items-center">
                    <div class="col">
                      <h3 class="fs-18 mb-0">訂單資訊</h3>
                    </div>
                  </div>
                </div>
                <div class="card-body">
                  <ul class="list-group list-group-minimal">
                    <li
                      class="list-group-item d-flex justify-content-between align-items-center"
                    >
                      小計
                      <span>
                        {{ storeCart.getTotalPriceOfSelectedItems }} 元</span
                      >
                    </li>
                    <li
                      class="list-group-item d-flex justify-content-between align-items-center"
                    >
                      運費
                      <span>{{ shipping.deliveryCost }} 元</span>
                    </li>
                    <!-- <li
                      class="list-group-item d-flex justify-content-between align-items-center"
                    >
                      折扣
                      <span>-25%</span>
                    </li> -->
                  </ul>
                </div>
                <div class="card-footer py-2">
                  <ul class="list-group list-group-minimal">
                    <li
                      class="list-group-item d-flex justify-content-between align-items-center text-dark fs-18"
                    >
                      總計
                      <span
                        >{{
                          shipping.deliveryCost +
                          storeCart.getTotalPriceOfSelectedItems
                        }}
                        元</span
                      >
                    </li>
                  </ul>
                </div>
              </div>
            </div>

            <!-- place order -->
            <div class="col-12 mt-1">
              <a
                @click="placeOrder"
                href="#!"
                class="btn btn-primary btn-lg btn-block"
                >下單</a
              >
            </div>
          </div>
        </aside>
      </div>
    </div>
  </section>
</template>
<script>
const API_URL = import.meta.env.VITE_API_URL;
import { useCartStore, useNavbarStore } from "../store";
import Swal from "sweetalert2";
export default {
  data() {
    return {
      MVC_URL: import.meta.env.VITE_MVC_URL,
      IMG_URL: import.meta.env.VITE_IMAGE_URL,
      recipientName: "",
      recipientAddress: "",
      billRecipientName: "",
      billRecipientAddress: "",
      useMemberInfo: false,
      shippingMethods: [],
      selectedShippingMethod: null,
      shipping: {},
      storeNav: useNavbarStore(),
      storeCart: useCartStore(),
    };
  },
  methods: {
    async fetchShippingMethods() {
      try {
        const response = await fetch(`${API_URL}/Shippings`, {
          method: "GET",
        });

        if (response.ok) {
          const data = await response.json();
          console.log(data);
          this.shippingMethods = data;
          // 給預設值
          this.selectedShippingMethod = data[0].shippingId;
          //更新 shipping 變數
          this.shipping = data[0];
        } else {
          console.error("Failed to fetch shipping methods");
        }
      } catch (err) {
        console.error(err);
      }
    },
    //提交訂單
    async placeOrder() {
      try {
        // 檢查姓名和地址是否為空
        if (this.recipientName == "") {
          alert("請輸入完整的姓名和地址");
          return;
        }
        // 檢查是否選擇了寄送方式
        if (!this.selectedShippingMethod) {
          alert("請選擇寄送方式");
          return;
        }

        const currentDateTime = new Date();
        const DateTime = new Date(
          currentDateTime.getTime() + 8 * 60 * 60 * 1000
        );

        //要發送的資料
        var orderData = {
          orderId: 0,
          memberId: this.storeNav.memberId,
          paymentStatusId: 1,
          paymentId: 1,
          shippingId: this.selectedShippingMethod,
          recipientName: this.recipientName,
          recipientAddress: this.recipientAddress,
          billRecipientName: this.billRecipientName,
          billRecipientAddress: this.billRecipientAddress,
          shippingStatusId: 1,
          creationTime: DateTime.toISOString(),
          completionTime: null,
          orderStatusId: 2,
        };

        const response = await fetch(`${API_URL}/Orders`, {
          method: "POST",
          body: JSON.stringify(orderData),
          headers: {
            "content-type": "application/json",
          },
        });

        if (response.ok) {
          const data = await response.json();
          console.log(data);
          await this.submitOrderDetails(data);
        } else {
        }
      } catch (error) {
        console.error("訂單提交失敗", error.message);
      }
    },
    async submitOrderDetails(data) {
      const orderDetailsData = [];
      for (const item of this.storeCart.selectedItems) {
        const detail = {
          orderId: data.orderId,
          productId: item.productId || null,
          inspirationId: item.inspirationId || null,
          customizedId: item.customizedId || null,
          quantity: item.quantity,
          unitPrice: item.price,
        };
        //console.log("OrderDetail JSON data:", detail);
        orderDetailsData.push(detail);
      }

      const detailsResponse = await fetch(`${API_URL}/OrderDetails`, {
        method: "POST",
        body: JSON.stringify(orderDetailsData),
        headers: {
          "content-type": "application/json",
        },
      });
      if (detailsResponse.ok) {
        await this.storeCart.deletePurchasedProduct();
        this.storeCart.fetchCartItems(this.storeNav.memberId);
        //await this.SendToNewebPay(orderDetailsData);
        Swal.fire({
          title: "訂單建立成功",
          icon: "success",
          showDenyButton: true,
          confirmButtonText: "繼續購物",
          denyButtonText: `檢視訂單`,
          denyButtonColor: "#3085d6",
        }).then((result) => {
          if (result.isConfirmed) {
            this.$router.push("/product_listing");
          } else if (result.isDenied) {
            this.$router.push("/profile_order");
          }
        });
      } else {
        //如果details 建立失敗，要刪除前面建立的訂單
      }
    },
    async SendToNewebPay() {
      // 組合表單資料
      const response = await fetch("/api/NewebPay/SendToNewebPay", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          ChannelID: 1,
          MerchantID: "MS151844406",
          MerchantOrderNo: this.orderId, // 假設這個變數已經在其他地方定義
          ItemDesc: orderDetailsData, // 假設這個變數已經在其他地方定義
          Amt: this.storeCart.getTotalPriceOfSelectedItems(), // 假設這個方法已經在其他地方定義
          ExpireDate: new Date(Date.now() + 3 * 24 * 60 * 60 * 1000)
            .toISOString()
            .slice(0, 10)
            .replace(/-/g, ""),
          ReturnURL: `${window.location.origin}/Home/CallbackReturn`,
          CustomerURL: `${window.location.origin}/Home/CallbackCustomer`,
          NotifyURL: `${window.location.origin}/Home/CallbackNotify`,
          ClientBackURL: window.location.origin,
          Email: "modpack@test.com",
        }),
      });
      if (response.ok) {
        // 呼叫藍新金流 API
        const responseJson = await response.json();
        const form = document.createElement("form");
        form.method = "post";
        form.action = "https://ccore.newebpay.com/MPG/mpg_gateway"; // 藍新金流驗證網址(測試環境)

        for (const key in responseJson) {
          if (responseJson.hasOwnProperty(key)) {
            const hiddenField = document.createElement("input");
            hiddenField.type = "hidden";
            hiddenField.name = key;
            hiddenField.value = responseJson[key];
            form.appendChild(hiddenField);
          }
        }
        document.body.appendChild(form);
        form.submit();
      } else {
        console.error("Error:", err);
        alert("送出至藍新金流失敗");
      }
    },
  },
  watch: {
    selectedShippingMethod: function (newVal) {
      const selectedShipping = this.shippingMethods.find(
        (shipping) => shipping.shippingId === newVal
      );
      if (selectedShipping) {
        // 更新 shipping 變數
        this.shipping = selectedShipping;
      }
    },
    useMemberInfo: function (newVal) {
      if (newVal) {
        // 如果复选框被选中，则将会员信息带入到收件人姓名与地址中
        this.recipientName = this.storeNav.memberInfo.name;
        this.recipientAddress = this.storeNav.memberInfo.address;
        this.billRecipientName = this.storeNav.memberInfo.name;
        this.billRecipientAddress = this.storeNav.memberInfo.address;
      } else {
        // 如果复选框未被选中，则清空收件人姓名与地址
        this.recipientName = "";
        this.recipientAddress = "";
        this.billRecipientName = "";
        this.billRecipientAddress = "";
      }
    },
  },
  mounted() {
    //this.storeNav.getMemberIdFromCookie();
    this.fetchShippingMethods();
    //this.storeCart.fetchCartItems(this.storeNav.memberId);
    //this.storeNav.fetchMemberInfo();
  },
  components: {},
};
</script>
<style></style>
