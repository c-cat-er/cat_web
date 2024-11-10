<template>
    <v-footer app>
        <v-container>
            <v-row class="footer-section justify-content-around">
                <v-col cols="auto" md="3" class="footer-list">
                    <h5>關於我們</h5>
                    <ul>
                        <li>Niseko 民宿</li>
                        <li>電話 | 111111111 ??</li>
                        <li>電郵 | <a href="mailto:lesson@chaseforsnow.com">lesson@chaseforsnow.com ??</a></li>
                        <li>地址 | 日本縣 ??</li>
                        <li>服務時間週一至週五 09:00-18:00 ??</li>
                        <li><a href="#">常見問答 <span class="external-link">🔗</span></a></li>
                        <li><a href="#">聯絡我們 <span class="external-link">🔗</span></a></li>
                        <li><a href="#">特價訂閱 <span class="external-link">🔗</span></a></li>
                    </ul>
                </v-col>
                <v-col cols="auto" md="3" class="footer-list">
                    <h5>實用資訊</h5>
                    <ul>
                        <li><a href="#">日本天氣 <span class="external-link">🔗</span></a></li>
                        <li><a href="#">日本環境 <span class="external-link">🔗</span></a></li>
                        <li><a href="#">遊日攻略 <span class="external-link">🔗</span></a></li>
                    </ul>
                </v-col>
                <v-col cols="auto" md="3" class="footer-list">
                    <h5>合作夥伴</h5>
                    <ul>
                        <li><a href="#">夥伴中心 <span class="external-link">🔗</span></a></li>
                        <li><a href="#">支援中心 <span class="external-link">🔗</span></a></li>
                        <li><a href="#">爭議中心 <span class="external-link">🔗</span></a></li>
                    </ul>
                </v-col>
                <v-col cols="auto" md="3" class="footer-list">
                    <h5>隱私&政策</h5>
                    <ul>
                        <li><a href="#">著作權 <span class="external-link">🔗</span></a></li>
                        <li><a href="#">隱私權 <span class="external-link">🔗</span></a></li>
                        <li><a href="#">條款與細則 <span class="external-link">🔗</span></a></li>
                        <li><a href="#">Cookie 政策 <span class="external-link">🔗</span></a></li>
                        <li><a href="#">法律聲明 <span class="external-link">🔗</span></a></li>
                    </ul>
                </v-col>
                <v-col cols="auto" md="3" class="footer-list">
                    <h5>網站相關</h5>
                    <ul>
                        <li><a href="#">🌍語言切換(點跳視窗) <span class="external-link">🔗</span></a></li>
                        <li><a href="#">地圖導覽 <span class="external-link">🔗</span></a></li>
                        <li><a href="#">🎧使用幫助 <span class="external-link">🔗</span></a></li>
                        <li><a href="#">意見箱 <span class="external-link">🔗</span></a></li>
                        <li><a href="#">網站維護 <span class="external-link">🔗</span></a></li>
                        <li><a href="#">系統維護 <span class="external-link">🔗</span></a></li>
                    </ul>
                    <div aria-label-zh="下載App" aria-label-en="??" aria-label-jp="??">下載App</div>
                </v-col>
            </v-row>
            <v-row class="footer-rights">
                <v-col>
                    Copyright © 2024 by Niseko - <a href="#">條款及細則 Terms & Conditions</a>
                </v-col>
            </v-row>
        </v-container>
    </v-footer>
</template>

<script setup>
</script>

<style scoped>
    footer-section {
        font-size: 12px;
        width: 100%;
        height: auto;
        background-color: cornflowerblue;
        position: relative; /*fixed*/
        bottom: 0;
    }

    .footer-rights {
        text-align: center;
    }
</style>
