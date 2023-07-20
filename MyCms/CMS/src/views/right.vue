<template>
  <div class="right">
    <el-row>
      <!-- 48小时阅读排行 -->
      <el-col class="one">
        <div class="read">
          <p>48小时阅读排行</p>
          <el-button class="gengduo1" size="small" type="text">
            >>查看更多
          </el-button>
        </div>
        <el-table :data="tableData" :show-header="false" class="table1">
          <el-table-column header-align="center" class="column1">
            <template slot-scope="scope">
              <el-link :underline="false">
                {{ scope.row.id }}{{ "." }}
                {{ scope.row.shijian }}
              </el-link>
            </template>
          </el-table-column>
        </el-table>
      </el-col>
      <!-- 10天编辑推荐 -->
      <el-col class="two">
        <div class="read">
          <p>10天编辑推荐</p>
          <el-button class="gengduo1" size="small" type="text">
            >>查看更多
          </el-button>
        </div>
        <el-table :data="writer" :show-header="false" class="table2">
          <el-table-column header-align="center">
            <template slot-scope="scope" class="column1">
              <el-link :underline="false">
                {{ scope.row.id }}{{ "." }}
                {{ scope.row.title }}
              </el-link>
            </template>
          </el-table-column>
        </el-table>
      </el-col>

      <!-- 10天评论排行文章 -->
      <el-col class="three">
        <div>
          <p>10天评论排行文章</p>
          <el-button class="gengduo1" size="small" type="text">
            >>查看更多
          </el-button>
        </div>

        <el-table :data="rebang" :show-header="false" class="table3">
          <el-table-column label="10天评论排行" header-align="center">
            <template slot-scope="scope">
              <!-- 内容和过滤-->
              <el-link :underline="false">
                {{ scope.row.id }}{{ "." }}
                {{ scope.row.title | ellipsis }}
              </el-link>
            </template>
          </el-table-column>
        </el-table>
      </el-col>
    </el-row>
  </div>
</template>

<script>
export default {};
</script>


<style>
/* 48小时阅读排行 */
.one {
  margin-left: 0 !important;
  background-color: rgba(207, 207, 207, 0.158);
  text-align: left;
  font-size: 21px;
  border: 1.5px solid rgba(207, 207, 207, 0.158) !important;
  border-radius: 7px;
  margin-top: 50px;
}
.one p {
  padding: 3px;
  margin: 3px;
  width: 160px !important;
  float: left;
}
.one button {
  float: right;
  margin-right: 8px;
  margin-top: 4px;
  font-size: 16px;
  background-color: rgba(207, 207, 207, 0);
}
.table1 {
  border-bottom: none !important;
}
.el-table {
  margin-top: 40px;
  border-left: none !important;
  border-right: none !important;
  border-bottom: none !important;
  font-size: 19px;
}
.el-table td,
.el-table th.is-leaf {
  border-bottom: none;
  background-color: rgba(207, 207, 207, 0.158);
  border-top: 1.3px solid rgba(207, 207, 207, 0.158) !important;
}
.el-table::before {
  height: 0px;
}
.el-table__row {
  padding: 0px !important;
  height: 35px;
}
.gengduo1 {
  margin-top: 5px;
}

/* 10天编辑推荐 */
.two {
  margin-left: 0 !important;
  background-color: rgba(207, 207, 207, 0.158);
  text-align: left;
  font-size: 21px;
  border: 1.5px solid rgba(207, 207, 207, 0.158) !important;
  border-radius: 7px;
  margin-top: 50px;
}
.two p {
  padding: 3px;
  margin: 3px;
  width: 160px !important;
  float: left;
}
.two button {
  float: right;
  margin-right: 8px;
  margin-top: 4px;
  font-size: 16px;
  background-color: rgba(207, 207, 207, 0);
}
.table2 {
  border-bottom: none !important;
}

/* 10天评论排行文章 */
.three {
  margin-left: 0 !important;
  background-color: rgba(207, 207, 207, 0.158);
  text-align: left;
  font-size: 21px;
  border: 1.5px solid rgba(207, 207, 207, 0.158) !important;
  border-radius: 7px;
  margin-top: 50px;
}
.three p {
  padding: 3px;
  margin: 3px;
  width: 180px !important;
  float: left;
}
.three button {
  float: right;
  margin-right: 8px;
  margin-top: 4px;
  font-size: 16px;
  background-color: rgba(207, 207, 207, 0);
}
.table3 {
  border-bottom: none !important;
}
/* row总样式 */
.el-footer {
  width: 100vm !important;
  margin-top: 30px !important;
}
.el-row {
  margin-top: auto;
  width: 100vm;
  margin-left: 0px !important;
}
</style>