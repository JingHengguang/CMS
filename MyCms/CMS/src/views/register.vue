<template>
  <div class="register-wrapper">
    <el-card header="欢迎注册文章管理系统">
      <el-form
        :model="ruleForm2"
        status-icon
        :rules="rules"
        ref=""
        label-width="100px"
        class="demo-ruleForm"
        @keyup.enter.native="submitForm('ruleForm')"
      >
        <el-form-item label="用户名" prop="username">
          <el-input
            type="text"
            v-model.trim="ruleForm2.username"
            auto-complete="off"
            placeholder="请输入用户名"
          ></el-input>
        </el-form-item>
        <el-form-item label="密码" prop="password">
          <el-input
            type="password"
            v-model.trim="ruleForm2.password"
            auto-complete="off"
            placeholder="请输入密码"
          ></el-input>
        </el-form-item>
        <el-form-item label="确定密码" prop="checkPass">
          <el-input
            type="password"
            v-model.trim="ruleForm2.checkPass"
            auto-complete="off"
            placeholder="确认密码"
          ></el-input>
        </el-form-item>
        <el-form-item>
          <el-button type="info" @click="vilidatePass('ruleForm2')"
            >注册</el-button
          >
          <el-button type="info" @click="submitForm('ruleForm')"
            >取消</el-button
          >
          <!-- <p class="login">已有账号？立即登录</p> -->
        </el-form-item>
      </el-form>
    </el-card>
  </div>
</template>
<script>
import { registerList } from "../api/users";

export default {
  name: "Register",
  data() {
    return {
      ruleForm2: {
        username: "",
        password: "",
        checkPass: "",
      },
      rules: {
        username: [
          { required: true, message: "用户名不能为空", trigger: "blur" },
        ],
        password: [
          { required: true, message: "密码不能为空", trigger: "blur" },
        ],
        checkPass: [
          { required: true, message: "密码不能为空", trigger: "blur" },
        ],
      },
      UserRoleId: 2,
    };
  },
  methods: {
    vilidatePass: function () {
      let username = this.ruleForm2.username;
      let password = this.ruleForm2.password;
      let checkPass = this.ruleForm2.checkPass;
      //判断是否都有填写内容
      if (
        (username !== undefined && username.trim().length === 0) ||
        (password !== undefined && password.trim().length === 0) ||
        (checkPass !== undefined && checkPass.trim().length === 0)
      ) {
        this.$message({ type: "error", message: "用户名或密码或确认密码不能为空" });
        return;
      }
      //先进行判断是否有相同的账号 如果有提示该账号已经注册 否则注册账号成功
      let params = {
        username: this.ruleForm2.username,
        password: this.ruleForm2.password,
        checkPass: this.ruleForm2.checkPass,
        UserRoleId: this.UserRoleId,
      };
      registerList(params)
        .then((res) => {
          console.log(res);
          if (res.code === 200) {
            alert(res.msg);
            this.$router.push("/login");
          } else {
            alert(res.msg);
            this.ruleForm2.username='';
            this.ruleForm2.password='';
            this.ruleForm2.checkPass='';
          }
        })
        .catch((err) => {
          {
            console.log(err);
          }
        });
    },
    //取消注册
    submitForm: function () {
      this.$router.push("/login");
    },
  },
};
</script>

<style scoped>
.register-wrapper {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 100vh;
  background-color: #f7efef28;
  background-image: url("../assets/2.jpeg");
}

.title {
  font-size: 26px;
  line-height: 50px;
  font-weight: bold;
  margin: 10px;
  text-align: center;
  margin: auto;
}
.el-form-item {
  text-align: center;
}
.login {
  margin-top: 10px;
  font-size: 14px;
  line-height: 22px;
  color: #224e64f5;
  cursor: pointer;
  text-align: left;
  text-indent: 8px;
  width: 160px;
  margin-left: 100px;
}
.login:hover {
  color: #1d7bd3e1;
}
.code >>> .el-form-item__content {
  display: flex;
  align-items: center;
  justify-content: space-between;
}
.el-card,
.el-message {
  overflow: hidden;
  border-radius: 20px;
  background-color: rgba(215, 216, 236, 0.301);
  width: 500px;
  height: 400px;
  font-size: 25px;
}
.el-button + .el-button {
  margin-left: 150px;
}
.el-input__inner {
  background-color: rgba(255, 255, 255, 0.199);
}
</style>