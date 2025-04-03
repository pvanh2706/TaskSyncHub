<template>
    <div>
      <el-table :data="tasks" border>
        <el-table-column prop="title" label="Công việc" />
        <el-table-column label="Hành động">
          <template #default="{ row }">
            <el-button type="danger" @click="$emit('deleteTask', row.id)">Xóa</el-button>
          </template>
        </el-table-column>
      </el-table>
      
      <el-table :data="tableData" border style="width: 100%">
    <!-- Cột Nhập Text -->
    <el-table-column label="Tên">
      <template #default="{ row }">
        <el-input v-model="row.name" placeholder="Nhập tên" />
      </template>
    </el-table-column>

    <!-- Cột Chọn Ngày -->
    <el-table-column label="Ngày sinh">
      <template #default="{ row }">
        <el-date-picker 
          v-model="row.birthday" 
          type="date" 
          placeholder="Chọn ngày"
          format="YYYY-MM-DD"
        />
      </template>
    </el-table-column>

    <!-- Cột Chọn Option -->
    <el-table-column label="Giới tính">
      <template #default="{ row }">
        <el-select v-model="row.gender" placeholder="Chọn giới tính">
          <el-option label="Nam" value="male"></el-option>
          <el-option label="Nữ" value="female"></el-option>
        </el-select>
      </template>
    </el-table-column>

    <!-- Cột Hành Động -->
    <el-table-column label="Hành động">
      <template #default="{ row }">
        <el-button type="primary" @click="submitRow(row)">Lưu</el-button>
      </template>
    </el-table-column>
  </el-table>


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
  const tableData = ref([
    { name: 'aa', birthday: '2023-01-01', gender: 'male' },
    { name: 'bb', birthday: '2023-01-02', gender: 'female' },
  ]);
  
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

  // Submit row
  const submitRow = (row) => {
    console.log('Submitted row:', row);
  };
</script>
  