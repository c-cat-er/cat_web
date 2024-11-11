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
                客製化背包
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
      <div class="row justify-content">
        <div class="col-lg-12">
          <div class="row gutter-2 align-items-end">
            <div class="col-md-6">
              <h1 class="mb-0">靈感商品</h1>
              <span class="eyebrow">{{ inspirations.length }} products</span>
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
        <!-- content -->
        <div class="col-lg-12">
          <div class="row gutter-2 gutter-lg-3">
            <div
              class="col-6 col-md-2"
              v-for="inspiration in paginatedInspirations"
              :key="inspiration.InspirationId"
            >
              <div class="product">
                <figure class="product-image">
                  <a
                    :href="`${MVC_URL}/ProductPage/Customizeddetail/${inspiration.InspirationId}`"
                  >
                    <img
                      :src="`${IMG_URL}/Inspiration/${inspiration.ImageFileName}`"
                      alt="Image"
                    />
                  </a>
                </figure>
                <div class="product-meta">
                  <h3 class="product-title">
                    <a href="#!">{{ inspiration.Name }} </a>
                  </h3>
                  <div class="product-price">
                    <span>{{ inspiration.SalePrice }}元</span>
                    <span class="product-action">
                      <a @click="addInspirationToCart(inspiration)" href="#!"
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
import { useCartStore, useNavbarStore } from "../store";
import Swal from "sweetalert2";
export default {
  components: {},
  data() {
    return {
      MVC_URL: import.meta.env.VITE_MVC_URL,
      IMG_URL: import.meta.env.VITE_IMAGE_URL,
      sortOption: "default",
      inspirationsPerPage: 9,
      currentPage: 1,
      inspirations: [],
      storeCart: useCartStore(),
      storeNav: useNavbarStore(),
    };
  },
  methods: {
    async fetchInspirations() {
      try {
        const response = await fetch(`${API_URL}/Inspirations`, {
          method: "GET",
        });

        if (response.ok) {
          const data = await response.json();
          this.inspirations = data;

          console.log(data);
        } else {
          console.log("Failed to fetch inspirations");
        }
      } catch (err) {
        console.log(err);
      }
    },
    setSortOption(option) {
      this.sortOption = option;
    },
    setCurrentPage(page) {
      this.currentPage = page;
    },
    async addInspirationToCart(inspiration) {
      try {
        const response = await fetch(`${API_URL}/Carts`, {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            memberId: this.storeNav.memberId,
            quantity: 1,
            inspirationId: inspiration.InspirationId,
          }),
        });
        if (response.ok) {
          this.storeCart.fetchCartItems(this.storeNav.memberId);
          Swal.fire("成功加入購物車");
        } else {
          Swal.fire("加入購物車失敗");
        }
      } catch (error) {
        console.log(error);
      }
    },
  },
  computed: {
    totalPages() {
      return Math.ceil(this.inspirations.length / this.inspirationsPerPage);
    },
    paginatedInspirations() {
      const startIndex = (this.currentPage - 1) * this.inspirationsPerPage;
      const endIndex = startIndex + this.inspirationsPerPage;

      if (this.sortOption === "highToLow") {
        this.inspirations.sort(
          (a, b) => parseFloat(b.SalePrice) - parseFloat(a.SalePrice)
        );
      } else if (this.sortOption === "lowToHigh") {
        this.inspirations.sort(
          (a, b) => parseFloat(a.SalePrice) - parseFloat(b.SalePrice)
        );
      }

      return this.inspirations.slice(startIndex, endIndex);
    },
  },
  mounted() {
    //this.storeNav.getMemberIdFromCookie();
    this.fetchInspirations();
  },
};
</script>
<style></style>
