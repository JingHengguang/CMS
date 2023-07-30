<template>
  <div class="container" :class="{ active: isActive }">
    <!-- 注册 -->
    <div class="form-container sign-up-container">
      <el-form :model="signUpUserFrom" :rules="rules" status-icon ref="signUpUserFrom" class="form">
        <h2>sign up/注册</h2>
        <el-form-item prop="username">
          <el-input type="text" v-model="signUpUserFrom.username" placeholder="请输入你的用户名..." autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item prop="password">
          <el-input type="password" show-password v-model="signUpUserFrom.password" placeholder="请输入密码..."
            autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item prop="pwdOk">
          <el-input type="password" show-password v-model="signUpUserFrom.pwdOk" placeholder="请再输入一次密码..."
            autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item prop="userPhoneNumber">
          <el-input type="password" show-password v-model="signUpUserFrom.userPhoneNumber" placeholder="请输入手机号..."
            autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item>
          <el-button @click="signUpClick">sign up</el-button>
          <el-button @click="resetForm('signUpUserFrom')">Reset</el-button>
        </el-form-item>
      </el-form>
    </div>


    <!-- 登录 -->
    <div class="form-container sign-in-container">
      <el-form :model="userFrom" :rules="rules" status-icon ref="userFrom" class="form">
        <h2>sign in/登录</h2>
        <el-form-item prop="username">
          <el-input type="text" v-model="userFrom.username" placeholder="请输入你的用户名..." autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item prop="password">
          <el-input type="password" show-password v-model="userFrom.password" placeholder="请输入密码..."
            autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item prop="verifyCode">
          <el-input class="verify_css" placeholder="请输入验证码" v-model="userFrom.verifyCode"
            @keyup.enter.native="signInClick('userFrom')">
          </el-input>
          <!--关键 ↓-->
          <div style="display: inline-block; vertical-align: top; margin-left: 5px;" id="v_container"></div>
        </el-form-item>
        <el-form-item>
          <el-button @click="signInClick">sign in</el-button>
          <el-button @click="resetForm('userFrom')">Reset</el-button>
        </el-form-item>

      </el-form>
    </div>

    <!--滑动覆盖 -->
    <div class="overlay_container">
      <div class="overlay">
        <!-- overlay left -->
        <div class="overlay_panel overlay_left_container">
          <h2>welcome back!</h2>
          <p>To keep connected with us please login with your personal info</p>
          <button @click="slideClick" id="sign-in">sign in</button>
        </div>
        <!-- overlay right -->
        <div class="overlay_panel overlay_right_container">
          <h2>hello friend!</h2>
          <p>Enter your personal details and start journey with us</p>
          <button @click="slideClick" id="sign-up">sign up</button>
        </div>
      </div>
    </div>




  </div>
</template>
  
<script>
import { GVerify } from "../../utils/VerifyCode"
import { login } from "../../api/users"
import { SetToken } from "../../utils/auth"

export default {
  data() {
    return {
      isActive: false,
      //登录
      userFrom: {
        username: "",
        password: "",
        verifyCode: ""
      },
      rules: {
        username: [{ required: true, message: "用户名不能为空", trigger: "blur" }],
        password: [{ required: true, message: "密码不能为空", trigger: "blur" },],
        pwdOk: [{ required: true, message: "确认密码不能为空", trigger: "blur" },],
        userPhoneNumber: [{ required: true, message: "请输入手机号", trigger: "blur" },],
        verifyCode: [{ required: true, message: '请输入验证码', trigger: 'blur' }]
      },
      verifyCode: null,

      //注册
      signUpUserFrom: {
        username: "",
        password: "",
        pwdOk: "",
        userPhoneNumber: ""
      }

    };

  },
  methods: {
    //注册和登录切换按钮
    slideClick() {
      this.isActive = !this.isActive;
    },
    //登录
    signInClick() {
      // 获取验证码
      var verifyCode = this.userFrom.verifyCode
      var verifyFlag = this.verifyCode.validate(verifyCode)
      if (!verifyFlag) {
        this.$notify.error({
          title: '系统提示',
          message: '验证码输入错误'
        })
        return;
      }

      login(this.userFrom).then(res => {
        //判断账号密码正确
        if (res.code === 200 && res.data.user.isActived === true) {
          localStorage.setItem("username", res.data.user.username);
          localStorage.setItem("userRoleId", res.data.user.userRoleId);
          localStorage.setItem("avatar", res.data.user.avatar);
          SetToken(res.data.token, res.data.refreshToken);
          this.$message({ type: "success", message: res.msg });
          // this.$router.push("/index/detailed");
          this.$router.go(0)
        } else {
          this.$notify.error({
            title: '系统提示',
            message: res.msg
          })
        }
      }).catch(
        err => {
          console.log(err);
        }
      )

    },
    //注册
    signUpClick() {

    },
    ///重置
    resetForm(formName) {
      this.$refs[formName].resetFields();
    }
  },
  mounted() {
    // 随机播放帧数
    this.verifyCode = new GVerify('v_container');
  }
};
</script>
  
<style scoped lang="less">
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}

body {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  background-color: #f6f5f7;
}

h2 {
  margin-bottom: 10px;
  font-size: 32px;
  text-transform: capitalize;
}


.form-container {
  position: absolute;
  top: 0;
  width: 50%;
  height: 100%;
  background-color: white;
  transition: all 0.6s ease-in-out;
  text-align: center;
}

.form {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  height: 100%;
  width: 100%;
  padding: 0 50px;

  .verify_css {
    width: 200px;

  }

  /deep/.el-input__inner {
    width: 100%;
    padding: 12px;
    background-color: #eee;
    border: none;
  }

  .el-form-item {
    width: 25vw;
    margin-top: 20px;
  }

}

// input {
//   width: 100%;
//   margin: 8px 0;
//   padding: 12px;
//   background-color: #eee;
//   border: none;
// }



.forget-password {
  display: inline-block;
  height: 20px;
  text-decoration: none;
  color: #bbb;
  text-transform: capitalize;
  font-size: 12px;
}

.forget-password:hover {
  color: lightslategray;
  border-bottom: 2px solid #ff4b2b;
}

button {
  background: #ff4b2b;
  padding: 10px 50px;
  border: 1px solid transparent;
  border-radius: 20px;
  text-transform: uppercase;
  color: white;
  margin-top: 10px;
  outline: none;
  transition: transform 80;
}

button:active {
  transform: scale(0.95);
}

.overlay_container {
  position: absolute;
  top: 0;
  width: 50%;
  height: 100%;
  z-index: 100;
  right: 0;
  overflow: hidden;
  transition: all 0.6s ease-in-out;
}

.overlay {
  position: absolute;
  width: 200%;
  height: 100%;
  left: -100%;
  background-color: #ff4b2b;
}

.overlay_panel {
  position: absolute;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  width: 50%;
  height: 100%;
  color: white;
  padding: 0 40px;
  text-align: center;
}

.overlay_panel button {
  background-color: transparent;
  border: 1px solid white;
}

.overlay_panel p {
  font-size: 12px;
  margin: 10px 0 15px 0;
}

.overlay_right_container {
  right: 0;
}

.container.active .sign-up-container {
  transform: translateX(100%);
  z-index: 5;
}

.container.active .sign-in-container {
  transform: translateX(100%);
}

.container.active .overlay_container {
  transform: translateX(-100%);
}

.container.active .overlay {
  transform: translateX(50%);
}
</style> 