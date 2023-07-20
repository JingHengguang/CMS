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
        @keyup.enter.native="submitForm('ruleForm')">
          
    <el-form-item label="设置头像:" >
      <el-upload       
        list-type="picture-card" 
        :action="uploadAvatarUrl" 
        :auto-upload="true" 
        :limit="imgLimit" 
        :multiple="false" 
        :show-file-list="true" 
        :on-preview="handlePictureCardPreview" 
        :on-remove="handleRemove" 
        :on-success="handleAvatarSuccess" 
        :before-upload="beforeAvatarUpload" 
        :on-exceed="handleExceed" 
        :on-error="imgUploadError"
        :hidden="avatarHidden"
        >
     
          <i class="el-icon-plus"></i>
      </el-upload>
      </el-form-item>

            
        <el-form-item label="用户名:" prop="username">
          <el-input
            type="text"
            v-model.trim="ruleForm2.username"
            auto-complete="off"
            placeholder="请输入用户名"
          ></el-input>
        </el-form-item>

        <el-form-item label="密码:" prop="password">
          <el-input
            type="password"
            v-model.trim="ruleForm2.password"
            auto-complete="off"
            placeholder="请输入密码"
          ></el-input>
        </el-form-item>

        <el-form-item label="确定密码:" prop="checkPass">
          <el-input
            type="password"
            v-model.trim="ruleForm2.checkPass"
            auto-complete="off"
            placeholder="确认密码"
          ></el-input>
        </el-form-item>

       <el-form-item label="备注:" prop="remarks">
          <el-input
            type="text"
            v-model.trim="ruleForm2.remarks"
            auto-complete="off"
            placeholder="请输入备注信息"
          ></el-input>
        </el-form-item>

        <el-form-item>
          <el-button type="primary" @click="vilidatePass('ruleForm2')"
            >注册</el-button
          >
          <el-button type="warning" @click="submitForm('ruleForm')"
            >取消</el-button
          >
        </el-form-item>

      </el-form>

      
    </el-card>
      <el-dialog :visible.sync="dialogVisibleImg">
          <img width="100%" :src="dialogImageUrl" alt="">
        </el-dialog>
    
  </div>
</template>
<script>
import { registerList } from "../api/users";

export default {
  name: "Register",
  data() {
    return {
    

       ///头像是否隐藏
       avatarHidden:false,

      uploadAvatarUrl: 'http://localhost:5000/files/uploadAvatar/', //上传图片接口地址
      imgLimit: 1,//最多可上传1张图片
      dialogImageUrl: '',//点击图片查看，本地图片路径
      dialogVisibleImg:false,//是否预览图片

      ruleForm2: {
        username: "",
        password: "",
        checkPass: "",
        remarks:"",
        UserRoleId: 3,
        avatarUrl: "",
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
      
    };
  },
  methods: {
     // 点击查看预览图片
 handlePictureCardPreview(file) { //预览图片时调用
   console.log("预览图片");
     
  this.dialogImageUrl = file.url;
  this.dialogVisibleImg = true;
 },
  // 点击删除头像
 handleRemove(file, fileList) { //移除图片
     console.log("删除头像");
     console.log(file, fileList);
     this.ruleForm2.avatarUrl="";   
 },
   //图片上传成功
  handleAvatarSuccess(res, file) {
   this.dialogImageUrl = URL.createObjectURL(file.raw);
    if (res.code == 200) {
     this.$message.success('图片上传成功!'); 
    //  this.avatarHidden=true;
     this.ruleForm2.avatarUrl=res.data;

    } else {
     this.$message.error('图片上传失败，请重新上传！');
    }
   },
 // 图片上传之前事件
 beforeAvatarUpload(file) { //文件上传之前调用做一些拦截限制
       const isJPG = file.type === "image/jpeg";
       const isLt2M = file.size / 1024 / 1024 < 2;
      if (!isJPG) {
        this.$message.error("上传头像图片只能是 JPG 格式!");
      }
      if (!isLt2M) {
        this.$message.error("上传头像图片大小不能超过 2MB!");
      }
      return isJPG && isLt2M;
  },
     //图片上传超过数量限制
   handleExceed(file, fileList) { 
    console.log(fileList);
    this.$message.error('只可以上传一张头像哦，请删除前一张');
   },
      //图片上传失败调用
   imgUploadError(err, file, fileList) { 
    console.log(err);
    console.log(file);
    console.log(fileList);
    this.$message.error('上传图片失败!');
   },


  //  注册按钮
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

      registerList(this.ruleForm2)
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
  width: 100vw;
  background-color: #D2691E;
  /* background-image: url("../assets/2.jpeg"); */
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
  height: auto;
  font-size: 25px;
}
.el-button + .el-button {
  margin-left: 150px;
}
.el-input__inner {
  background-color: rgba(255, 255, 255, 0.199);
}
</style>