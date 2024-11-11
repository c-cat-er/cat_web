//@/stores/ShoppingCartItemsStore.js
import { defineStore } from 'pinia';
import axiosInstance from '@/constants/axios';
import Cookies from 'js-cookie';
import { jwtDecode } from 'jwt-decode';

export const shoppingCartStore = defineStore('shoppingCart', {
    state: () => ({
        cartItems: [],
        selectedItems: [], //存到 Pinia 中
        FInitialAmount: 0,
        //discount: 0,
        //tax: 0,
        loading: false,
        error: null,
    }),
    actions: {
        async fetchCart() {
            this.loading = true;
            try {
                //获取当前登录会员的 ID
                const token = Cookies.get('token');
                const decodedToken = jwtDecode(token);
                const memberId = decodedToken.FMemberID;
                //console.log("memberId", memberId);
                const response = await axiosInstance.get(`/api/MemberShoppingCart/homestay/memberId/${memberId}`);
                //console.log('API response:', response.data);

                /* API 返回的數據中不包含房間名稱和房間代碼，需再另外 for 請求
                // 初始化 cartItems
                this.cartItems = response.data.$values.map(item => ({
                    ...item,
                    selected: false, // 初始化選中狀態
                    FHomestayName: '',  // 将要添加的民宿名称
                    FRoomCode: '',      // 将要添加的房间编号
                }));
                // 遍历每个购物车项目，根据产品ID (FProductID) 获取房间详情
                for (let item of this.cartItems) {
                    if (item.FProductType === 'H') { // 如果是民宿类型
                        const roomResponse = await axiosInstance.get(`/api/ProductHomestayRoom/${item.FProductID}`);
                        console.log('API roomResponse:', roomResponse.data);
                        const roomData = roomResponse.data;

                        // 檢查是否有房間資料，然後更新购物车项目中的民宿名称和房间编号
                        if (roomData.ProductHomestayRoomDTO.$values.length > 0) {
                            item.FHomestayName = roomData.FHomestayName;
                            item.FRoomCode = roomData.ProductHomestayRoomDTO.$values[0].FRoomCode;
                        }
                    }
                } */

                // API 返回的數據中已經包含房間名稱和房間代碼
                this.cartItems = response.data.$values.map(item => ({
                    ...item,
                    selected: false // 初始化選中狀態
                }));

                this.calculateInitialAmount();
            } catch (err) {
                this.error = err.message;
                console.error('Error fetching cart items:', err);
                this.cartItems = [];
            } finally {
                this.loading = false;
            }
        },
        calculateInitialAmount() {
            this.FInitialAmount = this.selectedItems.reduce((total, item) => total + item.FPrice, 0); //item.FPrice*item.quantity
        },
        selectItem(item) {
            if (item.selected) {
                // 如果選中，則添加到 selectedItems 中
                this.selectedItems.push(item);
            } else {
                // 如果取消選中，則從 selectedItems 中移除
                const index = this.selectedItems.findIndex(selectedItem => selectedItem.FShoppingCartID === item.FShoppingCartID);
                if (index !== -1) {
                    this.selectedItems.splice(index, 1);
                }
            }
            this.calculateInitialAmount();
        },
        async removeFromCart(shoppingCartID) {
            this.loading = true;
            try {
                //console.log("shoppingCartID", shoppingCartID);
                await axiosInstance.delete(`/api/MemberShoppingCart/homestay/shoppingCartId/${shoppingCartID}`);
                this.cartItems = this.cartItems.filter(item => item.FShoppingCartID !== shoppingCartID);
                this.selectedItems = this.selectedItems.filter(item => item.FShoppingCartID !== shoppingCartID);
                this.calculateInitialAmount();
            } catch (err) {
                this.error = err.message;
            } finally {
                this.loading = false;
            }
        },
    }
});
