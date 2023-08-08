<template>
  <el-container class="all_container">

    <el-drawer title="登录界面" :visible.sync="drawer" direction="rtl" size="60%" :with-header="false"
      :before-close="handleClose">
      <loginTemp ref="child"></loginTemp>
    </el-drawer>

    <!-- 头部 -->
    <el-header style="height: 60px;">
      <!-- 图标和标题 -->
     
      <el-menu mode="horizontal" >    
        <el-menu-item >  
          <span :underline="false"> <img  height="60px" src="../assets/blog.png" alt=""> 酸菜鱼科技</span>
        </el-menu-item>
      </el-menu>
      

      <!-- 主菜单栏 -->
      <el-menu :default-active="activeIndex" mode="horizontal" @select="handleSelect" active-text-color="#1183FB">    
        <el-menu-item index="1">首页</el-menu-item>
        <el-menu-item index="2">消息中心2</el-menu-item>
        <el-menu-item index="3">消息中心3</el-menu-item>
        <el-menu-item index="4">消息中心4</el-menu-item>
        <el-menu-item index="5">消息中心5</el-menu-item>
        <el-menu-item index="6">消息中心6</el-menu-item>
      </el-menu>

      <!-- 登录注册 -->
      <div>
        <el-link v-if="!islogin" type="info" :underline="false" style="color: black;" @click="drawer = true">Log
          in</el-link>
        <el-button v-if="!islogin"
          style="color: white;height: 48px; width: 86px;background-color: black; margin-left: 20px;"
          @click="drawer = true">Sign
          up</el-button>

        <el-menu v-if="islogin">
          <el-menu-item>

            <el-dropdown trigger="click">
              <span>
                <el-menu>
                  <el-menu-item>
                    <span class="el-dropdown-link">
                      {{ username }}
                    </span>
                    <el-avatar shape="square" :size="50" fit="fill" :src="avatarUrlFull"></el-avatar>
                  </el-menu-item>
                </el-menu>

              </span>
              <el-dropdown-menu slot="dropdown">
                <el-dropdown-item @click.native="handlelUpdateAvatar">修改头像</el-dropdown-item>
                <el-dropdown-item @click.native="handlelUpdatePwd">修改密码</el-dropdown-item>
                <el-dropdown-item @click.native="handlelogout">注销登录</el-dropdown-item>
              </el-dropdown-menu>

            </el-dropdown>
          </el-menu-item>
        </el-menu>

        <!-- <span v-if="islogin">admin</span> -->
      </div>

      <!-- Dialog对话框 -->
      <el-dialog title="修改头像" :visible.sync="visibleAvatar">

        
             <!-- 头像 -->
             <el-form label-width="100px">

                <el-form-item label="原头像:">
                    <img class="updateAvatarFile" :src="AvatarUrl" alt="头像加载失败...">
                </el-form-item>

                <el-form-item label="新头像：" label-width="100px" style="margin-top: 20px">
                    <el-upload list-type="picture-card" :action="uploadAvatarUrl" :auto-upload="true" :limit="limit"
                        :multiple="false" :show-file-list="true" :on-preview="handlePictureCardPreview"
                        :on-remove="handleRemove" :on-success="handleAvatarSuccess" :before-upload="beforeAvatarUpload"
                        :on-exceed="handleExceed" :on-error="imgUploadError">

                        <i class="el-icon-plus"></i>
                    </el-upload>
                </el-form-item>

                <el-form-item>
                    <el-dialog :visible.sync="dialogVisibleImg">
                        <img width="100%" :src="dialogImageUrl" alt="">
                    </el-dialog>
                </el-form-item>
            </el-form>

          <el-button type="primary" @click="handleSave">确 定</el-button>

      </el-dialog>

    </el-header>

    <el-container>
      <el-container>
        <!-- 中间 -->
        <el-main>
          <!-- 轮播图片 -->
          <el-carousel trigger="click" :interval="2000" :height="dataHeight" arrow="always">
            <el-carousel-item v-for="(item, index) in RotationChartList" :key="index">
              <img class="RotationCharImg"
                :src="'http://localhost:5000/files/RotationChart?RotationChartUrl=' + item.picture" alt="">
            </el-carousel-item>
          </el-carousel>

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

        </el-main>

        <!-- 底部 -->
        <el-footer>

          <!-- 联系我们 -->
          <div class="text">关注公众号</div>
          <div class="text">扫码微信群</div>
          <div class="text">扫码进QQ群</div>
          <div class="ICP">豫ICP备2020035082号-1</div>
          <!-- <div class="Police">
            <img src="../assets/备案图标(1).png" alt />
            <a target="_blank" href="http://www.beian.gov.cn/portal/registerSystemInfo?recordcode=41142202000050" style="
                  display: inline-block;
                  text-decoration: none;

                  height: 20px;
                  line-height: 20px;
                ">
              <p style="float: left; margin: 0px 0px 0px 5px; color: #939393">
                豫公网安备 41170202000372号
              </p>
            </a>
          </div> -->





        </el-footer>

      </el-container>

      <!-- 右侧栏 -->
      <el-aside width="200px">
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
          <el-col class="three">
            <div>
              <p>10天评论排行文章</p>
              <el-button class="gengduo1" size="small" type="text">
                >>查看更多
              </el-button>
            </div>

            <el-table :data="rebang" :show-header="false" class="table3">
              <el-table-column label="10天评论排行" header-align="center">
                <template slot-scope="scope">
                  <!-- 内容和过滤-->
                  <el-link :underline="false">
                    {{ scope.row.id }}{{ "." }}
                    {{ scope.row.title | ellipsis }}
                  </el-link>
                </template>
              </el-table-column>
            </el-table>
          </el-col>
        </el-row>
      </el-aside>

    </el-container>

  </el-container>
