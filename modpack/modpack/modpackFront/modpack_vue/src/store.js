import { defineStore } from "pinia";
import Swal from "sweetalert2";
const API_URL = import.meta.env.VITE_API_URL;

export const useCartStore = defineStore("cart", {
  state: () => ({
    cartItems: [],
    shippingMethods: [],
    selectedItems: [],
  }),
  getters: {
    getTotalPrice: (state) =>
      state.cartItems.reduce(
        (total, item) => total + item.price * item.quantity,
        0
      ),
    getTotalPriceOfSelectedItems: (state) => {
      return state.cartItems.reduce((total, item) => {
        if (item.selected) {
          return total + item.price * item.quantity;
        }
        return total;
      }, 0);
    },
  },
  actions: {
    async fetchCartItems(memberId) {
      try {
        const response = await fetch(`${API_URL}/Carts/${memberId}`);
        if (response.status == 200) {
          const data = await response.json();
          this.cartItems = data;
        } else {
          this.cartItems = [];
        }
      } catch (error) {
        console.error("Error fetching cart data:", error);
      }
    },
    async deleteProduct(item) {
      try {
        const response = await fetch(`${API_URL}/Carts/${item.cartId}`, {
          method: "DELETE",
          headers: {
            "Content-Type": "application/json",
          },
        });
        if (response.ok) {
          Swal.fire("刪除成功");
          const cartIndex = this.cartItems.findIndex(
            (cartItem) => cartItem.cartId === item.cartId
          );
          if (cartIndex !== -1) {
            this.cartItems.splice(cartIndex, 1);
          }
          // 从选定的项目列表中删除产品
          this.removeFromSelectedItems(item);
        } else {
          console.error("刪除失敗:", response.statusText);
          Swal.fire("刪除失敗");
        }
      } catch (error) {
        console.error("刪除失敗:", error);
      }
    },
    async fetchShippingMethods() {
      try {
        const response = await fetch(`${API_URL}/Shippings`, {
          method: "GET",
        });

        if (response.ok) {
          const data = await response.json();
          this.shippingMethods = data;
        } else {
          console.error("Failed to fetch shipping methods");
        }
      } catch (err) {
        console.error(err);
      }
    },
    async clearCarts(memberId) {
      const clearResponse = await fetch(`${API_URL}/Carts/Clear${memberId}`, {
        method: "DELETE",
        headers: {
          "content-type": "application/json",
        },
      });
      if (clearResponse.ok) {
        Swal.fire("購物車清空成功");
      } else {
      }
    },
    async deletePurchasedProduct() {
      try {
        const response = await fetch(
          `${API_URL}/Carts/deletePurchasedProduct`,
          {
            method: "DELETE",
            headers: {
              "content-type": "application/json",
            },
            body: JSON.stringify(this.selectedItems.map((item) => item.cartId)),
          }
        );
        if (response.ok) {
          this.selectedItems = [];
        }
      } catch {}
    },
    async removeFromSelectedItems(item) {
      const index = this.selectedItems.findIndex(
        (selectedItems) => selectedItems.cartId == item.cartId
      );
      if (index !== -1) {
        this.selectedItems.splice(index, 1);
      }
    },
    async addToSelectedItems(item) {
      this.selectedItems.push(item);
    },
  },
});

export const useNavbarStore = defineStore("navbar", {
  state: () => ({
    memberId: "",
    memberInfo: [],
  }),
  actions: {
    getMemberIdFromCookie() {
      const match = document.cookie.match(/(?:^|;) ?memberId=([^;]*)(?:;|$)/);
      if (match) {
        this.memberId = match[1];
      }
    },
    async fetchMemberInfo() {
      try {
        const response = await fetch(`${API_URL}/Members/${this.memberId}`);
        const memberData = await response.json();
        console.log(memberData);
        this.memberInfo = memberData;
      } catch (error) {
        console.log("Error fetching memberInfo", error);
      }
    },
  },
});
