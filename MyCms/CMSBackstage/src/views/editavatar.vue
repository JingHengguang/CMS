<template>

    <div class="fullUser">
        <el-button type="success" plain icon="el-icon-arrow-left" @click="handleCancel">返回主页</el-button>
        <div class="userinfo">
            <!-- 头像 -->
            <el-form label-width="100px">
                <el-form-item label="原头像:">
                    <img class="updateAvatarFile" :src="AvatarUrl" alt="头像加载失败...">
                </el-form-item>

                <el-form-item label="新头像：" label-width="100px" style="margin-top: 20px">
                    <el-upload list-type="picture-card" :action="uploadAvatarUrl" :auto-upload="true" :limit="limit"
                        :multiple="false" :show-file-list="true" :on-preview="handlePictureCardPreview"
                        :on-remove="handleRemove" :on-success="handleAvatarSuccess" :before-upload="beforeAvatarUpload"
                        :on-exceed="handleExceed" :on-error="imgUploadError">

                        <i class="el-icon-plus"></i>
                    </el-upload>
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
import { getUserId } from "../utils/auth";
import { modUser, getUserById } from "../api/users"

export default {
    data() {
        return {
            //上传图片接口地址
            uploadAvatarUrl: 'http://localhost:5000/files/uploadAvatar/',
            //点击查看图片弹框
            dialogVisibleImg: false,
            //点击图片查看，本地图片路径
            dialogImageUrl: '',
            //获取头像接口地址
            baseUrl: "http://localhost:5000/files/avatarPath?avatarUrl=",
            //最大上传数量
            limit: 1,
            //当前头像地址
            currentAvatarUrl: "UploadFiles\\DefaultImage\\default.jpg",

            //个人信息数据
            formData: {
                currentId: 0,
                username: "",
                password: "",
                userRoleId: 0,
                avatarUrl: "",
                remarks: "",
            },

        };
    },
    //函数
    methods: {
        //刷新用户信息
        refUserInfo() {

            //获取到当前用户的信息 
            getUserById(this.formData.currentId)
                .then((res) => {
                    if (res.code == 200) {
                        console.log(res);

                        this.formData.username = res.data.username;
                        this.formData.userRoleId = res.data.userRoleId;
                        this.formData.remarks = res.data.remarks;
                        this.currentAvatarUrl = res.data.avatarUrl;
                        this.formData.password = res.data.password;
                        console.log(this.formData);
                        console.log("刷新成功");
                    }

                })
                .catch((err) => {
                    console.log("调用失败：---\n" + err);
                })
        },
        // 点击查看预览图片
        handlePictureCardPreview(file) { //预览图片时调用
            console.log("预览图片");
            console.log(file);

            this.dialogImageUrl = file.url;
            this.dialogVisibleImg = true;
        },
        // 点击删除头像
        handleRemove(file, fileList) { //移除图片
            console.log("删除头像");
            console.log(fileList);
            this.formData.avatarUrl = "";
        },
        // 图片上传之前事件
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
        //图片上传成功
        handleAvatarSuccess(res, file) {

            this.dialogImageUrl = URL.createObjectURL(file.raw);
            if (res.code == 200) {
                this.$message.success('图片上传成功!');
                this.formData.avatarUrl = res.data

            } else {
                this.$message.error('图片上传失败，请重新上传！');
            }
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
        ///个人信息取消按钮
        handleCancel() {
            router.push("/home");
        },
        ///个人信息确定按钮
        handleSave() {

            console.log(this.formData);

            if (this.formData.avatarUrl == "") {
                this.$message({ type: "error", message: "请上传一张头像" });
                return;
            }



            modUser(this.formData.currentId, this.formData)
                .then((res) => {
                    console.log(res);
                    //  this.$router.go(0);
                    this.$router.push(
                        {
                            name: "layout",
                            params: {
                               avatarUrl:this.formData.avatarUrl
                            }
                        });

                    // router.push("/home");
                    this.$message({ type: "success", message: res.msg });
                })
                .catch((err) => {
                    console.log(err);
                });

        },

    },

    computed: {
        //头像地址'
        AvatarUrl() {
            var avatarFullUrl = this.baseUrl + this.currentAvatarUrl;

            return avatarFullUrl;
        },
    },
    //页面数据加载完毕执行
    mounted() {
        //获取当前用户
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

.updateAvatarFile {
    width: 150px;
    height: 150px;
    margin-top: 15px;
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