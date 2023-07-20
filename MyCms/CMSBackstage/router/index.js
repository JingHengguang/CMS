//引入中间件
import Vue from 'vue'
import VueRouter from 'vue-router'
import routes from './routes'
import { IsLogin } from '../src/utils/auth'
import bus from "../src/utils/params"
//创建router实例
let router = new VueRouter({
    mode: "history",
    //定义路由
    routes

})

router.beforeEach((to,from,next)=>{
         let isAuth=IsLogin();

         document.title = to.meta.title;
           
            // console.log(to.path);
            if (to.path == '/login'||to.path=='/register') {
               if (isAuth) {              
                  // alert('你已经登录过了')
                  next('home')
                  
               } else {
                  next()
               }
               return;
            } 
            else {
               if (isAuth) {
                  next()
               } else {
                  bus.$message({ type: "error", message: "你没有登陆哦" });
                  //跳到指定路由
                  next('/login')
               }
          
            }
})

//注册路由中间件
Vue.use(VueRouter)

export default router