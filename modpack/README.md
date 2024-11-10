# 期末專題作品-客製化背包電商網站 modpack

[本人 LINE](https://line.me/ti/p/0P9CIyIVhD)

## 介紹

- 作者：我+3 人
- 時間：2024-01 ~ 2024-03 開發的
- [圖片瀏覽](https://github.com/c-cat-er/ModPack/tree/main/images)
- 影片預覽：

1. [-1](https://youtube.com/clip/UgkxlgAw5g9jDJqhSY6UffTJp1DeR1SfrYl8?si=N_eIJupCFEzPkbLl)
2. [-2](https://youtube.com/clip/UgkxKdq9HAPkYTYCaAron5E3FRTEyuzmal-P?si=lptl0Bh_sbRphCKk)
3. [-3](https://youtube.com/clip/UgkxufyE_C1COCw1fIsYwZ8zbPY4nDqpQZpe?si=X-y9RUBYOBQonTCP)
4. [-4](https://youtube.com/clip/UgkxfSzs0K9cu64nsxCUk7AYbJe4t_wU9KTa?si=QIvGfPK7TLk-VQOm)
5. [-5](https://youtube.com/clip/UgkxjajytInGkyi_TV6-wtrm2DpWDuPD6At5?si=FB0ONMIF1jCGyGFz)

- 所用語言：C#、LINQ。
- 所用框架：.NET Core Web API + .NET Core Web MVC + Vuejs。
- 所用資料庫：MSSQL。
- 備註：前一份專案版本，內 url 皆為 host 自訂之名，故需先行變更 C:\Windows\System32\drivers\etc\hosts 的 # 127.0.0.1 和 # ::1 為 modpack.com 方可順利啟用。此版本已修復回，可直接啟動。
- 啟用帳密：

1. 前台 A:test4 P:1234。
2. 後台 A:mac P:1111。

## 我負責部分

- 專管

1. 使用 Draw.io、Git/Github/GitHub-Desktop/Git-Bash、UML。
2. 參與 MS-SQL Server 資料表設計、建置與正規化。

- modpack 後台

1. AdminUser 員工後台登入所有相關<br>
   1.1. 基本登入驗證，和使用 Session 緩存登入狀態。(含 JWT-api)
2. AdminEmployeeInfo 員工資料管理所有相關<br>
   2.1. 使用 Linq 設置僅可瀏覽權限較小之各員工資料，和使用 PageList 做分頁的 CRUD 員工表+職位表資訊。<br>
   2.2. PageList 修改中。
3. AdminImageCarousel 首頁圖片管理所有相關<br>
   3.1. 顯示複合表格，和使用 SignalR & CRUD 管理前台首頁圖片。
4. AdminServiceRecords 所有客服相關<br>
   4.1. 顯示單表格，和使用 SignalR 及時顯示前台提問表單送出的信息，使用 Partial View 更新表格，和即時回覆信息給前端會員信息窗口。
5. AdminDataAnlysis 數據分析所有相關<br>
   5.1. 顯示複合 EF 表，和使用 ajax & fetch api & json & Chart.js 和使用 Partial View 來顯示與不刷新渲染 bar & line 表格和圖。
6. StoreLocationsDTO 門市管理所有相關<br>
   6.1. 基本 RestfulAPI CRUD。

- modpackApi

1. JWT Token API
2. RESTful API
3. StoreLocationsDTO API

- modpackFront 前台

1. HomeController 首頁部分相關<br>
   1.1. Home 首頁*輪播圖：使用 SignalR 接收後端通知來即時更新圖片和文字。<br>
   1.2. 首頁*產品列表(加載更多)：使用 Fetch-API & json 動態添加可設置數量的產品列表。 <br>
   1.3. Service 即時客服頁：使用 SignalR 即時傳信息到後台。

# 或要看其他專案

- [WinForm playfood 連結 1](https://github.com/c-cat-er/playfood)
- [WinForm playfood 連結 2](https://github.com/c-cat-er/playfood)
