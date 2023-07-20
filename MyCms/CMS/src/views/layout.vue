<template>
  <el-container>
    <div class="all">
     
      <!-- 顶部 -->
      <div>
        <!-- 头部 -->
        <el-row style="background-color:green;">

          <el-col class="header1">就爱科技</el-col>
          <span style="font-size:20px;">欢迎用户：{{ username }}   </span>
          <span style="font-size:20px;margin-left: 10px;">登录时间：{{ loginTime }}</span>
          <el-button v-if="islogin" class="login" type="primary" plain @click="logout">注销</el-button>
          <el-button v-else class="login" type="primary" plain @click="logout">登录</el-button>
        </el-row>

        <!-- 轮播图片 -->
        <el-main style="padding: 0;margin: 3px;"> 
          <el-carousel trigger="click" :interval="2000" :height="dataHeight" arrow="always" style="background-color: red;">
            <el-carousel-item  v-for="(item,index) in RotationChartList" :key="index">
            <img class="RotationCharImg" :src="'http://localhost:5000/files/RotationChart?RotationChartUrl='+item.picture" alt="">
            </el-carousel-item>
         
            
          </el-carousel>
        </el-main>
        
      </div>

      <div class="left">
        <el-tabs type="border-card">
          <el-tab-pane style="font-size: 20px;" label="新闻实事">
            <router-view name="detailed"></router-view>
          </el-tab-pane>
          <el-tab-pane label="公司动态">
            <router-view name="dynamic"></router-view>
          </el-tab-pane>
          <el-tab-pane label="大神风采">
            <router-view name="mien"></router-view>
          </el-tab-pane>
          <el-tab-pane label="关于我们">
            <router-view name="about"></router-view>
          </el-tab-pane>
        </el-tabs>
      </div>

      <div class="right">
        <el-row>
          <!-- 48小时阅读排行 -->
          <el-col class="one">
            <div class="read">
              <p>48小时阅读排行</p>
              <el-button class="gengduo1" size="small" type="text">

              </el-button>
            </div>
            <el-table :data="reader" :show-header="false" class="table2">
              <el-table-column header-align="center">
                <template slot-scope="scope" class="column1">
                  <el-link :underline="false">
                    <span @click="hour(scope)">文章名:</span>
                    {{ scope.row.title }}
                  </el-link>
                </template>
              </el-table-column>
            </el-table>
          </el-col>
          <!-- 10天编辑推荐 -->
          <el-col class="two">
            <div class="read">
              <p>10天编辑推荐</p>
              <el-button class="gengduo1" size="small" type="text">

              </el-button>
            </div>
            <el-table :data="writer" :show-header="false" class="table2">
              <el-table-column header-align="center">
                <template slot-scope="scope" class="column1">
                  <el-link :underline="false">
                    <span @click="readingrecommends(scope)">文章名:</span>
                    {{ scope.row.title }}
                  </el-link>
                </template>
              </el-table-column>
            </el-table>
          </el-col>

          <!-- 10天评论排行文章 -->
          <!-- <el-col class="three">
            <div>
              <p>10天评论排行文章</p>
              <el-button class="gengduo1" size="small" type="text">
                >>查看更多
              </el-button>
            </div> -->

          <!-- <el-table :data="rebang" :show-header="false" class="table3">
              <el-table-column label="10天评论排行" header-align="center">
                <template slot-scope="scope"> -->
          <!-- 内容和过滤-->
          <!-- <el-link :underline="false">
                    {{ scope.row.id }}{{ "." }}
                    {{ scope.row.title | ellipsis }}
                  </el-link>
                </template>
              </el-table-column>
            </el-table>
          </el-col> -->
        </el-row>
      </div>

   <!-- 底部 -->
      <div class="bottom">
        <!-- 联系我们 -->
        <el-row class="EWM">
          <el-col class="gongzhong">
            <div class="image">
              <div class="block" v-for="fit in fits" :key="fit">
                <span class="demonstration"></span>
                <el-image style="width: 150px; height: 150px" :src="url4" :fit="fit"></el-image>
              </div>
              <div class="text">关注公众号</div>
            </div>
          </el-col>
          <el-col class="weixin">
            <div class="image">
              <div class="block" v-for="fit in fits" :key="fit">
                <span class="demonstration"></span>
                <el-image style="width: 150px; height: 150px" :src="url5" :fit="fit"></el-image>
              </div>
              <div class="text">扫码微信群</div>
            </div>
          </el-col>
          <el-col class="qq">
            <div class="image">
              <div class="block" v-for="fit in fits" :key="fit">
                <span class="demonstration"></span>
                <el-image style="width: 150px; height: 150px" :src="url6" :fit="fit"></el-image>
              </div>
              <div class="text">扫码进QQ群</div>
            </div>
          </el-col>
        </el-row>
        <!-- 备案号 -->
        <el-footer>
          <div>
            <div class="ICP">豫ICP备2020035082号-1</div>
            <div class="Police">
              <img src="../assets/备案图标(1).png" alt />
              <a target="_blank" href="http://www.beian.gov.cn/portal/registerSystemInfo?recordcode=41142202000050"
                style="
                  display: inline-block;
                  text-decoration: none;

                  height: 20px;
                  line-height: 20px;
                ">
                <p style="float: left; margin: 0px 0px 0px 5px; color: #939393">
                  豫公网安备 41170202000372号
                </p>
              </a>
            </div>
          </div>
        </el-footer>
          <!-- <el-backtop target=".page-component__scroll .el-scrollbar__wrap"></el-backtop> -->
      </div>
    </div>
  </el-container>
