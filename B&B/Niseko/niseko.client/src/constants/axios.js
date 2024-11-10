//3. @/constants/axios.js set axios
import axios from 'axios';
import Cookies from 'js-cookie';
const axiosInstance = axios.create({
    baseURL: 'http://localhost:5038' || import.meta.env.VITE_APP_API_BASE_URL, //change server api
    timeout: 40000,
    withCredentials: true //允許發送 cookie
    //headers: { 'X-Custom-Header': 'foobar' } //??
});

//request Interceptor 請求攔截
axiosInstance.interceptors.request.use(
    config => {
        //from cookie get token and add to header
        //const token = localstore.getItem('token');
        const token = Cookies.get('token');
        if (token) {//授權標頭
            config.headers.Authorization = `Bearer ${token}`;
        }
        //config.headers['Content-Type'] = 'application/json';
        //config.headers['Accept'] = 'application/json';
        //config.headers['User-Agent'] = 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36';
        //config.headers['Cache-Control'] = 'no-cache';
        //config.headers['Referer'] = 'https://www.example.com/previous-page';
        //config.headers['Cookie'] = 'sessionId=abc123';
        //config.headers['X-Requested-With'] = 'XMLHttpRequest';
        //config.headers['Accept-Language'] = 'en-US,en;q=0.9,fr;q=0.8';
        //config.headers['Accept-Encoding'] = 'gzip, deflate, br';
        return config;
    },
    error => {
        return Promise.reject(error);
    }
);

//response interceptor 回應攔截
axiosInstance.interceptors.response.use(
    response => response,
    error => {
        if (error.response) {
            if (error.response.status === 401) {
                //未授權，重定向到登錄頁面
                window.location.href = '/login';
            }
            //other statecode?
            console.error('響應錯誤：', error.response);
        } else if (error.request) {
            //no response
            console.error('請求錯誤：', error.request);
        } else {
            //設置請求時發生錯誤
            console.error('錯誤消息：', error.message);
        }
        return Promise.reject(error);
    }
);
export default axiosInstance;
