<template>
  <div>
    <!-- 卡片 -->
    <el-card class="box-card">
      <!-- 搜索框 与 新增框 表单-->
      <el-form :inline="true" class="demo-form-inline">

        <el-button type="success" icon="el-icon-check" style="margin-right:50px" circle></el-button>
         
        <el-form-item label="查询文章标题">
          <el-input placeholder="查询文章标题" style="width: 300px" v-model="msg"></el-input>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" style="margin-left: 20px" @click="handlesearch">查询</el-button>
        </el-form-item>


        <div v-if="userRoleId==1">
          <el-form-item>
            <el-button type="primary" style="margin-left: 20px">开启推荐</el-button>
          </el-form-item>

          <el-form-item>
            <el-button type="primary" style="margin-left: 20px">取消推荐</el-button>
          </el-form-item>

          <el-form-item>
            <el-button type="primary" style="margin-left: 20px">通过审核</el-button>
          </el-form-item>

          <el-form-item>
            <el-button type="primary" style="margin-left: 20px">取消审核</el-button>
          </el-form-item>

          <el-form-item>
            <el-button type="primary" style="margin-left: 20px">退回</el-button>
          </el-form-item>

        </div>

      </el-form>
    </el-card>
    <el-table :data="tableData" style="width: 100%" border>



      <el-table-column label="选择" width="150">
        <template>
          <!-- `checked` 为 true 或 false -->
          <el-checkbox size="20px" checked="" />
        </template>
      </el-table-column>

      <el-table-column label="审核状态" width="100">
        <template>
          <i class="el-icon-time"></i>
          <!-- <span style="margin-left: 10px">{{ scope.row.isCheck }}</span> -->
          <span style="margin-left: 10px">审核失败</span>
        </template>
      </el-table-column>


      <el-table-column label="作者" width="150">
        <template slot-scope="scope">
          <i class="el-icon-time"></i>
          <span style="margin-left: 10px">{{ scope.row.user.username }}</span>
        </template>
      </el-table-column>
      <el-table-column label="标题" width="150">
        <template slot-scope="scope">
          <i class="el-icon-time"></i>
          <span style="margin-left: 10px">{{ scope.row.title }}</span>
        </template>
      </el-table-column>
      <el-table-column label="图片显示" width="200" height="150">
        <template slot-scope="scope">
          <!-- //动弹绑定src并拼接获取到的图片路径 -->
          <img :src="
            'http://localhost:5000/files/picPath?picUrl=' +
            scope.row.coverPictureUrl
          " alt="" />
        </template>
      </el-table-column>
      <el-table-column label="创建时间" width="200">
        <template slot-scope="scope">
          <i class="el-icon-time"></i>
          <span style="margin-left: 10px">{{ scope.row.createdTime }}</span>
        </template>
      </el-table-column>

      <el-table-column label="更新时间" width="200">
        <template slot-scope="scope">
          <i class="el-icon-time"></i>
          <span style="margin-left: 10px">{{ scope.row.updatedTime }}</span>
        </template>
      </el-table-column>

      <el-table-column label="文章信息" width="150">
        <template slot-scope="scope">
          <el-popover trigger="hover" placement="top">
            <p>内容: {{ scope.row.content.words }}</p>
            <p>简介: {{ scope.row.synopsis }}</p>
            <p>点赞: {{ scope.row.likeNum }}</p>
            <div slot="reference" class="name-wrapper">
              <el-tag size="medium">请触碰查看</el-tag>
            </div>
          </el-popover>
        </template>
      </el-table-column>


      <!-- <el-table-column label="操作" width="200">
        <template slot-scope="scope">
          <el-switch @change="handleelect(scope.$index, scope.row)" v-model="scope.row.isRecommend"
            style="margin-left: 15px" active-text="开启推荐">
          </el-switch>
          <el-button type="success" icon="el-icon-check" circle></el-button>
        </template>
      </el-table-column> -->

      <el-table-column label="操作" width="200">
        <template slot-scope="scope">
          <el-button size="mini" @click="handleEdit(scope.$index, scope.row)">编辑</el-button>
          <el-button size="mini" type="danger" @click="handleDelete(scope.$index, scope.row)">删除</el-button>

        </template>
      </el-table-column>
    </el-table>
    <!-- Dialog对话框 -->
    <el-dialog title="用户信息" :visible.sync="dialogFormVisible">
      <el-form :model="formdata">
        <el-form-item label="标题" label-width="100px">
          <el-input v-model="formdata.title" style="width: 400px" autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item label="文章描述" label-width="100px">
          <el-input v-model="formdata.synopsis" style="width: 400px" autocomplete="off"></el-input>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="handleCancel">取 消</el-button>
        <el-button type="primary" @click="handleSave">确 定</el-button>
      </div>
    </el-dialog>
    <div class="block">
      <el-pagination :current-page="pager.currentPage" :page-size="pager.pageSize"
        layout="prev, pager, next, jumper,sizes, total" :total="pager.totalSize" :page-sizes="[5, 10, 15, 20]"
        @size-change="handleSizeChange" @current-change="handleCurrentChange">
      </el-pagination>
    </div>
  </div>
