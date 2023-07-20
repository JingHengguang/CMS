
<template>
  <div>
    <!-- 卡片 -->
    <el-card class="typeBoxCard">
      <!-- 搜索框 与 新增框 表单-->
      <el-form :inline="true" class="demo-form-inline">

        <el-form-item label="查询类型">
          <el-input placeholder="查询类型" style="width: 300px" v-model="msg"></el-input>
        </el-form-item>

        <el-form-item>
          <el-button type="primary" style="margin-left: 20px" @click="handlesearch">查询</el-button>
        </el-form-item>

        <el-form-item>
          <el-button type="warning" style="margin-left: 50px" @click="handleadd">新增</el-button>
        </el-form-item>

      </el-form>
    </el-card>

    <el-table :data="tableData" style="width: 100%" border>
      <el-table-column label="id" width="150">
        <template slot-scope="scope">
          <i class="el-icon-time"></i>
          <span style="margin-left: 10px">{{ scope.row.id }}</span>
        </template>
      </el-table-column>

      <el-table-column label="类型名称" width="200">
        <template slot-scope="scope">
          <i class="el-icon-time"></i>
          <span style="margin-left: 10px">{{ scope.row.type }}</span>
        </template>
      </el-table-column>

      <el-table-column label="备注" width="200">
        <template slot-scope="scope">
          <i class="el-icon-time"></i>
          <span style="margin-left: 10px">{{ scope.row.remarks }}</span>
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

      <el-table-column label="操作">
        <template slot-scope="scope">
          <el-button size="mini" @click="handleEdit(scope.$index, scope.row)">编辑</el-button>
          <el-button size="mini" type="danger" @click="handleDelete(scope.$index, scope.row)">删除</el-button>
        </template>
      </el-table-column>
    </el-table>

    <!-- Dialog对话框 -->
    <el-dialog title="类型信息" :visible.sync="dialogFormVisible">
      <el-form :model="formdata">
        <el-form-item label="类型名称" label-width="100px">
          <el-input v-model="formdata.type" style="width: 400px" autocomplete="off"></el-input>
        </el-form-item>

        <el-form-item label="备注" label-width="100px">
          <el-input v-model="formdata.remarks" style="width: 400px" autocomplete="off"></el-input>
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
import { getList, getListByQuery, addArticleType, modArticleType, delArticleType } from '../api/types';

export default {
  data() {
    return {

      //表格中的信息
      tableData: [],

      //  当前选择行的索引
      curIndex: 0,

      //编辑或新增模板数据
      formdata: {
        id: 0,
        type: "",
        remarks: ""
      },

      //查询文本框的值
      msg: "",

      //编辑或新增模态框
      dialogFormVisible: false,

      pager: {
        //每页显示条目个数
        pageSize: 5,
        currentPage: 1,
        totalSize: 0,
      },
    };
  },
  //函数
  methods: {
    //查询
    handlesearch() {
      if (this.msg && this.msg.length > 0) {

        let obj = {
          Type: this.msg,
          currentPage: this.pager.currentPage,
          PageSize: this.pager.pageSize
        }
        getListByQuery(obj)
          .then(({ data }) => {
            console.log(data);
            this.tableData = data.data;
            this.pager = data.pager;
          })
          .catch((err) => {
            console.log(err);
          });

      } else {
        this.fetchData();
      }
    },
    // 新增
    handleadd() {
      this.dialogFormVisible = true;
      this.formdata.type = "";
      this.formdata.remarks = "";
    },
    // 编辑
    handleEdit(index, row) {
      this.dialogFormVisible = true;
      this.formdata.id = row.id;
      this.formdata.type = row.type;
      this.formdata.remarks = row.remarks;

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
          delArticleType(row.id)
            .then((res) => {
              if (res.code == 200) {
                this.$message({ type: "success", message: res.msg });
                this.fetchData();
              }
            })
            .catch((err) => {
              console.log(err);
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

      if (this.formdata.id == 0) {
        //新增类型
        addArticleType(this.formdata)
          .then((res) => {
            console.log(res);
            if (res.code == 200) {
              this.tableData.push(res.data);
              this.$message({ type: "success", message: res.msg });
              this.fetchData();
            } else {
              this.$message({ type: "error", message: res.msg });
            }

          })
          .catch((err) => {
            console.log(err);
          });
      }
      else {
        //修改类型
        modArticleType(this.formdata.id, this.formdata)
          .then((res) => {
            if (res.code == 200) {
              this.tableData.splice(this.curIndex, 1, res.data);
              this.$message({ type: "success", message: res.msg });
              this.fetchData();
            }
          })
          .catch((err) => {
            console.log(err);
          });

      }
    },
    //拉取数据
    fetchData() {
      getList(this.pager)
        .then((res) => {
          if (res.code == 200) {
            this.tableData = res.data.data;
            this.pager = res.data.pager;
          }

        })
        .catch((err) => {
          console.log(err);
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
  },
  //页面数据加载完毕执行
  mounted() {
    this.fetchData();
  },
};
</script>


<style scoped>
.typeBoxCard {
  display: flex;
  align-items: center;
  justify-content: center;
}
</style>