//引入中间件
import Vue from 'vue'
import VueRouter from 'vue-router'
import routes from './routes'
// import { IsLogin } from '../src/utils/auth'

//创建router实例
let router = new VueRouter({
    mode: "history",
    //定义路由
    routes

})

router.beforeEach((to,from,next)=>{
        
         document.title = to.meta.title;   
          
            // console.log(to.path);
            // console.log(from);
            next();

            //判断是否登录

            // let isAuth=IsLogin();
            // if (to.path == '/login'||to.path=='/register') {
            //    if (isAuth) {              
            //       // alert('你已经登录过了')
            //       next('index')
                  
            //    } else {
            //       next()
            //    }
            //    return;
            // } 
            // else {
            //    if (isAuth) {
            //       next()
            //    } else {
            //       // alert('你没有登陆哦');
            //       // this.$message({ type: "error", message:"你没有登陆哦,请先登录" });
            //       //跳到指定路由
            //       next('login')
            //    }
            // }
})
// ----------------------------------------------------------------------------------------------------------------------------------------
// router.beforeEach((to, from, next) => {
//     // 判断to准备去的路由，是否是登录界面的路径，如果是处理一下1，如果不是也处理一下2
//     // 处理1 判断当前是否登录，如果已经登录，则不允许去到登录界面；如果未登录则放行
//     // 处理2 判断当前是否登录，如果已经登录，则放行，；如果未登录不能去
//     let isAuth = IsLogin()

//       console.log(from);

//     if (to.path === '/login') {
       

//         if (from.path === "system/dashboard") {
//             if (isAuth) {
//                 next('/system')
//             } else {
//                 next()
//             }
//         }else {
//             if (isAuth) {
//                 next('/index')
//             } else {
//                 next()
//             }
//         }

//     } 
//     else {
//         if (isAuth) {
//             next()
//         } else {
//             if (to.path === '/register') {
//                 next()
//             }
//         }
//     }
// })

//注册路由中间件
Vue.use(VueRouter)

export default router