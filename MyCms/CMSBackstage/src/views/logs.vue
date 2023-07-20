<template>
  <div>
    <el-card class="box-card">
      <h1 style="color: purple" class="texttitle">日志信息</h1>
    </el-card>

     <!-- ///
       操作方法 MethodName
       异常信息 Exception
       方法返回信息   ReturnValue
       服务名称  ServiceName
       操作时间   updatedTime
     -->
    <el-table :data="tableData" style="width: 100%" border>
     
      <el-table-column label="用户名称" width="180">
        <template slot-scope="scope">
          <i class="el-icon-time"></i>
          <span style="margin-left: 10px">{{ scope.row.id }}</span>
        </template>
      </el-table-column>

      <el-table-column label="操作方法" width="180">
        <template slot-scope="scope">
          <i class="el-icon-time"></i>
          <span style="margin-left: 10px">{{ scope.row.clientIpAddress }}</span>
        </template>
      </el-table-column>

      <el-table-column label="操作时间" width="180">
        <template slot-scope="scope">
          <el-popover trigger="hover" placement="top">
            <p>参数: {{ scope.row.parameters }}</p>
            <p>返回数据: {{ scope.row.returnValue }}</p>
            <p>服务名称: {{ scope.row.serviceName }}</p>
            <p>方法名称: {{ scope.row.methodName }}</p>
            <div slot="reference" class="name-wrapper">
              <el-tag size="medium">{{ scope.row.executionDuration }}</el-tag>
            </div>
          </el-popover>
        </template>
      </el-table-column>

      
      <el-table-column label="备注" width="180">
        <template slot-scope="scope">
          <i class="el-icon-time"></i>
          <span style="margin-left: 10px">{{ scope.row.remarks }}</span>
        </template>
      </el-table-column>
      <el-table-column label="创建时间" width="250">
        <template slot-scope="scope">
          <i class="el-icon-time"></i>
          <span style="margin-left: 10px">{{ scope.row.createdTime }}</span>
        </template>
      </el-table-column>
      <el-table-column label="创建时间" width="250">
        <template slot-scope="scope">
          <i class="el-icon-time"></i>
          <span style="margin-left: 10px">{{ scope.row.createdTime }}</span>
        </template>
      </el-table-column>

      <!-- <el-table-column label="操作">
        <template slot-scope="scope">
          <el-button size="mini" @click="handleEdit(scope.$index, scope.row)">编辑</el-button>
          <el-button size="mini" type="danger" @click="handleDelete(scope.$index, scope.row)">删除</el-button>
        </template>
      </el-table-column> -->
    </el-table>
    <div class="block">
      <el-pagination :current-page="pager.pageIndex" :page-size="pager.pageSize"
        layout=" prev, pager, next, sizes,jumper,total" :total="pager.rowTotal" :page-sizes="[5, 10, 15, 20]"
        @size-change="handleSizeChange" @current-change="handleCurrentChange">
      </el-pagination>
    </div>
  </div>
</template>


<script>

import { getLogsList } from "../api/logs"
export default {
  data() {
    return {
      // tableData: [
      //   {
      //     id: 1,
      //     clientIpAddress: "127.0.0.1:8083",
      //     parameters: "测试数据1",
      //     returnValue: "测试数据1",
      //     createdTime: "2021-7-27",
      //     remarks: "种子数据",
      //   },
      //   {
      //     id: 2,
      //     clientIpAddress: "127.0.0.1:8081",
      //     parameters: "测试数据2",
      //     returnValue: "测试数据2",
      //     createdTime: "2021-7-25",
      //     remarks: "种子数据",
      //   },
      //   {
      //     id: 3,
      //     clientIpAddress: "127.0.0.1:8084",
      //     parameters: "测试数据3",
      //     returnValue: "测试数据3",
      //     createdTime: "2021-7-28",
      //     remarks: "种子数据",
      //   },
      // ],

      tableData: [],
      pager: {
        //每页显示条目个数
        pageSize: 10,
        currentPage: 1,
        totalSize: 0,
      },
    };
  },
  methods: {
    handleEdit(index, row) {
      console.log(index, row);
    },
    handleDelete(index, row) {
      console.log(index, row);
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

    //刷新日志
    fetchData() {
      getLogsList(this.pager)
        .then((res) => {
          console.log(res);
          
          if (res.code == 200) {
            this.tableData = res.data.data;
            this.pager = res.data.pager;
            console.log(  this.tableData);
          }

        })
        .catch((err) => {
          console.log(err);
        });
    }


  },
  mounted() {
    //在组件内部渲染界面之后执行的函数
    //前端发起请求 后端传数据给前端

    //刷新数据
    this.fetchData();
  },
};
</script>


<style>
.texttitle {
  text-align: center;
}
</style>