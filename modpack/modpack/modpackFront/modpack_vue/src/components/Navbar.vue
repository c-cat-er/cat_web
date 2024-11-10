<template>
  <!-- header -->
  <header class="header header-dark bg-dark header-sticky">
    <div class="container">
      <div class="row">
        <!-- navbar -->
        <nav class="navbar navbar-expand-lg navbar-dark">
          <a :href="`${MVC_URL}`" class="navbar-brand order-1 order-lg-2"
            ><img :src="`${IMG_URL}/modpack_logo.svg`" alt="Logo"
          /></a>
          <button
            class="navbar-toggler order-2"
            type="button"
            data-toggle="collapse"
            data-target=".navbar-collapse"
            aria-controls="navbarMenu"
            aria-expanded="false"
            aria-label="Toggle navigation"
          >
            <span class="navbar-toggler-icon"></span>
          </button>

          <div
            class="collapse navbar-collapse order-3 order-lg-1"
            id="navbarMenu"
          >
            <ul class="navbar-nav mr-auto">
              <!-- 精選商品 -->
              <li class="nav-item">
                <router-link to="/product_listing" class="nav-link"
                  >精選商品</router-link
                >
              </li>
              <li class="nav-item">
                <router-link to="/custom_listing" class="nav-link"
                  >客製化背包</router-link
                >
              </li>
              <li class="nav-item">
                <a :href="`${MVC_URL}/Home/TestService/1`" class="nav-link"
                  >客服</a
                >
              </li>
            </ul>
          </div>

          <div
            class="collapse navbar-collapse order-4 order-lg-3"
            id="navbarMenu2"
          >
            <ul class="navbar-nav ml-auto">
              <!-- 登入 -->
              <li class="nav-item">
                <a :href="`${MVC_URL}/Authentication/Logout`" class="nav-link"
                  >登出</a
                >
              </li>
              <!-- 會員資料 -->
              <li class="nav-item dropdown">
                <a
                  class="nav-link dropdown-toggle"
                  href="#!"
                  id="navbarDropdown-10"
                  role="button"
                  data-toggle="dropdown"
                  aria-haspopup="true"
                  aria-expanded="false"
                  >{{ storeNav.memberInfo.name }}
                </a>
                <ul class="dropdown-menu" aria-labelledby="navbarDropdown-10">
                  <li>
                    <a
                      class="dropdown-item"
                      :href="`${MVC_URL}/UserProfile/Profile`"
                      >基本資料</a
                    >
                  </li>
                  <li>
                    <router-link to="/profile_order" class="dropdown-item"
                      >訂單</router-link
                    >
                  </li>
                </ul>
              </li>
              <!-- 搜尋框 -->
              <li class="nav-item">
                <a data-toggle="modal" data-target="#search" class="nav-link"
                  ><i class="icon-search"></i
                ></a>
              </li>
              <!-- 購物車 -->
              <li class="nav-item cart">
                <a data-toggle="modal" data-target="#cart" class="nav-link"
                  ><span>購物車</span
                  ><span style="background-color: red; color: white"
                    ><strong>{{ storeCart.cartItems.length }}</strong></span
                  ></a
                >
              </li>
            </ul>
          </div>
        </nav>
      </div>
    </div>
  </header>
  <!-- 購物車 -->
  <div
    class="modal fade sidebar"
    id="cart"
    tabindex="-1"
    role="dialog"
    aria-labelledby="cartLabel"
    aria-hidden="true"
  >
    <div class="modal-dialog" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="cartLabel">購物車</h5>
          <button
            type="button"
            class="close"
            data-dismiss="modal"
            aria-label="Close"
          >
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <div class="row gutter-3" v-for="item in storeCart.cartItems">
            <div class="col-12">
              <div class="cart-item cart-item-sm">
                <div class="row align-items-center">
                  <div class="col-lg-9">
                    <div class="media media-product">
                      <a href="#!"
                        ><img
                          :src="`${IMG_URL}/${item.imageFile}/${item.imageFileName}`"
                          alt="Image"
                      /></a>
                      <div class="media-body">
                        <h5 class="media-title mb-1">
                          {{ item.name }}
                        </h5>
                        <h5 class="media-title mb-1">
                          單價：{{ item.price }}元
                        </h5>
                        <h5 class="media-title mb-1">
                          數量：{{ item.quantity }}
                        </h5>
                        <h5 class="media-title mb-1">
                          小計：
                          {{ item.price * item.quantity }}元
                        </h5>
                      </div>
                    </div>
                  </div>
                  <div class="col-lg-3 text-center text-lg-right">
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
                  <a
                    href="#!"
                    class="cart-item-close"
                    @click="this.deleteProduct(item)"
                    ><i class="icon-x"></i
                  ></a>
                </div>
              </div>
            </div>
          </div>
          <div>總計：{{ storeCart.getTotalPrice }}元</div>
        </div>
        <div class="modal-footer">
          <div class="container-fluid">
            <div class="row gutter-0">
              <div class="col d-none d-md-block">
                <router-link
                  to="/cart"
                  class="btn btn-lg btn-block btn-secondary"
                  >檢視購物車</router-link
                >
              </div>
              <div class="col-3">
                <button
                  @click="this.clearCarts(this.storeNav.memberId)"
                  class="btn btn-lg btn-block btn-danger"
                >
                  清空
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>

  <!-- 搜尋框 -->
  <div
    class="modal fade search"
    id="search"
    tabindex="-1"
    role="dialog"
    aria-hidden="true"
    data-backdrop="static"
  >
    <div class="modal-dialog" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <input
            type="text"
            class="form-control"
            placeholder="請輸入您要搜尋的內容"
            aria-label="請輸入您要搜尋的內容"
            aria-describedby="button-addon2"
            v-model="searchText"
            @input="fetchFilteredProducts"
          />
          <button
            type="button"
            class="close"
            data-dismiss="modal"
            aria-label="Close"
            @click="(filteredProducts = []), (searchText = '')"
          >
            <span aria-hidden="true">&times;</span>
          </button>
          <div
            class="dropdown-menu"
            style="display: block"
            aria-labelledby="dropdownMenuButton"
            v-if="filteredProducts.length > 0"
          >
            <a
              class="dropdown-item"
              href="#"
              v-for="product in filteredProducts"
              :key="product.ProductId"
              @click="selectProduct(product.ProductId)"
            >
              {{ product.Name }}
              <!-- 显示产品的名称 -->
            </a>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
