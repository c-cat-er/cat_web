<template>
  <!-- footer -->
  <footer class="bg-dark text-white py-0">
    <div class="container">
      <div class="row separated">
        <div class="col-lg-6 py-10">
          <div class="row">
            <div class="col-md-8">
              <p>
                十九世紀後半開始，「南部背包」成為台灣外銷的驕傲。北部背包說的即是今日「台北大稻埕」一帶，在輝煌的全盛時期，當地聚集了約兩百多家背包店，而「ModPack」正是當中的代表。
              </p>
            </div>
          </div>
          <div class="row">
            <div class="col-md-10">
              <ul class="list-group list-group-columns">
                <li class="list-group-item">
                  <a :href="`${MVC_URL}/Home/AboutUs`">關於我們</a>
                </li>
                <li class="list-group-item">
                  <a :href="`${MVC_URL}/Home/ReachOut`">聯絡我們</a>
                </li>
                <li class="list-group-item">
                  <a :href="`${MVC_URL}/Home/FAQ`">FAQ</a>
                </li>
                <li class="list-group-item">
                  <a :href="`${MVC_URL}/Home/GoBlog`">部落格</a>
                </li>
                <li class="list-group-item">
                  <a :href="`${MVC_URL}/Home/PrivacyPolicy`">隱私權政策</a>
                </li>
                <li class="list-group-item">
                  <a :href="`${MVC_URL}/Home/StoreLocations`">門市位置</a>
                </li>
              </ul>
            </div>
          </div>
        </div>
        <div class="col-lg-6 py-10">
          <div class="row justify-content-end">
            <div class="col-lg-10">
              <h4 class="eyebrow mb-2">Subscribe</h4>
              <div class="input-group">
                <input
                  type="text"
                  class="form-control form-control-lg"
                  placeholder="Email"
                  aria-label="Subscribe"
                  aria-describedby="button-addon2"
                />
                <div class="input-group-append">
                  <button
                    class="btn btn-white"
                    type="button"
                    id="button-addon2"
                  >
                    Subscribe
                  </button>
                </div>
              </div>
            </div>
          </div>
          <div class="row justify-content-end">
            <div class="col-lg-10">
              <h4 class="eyebrow mb-2">Follow us</h4>
              <nav class="nav nav-icons">
                <a class="nav-link" href="#!"
                  ><i class="icon-facebook-o"></i
                ></a>
                <a class="nav-link" href="#!"><i class="icon-twitter-o"></i></a>
                <a class="nav-link" href="#!"><i class="icon-youtube-o"></i></a>
              </nav>
            </div>
          </div>
        </div>
      </div>
    </div>
  </footer>
</template>
<script>
export default {
  data() {
    return {
      MVC_URL: import.meta.env.VITE_MVC_URL,
    };
  },
};
</script>
<style></style>
