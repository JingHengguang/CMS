<template>
   <div>
    <el-calendar v-model="value"></el-calendar>
   </div>
</template>

 
<script>



export default {
  data() {
    return {
     
     value: new Date()
    };
  },
  //函数
methods: {

  },
  //页面数据加载完毕执行
mounted() {

   
  },
};
</script>

<style>

</style>