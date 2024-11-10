<template>
  <Navbar></Navbar>
  <router-view></router-view>
  <Footer></Footer>
</template>
<script>
import Navbar from "./components/Navbar.vue";
import Footer from "./components/Footer.vue";

export default {
  components: {
    Footer,
    Navbar,
  },
};
</script>
<style></style>
