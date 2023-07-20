// 引入导出的axios
import request from "../utils/request";



//查询所有的autoinfo信息 get请求
export function getlist(params){
    return request.get('/logs',{params:params})
}

