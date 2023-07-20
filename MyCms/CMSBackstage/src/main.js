//引入组件
import Vue from 'vue'
import App from './App.vue' 
import router from '../router'
import * as echarts from 'echarts';
import ElementUI from 'element-ui';
import 'element-ui/lib/theme-chalk/index.css';


//全局引入
Vue.prototype.$echarts = echarts

Vue.config.productionTip = false


Vue.use(ElementUI);


new Vue({
  //通过router配置参数注入路由
  router,
  render: h => h(App),
}).$mount('#app')
