<template>
  <!-- hero -->
  <section class="hero hero-small bg-gray text-white">
    <div class="container">
      <div class="row gutter-2 gutter-md-4 align-items-end">
        <div class="col-md-6 text-center text-md-left">
          <h1 class="mb-0">{{ storeNav.memberInfo.name }} 的訂單</h1>
        </div>
      </div>
    </div>
  </section>
  <!-- listing -->
  <section class="pt-5">
    <div class="container">
      <div class="row gutter-4 justify-content-between">
        <!-- content -->
        <div class="col-lg-9">
          <div class="row">
            <div class="col">
              <div class="tab-content" id="myTabContent">
                <!-- orders -->
                <div
                  class="tab-pane fade show active"
                  id="sidebar-1-2"
                  role="tabpanel"
                  aria-labelledby="sidebar-1-2"
                >
                  <div class="row">
                    <div class="col-12">
                      <h3 class="mb-0">訂單列表</h3>
                      <span class="eyebrow">{{ orders.length }}筆</span>
                    </div>
                  </div>
                  <div
                    v-for="order in paginatedOrders"
                    :key="order.orderId"
                    class="row gutter-2"
                  >
                    <div class="col-12">
                      <div class="order">
                        <div class="row align-items-center">
                          <!-- 訂單編號 -->
                          <div class="col-lg-4">
                            <h3 class="order-number">
                              訂單編號 {{ order.orderId }}
                            </h3>
                            <a
                              href="#"
                              class="action eyebrow underline"
                              @click.prevent="showOrderDetails(order.orderId)"
                            >
                              訂單明細
                            </a>
                          </div>
                          <!-- 訂單狀態 -->
                          <div class="col-lg-4">
                            <span class="order-orderStatus"
                              >訂單狀態：{{ order.orderStatusName }}</span
                            >
                          </div>
                          <!-- 訂購產品 -->
                          <div class="col-lg-4">
                            <ul class="order-preview justify-content-end">
                              <li
                                v-if="orderDetails[order.orderId]"
                                v-for="detail in orderDetails[order.orderId]"
                                :key="detail.detailsId"
                              >
                                <a
                                  href="javascript:;"
                                  :title="detail.name"
                                  data-toggle="tooltip"
                                  data-placement="top"
                                  style="color: black"
                                >
                                  <img
                                    :src="`${IMG_URL}/${detail.imageFile}/${detail.imageFileName}`"
                                    alt="image" /><br
                                /></a>
                              </li>
                            </ul>
                          </div>
                          <!-- 訂單明細 -->
                          <div
                            class="col-lg-12"
                            v-if="selectedOrderId === order.orderId"
                          >
                            <ul class="nav nav-tabs" id="orderTabs">
                              <li class="nav-item">
                                <a
                                  class="nav-link active"
                                  id="paymentDetails-tab"
                                  data-toggle="tab"
                                  href="#paymentDetails"
                                  >付款資訊</a
                                >
                              </li>
                              <li class="nav-item">
                                <a
                                  class="nav-link"
                                  id="shippingDetails-tab"
                                  data-toggle="tab"
                                  href="#shippingDetails"
                                  >寄送資訊</a
                                >
                              </li>
                              <li class="nav-item">
                                <a
                                  class="nav-link"
                                  id="orderProductDetails-tab"
                                  data-toggle="tab"
                                  href="#orderProductDetails"
                                  >產品明細</a
                                >
                              </li>
                            </ul>

                            <div class="tab-content">
                              <div
                                class="tab-pane fade show active"
                                id="paymentDetails"
                              >
                                <ul class="order-details">
                                  <li>
                                    <strong>付款狀態：</strong
                                    >{{ order.paymentStatusName }}
                                  </li>
                                  <li>
                                    <strong>付款方式：</strong
                                    >{{ order.paymentName }}
                                  </li>
                                  <hr />
                                  <li>
                                    <strong>產品總額：</strong>
                                    {{ calculateTotalPrice(order.orderId) }} 元
                                  </li>
                                  <li>
                                    <strong>運費：</strong>
                                    {{ order.shippingCost }} 元
                                  </li>
                                  <li>
                                    <strong>總計：</strong
                                    >{{
                                      calculateTotalPrice(order.orderId) +
                                      order.shippingCost
                                    }}
                                    元
                                  </li>
                                </ul>
                              </div>
                              <div class="tab-pane fade" id="shippingDetails">
                                <ul class="order-details">
                                  <li>
                                    <strong>出貨狀態：</strong>
                                    {{ order.shippingStatusName }}
                                  </li>
                                  <hr />
                                  <li>
                                    <strong>會員姓名：</strong>
                                    {{ storeNav.memberInfo.name }}
                                  </li>
                                  <li>
                                    <strong>會員信箱：</strong>
                                    {{ storeNav.memberInfo.email }}
                                  </li>
                                  <li>
                                    <strong>會員電話：</strong>
                                    {{ storeNav.memberInfo.phone }}
                                  </li>
                                  <hr />
                                  <li>
                                    <strong>收件人姓名：</strong>
                                    {{ order.recipientName }}
                                  </li>
                                  <li>
                                    <strong>收件人地址：</strong>
                                    {{ order.recipientAddress }}
                                  </li>
                                  <li>
                                    <strong>帳單收件人姓名：</strong>
                                    {{ order.billRecipientName }}
                                  </li>
                                  <li>
                                    <strong>帳單收件人地址：</strong>
                                    {{ order.billRecipientAddress }}
                                  </li>
                                </ul>
                              </div>
                              <div
                                class="tab-pane fade"
                                id="orderProductDetails"
                              >
                                <ul
                                  class="order-details"
                                  v-if="orderDetails[order.orderId]"
                                  v-for="detail in orderDetails[order.orderId]"
                                  :key="detail.detailsId"
                                >
                                  <li>
                                    <strong>訂購產品：</strong>
                                    {{ detail.name }}
                                  </li>
                                  <li>
                                    <strong>產品單價：</strong>
                                    {{ detail.unitPrice }} 元
                                  </li>
                                  <li>
                                    <strong>訂購數量：</strong>
                                    {{ detail.quantity }}
                                  </li>
                                  <li>
                                    <strong>小計：</strong>
                                    {{ detail.subtotal }} 元
                                  </li>
                                </ul>
                              </div>
                            </div>

                            <button
                              class="close-btn"
                              @click="selectedOrderId = null"
                            >
                              &times;
                            </button>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <!-- pagination -->
                  <div class="row">
                    <div class="col">
                      <ul class="pagination">
                        <li
                          v-for="page in totalPages"
                          :key="page"
                          class="page-item"
                          :class="{ active: page === currentPage }"
                        >
                          <a
                            class="page-link"
                            href="#"
                            @click.prevent="setCurrentPage(page)"
                            >{{ page }}</a
                          >
                        </li>
                      </ul>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- / content -->
      </div>
    </div>
  </section>
  <!-- listing -->
