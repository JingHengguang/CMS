<template>
  <div class="loginContainer">
    <el-card header="欢迎登录文章管理系统">
      <el-form :model="formData" status-icon :rules="rules" ref="ruleForm" label-width="100px" class="demo-ruleForm"
        label-position="left" @keyup.enter.native="submitForm('ruleForm')">

        <el-form-item label="用户名" prop="username">
          <el-input type="text" v-model="formData.username" autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item label="密码" prop="password">
          <el-input type="password" v-model="formData.password"></el-input>
        </el-form-item>
        <el-form-item>
          <el-button class="loginBtn" type="primary" @click="submitForm('ruleForm')">登录</el-button>

          <!-- <el-button type="success" @click="resetForm('ruleForm')">注册</el-button> -->

        </el-form-item>
      </el-form>
    </el-card>
    <!-- <el-button @click="resetForm('ruleForm')" style=";">注册</el-button>    -->
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
              // console.log(res);
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

<style>
.loginContainer {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 100vh;
  width: 100vw;


  background-image: url("../assets/login3.webp");
  background-repeat: no-repeat;
  background-size: 100%, 100%;
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

.loginBtn {
  width: 200px;
  background: green;
}

.el-form-item__label {
  font-size: 20px;
}

.demo-ruleForm {
  /* display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center; */
  text-align: center;
  margin: auto;
}
</style>