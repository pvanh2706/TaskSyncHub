<template>
  <el-menu mode="horizontal" router>
    <el-menu-item index="/">Trang chủ</el-menu-item>
    <el-menu-item index="/task">Công việc</el-menu-item>
    <el-menu-item index="/create">Tạo issue</el-menu-item>
    <el-menu-item index="/config">Config</el-menu-item>
  </el-menu>
  <router-view></router-view>
</template>

<script>
export default {
  name: 'App'
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
