<template>
    <div>
      <h1>Danh sách Công Việc 1</h1>
  
      <!-- Nút Thêm Task (Chỉ để Test Vuex) -->
      <el-button type="primary" @click="addNewTask">Thêm Task Mẫu</el-button>
  
      <!-- Nút Xóa Task Đầu Tiên -->
      <el-button type="danger" @click="removeFirstTask" v-if="tasks.length > 0">
        Xóa Task Đầu Tiên
      </el-button>
  
      <el-button type="success" @click="saveTasksToCache">Lưu Task vào Cache</el-button>
      <el-button type="info" @click="loadTasksFromCache">Lấy Task từ Cache</el-button>
      <ul>
        <li v-for="task in tasks" :key="task.id">
          <span :style="{ textDecoration: task.completed ? 'line-through' : 'none' }">
            {{ task.title }}
          </span>
          <el-button size="small" @click="toggleTask(task.id)">
            {{ task.completed ? "Hoàn tác" : "Hoàn thành" }}
          </el-button>
        </li>
      </ul>
    </div>
  </template>
  
  <script setup>
  // import { computed } from 'vue';
  import { useStore } from 'vuex';
  import { ref } from 'vue';
  
  // Lấy store Vuex
  const store = useStore();
  const tasks = ref([]);
  
  // Lấy danh sách task từ Vuex
  tasks.value = store.state.tasks;

  // Thêm một task mới
  const addNewTask = () => {
    const newTask = {
      id: Date.now(),
      title: 'Công việc mới',
      completed: false
    };
    store.dispatch('addTask', newTask);
    tasks.value = [...store.state.tasks];
  };
  
  // Xóa task đầu tiên trong danh sách
  const removeFirstTask = () => {
    if (tasks.value.length > 0) {
      store.dispatch('removeTask', tasks.value[0].id);
    }
  };
  
  // Chuyển trạng thái hoàn thành
  const toggleTask = (taskId) => {
    store.dispatch('toggleTaskStatus', taskId);
  };

  // Lưu task vào cache
  const saveTasksToCache = () => {
    localStorage.setItem('tasks', JSON.stringify(store.state.tasks));
  };

  // Lấy task từ cache
  const loadTasksFromCache = () => {
    const cachedTasks = localStorage.getItem('tasks');
    if (cachedTasks) {
      tasks.value = JSON.parse(cachedTasks);
    }
  };
</script>
  