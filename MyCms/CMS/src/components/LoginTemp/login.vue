<template>
  <div class="container" :class="{ active: isActive }">
    <!-- 注册 -->
    <div class="form-container sign-up-container">
      <div class="form">
        <h2>sign up/注册</h2>
        <input type="text" name="username" id="username" placeholder="Username..." />
        <input type="password" name="password" id="password" placeholder="Password..." />
        <button class="signUp">sign up</button>
      </div>
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
        </el-form-item>
      </el-form>
    </div>



    <!-- <div class="form-container sign-in-container">
      <div class="form">
        <h2>sign in/登录</h2>
        <input type="text" placeholder="请输入你的邮箱...">
        <input type="password" placeholder="Password...">
        <button class="signIn">sign in</button>
      </div>
    </div> -->

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

export default {
  data() {

    return {
      container: document.getElementsByClassName('container')[0],
      signIn: document.getElementById('sign-in'),
      signUp: document.getElementById('sign-up'),
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
        verifyCode: [{ required: true, message: '请输入验证码', trigger: 'blur' }]
      },
      verifyCode: null,

    };



  },
  methods: {
    //注册和登录切换按钮
    slideClick() {
      this.isActive = !this.isActive;
    },
    //登录
    signInClick() {
      console.log(this.userFrom);

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