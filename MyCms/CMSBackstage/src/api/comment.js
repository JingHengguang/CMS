// 引入导出的axios
import request from "../utils/request";

//评论 post请求
export function comment(data) {
    return request.post('/comments', data)
}

//通过文章id获取评论 get请求
export function getComment(id) {
    return request.get(`/comments/${id}`)
}