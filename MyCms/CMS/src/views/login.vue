<template>
  <div class="loginContainer">
    
    <el-card header="欢迎登录文章管理系统">
      <el-form
        :model="formData"
        status-icon
        :rules="rules"
        ref="ruleForm"
        label-width="100px"
        class="demo-ruleForm"
        label-position="left"
        @keyup.enter.native="submitForm('ruleForm')"
      >
  
        <el-form-item label="用户名"  prop="username">
          <el-input      
            type="text"
            v-model="formData.username"
            autocomplete="off"
          ></el-input>
        </el-form-item>
        <el-form-item label="密码" prop="password">
          <el-input         
            type="password"
            v-model="formData.password"            
          ></el-input>
        </el-form-item>
        <el-form-item>
          <el-button class="loginBtn" type="primary" @click="submitForm('ruleForm')"
            >登录</el-button
          >
         
           
          <el-button type="success" @click="resetForm('ruleForm')">注册</el-button>
           
        </el-form-item>
      </el-form>
    </el-card>
 
  <!-- <div class="login">
    <img :src="imgSrc" width="100%" height="100%" alt="" />
  <div class="loginPart">
    <h2>用户登录</h2>
  <el-form>
    <div class="inputElement">
      <el-input v-model="username" placeholder="请输入用户名/手机号 "></el-input>
    </div>
    <div class="inputElement">
      <el-input v-model="password" placeholder="请输入密码 "></el-input>
    </div>
    <div>
    <el-button type="primary" >登录</el-button>
    </div>
    <div style="text-align: right;color: white;">
        <el-link type="warning">没有账号？去注册</el-link>
    </div>
  </el-form>
  </div>
  </div> -->

 </div>
</template>


<script>
import { login } from "../api/users";
import { SetToken } from "../utils/auth";

export default {
  data() {
    return {
      formData: {
        username: "",
        password: "",
      },
      rules: {
        username: [
          { required: true, message: "用户名不能为空", trigger: "blur" },
        ],
        password: [
          { required: true, message: "密码不能为空", trigger: "blur" },
        ],
      },
    };
  },
  methods: {
    submitForm(formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
          login(this.formData)
            .then((data) => {
              let res = data;
              console.log(res);

             if (res.code === 200) {
                  this.$message({ type: "success", message: res.msg });
                  localStorage.setItem("username", data.data.user.username);
                  localStorage.setItem("id", data.data.user.id);              
                  SetToken(res.data.token, res.data.refreshToken);
         
                  this.$router.push("/index");
                } else {
                  this.$message({ type: "error", message: res.msg });
                  console.log("error submit!!");
                  return false;
                }
                 
            })
            .catch((err) => {
              console.log(err);
              this.$message({ type: "error", message: err });
            });
        }
      });
    },
    resetForm(formName) {
      this.$refs[formName].resetFields();
      this.$router.push("/register");
    },
  },
};
</script>

<style scoped>

.loginPart{
    position:absolute;
    /*定位方式绝对定位absolute*/
    top:50%;
    left:50%;
    /*顶和高同时设置50%实现的是同时水平垂直居中效果*/
    transform:translate(-50%,-50%);
    /*实现块元素百分比下居中*/
    width:450px;
    padding:50px;
    background: rgba(0,0,0,.5);
    /*背景颜色为黑色，透明度为0.8*/
    box-sizing:border-box;
    /*box-sizing设置盒子模型的解析模式为怪异盒模型，
    将border和padding划归到width范围内*/
    box-shadow: 0px 15px 25px rgba(0,0,0,.5);
    /*边框阴影  水平阴影0 垂直阴影15px 模糊25px 颜色黑色透明度0.5*/
    border-radius:15px;
    /*边框圆角，四个角均为15px*/
  }
  .loginPart h2{
    margin:0 0 30px;
    padding:0;
    color: #fff;
    text-align:center;
    /*文字居中*/
  }
  .loginPart .inputbox{
    position:relative;
  }
  .loginPart .inputElement input{
    width: 100%;
    padding:10px 0;
    font-size:16px;
    color:#fff;
    letter-spacing: 1px;
    /*字符间的间距1px*/
    margin-bottom: 30px;
    border:none;
    border-bottom: 1px solid #fff;
    outline:none;
    /*outline用于绘制元素周围的线
    outline：none在这里用途是将输入框的边框的线条使其消失*/
    background: transparent;
    /*背景颜色为透明*/
  }

  .login{
    width:100%;
    height:100%;
  }
/* ------------- */
.loginContainer {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 100vh;
  width: 100vw;
  /* background-color: #dabebe; */
  
  background-image: url("../assets/login3.webp");
  background-repeat: no-repeat;
  background-size: 100%,100%;
  background-attachment: fixed;
}
.el-card__header {
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 25px;

}
.el-card__body {
  width: 450px;
}
.el-card,
.el-message {
  border-radius: 20px;
  background-color: rgba(46, 55, 182, 0.3);
}
.el-input__inner {
  background-color: rgba(255, 255, 255, 0.5);
}
.loginBtn{
       width: 200px;
       background: green;
}
*{
    color: black;
      font-weight: bold;
}
.el-form-item__label{
  font-size: 20px;
}
.demo-ruleForm{
  /* display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center; */
  text-align: center;
    margin: auto;
}

</style>