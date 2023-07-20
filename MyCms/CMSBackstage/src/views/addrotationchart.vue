
<template>

  <div>

    <el-button type="success" plain icon="el-icon-arrow-left" @click="handleCancel">返回上一页</el-button>

    <div class="userinfo">

      <!-- 头像 -->
      <el-form label-width="100px">
        <el-form-item label="原轮播图:" v-if="isAdd">
          <img class="updateAvatarFile" :src="'http://localhost:5000/files/RotationChart?RotationChartUrl='
          + oldPicture" alt="头像加载失败...">
        </el-form-item>

        <el-form-item label="新轮播图：" style="margin-top: 20px">
          <el-upload list-type="picture-card" :action="uploadRotationChartUrl" :auto-upload="true" :limit="limit"
            :multiple="false" :show-file-list="true" :on-preview="handlePictureCardPreview" :on-remove="handleRemove"
            :on-success="handleAvatarSuccess" :before-upload="beforeAvatarUpload" :on-exceed="handleExceed"
            :on-error="imgUploadError">

            <i class="el-icon-plus"></i>
          </el-upload>
        </el-form-item>

        <el-form-item label="备    注：" style="margin-top: 20px; width: 400px;">
          <el-input v-model="formData.remarks" autocomplete="off"></el-input>
        </el-form-item>

        <el-form-item>
          <el-dialog :visible.sync="dialogVisibleImg">
            <img width="100%" :src="dialogImageUrl" alt="">
          </el-dialog>
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


import router from "../../router";
import { addRotationChart,modRotationChart } from "../api/rotationChart"
export default {
  data() {
    return {
      //上传图片接口地址
      uploadRotationChartUrl: 'http://localhost:5000/files/uploadRotationChart/',
      //点击图片查看，本地图片路径
      dialogImageUrl: '',
      //点击查看图片弹框
      dialogVisibleImg: false,
      limit: 1,

      //新上传的轮播图
      oldPicture: "",

      formData: {
        currentId: 0,
        picture: "",
        remarks: ""
      },
      from: {}

    };
  },

  //函数
  methods: {
    ///取消按钮
    handleCancel() {
      router.push("/pictureinfo/rotationchartinfo");
    },
    ///确定按钮
    handleSave() {
      console.log(this.formData);
      if (this.formData.currentId == 0) {

        if (this.formData.picture.trim() == '') {
          return this.$message({ type: "error", message: "轮播图不能为空" });
        }

        //新增
        addRotationChart(this.formData)
          .then((res) => {
            console.log(res);
            if (res.code == 200) {
              this.$message({ type: "success", message: "上传轮播图成功" });
              router.push("/pictureinfo/rotationchartinfo");
            }
          })
          .catch((err) => {
            console.log(err);
          })
      } else {
           
        //修改
        modRotationChart(this.formData.currentId, this.formData)
          .then((res) => {
              if (res.code == 200) {
              this.$message({ type: "success", message: "修改轮播图成功" });
              router.push("/pictureinfo/rotationchartinfo");
            }
          })
          .catch((err) => {
            console.log(err);

          })
      }

    },

    // 点击查看预览图片
    handlePictureCardPreview(file) { //预览图片时调用
      console.log("预览图片");
      console.log(file);

      this.dialogImageUrl = file.url;
      this.dialogVisibleImg = true;
    },
    //点击删除轮播图
    handleRemove(file, fileList) { //移除图片
      console.log("删除轮播图");
      console.log(fileList);
      this.formData.picture = this.oldPicture;
    },
    //图片上传成功
    handleAvatarSuccess(res, file) {

      this.dialogImageUrl = URL.createObjectURL(file.raw);
      if (res.code == 200) {
        this.$message.success('图片上传成功!');
        this.formData.picture = res.data

      } else {
        this.$message.error('图片上传失败，请重新上传！');
      }
    },

    //图片上传之前事件
    beforeAvatarUpload(file) { //文件上传之前调用做一些拦截限制
      console.log("图片上传之前事件");
      console.log(file);

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
      this.$message.error('只能上传一张头像哦，请删除前一张后重试');
    },
    //图片上传失败调用
    imgUploadError(err, file, fileList) {
      console.log("图片上传失败调用");
      console.log(err);
      console.log(file);
      console.log(fileList);
      this.$message.error('上传图片失败!');
    },



  },
  computed: {
    isAdd() {
      return this.formData.currentId > 0;

    }
  },
  //页面数据加载完毕执行
  mounted() {


    var res = this.$route.query

    console.log(res);

    //如果res是空对象
    if (Object.keys(res).length != 0) {
      this.formData.currentId = res.id;
      this.oldPicture = res.picture;
      this.formData.picture = res.picture;
      this.formData.remarks = res.remarks;
    }

    console.log(this.formData);




    //  bus.$on("editRotationChart",(data)=>{
    //         console.log(data);
    //         this.from=data;
    //  })


  },
};
</script>

<style >
.userinfo {
  display: flex;
  align-items: center;
  justify-content: center;
  /* justify-items: center; */
}

.updateAvatarFile {
  /* width: 150px; */
  height: 150px;
  margin-top: 15px;
}

.btn {
  display: flex;
  justify-content: right;
  margin-top: 30px;

}

.btn>.el-button {
  width: 300px;
  text-align: center;
}
</style>