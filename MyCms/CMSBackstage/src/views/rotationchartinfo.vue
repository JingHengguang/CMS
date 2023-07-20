
<!--轮播图 -->
<template>
  <div>
    <el-form :inline="true" class="demo-form-inline">
      <el-form-item label="备注">
        <el-input placeholder="查询轮播图" style="width: 300px" v-model="msg"></el-input>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" style="margin-left: 20px" @click="handlesearch">查询</el-button>
      </el-form-item>
      <el-form-item>
        <el-button type="warning" style="margin-left: 50px" @click="handleadd">新增</el-button>
      </el-form-item>
    </el-form>

    <el-table :data="tableData" style="width: 100%" border>
      <el-table-column label="id" width="150">
        <template slot-scope="scope">
          <i class="el-icon-time"></i>
          <span style="margin-left: 10px">{{ scope.row.id }}</span>
        </template>
      </el-table-column>

      <el-table-column label="图片显示" width="200" height="150">
        <template slot-scope="scope">
          <!-- //动弹绑定src并拼接获取到的图片路径 -->
          <img :src="
            'http://localhost:5000/files/picPath?picUrl=' +
            scope.row.picture
          " alt="" />
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
          <el-button size="mini" @click="handleEdit(scope.$index, scope.row)">编辑
          </el-button>
          <el-button size="mini" type="danger" @click="handleDelete(scope.$index, scope.row)">删除</el-button>

        </template>
      </el-table-column>
    </el-table>
  <!-- 页码大小 -->
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
import { getRotationChartList,GetRotationChartsByremark } from "../api/rotationChart"
// import bus from "../utils/params"

export default {
  data() {
    return {
      
      //备注框内容
      msg:"",
      //初始化一些数据
      tableData: [],

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

    //刷新数据
    fetchData() {
      //获取所有轮播图
      getRotationChartList(this.pager)
        .then((res) => {

          if (res.code == 200) {
            console.log(res);
            this.tableData = res.data.data;
            this.pager = res.data.pager;
          }

        })
        .catch((err) => {
          console.log(err);
        });
    },
    //编辑
    handleEdit(index, row) {
      console.log(index);
      console.log(row);

      // bus.$emit("editRotationChart",row);
      this.$router.push(
        {
          path: "/pictureinfo/addrotationchart",
          query: {
            id: row.id,
            picture: row.picture,
            remarks: row.remarks
          }
        });
    },
    //删除当前行
    handleDelete(index, row) {
      console.log(index);
      console.log(row);

    },
   
   //查询按钮
    handlesearch(){
           var obj = {
               remarks:this.msg??"",
               currentPage:this.pager.currentPage,
               pageSize:this.pager.pageSize,

           }
            
          GetRotationChartsByremark(obj)
           .then((res)=>{
            if (res.code == 200) {
              console.log(res);
              
            this.tableData = res.data.data;
            this.pager = res.data.pager;
          }              
           })
          .catch((err)=>{
               console.log(err);             
          });
    },
    //新增
    handleadd() {
      this.$router.push({
         path:"/pictureinfo/addrotationchart"
      })
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
img {
  height: 100px;
}
</style>