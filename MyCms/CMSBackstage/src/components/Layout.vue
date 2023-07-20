<template>
  <div>
    <el-container>
      <el-aside :style="asideStyle">
        <Navbar :menus="menus" :collapse="isCollapse"></Navbar>
      </el-aside>
      <el-container>
        <!-- 头部 -->
        <el-header class="layHeader">
          <span> 后台管理系统 </span>

          <div>
            <img class="AvatarFile" :src="AvatarUrl" alt="头像加载失败...">

            <el-dropdown style="font-size: 30px;color: green">
              <span>
                {{ formData.username }}<i class="el-icon-arrow-down el-icon--right"></i>
              </span>
              <el-dropdown-menu slot="dropdown">
                <el-dropdown-item @click.native="handIeUpdateAvatar">修改个人信息</el-dropdown-item>
                <el-dropdown-item @click.native="handlelogout">注销登录</el-dropdown-item>
              </el-dropdown-menu>
            </el-dropdown>
          </div>

        </el-header>

        <!-- 中间主体 -->
        <el-main class="layMain">
          <router-view></router-view>
        </el-main>

        <!-- 底部 -->
        <el-footer class="layFooter">
          <svg class="icon" aria-hidden="true">
            <use xlink:href="#icon-beian"></use>
          </svg>
          Copyright 2021-2021 lucas信息科技 All rights Reserved.
        </el-footer>
      </el-container>
    </el-container>

    <!-- //注销登录 -->
    <el-dialog title="提示" :visible.sync="dialogVisible" width="30%">
      <span>你确定要注销登录吗?</span>
      <span slot="footer" class="dialog-footer">
        <el-button @click="dialogVisible = false">取 消</el-button>
        <el-button type="primary" @click="confimlogout">确 定</el-button>
      </span>
    </el-dialog>

    <el-dialog title="修改头像" :visible.sync="editAvatar" width="50%">


    </el-dialog>


  </div>
</template>

<script>
import Navbar from "./Navbar/Index.vue";
import routes from "../../router/routes";
import { getUserById } from "../api/users"
import { getUserId, LogOut } from "../utils/auth";