</template>


<script>
import { getList, getByTitle, delArticle, modArticle, getListByUserId } from "../api/article";
import { getUserRoleId, getUserId } from "../utils/auth";

export default {
  props: {
    isRecommend: {
      type: Boolean,
    },
  },
  data() {
    return {
      //初始化一些数据
      tableData: [],
      currentIndex: 0,
      dialogFormVisible: false,
      pager: {
        //每页显示条目个数
        pageSize: 5,
        currentPage: 1,
        totalSize: 0,
      },
      //用户id
      userId: 0,
      //用户角色id
      userRoleId: 0,
      formdata: {
        id: 0,
        title: "",
        synopsis: "",
        IsRecommend: this.isRecommend,
      },
      msg: "",
      curIndex: 0,

    };
  },
  methods: {
    //拉取数据
    fetchData() {
      let params = {
        pager: this.pager,
      };

      //如果是超级管理员就是
      if (this.userRoleId == 1) {
        ///h获取所有文章
        getList(params.pager)
          .then((res) => {
            if (res.code == 200) {
              this.tableData = res.data.data;
              this.pager = res.data.pager;
            }

          })
          .catch((err) => {
            console.log(err);
          });
      } else {
        //根据当前用户id获取文章信息
        getListByUserId(this.userId, params.pager)
          .then((res) => {
            if (res.code == 200) {
              this.tableData = res.data.data;
              this.pager = res.data.pager;
            }

          })
          .catch((err) => {
            console.log(err);
          });

      }

    },
    //查询
    handlesearch() {
      if (this.msg && this.msg.length > 0) {
        console.log(this.msg);
        getByTitle({ QueryArticleDatas: this.msg })
          .then((data) => {
            console.log(data);
            if (data.code === 1000) {
              //往空数组里放对象的属性值
              this.tableData = data.data.data;
              this.pager.totalSize = this.tableData.length;

              console.log(this.tableData);
              // this.tableData =[];
              // this.tableData.push(data.data.data);
            } else {
              this.tableData = data.data.data;
            }
          })
          .catch((err) => {
            console.log(err);
          });
      } else {
        this.fetchData();
      }
    },
    // 编辑
    handleEdit(index, row) {
      this.dialogFormVisible = true;
      this.formdata.id = row.id;
      this.formdata.title = row.title;
      this.formdata.synopsis = row.synopsis;

      this.curIndex = index;
    },
    // 删除
    handleDelete(index, row) {
      console.log(index, row);
      console.log(row.id);
      //通过row.id找到当前的id
      this.$confirm("此操作将永久删除该数据, 是否继续?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      })
        .then(() => {
          delArticle(row.id).then(({ data }) => {
            console.log(data);
            this.tableData.splice(index, 1);
            this.pager.totalSize -= 1;
            let nun = parseInt(this.pager.totalSize / this.pager.pageSize) + 1;
            if (
              //判断取余后为零 pager.pageindex-1
              parseInt(this.pager.totalSize % this.pager.pageSize) === 0 &&
              this.pager.currentPage === nun
            ) {
              this.pager.currentPage -= 1;
            }
            //重新拉取
            this.fetchData();
          });
          this.$message({
            type: "success",
            message: "删除成功!",
          });
        })
        .catch(() => {
          this.$message({
            type: "info",
            message: "已取消删除",
          });
        });
    },
    // 取消按钮
    handleCancel() {
      this.dialogFormVisible = false;
    },
    // 保存按钮
    handleSave() {
      this.dialogFormVisible = false;
      console.log(this.formdata);
      modArticle(this.formdata.id, this.formdata).then((res) => {
        console.log(res);
        this.tableData.splice(this.curIndex, 1, res.data);
        this.fetchData();
      });
    },
    // 通过页码来拉取数据
    handleCurrentChange(val) {
      this.pager.currentPage = val;
      this.fetchData();
    },
    // 改变页大小来拉取数据
    handleSizeChange(val) {
      this.pager.pageSize = val;
      this.fetchData();
    },
    //推选文章
    handleelect(index, row) {
      console.log(index, row);
      console.log(row.id);

      this.formdata.id = row.id;
      this.formdata.IsRecommend = !this.formdata.IsRecommend;
      this.formdata.title = row.title;
      this.formdata.synopsis = row.synopsis;

      console.log(this.formdata);

      modArticle(this.formdata.id, this.formdata)
        .then((res) => {
          console.log(res);
        })
        .catch((err) => {
          console.log(err);
        });
    },
  },
  mounted() {
    // //获取到当前用户的角色id
    this.userRoleId = getUserRoleId();
    //获取当前用户id
    this.userId = getUserId();
    this.fetchData();
  },
};
</script>


<style scoped>
img {
  height: 100px;
}
</style>