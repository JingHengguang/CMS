// 引入导出的axios
import request from "../utils/request";


//通过id获取指定用户
export function getUserById(id) {
  return request.get(`/users/${id}`)
}

//通过username去查询users里的信息  post请求
export function getByUsername(data) {
  return request.post(`/Users/queryUser`,data);
}

//获取用户列表
export function getList(params) {
  return request.get('/users',{params:params})
} 


//注册用户post请求
export function registerList(data){
  return request.post(`/Register`,data)
}


//新增 post请求
export function addUser(data) {
  return request.post('/users', data)
}

//更改 put请求
export function modUser(id, data) {
  return request.put(`/users/${id}`, data)
}


//删除 delete请求
export function delUser(id) {
  return request.delete(`/users/${id}`)
}


//用户登录获取token
export function login(data) {
  return request.post('/users/token', data)
}