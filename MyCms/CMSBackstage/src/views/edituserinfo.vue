<template>
  <div class="fullUser">
    <el-button type="success" plain icon="el-icon-arrow-left" @click="handleCancel">返回主页</el-button>
    <div class="userinfo">
      <!-- //基本信息 -->
      <el-form :model="formData" ref="ruleForm" label-width="80px" label-position="left"
        @keyup.enter.native="submitForm('ruleForm')">
        <el-form-item label="用户名:">
          <el-input v-model="formData.username" autocomplete="off" readonly></el-input>
        </el-form-item>
        <el-form-item label="旧密码:">
          <el-input v-model="formData.oldPassword" autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item label="新密码:">
          <el-input v-model.trim="formData.password" autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item label="确认密码:">
          <el-input v-model.trim="formData.passwordOk" autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item label="用户角色:">
          <el-input readonly value="超级管理员" v-if="formData.userRoleId == 1" autocomplete="off"></el-input>
          <el-input readonly value="管理员" v-if="formData.userRoleId == 2" autocomplete="off"></el-input>
          <el-input readonly value="普通用户" v-else-if="formData.userRoleId == 3" autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item label="备注:">
          <el-input v-model="formData.remarks" autocomplete="off"></el-input>
        </el-form-item>

      </el-form>

    </div>


    <div class="btn">
      <el-button type="primary" plain @click="handleSave" style="font-size: 20px;">
        确 定
        <i class="el-icon-arrow-right el-icon--right"></i>
      </el-button>
    </div>
  </div>



</template>


<script>
import { getUserId } from "../utils/auth";
import {modUser,getUserById } from "../api/users"
import router from "../../router";


export default {
  data() {
    return {
       

      //旧密码
      oldPassword: 0,

      //个人信息模态框数据
      formData: {
        currentId: 0,
        username: "",
        oldPassword: "",
        password: "",
        passwordOk: "",
        userRoleId: 0,
        avatarUrl: "",
        remarks: "",
      },
    };
  },

  methods: {
    //刷新用户信息
    refUserInfo(UserId) {
      //获取到当前用户的信息 
      getUserById(UserId)
        .then(({ data }) => {
          this.formData.username = data.username;
          this.formData.userRoleId = data.userRoleId;
          this.formData.remarks = data.remarks;
          this.currentAvatarUrl = data.avatarUrl;
          this.oldPassword = data.password;
          console.log("刷新成功");
        })
        .catch((err) => {
          console.log("调用失败：---\n" + err);
        })
    },
    ///个人信息取消按钮
    handleCancel() {
      router.push("/home");
    },
    ///个人信息确定按钮
    handleSave() {

      if (this.formData.password == "" || this.formData.passwordOk == "") {
        this.$message({ type: "error", message: "密码不可为空" });
        return;
      }

      if (this.oldPassword !== this.formData.oldPassword) {
        this.$message({ type: "error", message: "旧密码输入错误" });
        return;
      }
      if (this.formData.password.trim() !== this.formData.passwordOk.trim()) {
        this.$message({ type: "error", message: "两次密码输入不一致" });
        return;
      }
       console.log(this.formData);   
          
      modUser(this.formData.currentId, this.formData)
        .then((res) => {      
           router.push("/home");
           this.$message({ type: "success", message: res.msg });
        })
        .catch((err) => {
          console.log(err);
        });
         
    },
  },
  mounted() {
    //界面加载完获取用户的id 和 名称 
    // this.formData.username = getUsername();
    this.formData.currentId = getUserId();
    this.refUserInfo(this.formData.currentId);

  },
};
</script>

<style scoped>
.fullUser {
  height: 100%;
  background-color: plum;
  font-weight: bold;
}

.userinfo {
  display: flex;
  align-items: center;
  justify-content: center;

}

.el-form-item {
  margin: 0px;
  margin-top: 20px;
  width: 500px;
}

.btn {
  display: flex;
  justify-content: center;
  margin-top: 30px;

}

.btn>.el-button {
  width: 300px;
  text-align: center;
}


</style>