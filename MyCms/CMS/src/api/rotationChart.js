// 引入导出的axios
import request from "../utils/request";


//获取轮播图列表
export function getRotationChartList(params) {
    return request.get('/RotationChart',{params:params})
  } 

