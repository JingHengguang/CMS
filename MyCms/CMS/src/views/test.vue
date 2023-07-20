<template>
  <div>
    <div v-for="item in list" :key="item.id">
      <div id="title">标题: {{ item.title }}</div>
      <div>
        <img v-if="item.coverPictureUrl" id="beijing" :src="src + item.coverPictureUrl"  />
        <!-- //动弹绑定src并拼接获取到的图片路径 -->
      </div>
    
      <div id="content" v-html="item.content.words"></div>
    </div>
  </div>
</template>


<script>
import { getList } from "../api/article";

export default {
  data() {
    return {
      list: [],
      pager: {
        //每页显示条目个数
        pageSize: 20,
        currentPage: 1,
        totalSize: 0,
      },
      src: "http://localhost:5000/files/picPath?picUrl=",
    };
  },
  methods: {
    fetchData() {
      getList(this.pager)
        .then((res) => {
          this.list = res.data.data;
          console.log(res);
        })
        .catch((err) => {
          console.log(err);
        });
    },
  },
  mounted() {
    this.fetchData();
  },
};
</script>


<style>

#beijing{
    height: 200px;
    width: 100px;
}


</style>