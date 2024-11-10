<template>
  <!-- breadcrumbs -->
  <section class="breadcrumbs separator-bottom">
    <div class="container">
      <div class="row">
        <div class="col">
          <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
              <li class="breadcrumb-item"><a :href="`${MVC_URL}`">主頁</a></li>
              <li class="breadcrumb-item active" aria-current="page">
                精選商品
              </li>
            </ol>
          </nav>
        </div>
      </div>
    </div>
  </section>
  <!-- listing -->
  <section class="pt-6">
    <div class="container">
      <div class="row justify-content-end">
        <div class="col-lg-9">
          <div class="row gutter-2 align-items-end">
            <div class="col-md-6">
              <h1 class="mb-0">精選商品</h1>
              <span class="eyebrow">{{ products.length }} products</span>
            </div>
            <div class="col-md-6 text-md-right">
              <div class="dropdown">
                <a
                  class="btn btn-outline-secondary btn-sm dropdown-toggle"
                  href="#!"
                  role="button"
                  id="dropdownMenuLink"
                  data-toggle="dropdown"
                  aria-haspopup="true"
                  aria-expanded="false"
                >
                  排序
                </a>

                <div class="dropdown-menu" aria-labelledby="dropdownMenuLink">
                  <a
                    class="dropdown-item"
                    @click="setSortOption('default')"
                    href="#!"
                    >預設排序</a
                  >
                  <a
                    class="dropdown-item"
                    @click="setSortOption('highToLow')"
                    href="#!"
                    >價格由高至低</a
                  >
                  <a
                    class="dropdown-item"
                    @click="setSortOption('lowToHigh')"
                    href="#!"
                    >價格由低至高</a
                  >
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

      <div class="row gutter-4">
        <!-- sidebar -->
        <aside class="col-lg-3 sidebar">
          <!-- 搜尋框 -->
          <!-- <div class="widget">
            <span
              class="widget-collapse d-lg-none"
              data-toggle="collapse"
              data-target="#collapse-search"
              aria-expanded="false"
              aria-controls="collapse-search"
              role="button"
            >
              搜尋
            </span>
            <div class="d-lg-block collapse" id="collapse-search">
              <span class="widget-title">搜尋</span>
              <div class="widget-content"> -->
          <!-- 搜尋表單 -->
          <!-- <div class="input-group">
                  <input
                    type="text"
                    class="form-control"
                    placeholder="輸入關鍵字"
                    v-model="searchText"
                    @input="fetchFilteredProducts"
                  />
                </div> -->
          <!-- 下拉菜单 -->
          <!-- <ul v-if="filteredProducts.length > 0" class="dropdown-menu">
                  <li
                    v-for="product in filteredProducts"
                    :key="product.ProductId"
                  >
                    {{ product.Name }}
                  </li>
                </ul> 
              </div>
            </div>
          </div> -->
          <!-- 品牌 -->
          <div class="widget">
            <span
              class="widget-collapse d-lg-none"
              data-toggle="collapse"
              data-target="#collapse-brand"
              aria-expanded="false"
              aria-controls="collapse-brand"
              role="button"
            >
              按品牌篩選
            </span>
            <div class="d-lg-block collapse" id="collapse-brand">
              <span class="widget-title">品牌</span>
              <div class="widget-content">
                <div class="custom-control custom-checkbox">
                  <input
                    type="checkbox"
                    class="custom-control-input"
                    id="customCheck1"
                    v-model="selectedBrands"
                    value="NIKE"
                  />
                  <label class="custom-control-label" for="customCheck1"
                    >NIKE</label
                  >
                </div>
                <div class="custom-control custom-checkbox">
                  <input
                    type="checkbox"
                    class="custom-control-input"
                    id="customCheck2"
                    v-model="selectedBrands"
                    value="The North Face"
                  />
                  <label class="custom-control-label" for="customCheck2"
                    >The North Face</label
                  >
                </div>
                <div class="custom-control custom-checkbox">
                  <input
                    type="checkbox"
                    class="custom-control-input"
                    id="customCheck3"
                    v-model="selectedBrands"
                    value="Arcteryx 始祖鳥"
                  />
                  <label class="custom-control-label" for="customCheck3"
                    >Arcteryx 始祖鳥</label
                  >
                </div>
                <div class="custom-control custom-checkbox">
                  <input
                    type="checkbox"
                    class="custom-control-input"
                    id="customCheck4"
                    v-model="selectedBrands"
                    value="PORTER INTERNATIONAL"
                  />
                  <label class="custom-control-label" for="customCheck4"
                    >PORTER INTERNATIONAL</label
                  >
                </div>
                <div class="custom-control custom-checkbox">
                  <input
                    type="checkbox"
                    class="custom-control-input"
                    id="customCheck5"
                    v-model="selectedBrands"
                    value="Samsonite 新秀麗"
                  />
                  <label class="custom-control-label" for="customCheck5"
                    >Samsonite 新秀麗
                  </label>
                </div>
              </div>
            </div>
          </div>
          <!-- 類別 -->
          <div class="widget">
            <span
              class="widget-collapse d-lg-none"
              data-toggle="collapse"
              data-target="#collapse-category"
              aria-expanded="false"
              aria-controls="collapse-category"
              role="button"
            >
              按類別篩選
            </span>
            <div class="d-lg-block collapse" id="collapse-category">
              <span class="widget-title">類別</span>
              <div class="widget-content">
                <div class="custom-control custom-checkbox">
                  <input
                    type="checkbox"
                    class="custom-control-input"
                    id="customCheckCategory1"
                    v-model="selectedCategory"
                    value="後背包"
                  />
                  <label class="custom-control-label" for="customCheckCategory1"
                    >後背包</label
                  >
                </div>
                <div class="custom-control custom-checkbox">
                  <input
                    type="checkbox"
                    class="custom-control-input"
                    id="customCheckCategory2"
                    v-model="selectedCategory"
                    value="旅行包"
                  />
                  <label class="custom-control-label" for="customCheckCategory2"
                    >旅行包</label
                  >
                </div>
                <div class="custom-control custom-checkbox">
                  <input
                    type="checkbox"
                    class="custom-control-input"
                    id="customCheckCategory3"
                    v-model="selectedCategory"
                    value="公事包"
                  />
                  <label class="custom-control-label" for="customCheckCategory3"
                    >公事包</label
                  >
                </div>
                <div class="custom-control custom-checkbox">
                  <input
                    type="checkbox"
                    class="custom-control-input"
                    id="customCheckCategory4"
                    v-model="selectedCategory"
                    value="斜肩包"
                  />
                  <label class="custom-control-label" for="customCheckCategory4"
                    >斜肩包</label
                  >
                </div>
              </div>
            </div>
          </div>
          <!-- 價格 -->
          <div class="widget">
            <span
              class="widget-collapse d-lg-none"
              data-toggle="collapse"
              data-target="#collapse-price"
              aria-expanded="false"
              aria-controls="collapse-price"
              role="button"
            >
              按價格篩選
            </span>
            <div class="d-lg-block collapse" id="collapse-price">
              <span class="widget-title">價格</span>
              <div class="widget-content mt-5 col-10">
                <Slider
                  class="styleBlue"
                  v-model="value"
                  :min="0"
                  :max="10000"
                  :format="format"
                ></Slider>
              </div>
            </div>
          </div>
        </aside>

        <!-- content -->
        <div class="col-lg-9">
          <div class="row gutter-2 gutter-lg-3">
            <div
              class="col-6 col-md-4"
              v-for="product in paginatedProducts"
              :key="product.ProductId"
            >
              <div class="product">
                <figure class="product-image">
                  <a
                    :href="`${MVC_URL}/ProductPage/Productsdetail/${product.ProductId}`"
                  >
                    <img
                      :src="`${IMG_URL}/product/${product.ImageFileName}`"
                      alt="Image"
                      style="display: block; margin: 0 auto"
                    />
                  </a>
                </figure>
                <div
                  class="product-meta d-flex justify-content-center align-items-center flex-column row"
                  style="padding-right: 0"
                >
                  <h3 class="product-title col-12 text-center">
                    <a href="#!">{{ product.Name }} </a>
                  </h3>
                  <div
                    class="product-price d-flex align-item-center col-12 row text-center"
                  >
                    <span
                      style="
                        width: 100%;
                        background-color: rgb(69, 68, 68);
                        color: azure;
                      "
                      class="col-12 d-flex justify-content-center"
                      >{{ product.SalePrice }}元</span
                    >
                    <span
                      class="product-action col-12 d-flex justify-content-center"
                      style="margin-left: 0"
                    >
                      <a @click="addProductToCart(product)" href="#!"
                        >加入購物車</a
                      >
                    </span>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="row">
            <div class="col">
              <nav class="d-inline-block">
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
              </nav>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>
