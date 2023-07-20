<template>
  <el-form ref="form" :model="form" label-width="80px">
    <el-form-item label="文章标题" style="width: 500px">
      <el-input v-model="form.title"></el-input>
    </el-form-item>
    <el-form-item label="文章类型" style="margin-top: 30px">
      <el-select v-model="form.typeId" placeholder="请选择文章类型">
        <el-option v-for="(type, index) in types" :key="index" :label="type.type" :value=type.id></el-option>
      </el-select>
    </el-form-item>
    <el-form-item label="封面图" style="margin-top: 66px">
      <el-upload :headers="uploadActionHeaders" :action="uploadActionUrl" :on-success="handleAvatarSuccess"
        :on-preview="handlePreview" :on-remove="handleRemove" :url="dialogFormAvatarFullUrl"
        :before-upload="beforeAvatarUpload" list-type="picture" :multiple="false">
        <el-button type="primary" v-once v-if="!hiddenUploadBtn">点击上传
        </el-button>
      </el-upload>
    </el-form-item>
    <el-form-item label="文章简介" style="margin-top: 50px">
      <el-input type="textarea" v-model="form.synopsis"></el-input>
    </el-form-item>

    <div style="margin-top: 50px">
        <div id="div1"></div>
    </div>
    
    <el-form-item>
      <el-button type="primary" @click="onSubmit">立即发布</el-button>
      <el-button>取消</el-button>
    </el-form-item>
  </el-form>
</template>

<script>
import { addArticle } from "../api/article";
import { getUsername } from "../utils/auth";
import { getList } from "../api/types";
import E from "wangeditor";

export default {
  data() {
    return {
      hiddenUploadBtn: false,
      types: [],
      form: {
        user: "",
        title: "",
        typeId: "",
        synopsis: "",
        words: "",
      },
      pager: {
        //每页显示条目个数
        pageSize: 100,
        currentPage: 1,
        totalSize: 0,
      },
      dialogFormAvatarFullUrl: "",
    };
  },
  methods: {
    //刷新类型表
    fetchData() {
      getList(this.pager)
        .then((res) => {
          if (res.code == 200) {
            this.types = res.data.data;
            this.pager = res.data.pager;
            console.log(this.types);
          }

        })
        .catch((err) => {
          console.log(err);
        });

    },
    // 文件上传前
    beforeAvatarUpload(file) {
      // console.log(this.hiddenUploadBtn);

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
    // 图片上传成功后
    handleAvatarSuccess(res, file) {
      console.log(res);
      // console.log(file);

      // 显示使用完整路径
      let fullPath = URL.createObjectURL(file.raw);

      this.dialogFormAvatarFullUrl = fullPath;

      //隐藏上传按钮
      this.hiddenUploadBtn = true;
    },

    //删除文件时
    handleRemove(file, fileList) {
      console.log(file, fileList);
      this.fileLength = 0;

      //显示上传按钮
      this.hiddenUploadBtn = false;
    },

    // 预览文件时
    handlePreview(file) {
      console.log(file);
    },
    //发布文章
    onSubmit() {
      this.form.words = this.editor.txt.html();

      console.log(this.form);
      if (
        this.form.words != "" &&
        this.form.title != "" &&
        this.form.synopsis != "" &&
        new Number(this.form.typeId) > 0
      ) {


        addArticle(this.form)
          .then((res) => {
            //提交成功  发表文章成功
            //alert(res.msg);
            this.$message({ type: "success", message: res.msg });
            this.$router.push("/artist/articleinfo");

            console.log(res.data);
          })
          .catch((err) => {
            console.log(err);
          });
      } else {
        alert("注意你有未填写的内容哦!");
      }
    },
  },

  computed: {
    uploadActionHeaders() {
      return {
        Authorization: "Bearer " + "token",
      };
    },
    uploadActionUrl() {
      let baseUrl = "http://localhost:5000/";
      let url = "files/uploadfiles";
      // console.log("上传封面图："+baseUrl + url);
      return baseUrl + url;
    },

  },

  mounted() {
    const editor = new E(`#div1`);

      // 配置 server 接口地址
    editor.config.uploadImgServer =
      "http://localhost:5000/files/uploadWangEditor";

    editor.config.debug = true; // 开启debug模式

    editor.config.uploadImgMaxSize = 2 * 1024 * 1024; // 2M

    editor.config.height = 500;

    editor.config.uploadImgHooks = {
      // 图片上传并返回结果，但图片插入错误时触发
      fail: function (xhr, editor, result) {
        console.log(result);
      },
      success: function (xhr, editor, result) {
        // 图片上传并返回结果，图片插入成功之后触发
        console.log(result, "success");
      },
    };

    editor.config.uploadImgTimeout = 50000;

    editor.config.onchange = (newHtml) => {
      this.form.word = newHtml;
      console.log(this.form.word);
    };
    // 创建编辑器
    editor.create();

    this.editor = editor;
        
    // editor.config.onchange = (newHtml) => {
    //   this.editorData = newHtml;
    //   console.log(this.editorData);
    // };
    // // 创建编辑器
    // editor.create();

    // this.editor = editor;

    //界面加载完获取用户的 名称 
    this.form.user = getUsername();
    // console.log(this.form);
    // console.log(this.pager);

    this.fetchData();

  },
};
</script>

<style >
.hiddenBtn {
  pointer-events: none;
}

.el-form-item__content {
  line-height: 0;
}
</style>