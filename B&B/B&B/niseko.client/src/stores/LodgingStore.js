//@/stores/LodgingStore.js 管理篩選條件和房間數據
import { defineStore } from 'pinia';
import axiosInstance from '@/constants/axios';

// 提取第一張圖片或者返回預設圖片
function getRoomImage(FRoomImages) {
    return Array.isArray(FRoomImages?.$values) && FRoomImages.$values.length > 0
        ? FRoomImages.$values[0]  // 提取第一張圖片
        : 'default.jpg';  // 使用預設圖片
}

export const useLodgingStore = defineStore('lodging', {
    state: () => ({
        priceRange: [0, 500], //default
        locations: [],
        selectedLocations: [],
        rooms: [],
        totalRooms: 0, // 房間總數，用於判斷是否還有更多房間
        currentPage: 1, // 當前頁碼
        perPage: 6, // 每頁顯示的數量
        loading: false, // 加載狀態
    }),
    actions: {
        async fetchSingleRoom(roomID) {
            try {
                this.loading = true;
                const response = await axiosInstance.get(`/api/ProductHomestayRoom/${roomID}`);
                //console.log('API Response:', response.data);
                const homestayData = response.data; // 取得單一房間資料

                if (homestayData && homestayData.ProductHomestayRoomDTO) {
                    const roomDetails = homestayData.ProductHomestayRoomDTO.$values?.[0]; // 展開 $values 中的房間數據

                    if (roomDetails) {
                        // 處理數據格式，並將房間添加到 store 的 rooms 列表中
                        const newRoom = {
                            ...roomDetails,
                            FHomestayID: homestayData.FHomestayID,
                            FHomestayCode: homestayData.FHomestayCode,
                            FHomestayName: homestayData.FHomestayName,
                            FDescription: homestayData.FDescription,
                            FAddressID: homestayData.FAddressID,
                            FAddressName: homestayData.FAddressName,
                            FHomestayRoomID: roomDetails.FHomestayRoomID,
                        };
                        // 檢查 rooms 中是否已經包含這個房間，避免重複
                        if (!this.rooms.some(room => room.FHomestayRoomID === newRoom.FHomestayRoomID)) {
                            this.rooms.push(newRoom);
                        }
                    }
                }
            } catch (error) {
                console.error('Failed to fetch room data:', error);
            } finally {
                this.loading = false; // 確保在數據加載完畢後，將 loading 設置為 false
            }
        },
        async fetchLocations() {
            try {
                const response = await axiosInstance.get('/api/TLocation');
                //console.log('API Response:', response.data);
                // 提取 $values 中的數據
                const locationsData = response.data.$values;
                //console.log('API locationsData:', locationsData);
                this.locations = locationsData.map(location => ({
                    text: location.FLocationName,
                    value: location.FLocationID
                }));
            } catch (error) {
                console.error('Failed to fetch locations:', error);
                this.locations = [];
            }
        },
        //k-p, 使用分頁
        async fetchRooms(page = 1, perPage = 6) {
            try {
                const response = await axiosInstance.get(`/api/ProductHomestays?page=${page}&perPage=${perPage}`);
                console.log('API Response:', response.data);
                const homestayData = response.data.Values?.$values ?? []; //取得房間列表數據

                //k-p, 提取每個民宿中的房間數據，並展開成單一房間列表
                this.rooms = homestayData.flatMap(homestay => {
                    if (homestay.ProductHomestayRoomDTO && homestay.ProductHomestayRoomDTO.$values) {
                        return homestay.ProductHomestayRoomDTO.$values.map(room => {
                            //檢查 RoomImages 是否存在並提取第一張圖片，如果沒有則使用 'default.jpg'
                            const roomImage = Array.isArray(room.FRoomImages?.$values) && room.FRoomImages.$values.length > 0
                                ? room.FRoomImages.$values[0]  // 提取第一張圖片
                                : 'default.jpg';  // 使用預設圖片
                            //console.log('RoomImages:', room.RoomImages);

                            return {
                                //展開房間表(TProductHomestayRoom)的其他所有屬性
                                ...room,
                                // 展開民宿表(TProductHomestay)的指定屬性
                                FHomestayCode: homestay.FHomestayCode,
                                FHomestayName: homestay.FHomestayName,
                                FDescription: homestay.FDescription,
                                FAddressID: homestay.FAddressID,
                                FAddressName: homestay.FAddressName,
                                RoomImages: roomImage,  // 儲存第一張圖片或預設圖片
                            };
                        });
                    } else {
                        return []; // 如果沒有房間表(TProductHomestayRoom)數據，返回空數組
                    }
                });
                console.log('this.rooms:', this.rooms);

                //k-p, 动态计算實際價格區間并更新 priceRange
                if (this.rooms.length > 0) {
                    const prices = this.rooms.map(room => room.FStayPrice);
                    this.priceRange = [Math.min(...prices), Math.max(...prices)];
                }
                this.totalRooms = response.data.TotalCount; // 獲取總數
                this.currentPage += 1; // 更新當前頁碼
            } catch (error) {
                console.error('Failed to fetch rooms:', error);
                //this.rooms = [];
            } finally {
                this.loading = false; // 加載完成後設置為 false
            }
        },
        updatePriceRange(range) {
            this.priceRange = range;
        },
        updateSelectedLocations(locations) {
            this.selectedLocations = locations;
        }
    },
    getters: {
        minPrice(state) {
            return Math.min(...state.rooms.map(room => room.FStayPrice));
        },
        maxPrice(state) {
            return Math.max(...state.rooms.map(room => room.FStayPrice));
        },
        hasMoreRooms(state) {
            return state.rooms.length < state.totalRooms;
        },
        filteredRooms(state) {
            return state.rooms.filter(room => {
                return (
                    //價格篩選：房間價格需要在 priceRange 之內
                    room.FStayPrice >= state.priceRange[0] &&
                    room.FStayPrice <= state.priceRange[1] &&
                    //地點篩選：如果選中任意地點，只顯示符合 selectedLocations 的房間
                    (state.selectedLocations.length === 0 || state.selectedLocations.includes(room.FAddressID))
                );
            });
        }
    }
});

//待優化
//分頁顯示未完成