<script>
const API_URL = import.meta.env.VITE_API_URL;
import Slider from "@vueform/slider";
import Swal from "sweetalert2";
import { useCartStore, useNavbarStore } from "../store";
export default {
  components: {
    Slider,
  },
  data() {
    return {
      MVC_URL: import.meta.env.VITE_MVC_URL,
      IMG_URL: import.meta.env.VITE_IMAGE_URL,
      products: [],
      productsPerPage: 9,
      currentPage: 1,
      selectedBrands: [],
      selectedFunctions: [],
      selectedCategory: [],
      sortOption: "default",
      value: [0, 10000],
      format: {
        prefix: "$",
        decimals: 2,
      },
      storeCart: useCartStore(),
      storeNav: useNavbarStore(),
    };
  },
  methods: {
    async fetchProducts() {
      try {
        const response = await fetch(`${API_URL}/Products`, {
          method: "GET",
        });

        if (response.ok) {
          const data = await response.json();
          //this.products = data.filter((product) => product.Category === "主包");
          this.products = data;
          console.log(data);
        } else {
          console.log("Failed to fetch products");
        }
      } catch (err) {}
    },
    async addProductToCart(product) {
      try {
        const existingCartItem = this.storeCart.cartItems.find(
          (item) => item.productId === product.ProductId
        );
        if (existingCartItem) {
          const response = await fetch(
            `${API_URL}/Carts/${existingCartItem.cartId}`,
            {
              method: "PUT",
              headers: {
                "Content-Type": "application/json",
              },
              body: JSON.stringify({
                cartId: existingCartItem.cartId,
                quantity: existingCartItem.quantity + 1,
                imageFileName: existingCartItem.imageFileName,
              }),
            }
          );
          if (response.ok) {
            Swal.fire("修改數量成功");
            this.storeCart.fetchCartItems(this.storeNav.memberId);
          } else {
            Swal.fire("修改數量失敗");
          }
        } else {
          const response = await fetch(`${API_URL}/Carts`, {
            method: "POST",
            headers: {
              "Content-Type": "application/json",
            },
            body: JSON.stringify({
              memberId: this.storeNav.memberId,
              quantity: 1,
              productId: product.ProductId,
            }),
          });
          if (response.ok) {
            this.storeCart.fetchCartItems(this.storeNav.memberId);
            Swal.fire("成功加入購物車");
          } else {
            Swal.fire("加入購物車失敗");
          }
        }
      } catch (error) {
        console.log(error);
      }
    },
    setSortOption(option) {
      this.sortOption = option;
    },
    setCurrentPage(page) {
      this.currentPage = page;
    },
  },
  computed: {
    totalPages() {
      return Math.ceil(this.products.length / this.productsPerPage);
    },
    paginatedProducts() {
      const startIndex = (this.currentPage - 1) * this.productsPerPage;
      const endIndex = startIndex + this.productsPerPage;

      //篩選品牌
      const filteredByBrands = this.products.filter((product) => {
        return (
          this.selectedBrands.length === 0 ||
          this.selectedBrands.some((name) => product.Name.includes(name))
        );
      });

      //篩選類別
      const filteredByCategory = filteredByBrands.filter((product) => {
        return (
          this.selectedCategory.length === 0 ||
          this.selectedCategory.includes(product.Category)
        );
      });
      // 篩選價格
      const filteredByPrice = filteredByCategory.filter((product) => {
        const productPrice = parseFloat(product.SalePrice);
        return productPrice >= this.value[0] && productPrice <= this.value[1];
      });

      //return this.products.slice(startIndex, endIndex);

      //排序價格
      const sortedProducts = [...filteredByPrice]; // 使用新的數組以防止修改原始數據
      if (this.sortOption === "highToLow") {
        sortedProducts.sort(
          (a, b) => parseFloat(b.SalePrice) - parseFloat(a.SalePrice)
        );
      } else if (this.sortOption === "lowToHigh") {
        sortedProducts.sort(
          (a, b) => parseFloat(a.SalePrice) - parseFloat(b.SalePrice)
        );
      }

      return sortedProducts.slice(startIndex, endIndex);
    },
  },
  mounted() {
    //this.storeNav.getMemberIdFromCookie();
    this.fetchProducts();
  },
};
</script>
<style src="@vueform/slider/themes/default.css"></style>
<style>
.styleBlue {
  --slider-connect-bg: #013a80;
  --slider-tooltip-bg: #013a80;
  --slider-handle-ring-color: #013a80;
}
</style>
