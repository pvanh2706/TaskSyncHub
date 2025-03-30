<script setup>
  import { ref } from 'vue';
  import TaskList from '@/components/TaskList.vue';

  const tasks = ref([
    { id: 1, title: 'Viết tài liệu dự án' },
    { id: 2, title: 'Cấu hình Vue Router' },
  ]);

  const deleteTask = (taskId) => {
    tasks.value = tasks.value.filter(task => task.id !== taskId);
  };
</script>

<template>
  <div>
    <TaskList :tasks="tasks" @deleteTask="deleteTask" />
  </div>
</template>
