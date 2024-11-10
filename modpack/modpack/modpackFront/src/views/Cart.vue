<template>
  <div class="container cart">
    <div class="row"><h1 class="text-center m-3">我的購物車</h1></div>
    <h4 class="text-center">[購物車]:{{ total }}元</h4>
    <table class="table text-center align-middle">
      <thead>
        <tr>
          <td>名稱</td>
          <td>價格</td>
          <td>圖片</td>
          <td>移除商品</td>
        </tr>
      </thead>
      <tbody>
        <tr v-for="p in cartProducts">
          <td>{{ p.name }}</td>
          <td>{{ p.price }}</td>
          <td>
            <img :src="p.photo_path" style="width: 100px" />
          </td>
          <td>
            <button @click="removeFromCart(p)">移除商品</button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>
<script>
export default {
  data() {
    return {
      cartProducts: [
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
      total: 100,
    };
  },
  methods: {
    removeFromCart(product) {
      this.cartProducts.splice(this.cartProducts.indexOf(product), 1);
    },
  },
};
</script>
<style>
.cart {
  margin: 10px;
  padding: 5px;
  background-color: burlywood;
  border: 3px solid black;
  font-weight: bold;
}
</style>
