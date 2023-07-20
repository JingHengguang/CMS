<template>
  <el-menu
    default-active="2"
    class="el-menu-vertical-demo"
    background-color="#030303"
    text-color="#808080"
    active-text-color="#F5F5DC"
    :collapse-transition="false"
    router
    unique-opened
    :collapse="collapse"   
  >
    <NavbarItem :menus="menus"></NavbarItem>
  </el-menu>
</template>


<script>
import NavbarItem from "./NavbarItem.vue";

export default {
  components: { NavbarItem },
  props: {
    menus: {
      //传递的类型是数组
      type: Array,
      require: true,
    },
    collapse: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {};
  },
};
</script>
