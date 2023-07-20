
<template>
  <div>
    <!-- 卡片 -->
    <el-card class="typeBoxCard">
        <el-button :click="this.$router.go(-1)">返回上一页</el-button>
           404这是一个错误的网页
    </el-card>
  </div>
</template>


<script>

export default {
  data() {
    return {

    };
  },
  //函数
  methods: {
  }
  
  }
</script>


<style scoped>
.typeBoxCard {
  display: flex;
  align-items: center;
  justify-content: center;
}
</style>