</template>

<script>
import { recArticle, readArticle } from "../api/article";
import { getRotationChartList } from "../api/rotationChart"
import {
  ClearToken,
  clearUsername,
  getAvatar,
  getToken,
  getUsername,
} from "../utils/auth";

//引入登录界面
import loginTemp from '../components/LoginTemp/login.vue'


export default {
  components: { loginTemp },

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

      //修改头像模态框
      visibleAvatar: false,
      // 默认选中首页
      activeIndex: '1',
      //用户名
      username: "",
      //头像地址
      avatarUrl: "",
      //上传头像地址
      uploadAvatarUrl: "http://localhost:5000/Files/uploadAvatar",

      islogin: false,
      drawer: false,

      formData: {
        id: 0,
        name: " ",
        password: " ",
        userPhoneNumber: " ",
        avatar: "",
        age: 0,
        gender: "",
        remarks: "",
        qqNumber: "",
        weChat: "",
      },

      //登录时间
      loginTime: "",

      /*  48小时阅读排行 */
      reader: [],
      /* 10天编辑推荐 */
      writer: [],
      /* 10天评论排行 */
      rebang: [],

      id: "",
      /*  最新文章封面图 */
      fits: [],

      RotationChartList: [],
      pager: {
        //每页显示条目个数
        pageSize: 20,
        currentPage: 1,
        totalSize: 0,
      },
    };
  },
  props: {
    dataHeight: {
      type: String,
      default: '520px'
    }
  },
  methods: {
    //判断是否有token
    isToken(tokenOK) {
      this.islogin = tokenOK;
    },
    // 菜单栏选择时触发
    handleSelect(key, keyPath) {
      console.log(key, keyPath);
    },
    //抽屉关闭时
    handleClose(done) {
      this.$confirm('信息未保存，确认要关闭吗？')
        .then(_ => {
          this.$refs.child.resetForm('userFrom');
          this.$refs.child.resetForm('signUpUserFrom');
          console.log(_);
          done();
        })
        .catch(_ => {
          console.log(_);
        });
    },
    //修改头像
    handlelUpdateAvatar() {
      this.visibleAvatar = true;
      this.formData.id = 0;
      this.formData.avatar = 'http://localhost:5000/files/avatarPath?avatarUrl=' + this.avatarUrl;

      console.log(this.formData);
    },

    //上传成功后
    handleAvatarSuccess(res, file) {
      this.imageUrl = URL.createObjectURL(file.raw);
      if (res.code == 200) {
        this.avatarUrl = "http://localhost:5000/Files/avatarPath?avatarUrl=" + res.data;
        this.formData.avatar = res.data;
        this.$message.success('图片上传成功!');
      } else {
        this.$message.error('头像上传失败，请重新上传！');
      }
    },
    //上传前
    beforeAvatarUpload(file) {
      const isJPG = file.type === 'image/jpeg';
      const isLt2M = file.size / 1024 / 1024 < 2;

      if (!isJPG) {
        this.$message.error('上传头像图片只能是 JPG 格式!');
      }
      if (!isLt2M) {
        this.$message.error('上传头像图片大小不能超过 2MB!');
      }
      return isJPG && isLt2M;
    },

    // 模态框取消按钮
    handleCancel() {
      console.log("1231231212");
      this.visibleAvatar = false;
    },
    // 模态框保存按钮
    handleSave() {
      this.visibleAvatar = false;
    },
    // --------------
    //获取当前时间
    currentTime(date) {
      var year = date.getFullYear();
      var month = date.getMonth() + 1;
      var day = date.getDate();
      var hours = date.getHours();
      var minutes = date.getMinutes();
      var seconds = date.getSeconds();
      this.loginTime = year + "-" + month + "-" + day + "\t" + hours + ":" + minutes + ":" + seconds
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
    RotationList() {
      getRotationChartList(this.pager)
        .then((res) => {
          if (res.code == 200) {
            this.RotationChartList = res.data.data;
            this.pager = res.data.pager;
          }
        })
        .catch(err => {
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
  computed: {
    ///获取头像
    avatarUrlFull() {
      return 'http://localhost:5000/files/avatarPath?avatarUrl=' + this.avatarUrl;
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
  //完成模版渲染后执行 
  mounted() {
    this.username = getUsername();
    this.avatarUrl = getAvatar();


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
    // 48小时文章阅读
    this.readed();

    this.RotationList();

    //获取当前时间
    this.currentTime(new Date());
  },
};
</script>

<style scoped lang="less">
html {
  height: 100%;
}

body {
  height: 100%;
  width: 100%;
  margin: 0;
  padding: 0;
}

.all_container {
  .el-menu {
    border-right: none;
  }

  .el-menu--horizontal>.el-menu-item {
    border-bottom: none;
    text-decoration: none;
  }

  .el-aside,
  .el-header,
  .el-main,
  .el-footer {
    color: #333;
    text-align: center;
    padding: 0;
    margin: 5px;
  }

  .el-header {
    display: flex;
    align-items: center;
    justify-content: space-between;
  }

  .el-main {
    background-color: rebeccapurple;

    .RotationCharImg {
      width: 100%;
      // height: 100vh;
    }
  }
}


/* 下拉菜单 */
.el-dropdown-link ,.el-menu--horizontal>.el-menu-item {
  /* cursor: pointer; */
  // color: white;
  font-size: 20px;
}

.avatar-uploader .el-upload {
  border: 1px dashed #d9d9d9;
  border-radius: 6px;
  cursor: pointer;
  position: relative;
  overflow: hidden;
}

.avatar-uploader .el-upload:hover {
  border-color: #409EFF;
}

.avatar-uploader-icon {
  font-size: 28px;
  color: #8c939d;
  width: 178px;
  height: 178px;
  line-height: 178px;
  text-align: center;
}

.avatar {
  width: 178px;
  height: 178px;
  display: block;
}


// ---------------------------------------------------------------------------------
// .el-carousel__item:nth-child(2n) {
//   background-color: #99a9bf;
// }

// .el-carousel__item:nth-child(2n+1) {
//   background-color: #d3dce6;
// }

// td {
//   padding: 1px !important;
//   text-align: center;
//   border-bottom: none;
// }

// .cell {
//   text-align: center;
// }



// a:hover {
//   color: #606266 !important;
// }

// .left {
//   margin-left: 5px;
//   width: calc(100vw - 347px);
//   float: left;
//   height: 400px;
// }

// .right {
//   float: left;
//   width: 320px !important;
//   background-color: rgba(207, 207, 207, 0.158);
//   /* height: 100% !important; */
// }

// .bottom {
//   float: left;
//   width: calc(100vw - 18px) !important;
//   padding: 0px;
//   background-color: rgba(207, 207, 207, 0.158);
// }

// .bottom .image {
//   width: 150px;
//   height: 150px;
// }


// .login {
//   width: 72px;
//   height: 40px;
//   float: right;
//   margin-right: 20px;
//   font-size: 17px;
//   margin-top: 10px !important;
// }



// .el-tabs {
//   height: 800px;
// }


// .el-tabs__item {
//   /* width: 297.8px !important; */
//   font-size: 40px;
//   text-align: center;
// }

// /* 48小时阅读排行 */
// .one {
//   margin-left: 0 !important;
//   background-color: rgba(207, 207, 207, 0.158);
//   text-align: left;
//   font-size: 21px;
//   border: 1.5px solid rgba(207, 207, 207, 0.158) !important;
//   border-radius: 7px;
//   margin-top: 50px;
// }

// .one p {
//   padding: 3px;
//   margin: 3px;
//   width: 160px !important;
//   float: left;
// }

// .one button {
//   float: right;
//   margin-right: 8px;
//   margin-top: 4px;
//   font-size: 16px;
//   background-color: rgba(207, 207, 207, 0);
// }

// .table1 {
//   border-bottom: none !important;
// }

// .el-table {
//   margin-top: 40px;
//   border-left: none !important;
//   border-right: none !important;
//   border-bottom: none !important;
//   font-size: 19px;
// }

// .el-table td,
// .el-table th.is-leaf {
//   border-bottom: none;
//   background-color: rgba(207, 207, 207, 0.158);
//   border-top: 1.3px solid rgba(207, 207, 207, 0.158) !important;
// }

// .el-table::before {
//   height: 0px;
// }

// .el-table__row {
//   padding: 0px !important;
//   height: 35px;
// }

// .gengduo1 {
//   margin-top: 5px;
// }

// /* 10天编辑推荐 */
// .two {
//   margin-left: 0 !important;
//   background-color: rgba(207, 207, 207, 0.158);
//   text-align: left;
//   font-size: 21px;
//   border: 1.5px solid rgba(207, 207, 207, 0.158) !important;
//   border-radius: 7px;
//   margin-top: 50px;
// }

// .two p {
//   padding: 3px;
//   margin: 3px;
//   width: 160px !important;
//   float: left;
// }

// .two button {
//   float: right;
//   margin-right: 8px;
//   margin-top: 4px;
//   font-size: 16px;
//   background-color: rgba(207, 207, 207, 0);
// }

// .table2 {
//   border-bottom: none !important;
// }

// /* 10天评论排行文章 */
// .three {
//   margin-left: 0 !important;
//   background-color: rgba(207, 207, 207, 0.158);
//   text-align: left;
//   font-size: 21px;
//   border: 1.5px solid rgba(207, 207, 207, 0.158) !important;
//   border-radius: 7px;
//   margin-top: 50px;
// }

// .three p {
//   padding: 3px;
//   margin: 3px;
//   width: 180px !important;
//   float: left;
// }

// .three button {
//   float: right;
//   margin-right: 8px;
//   margin-top: 4px;
//   font-size: 16px;
//   background-color: rgba(207, 207, 207, 0);
// }

// .table3 {
//   border-bottom: none !important;
// }

// /* row总样式 */
// // .el-footer {
// //   width: 100vm !important;
// //   margin-top: 30px !important;
// // }

// .el-row {
//   margin-top: auto;
//   width: 100vm;
//   margin-left: 0px !important;
// }

// /* 联系我们样式 */
// .EWM {
//   width: 100vm !important;
//   display: -webkit-flex;
//   display: flex;
//   -webkit-justify-content: space-around;
//   justify-content: space-around;
//   margin-top: 40px !important;
// }

// // .block {
// //   width: 150px !important;
// //   height: 350px !important;
// // }

// .image {
//   margin-top: auto;
// }

// .text {
//   margin-left: 35px;
// }

// .gongzhong {
//   height: 150px;
//   width: 150px !important;
// }

// .weixin {
//   height: 150px;
//   width: 150px !important;
// }

// .qq {
//   height: 150px;
//   width: 150px !important;
// }

// /* 底部 */
// // .el-footer .el-row {
// //   margin-top: 10px;
// // }

// .ICP {
//   text-align: center;
// }

// .Police {
//   text-align: center;
// }

// .el-image__error,
// .el-image__placeholder {
//   width: 100%;
//   height: 100%;
// }
</style>
