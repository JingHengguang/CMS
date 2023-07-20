<template>
  <div>
    <div class="navbarItem" v-for="menu in menus" :key="menu.path">
      <el-submenu
        v-if="menu.children && menu.children.length > 0"
        :index="menu.path"
      >
      
        <template slot="title">
          <!-- 根目录 底下可以继续打开的-->
          <i :class="menu.meta.icon" style="color:rgb(250, 250, 176)"></i>
          <span style="font-size: 15px;">{{ menu.meta.title }}</span>
        </template>
        <!-- 再次调用改组件 嵌套循环遍历-->
        <navbar-item :menus="menu.children"></navbar-item>
      </el-submenu>
      <el-menu-item v-else :index="menu.path">
        <i :class="menu.meta.icon" style="color:rgb(250, 250, 176)"></i>
        <span slot="title" style="font-size: 15px;">{{ menu.meta.title }}</span>
      </el-menu-item>
    </div>
  </div>
</template>

<script>
export default {
  name: "NavbarItem",
  props: {
    menus: {
      type: Array,
      require: true,
    },
  },
};
</script>

<style scoped>
.navbarItem{
   margin: 0,10;
}



</style>