const API_URL = import.meta.env.VITE_API_URL;
const MVC_URL = import.meta.env.VITE_MVC_URL;
import { useCartStore, useNavbarStore } from "../store";
export default {
  data() {
    return {
      MVC_URL: import.meta.env.VITE_MVC_URL,
      IMG_URL: import.meta.env.VITE_IMAGE_URL,
      storeNav: useNavbarStore(),
      storeCart: useCartStore(),
      searchText: "",
      filteredProducts: [],
    };
  },
  methods: {
    async clearCarts(memberId) {
      await this.storeCart.clearCarts(memberId);
      this.storeCart.fetchCartItems(memberId);
    },
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
    async fetchFilteredProducts() {
      if (this.searchText.trim() != "") {
        try {
          const response = await fetch(
            `${API_URL}/Products/Key/${this.searchText}`,
            {
              method: "GET",
            }
          );
          if (response.ok) {
            const data = await response.json();
            console.log(data);
            this.filteredProducts = data;
          } else {
            console.log("Failed to fetch filtered products");
          }
        } catch (err) {
          console.error("Error fetching filtered products:", err);
        }
      }
    },
    async selectProduct(productId) {
      window.location.href = `${MVC_URL}/ProductPage/ProductsDetail/${productId}`;
    },
  },
  async mounted() {
    await new Promise((resolve) => setTimeout(resolve, 1000));
    await this.storeNav.getMemberIdFromCookie();
    await this.storeNav.fetchMemberInfo();
    this.storeCart.fetchCartItems(this.storeNav.memberId);
  },
};
</script>
<style></style>
