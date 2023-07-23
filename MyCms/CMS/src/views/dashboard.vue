<template>
  <div id="main" style="height: 100%; width: 100% "></div>
</template>


<script>


export default {
  mounted() {
    this.initSomething();
  },
  methods: {
    initSomething() {
      // 基于准备好的dom，初始化echarts实例     
      let myChart = this.$echarts.init(document.getElementById("main"));

      //绘制图表
      myChart.setOption({
        title: {
          text: "文章发表采集",
        },
        tooltip: {},
        xAxis: {
          data: ["科学类", "自然科学", "社会科学", "漫画类", "旅游杂志", "健康类"],
        },
        yAxis: {},
        series: [
          {
            name: "销量",
            type: "bar",
            data: [5, 20, 36, 10, 10, 20],
          },
        ],
      });
    },
  },
};
</script>


<style  scoped lang="less">
</style>