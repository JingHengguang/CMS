// 引入导出的axios
import request from "../utils/request";


//通过title去查询articles里的信息  post请求
export function getByTitle(data) {
  return request.post(`/articles/queryArticle`,data);
}


//查询所有的article信息 get请求
export function getList(params){
    return request.get('/articles',{params:params})
}

//新增 post请求
export function addArticle(data){
  return request.post('/articles',data)
}

//更改 put请求
export function modArticle(id,data){
  return request.put(`/articles/${id}`,data)
}   


//删除 delete请求
export function delArticle(id){
  return request.delete(`/articles/${id}`)
}


//根据id 查看某篇文章内容  get请求
export function getbyId(id){
  return request.get(`/articles/${id}`)
}


//查看推荐的文章 get请求
export function recArticle(){
  return request.get(`/articles/isRecommend`)
}


//查看48小时阅读排行 get请求
export function readArticle(){
  return request.get(`/articles/reading`)
}


//更改 put请求
export function putReadNUm(id){
  return request.put(`/articles/putRead/${id}`)
}   