</template>
<script>
const API_URL = import.meta.env.VITE_API_URL;
import { useCartStore, useNavbarStore } from "../store";
export default {
  components: {},
  data() {
    return {
      MVC_URL: import.meta.env.VITE_MVC_URL,
      IMG_URL: import.meta.env.VITE_IMAGE_URL,
      orders: [],
      orderDetails: [],
      ordersPerPage: 10,
      currentPage: 1,
      selectedOrderId: null,
      storeCart: useCartStore(),
      storeNav: useNavbarStore(),
    };
  },
  methods: {
    async fetchOrders() {
      try {
        const response = await fetch(
          `${API_URL}/Orders/${this.storeNav.memberId}`
        );
        const data = await response.json();
        console.log(data);
        this.orders = data;

        //迭代orders
        for (const order of this.orders) {
          await this.fetchOrderDetail(order.orderId);
        }
      } catch (error) {
        console.error("error fetching orders", error);
      }
    },

    async fetchOrderDetail(orderId) {
      try {
        const response = await fetch(`${API_URL}/OrderDetails/${orderId}`);
        const data = await response.json();
        console.log(data);
        this.orderDetails[orderId] = data;
      } catch (error) {
        console.error(error);
      }
    },
    setCurrentPage(page) {
      this.currentPage = page;
    },
    showOrderDetails(orderId) {
      this.selectedOrderId = orderId;
    },
    getOrderById(orderId) {
      return this.orders.find((order) => order.orderId === orderId);
    },
    calculateTotalPrice(orderId) {
      if (this.orderDetails[orderId]) {
        return this.orderDetails[orderId].reduce(
          (sum, detail) => sum + detail.subtotal,
          0
        );
      }
      return 0;
    },
  },
  computed: {
    totalPages() {
      return Math.ceil(this.orders.length / this.ordersPerPage);
    },
    paginatedOrders() {
      const startIndex = (this.currentPage - 1) * this.ordersPerPage;
      const endIndex = startIndex + this.ordersPerPage;
      return this.orders.slice(startIndex, endIndex);
    },
  },
  mounted() {
    //this.storeNav.getMemberIdFromCookie();
    this.fetchOrders();
    //this.storeNav.fetchMemberInfo();
    this.storeCart.fetchShippingMethods();
  },
};
</script>
<style>
.close-btn {
  position: absolute;
  top: 0;
  right: 0;
  padding: 5px 10px;
  background-color: grey; /* 添加背景颜色以突显关闭按钮 */
  border: 1px solid grey; /* 添加边框颜色 */
  color: white; /* 添加文本颜色 */
  cursor: pointer;
}
</style>