export default {
  components: { Navbar },

  data() {
    return {
      baseUrl: "http://localhost:5000/files/avatarPath?avatarUrl=",

      //左边页签
      PagesignList: [],
      //默认是折叠的
      isCollapse: false,

      dialogVisible: false,

      //头像模态框是否打开
      editAvatar: false,

      //旧密码
      oldPassword: 0,

      //个人信息模态框数据
      formData: {
        currentId: 0,
        avatarUrl: "UploadFiles\\DefaultImage\\default.jpg",
        username: "",
        userRoleId: 0,
        remarks: "",
      },
    };
  },
  methods: {
    //点击事件
    handleCollapse() {
      this.isCollapse = !this.isCollapse;
    },

    //修改个人信息
    handIeUpdateAvatar() {
      this.$router.push('/userinfo/edituserinfo');
    },

    //注销登录
    handlelogout() {
      this.dialogVisible = true;
    },
    //确定注销登录按钮
    confimlogout() {
      console.log("确定注销");
      LogOut();
      this.$router.push("/login");
    },

    //展示左边页签
    resolve(arr, parentPath = "") {
      let newArr = [];
      arr.forEach((route) => {
        // 当第一次parentPath='' 所以找不到任何元素 所以返回-1
        parentPath =
          parentPath && parentPath.lastIndexOf("/") !== parentPath.length - 1
            ? parentPath + "/"
            : parentPath;
        // console.log(parentPath);
        //assign 将源头数据给目标源
        let obj = Object.assign({}, route);
        obj.path = parentPath + obj.path;
        // console.log(obj.path);
        //判断是否有子路由 如过有递归处理子节点
        if (obj.children && obj.children.length > 0) {
          obj.children = this.resolve(obj.children, obj.path);
        }

        if (obj.name == 'user' ||
          obj.name == 'system' ||
          obj.name == 'types' ||
          obj.name == 'rotationchartinfo'||
          obj.name=='pictureinfo') {

          // 判断当前登录人是否为超级管理员
          if (this.formData.userRoleId == 1) {
            obj.meta.hidden = false;
            // console.log("是超级管理员");
          } else {
            obj.meta.hidden = true;
            // console.log("是管理员");
          }

        }

        //如果菜单是隐藏 当前路由不作为导航栏显示 只将其children加入到菜单栏
        if (obj.meta.hidden) {
          //如果存在子属性 将所有子节点添加到数组里 否则 不做处理
          if (obj.children && obj.children.length > 0) {
            obj.children.forEach((item) => {
              let temp = Object.assign({}, item);
              newArr.push(temp);
            });
          }
        } else {
          newArr.push(obj);
        }
      });
      return newArr;
    },
    //刷新用户信息
    refUserInfo() {
      //获取到当前用户的信息 
      getUserById(this.formData.currentId)
        .then((res) => {
          if (res.code == 200) {
            this.formData.username = res.data.username;
            this.formData.userRoleId = res.data.userRoleId;
            this.formData.avatarUrl = res.data.avatarUrl;

            this.PagesignList = this.resolve(routes);
            console.log("刷新成功layout");
          }
        })
        .catch((err) => {
          console.log("调用失败：---\n" + err);
        })
    },
  },
  computed: {
    menus() {
      return this.PagesignList;
    },

    //头像地址'
    AvatarUrl() {
      var avatarFullUrl = this.baseUrl + this.formData.avatarUrl;
      return avatarFullUrl;
    },
    asideStyle() {
      //如果是折叠auto 不是左侧导航栏width:200px
      return {
        width: this.isCollapse ? "auto" : "200px",
      };
    },
    iconDeg() {
      // 如果是折叠 icon-deg样式属性
      return {
        "icon-deg": this.isCollapse ? true : "",
      };
    },

  },
  mounted() {
    //界面加载完获取用户的id
    this.formData.currentId = getUserId();
    this.refUserInfo();


  },
  //监听路由去向
  watch: {
    '$route'(to, from) {
      if (to.path == "/") {
        console.log(to);
        console.log(from);
        if (this.$route.params.avatarUrl != "" && this.$route.params.avatarUrl != undefined) {
          this.formData.avatarUrl = this.$route.params.avatarUrl;
        }
      }
    }
  },


};
</script>

<style>
/* 中间有空隙解决 */
.el-menu {
  border-right: none;
}

/* 设置左边导航栏颜色 */
.el-aside {
  background-color: #673AB7;

}

/* 设置头部 */
.layHeader {
  height: 60px;
  line-height: 60px;
  /* background-color: #673AB7; */
  /* color: lightskyblue; */
  font-size: 40px;
  font-weight: bold;
  display: flex;
  align-items: center;
  justify-content: space-between
    /* font-family: "圆润"; */
}

/* 下拉菜单 */
.layHeader>div {
  cursor: pointer;
  font-size: 10px;
  /* color: chartreuse; */
  /* font-weight: bold; */
}

/* 右边主体部分 */
.layMain {
  background-color: #e9eef3;
  transition: background-color 0.25s ease;
  /* 设置自适应高度 */
  min-height: calc(100vh - 120px);
}

.layFooter {
  background-color: #e9eef3;
  display: flex;
  align-items: center;
  justify-content: center;
}


.el-dropdown {
  font-size: 30px;
  margin-left: 20px;
  color: green
}



body>.el-container {
  margin-bottom: 0px;
}


/* 设置翻转 */
.icon-deg {
  transform: rotate(-180deg);
}


.el-icon-arrow-down:before {
  font-size: 25px;
}

.AvatarFile {
  border-radius: 50%;
  width: 60px;
  height: 60px;
  margin-top: 19px;
}

.updateAvatar {
  display: flex;
  text-align: center;
}
</style>