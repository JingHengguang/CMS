// 引入导出的axios
import request from "../utils/request";

//点赞 post请求
export function isLike(data) {
    return request.post('/likes',data)
}