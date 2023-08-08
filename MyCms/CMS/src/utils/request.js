import axios from 'axios'
import { getToken } from '../utils/auth'


//创建axios实例
const instance = axios.create({

    baseURL: 'http://localhost:5000//',
    //设置请求超时时间 
    timeout: 6000,
})

// 设置拦截器
// 添加请求拦截器 作用是如果有token在headers里加入token验证
instance.interceptors.request.use(function (config) {
    // 在发送请求之前做些什么
    // console.log(config)
    let token = getToken();
    if (token) {
        config.headers['Authorization'] = 'Bearer ' + token
    }
    return config;
}, function (error) {
    // 对请求错误做些什么
    console.log(error)
    return Promise.reject(error);
});


// 添加响应拦截器
instance.interceptors.response.use(function (response) {
//    对响应数据做点什么
    // console.log(response)
    // console.log(response.data)

    if (response.status === 200  ) {
        return response.data
    }
    return response;

}, function (error) {
    // 对响应错误做点什么
    console.log(error)
    return Promise.reject(error);
});


export default instance