</template>

<script>
import { recArticle, readArticle } from "../api/article";
import {getRotationChartList} from "../api/rotationChart"
import {
  ClearToken,
  clearUsername,
  getToken,
  getUsername,
} from "../utils/auth";

export default {
  /*   热榜隐藏链接 */
  filters: {
    ellipsis(value) {
      if (!value) return "";
      if (value.length > 30) {
        return value.slice(0, 30) + "...";
      }
      return value;
    },
  },
  name: "Banner",
  data() {
    return {
      username: "",
      islogin: false,
      
      //登录时间
      loginTime:"",

      /*  48小时阅读排行 */
      reader: [],
      /* 10天编辑推荐 */
      writer: [],
      /* 10天评论排行 */
      rebang: [],

      id: "",
      /*  最新文章封面图 */
      fits: [],
       
       RotationChartList:[],
        pager: {
        //每页显示条目个数
        pageSize: 20,
        currentPage: 1,
        totalSize: 0,
      },
    };
  },
  props:{
    dataHeight:{
        type:String,
        default:'600px'
    }
  },
  methods: {   
     //获取当前时间
      currentTime(date){
          var  year=date.getFullYear();
          var  month= date.getMonth()+1;
          var  day=date.getDate();
          var  hours=date.getHours();
          var  minutes=date.getMinutes();
          var  seconds=date.getSeconds();

          this.loginTime=year+"-"+month+"-"+day+"\t"+hours+":"+minutes+":"+seconds
      },
    //判断是否有token
    isToken(tokenOK) {
      this.islogin = tokenOK;
    },
    //拉取推荐的文章
    recommend() {
      recArticle()
        .then((res) => {
          this.writer = res.data.data;
          console.log(res);
        })
        .catch((err) => {
          console.log(err);
        });
    },

    //48小时内的文章
    readed() {
      readArticle()
        .then((res) => {
          this.reader = res.data;
          console.log(res);
        })
        .catch((err) => {
          console.log(err);
        });
    },
    //获取轮播图
    RotationList(){
      getRotationChartList(this.pager)
      .then((res)=>{
        if (res.code==200) {
           this.RotationChartList=res.data.data;
           this.pager=res.data.pager;
        }               
      })
      .catch(err=>{
        console.log(err);    
      });
    },
    //跳转到该区域推荐文章的系列路由界面
    readingrecommends(scope) {
      console.log(scope.row.id);
      this.$router.push("/index/readrecommend?id=" + scope.row.id);
    },
    hour(scope) {
      console.log(scope.row.id);
      this.$router.push("/index/hourarticle?id=" + scope.row.id);
    },
    //点击注销按钮
    logout() {
      ClearToken();
      clearUsername();
      this.$router.push("/login");
    },
    setSize: function () {
      // 通过浏览器宽度（图片宽度）计算高度
      this.bannerHeigth = (400 / 1920) * this.screenwith;
    },
  },
  created() {
    var tokenOK = getToken();
    if (tokenOK == "") {
      tokenOK = false;
    } else {
      tokenOK = true;
    }

    this.isToken(tokenOK);
  },
  mounted() {
    this.username = getUsername();
    // 首次加载时,需要调用一次
    this.screenWidth = window.innerWidth;
    this.setSize();
    // 窗口大小发生改变时,调用一次
    window.onresize = () => {
      this.screenWidth = window.innerWidth;
      this.setSize();
    };
    //文章推荐
    this.recommend();
    //48小时文章阅读
    this.readed();
   
    this.RotationList();

     //获取当前时间
    this.currentTime(new Date());
  },
};
</script>

<style scoped>

.RotationCharImg{
   width: 100%;
   height: 100vh;
}
.el-carousel__item:nth-child(2n) {
  background-color: #99a9bf;
}

.el-carousel__item:nth-child(2n+1) {
  background-color: #d3dce6;
}

