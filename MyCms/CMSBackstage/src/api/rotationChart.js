// 引入导出的axios
import request from "../utils/request";


//获取轮播图列表
export function getRotationChartList(params) {
  return request.get('/RotationChart',{params:params})
} 


//模糊查询post请求
export function GetRotationChartsByremark(data){
  return request.post(`/RotationChart/GetRotationChartsByremark`,data)
}


//新增 post请求
export function addRotationChart(data) {
  return request.post('/RotationChart', data)
}

//更改 put请求
export function modRotationChart(id, data) {
  return request.put(`/RotationChart/${id}`, data)
}


//删除 delete请求
export function delRotationChart(id) {
  return request.delete(`/RotationChart/${id}`)
}


// //用户登录获取token
// export function login(data) {
//   return request.post('/users/token', data)
// }