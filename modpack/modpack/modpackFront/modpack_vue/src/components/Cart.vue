<template>
  <!-- breadcrumbs -->
  <section class="breadcrumbs separator-bottom">
    <div class="container">
      <div class="row">
        <div class="col">
          <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
              <li class="breadcrumb-item"><a :href="`${MVC_URL}`">首頁</a></li>
              <li class="breadcrumb-item active">購物車</li>
            </ol>
          </nav>
        </div>
      </div>
    </div>
  </section>

  <!-- hero -->
  <section class="hero">
    <div class="container">
      <div class="row">
        <div class="col text-center">
          <h1>購物車</h1>
        </div>
      </div>
    </div>
  </section>

  <section class="pt-0">
    <div class="container">
      <div class="row mb-0 d-none d-lg-flex">
        <div class="col">
          <div class="row pr-6">
            <div class="col-lg-6"><span class="eyebrow">產品名稱</span></div>
            <div class="col-lg-2 text-center">
              <span class="eyebrow">單價</span>
            </div>
            <div class="col-lg-2 text-center">
              <span class="eyebrow">數量</span>
            </div>
            <div class="col-lg-2 text-center">
              <span class="eyebrow">小計</span>
            </div>
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col cart-item-list cart-item-list-minimal">
          <!-- cart item -->
          <div
            class="cart-item"
            v-for="item in storeCart.cartItems"
            :key="item.cartId"
          >
            <div class="row align-items-center">
              <div class="col-12 col-lg-6">
                <div class="media media-product">
                  <!-- 選擇框 -->
                  <input
                    type="checkbox"
                    class="cart-item-checkbox"
                    @change="toggleSelection(item)"
                    :checked="item.selected"
                  />
                  <a href="#!"
                    ><img
                      :src="`${IMG_URL}/${item.imageFile}/${item.imageFileName}`"
                      alt="Image"
                  /></a>
                  <div class="media-body">
                    <h5 class="media-title">{{ item.name }}</h5>
                  </div>
                </div>
              </div>
              <div class="col-4 col-lg-2 text-center">
                <span class="cart-item-price">{{ item.price }} 元</span>
              </div>
              <div class="col-4 col-lg-2 text-center">
                <div class="counter">
                  <!-- 產品數量-1 -->
                  <span
                    class="counter-minus icon-minus"
                    @click="decQuantity(item)"
                  ></span>
                  <!-- 產品數量修改 -->
                  <input
                    type="text"
                    name="qty-1"
                    class="counter-value"
                    v-model="item.quantity"
                    @input="updateQuantity(item)"
                    min="1"
                    max="10"
                  />
                  <!-- 產品數量+1 -->
                  <span
                    class="counter-plus icon-plus"
                    @click="incQuantity(item)"
                  ></span>
                </div>
              </div>
              <div class="col-4 col-lg-2 text-center">
                <span class="cart-item-price"
                  >{{ item.price * item.quantity }} 元</span
                >
              </div>
              <!-- 刪除產品按鈕 -->
              <a href="#!" class="cart-item-close" @click="deleteProduct(item)"
                ><i class="icon-x"></i
              ></a>
            </div>
          </div>
        </div>
      </div>
      <div class="row justify-content-between">
        <div class="col-md-6 col-lg-4">
          <div class="inline-block">
            <span class="eyebrow">總計</span>
            <h4 class="h2">{{ storeCart.getTotalPriceOfSelectedItems }}元</h4>
          </div>
        </div>
        <div class="col-md-6 col-lg-4">
          <!-- <div class="input-group">
            <input
              type="text"
              class="form-control"
              placeholder="Coupon Code"
              aria-label="Coupon"
            />
            <div class="input-group-append">
              <button type="button" class="btn btn-secondary btn-ico">
                <i class="icon-arrow-right"></i>
              </button>
            </div>
          </div> -->
          <button
            @click="isSelectedItemsEmpty"
            class="btn btn-lg btn-primary btn-block mt-1"
          >
            前往下單
          </button>
        </div>
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
      storeCart: useCartStore(),
      storeNav: useNavbarStore(),
    };
  },
  methods: {
    async deleteProduct(item) {
      await this.storeCart.deleteProduct(item);
      this.storeCart.fetchCartItems(this.storeNav.memberId);
    },
    incQuantity(item) {
      if (item.quantity < 10) {
        // 限制最大值為10，根據需求調整
        item.quantity++;
        this.updateCartQuantity(item);
      }
    },
    decQuantity(item) {
      if (item.quantity > 1) {
        // 限制最小值為1，根據需求調整
        item.quantity--;
        this.updateCartQuantity(item);
      }
    },
    async toggleSelection(item) {
      if (item.selected) {
        item.selected = false;
        this.storeCart.removeFromSelectedItems(item);
      } else {
        item.selected = true;
        this.storeCart.addToSelectedItems(item);
      }
    },
    async updateCartQuantity(item) {
      try {
        const response = await fetch(`${API_URL}/Carts/${item.cartId}`, {
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            cartId: item.cartId,
            quantity: item.quantity,
            imageFileName: item.imageFileName,
          }),
        });
        const result = await response.text();
        if (response.ok) {
          console.log(result);
        } else {
          console.log(result);
        }
      } catch (error) {
        console.error(error);
      }
    },
    isSelectedItemsEmpty() {
      if (this.storeCart.selectedItems.length > 0) {
        this.$router.push("/checkout");
      } else {
        Swal.fire("請選擇一樣商品");
      }
    },
  },
  mounted() {
    //this.storeNav.getMemberIdFromCookie();
    //this.storeNav.fetchMemberInfo();
    //this.storeCart.fetchCartItems(this.storeNav.memberId);
  },
};
</script>
<style></style>
