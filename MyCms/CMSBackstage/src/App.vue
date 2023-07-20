<template>
  <!-- 路由的出口 -->
  <!-- 路由匹配到组件将渲染到这里 -->
     <router-view></router-view>
</template>

<script>
       
        export default {
  data() {
    return {
     
    
    };
  },
  //函数
methods: {

  },
  //页面数据加载完毕执行
mounted() {
   
  //关闭页面时清除localStorage
  // window.onbeforeunload=function () {
  //    var storage=window.localStorage;
  //    storage.clear(); 
  // }
     
  },
};
</script>

<style>
/* *{
    color: black;
      font-weight: bold;
} */
</style>
