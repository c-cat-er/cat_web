<template>
  <div class="col">
    <div class="card text-center">
      <img :src="photo_path" class="card-img-top" />
      <div class="card-body">
        <h5 class="card-title">{{ name }}</h5>
        <p class="card-text">{{ price }}元</p>
        <button class="btn btn-primary">購買</button>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  props: ["name", "price", "photo_path"],
};
</script>
