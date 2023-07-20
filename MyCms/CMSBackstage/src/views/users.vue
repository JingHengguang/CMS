<template>
  <div>
    <!-- 卡片 -->
    <el-card class="box-card">
      <!-- 搜索框 与 新增框 表单-->
      <el-form :inline="true" class="demo-form-inline">
        <el-form-item label="查询用户">
          <el-input
            placeholder="查询用户"
            style="width: 300px"
            v-model.trim="msg"
          ></el-input>
        </el-form-item>
        <el-form-item>
          <el-button
            type="primary"
            style="margin-left: 20px"
            @click="handlesearch"
            >查询</el-button
          >
        </el-form-item>
        <el-form-item>
          <el-button type="warning" style="margin-left: 50px" @click="handleadd"
            >新增</el-button
          >
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
      <el-table-column label="用户名" width="200">
        <template slot-scope="scope">
          <i class="el-icon-time"></i>
          <span style="margin-left: 10px">{{ scope.row.username }}</span>
        </template>
      </el-table-column>
      <el-table-column label="密码" width="200">
        <template slot-scope="scope">
          <i class="el-icon-time"></i>
          <span style="margin-left: 10px">{{ scope.row.password }}</span>
        </template>
      </el-table-column>
      <el-table-column label="拥有的角色" width="150">
        <template slot-scope="scope">
          <i class="el-icon-time"></i>
          <span style="margin-left: 10px">{{
            scope.row.userRole.roleName
          }}</span>
        </template>
      </el-table-column>
      <el-table-column label="是否使用" width="100">
        <template slot-scope="scope">
          <i class="el-icon-time"></i>
          <span style="margin-left: 10px">{{ scope.row.isActived }}</span>
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

      <el-table-column label="备注" width="200">
        <template slot-scope="scope">
          <i class="el-icon-time"></i>
          <span style="margin-left: 10px">{{ scope.row.remarks }}</span>
        </template>
      </el-table-column>
      <el-table-column label="操作" width="200" >
        <template slot-scope="scope">
          <el-button size="mini" @click="handleEdit(scope.$index, scope.row)"
            >编辑</el-button
          >
          <el-button
            size="mini"
            type="danger"
            @click="handleDelete(scope.$index, scope.row)"
            >删除</el-button
          >
        </template>
      </el-table-column>
    </el-table>
    <!-- Dialog对话框 -->
    <el-dialog title="用户信息" :visible.sync="dialogFormVisible">
      <el-form :model="formData">
        <el-form-item label="用户名" label-width="100px">
          <el-input
            v-model="formData.username"
            style="width: 400px"
            autocomplete="off"
          ></el-input>
        </el-form-item>
        <el-form-item label="密码" label-width="100px">
          <el-input
            v-model="formData.password"
            style="width: 400px"
            autocomplete="off"
          ></el-input>
        </el-form-item>
        <el-form-item label="用戶角色" label-width="100px">
          <el-select v-model="formData.UserRoleId" placeholder="请选择分配角色">
            <el-option label="普通用户" value="3"></el-option>
            <el-option label="管理员" value="2"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="备注" label-width="100px">
          <el-input
            v-model="formData.remarks"
            style="width: 400px"
            autocomplete="off"
          ></el-input>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="handleCancel">取 消</el-button>
        <el-button type="primary" @click="handleSave">确 定</el-button>
      </div>
    </el-dialog>
    <div class="block">
      <el-pagination
        :current-page="pager.currentPage"
        :page-size="pager.pageSize"
        layout="prev, pager, next, jumper,sizes, total"
        :total="pager.totalSize"
        :page-sizes="[5, 10, 15, 20]"
        @size-change="handleSizeChange"
        @current-change="handleCurrentChange"
      >
      </el-pagination>
    </div>
  </div>
</template>


<script>
import {
  getList,
  getByUsername,
  addUser,
  modUser,
  delUser,
} from "../api/users";
export default {
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
      formData: {
        id: 0,
        username: "",
        password: "",
        remarks: "",
        UserRoleId: "",
        AvatarUrl:"",
        userRole: {
          roleName: "",
        },
      },
      msg: "",
      curIndex: 0,
    };
  },
  methods: {
    //拉取数据
    fetchData() {

      console.log(this.pager);
      
      getList(this.pager)
        .then((res) => {
          console.log(res);
          this.tableData = res.data.data;
          this.pager = res.data.pager;
          console.log(this.tableData);
        })
        .catch((err) => {
          console.log(err);
        });

    },
    //查询用户
    handlesearch() {
      if (this.msg!="" && this.msg.length > 0) {

        console.log(this.msg);
             
             var obj={
                  username:this.msg,
                  pageSize: this.pager.pageSize,
                  currentPage: this.pager.currentPage,
             }
        getByUsername(obj)
          .then((res) => {
            console.log(res);
            if (res.code === 200) {
              //往空数组里放对象的属性值
              this.tableData = res.data.data;
              this.pager=res.data.pager;
              this.pager.totalSize = this.tableData.length;
            } else {
              this.tableData = res.data.data;
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
      this.formData.id = row.id;
      this.formData.username = row.username;
      this.formData.password = row.password;
      this.formData.remarks = row.remarks;

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
          delUser(row.id).then(({ data }) => {
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
    // 新增
    handleadd() {
      this.dialogFormVisible = true;
      this.formData.id = 0;
      this.formData.username = "";
      this.formData.password = "";
      this.formData.remarks = "";
      this.formData.UserRoleId = "";
    },
    // 取消按钮
    handleCancel() {
      this.dialogFormVisible = false;
    },
    // 保存按钮
    handleSave() {
      this.dialogFormVisible = false;
        console.log(this.formData);
      //新增
      if (this.formData.id === 0) {
        addUser(this.formData)
        .then((res) => {
          res.userRole = this.formData.userRole;
          console.log(res);
          this.tableData.push(res.data);
          this.fetchData();
        });
      } else {
        modUser(this.formData.id, this.formData)
        .then((res) => {      
          console.log(res);
          this.tableData.splice(this.curIndex, 1, res.data);
           console.log(this.formData);
          this.fetchData();
        })
        .catch((err)=>{
          console.log(err);
          });
      }
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
  created() {
    this.fetchData();
  },
};
</script>