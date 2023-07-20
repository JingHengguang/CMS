import request from '../utils/request'

//获取类型列表
export function getList(params) {
    return request.get('/ArticleTypes',{params:params})
  } 

  
//模糊查询类型 post请求
export function getListByQuery(data) {
    return request.post('/ArticleTypes/GetTypeByTypeName',data)
}


//新增类型 post请求
export function addArticleType(data){
  return request.post(`/ArticleTypes`,data)
}

//更改 put请求
export function modArticleType(id, data) {
  return request.put(`/ArticleTypes/${id}`, data)
}


//删除 delete请求
export function delArticleType(id) {
  return request.delete(`/ArticleTypes/${id}`)
}
