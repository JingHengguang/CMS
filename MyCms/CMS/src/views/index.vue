<template>
  <el-container>
    <div>
      <el-row v-for="(item, index) in List" :key="item.id">
        <el-col class="first">
          <!-- 文章的封面图 -->
          <div class="block" style="center"  @click="search(item)">
            <el-image
              v-if="item.coverPictureUrl"
              :src="src + item.coverPictureUrl"
              :fit="fit"
            ></el-image>
          </div>
        </el-col>
        <div>
          <el-col class="second">
            <!-- 文章标题 -->
            <div class="biaoti">
              <!-- <el-link :underline="false"> -->
                {{ index + 1 + "--" + item.title }}
              <!-- </el-link> -->
            </div>
            <!-- 文章作者 -->
            <div class="userid">
              {{ "作者：" + item.user.username }}
            </div>
            <!-- 文章简介 -->
            <div class="wenzhangjianjie">
              {{ "文章简介：" + item.synopsis }}
            </div>
          </el-col>
          <!-- 文章的阅读与评论与点赞与发布时间 -->
          <el-col class="third">
            <div>
              <el-tag v-for="tag in tags1" :key="tag.name1">
               
              </el-tag>
              <el-tag v-for="tag in tags1" :key="tag.name2">
         
                
              </el-tag>
              <el-tag>
                <i class="el-icon-time"></i>
                <b>发布时间：{{ item.createdTime }}</b>
              </el-tag>
            </div>
          </el-col>
        </div>
      </el-row>
    </div>
  </el-container>
</template>

<script>
import { getList } from "../api/article";

export default {
  name: "Banner",
  data() {
    return {
      /* 阅读量，评论量，发布时间 */
      tags1: [{ read: "10.3k", comment: "5.8k", time: "30分钟前" }],

      /* 文章标题，作者，文章简介 */
      List: [],
      /*  最新文章封面图 */
      fit: "contain",
      url1: require("../assets/5.jpeg"),
      pager: {
        //推荐最新文章前三篇
        pageSize: 3,
        currentPage: 1,
        totalSize: 0,
      },
      src: "http://localhost:5000/files/picPath?picUrl=",
    };
  },
  methods: {
    setSize: function () {
      // 通过浏览器宽度（图片宽度）计算高度
      this.bannerHeigth = (400 / 1920) * this.screenwith;
    },
    //拉取文章信息
    fetchData() {
      getList(this.pager)
        .then((res) => {
          this.List = res.data.data;
          console.log(res);
        })
        .catch((err) => {
          console.log(err);
        });
    },
    search(item) {
      console.log(item.id)
      this.$router.push("/index/news?id=" + item.id);
    },
  },
  mounted() {
    // 首次加载时,需要调用一次
    this.screenWidth = window.innerWidth;
    this.setSize();
    // 窗口大小发生改变时,调用一次
    window.onresize = () => {
      this.screenWidth = window.innerWidth;
      this.setSize();
    };
    this.fetchData();
  },
};
</script>

<style scoped>
i {
  width: 24px;
  height: 24px;
  font-size: 20px;
  margin-top: 15px;
  color: rgba(0, 0, 0, 0.521);
}
b {
  font-size: 10px !important;
  line-height: 24px !important;
  color: rgba(0, 0, 0, 0.438);
}
.first {
  height: 150px !important;
  width: 150px !important;
  margin-left: 0 !important;
}
.el-image {
  width: 100%;
  height: 100%;
}
.second {
  width: calc(100% - 150px) !important;
  float: left;
  margin-left: 0px;
}
.biaoti {
  font-style: oblique;
  margin-top: 10px;
  font-size: 20px;
}
.userid {
  margin-left: 10px;
  margin-top: 5px;
  font-size: 20px;
}
.wenzhangjianjie {
  margin-left: 10px;
  margin-top: 5px;
}
.third {
  width: calc(100% - 150px) !important;
  float: right !important;
}
.el-tag {
  background-color: #fff;
  border: none;
  margin: 0 auto;
}
/* row总样式 */
.el-row {
  margin-top: auto;
  margin-left: 50px;
}

.el-image__error,
.el-image__placeholder {
  width: 100%;
  height: 100%;
}
</style>
