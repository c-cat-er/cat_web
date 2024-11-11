<template>
  <div class="container shop">
    <div class="row">
      <h1 class="text-center m-3">nono商城</h1>
    </div>
    <div class="row">
      <Product v-for="p in products" :key="p.id" :price="p.price" :name="p.name" :photo_path="p.photo_path"></Product>
    </div>
  </div>
</template>
<script>
import Product from "../components/Product.vue";

export default {
  data() {
    return {
      products: [
        {
          id: "P001",
          name: "深色襯衫",
          price: 2500,
          photo_path: "/shirt.jpg",
        },
        {
          id: "P002",
          name: "深色牛仔褲",
          price: 1800,
          photo_path: "/jeans.jpg",
        },
        {
          id: "P003",
          name: "淺色t-shirt",
          price: 1000,
          photo_path: "/t-shirt.jpg",
        },
        {
          id: "P004",
          name: "淺色外套",
          price: 4000,
          photo_path: "/coat.jpg",
        },
        {
          id: "P005",
          name: "淺色拖鞋",
          price: 300,
          photo_path: "/slipper.jpg",
        },
      ],
    };
  },
  components: { Product },
};
</script>
<style scoped>
.shop {
  margin: 10px;
  padding: 5px;
  background-color: skyblue;
  border: 3px solid black;
  font-weight: bold;
}
</style>
