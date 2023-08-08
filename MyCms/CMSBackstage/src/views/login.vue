<template>
  <div class="loginContainer">
    <el-card header="欢迎登录文章管理系统" style="text-align: center; ">
      <el-form  style="font-size: 20px;" :model="formData" status-icon :rules="rules" ref="ruleForm" 
      label-width="100px"   
      @keyup.enter.native="submitForm('ruleForm')">
        
        <el-form-item label="账  号:" prop="username">
          <el-input type="text" v-model="formData.username" autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item label="密  码:" prop="password">
          <el-input type="password" show-password v-model="formData.password"></el-input>
        </el-form-item>
        <el-form-item>
          <el-button class="loginBtn" type="primary" @click="submitForm('ruleForm')">登录</el-button>
          <!-- <el-button type="success" @click="resetForm('ruleForm')">注册</el-button> -->

        </el-form-item>
      </el-form>
    </el-card>

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
            .then((res) => {
              if (res.code === 200 && res.data.user.userRoleId!=3) {
                  localStorage.setItem("username", res.data.user.username);
                  localStorage.setItem("id", res.data.user.id);
                  localStorage.setItem("userRoleId", res.data.user.userRoleId);
                  SetToken(res.data.token, res.data.refreshToken);
                  this.$message({ type: "success", message: res.msg });
                  this.$router.push("/home");        

              } else {
                this.$message({ type: "error", message: "用户登录失败" });
                console.log("密码或账号错误");
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
.loginContainer {
  display: flex;
  align-items: center;
  justify-content:end ;
  height: 100vh;
  width: 100vw;
  background-image: url("../assets/login.jpg");
  background-repeat: no-repeat;
  background-size: 100%, 100%;
  background-attachment: fixed;
}

.el-card__header {
  display: flex;
  align-items: center;
  justify-content: center;
}

.el-card__body {
  width: 450px;
}

.el-card,
.el-message {
  border-radius: 20px;
  background-color: rgba(194, 197, 236, 0.3);
  border-color:  black;
  border-width: 2px;
  margin-right: 40px;
}

.loginBtn {
  width: 200px;
  background: black;
}
*{
  font-size: 20px;
}
</style>