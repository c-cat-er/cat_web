//3. @/constants/axios.js set axios
import axios from 'axios';
import Cookies from 'js-cookie';
const axiosInstance = axios.create({
    baseURL: 'http://localhost:5038' || import.meta.env.VITE_APP_API_BASE_URL, //change server api
    timeout: 40000,
    withCredentials: true //���\�o�e cookie
    //headers: { 'X-Custom-Header': 'foobar' } //??
});

//request Interceptor �ШD�d�I
axiosInstance.interceptors.request.use(
    config => {
        //from cookie get token and add to header
        //const token = localstore.getItem('token');
        const token = Cookies.get('token');
        if (token) {//���v���Y
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

//response interceptor �^���d�I
axiosInstance.interceptors.response.use(
    response => response,
    error => {
        if (error.response) {
            if (error.response.status === 401) {
                //�����v�A���w�V��n������
                window.location.href = '/login';
            }
            //other statecode?
            console.error('�T�����~�G', error.response);
        } else if (error.request) {
            //no response
            console.error('�ШD���~�G', error.request);
        } else {
            //�]�m�ШD�ɵo�Ϳ��~
            console.error('���~�����G', error.message);
        }
        return Promise.reject(error);
    }
);
export default axiosInstance;