/* -------------------- */
td {
  padding: 1px !important;
  text-align: center;
  border-bottom: none;
}

.cell {
  text-align: center;
}

.el-link {
  font-size: 19px;
}

a:hover {
  color: #606266 !important;
}

.left {
  margin-left: 5px;
  width: calc(100vw - 347px);
  float: left;
  height: 400px;
}

.right {
  float: left;
  width: 320px !important;
  background-color: rgba(207, 207, 207, 0.158);
  /* height: 100% !important; */
}

.bottom {
  float: left;
  width: calc(100vw - 18px) !important;
  padding: 0px;
  background-color: rgba(207, 207, 207, 0.158);
}

.bottom .image {
  width: 150px;
  height: 150px;
}

/* 头部样式 */
.header1 {
  width: 800px !important;
  height: 50px;
  color: #57a1b8;
  line-height: 2;
  font-size: 30px;
  margin-left: 20px !important;
}

.login {
  width: 72px;
  height: 40px;
  float: right;
  margin-right: 20px;
  font-size: 17px;
  margin-top: 10px !important;
}



.el-tabs {
  height: 800px ;
}
 

.el-tabs__item {
  /* width: 297.8px !important; */
  font-size: 40px;
  text-align: center;
}

/* 48小时阅读排行 */
.one {
  margin-left: 0 !important;
  background-color: rgba(207, 207, 207, 0.158);
  text-align: left;
  font-size: 21px;
  border: 1.5px solid rgba(207, 207, 207, 0.158) !important;
  border-radius: 7px;
  margin-top: 50px;
}

.one p {
  padding: 3px;
  margin: 3px;
  width: 160px !important;
  float: left;
}

.one button {
  float: right;
  margin-right: 8px;
  margin-top: 4px;
  font-size: 16px;
  background-color: rgba(207, 207, 207, 0);
}

.table1 {
  border-bottom: none !important;
}

.el-table {
  margin-top: 40px;
  border-left: none !important;
  border-right: none !important;
  border-bottom: none !important;
  font-size: 19px;
}

.el-table td,
.el-table th.is-leaf {
  border-bottom: none;
  background-color: rgba(207, 207, 207, 0.158);
  border-top: 1.3px solid rgba(207, 207, 207, 0.158) !important;
}

.el-table::before {
  height: 0px;
}

.el-table__row {
  padding: 0px !important;
  height: 35px;
}

.gengduo1 {
  margin-top: 5px;
}

/* 10天编辑推荐 */
.two {
  margin-left: 0 !important;
  background-color: rgba(207, 207, 207, 0.158);
  text-align: left;
  font-size: 21px;
  border: 1.5px solid rgba(207, 207, 207, 0.158) !important;
  border-radius: 7px;
  margin-top: 50px;
}

.two p {
  padding: 3px;
  margin: 3px;
  width: 160px !important;
  float: left;
}

.two button {
  float: right;
  margin-right: 8px;
  margin-top: 4px;
  font-size: 16px;
  background-color: rgba(207, 207, 207, 0);
}

.table2 {
  border-bottom: none !important;
}

/* 10天评论排行文章 */
.three {
  margin-left: 0 !important;
  background-color: rgba(207, 207, 207, 0.158);
  text-align: left;
  font-size: 21px;
  border: 1.5px solid rgba(207, 207, 207, 0.158) !important;
  border-radius: 7px;
  margin-top: 50px;
}

.three p {
  padding: 3px;
  margin: 3px;
  width: 180px !important;
  float: left;
}

.three button {
  float: right;
  margin-right: 8px;
  margin-top: 4px;
  font-size: 16px;
  background-color: rgba(207, 207, 207, 0);
}

.table3 {
  border-bottom: none !important;
}

/* row总样式 */
.el-footer {
  width: 100vm !important;
  margin-top: 30px !important;
}

.el-row {
  margin-top: auto;
  width: 100vm;
  margin-left: 0px !important;
}

/* 联系我们样式 */
.EWM {
  width: 100vm !important;
  display: -webkit-flex;
  display: flex;
  -webkit-justify-content: space-around;
  justify-content: space-around;
  margin-top: 40px !important;
}

.block {
  width: 150px !important;
  height: 350px !important;
}

.image {
  margin-top: auto;
}

.text {
  margin-left: 35px;
}

.gongzhong {
  height: 150px;
  width: 150px !important;
}

.weixin {
  height: 150px;
  width: 150px !important;
}

.qq {
  height: 150px;
  width: 150px !important;
}

/* 底部 */
.el-footer .el-row {
  margin-top: 10px;
}

.ICP {
  text-align: center;
}

.Police {
  text-align: center;
}

.el-image__error,
.el-image__placeholder {
  width: 100%;
  height: 100%;
}
</style>
