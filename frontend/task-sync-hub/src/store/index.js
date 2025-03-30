import { createStore } from 'vuex';
import { reactive } from 'vue';
// Khởi tạo Vuex Store
const store = createStore({
  state() {
    return {
      tasks: [ // Danh sách công việc ban đầu
        { id: 1, title: 'Làm báo cáo', completed: false },
        { id: 2, title: 'Họp team', completed: true },
        { id: 3, title: 'Triển khai dự án', completed: false }
      ]
    };
  },
  mutations: {
    addTask(state, newTask) { // Thêm task vào danh sách
      // state.tasks = [...state.tasks, newTask];
      state.tasks.push(reactive(newTask))
      console.log("Mutation addTask - Tasks:", state.tasks);
    },
    removeTask(state, taskId) { // Xóa task theo ID
      state.tasks = state.tasks.filter(task => task.id !== taskId);
    },
    toggleTaskStatus(state, taskId) { // Đổi trạng thái task
      const task = state.tasks.find(task => task.id === taskId);
      if (task) {
        task.completed = !task.completed;
      }
    }
  },
  actions: {
    addTask({ commit }, newTask) { // Action gọi mutation `addTask` 
      commit('addTask', newTask);
    },
    removeTask({ commit }, taskId) { // Action gọi mutation `removeTask`
      commit('removeTask', taskId);
    },
    toggleTaskStatus({ commit }, taskId) { // Action gọi mutation `toggleTaskStatus`
      commit('toggleTaskStatus', taskId);
    }
  },
  getters: {
    allTasks: state => state.tasks, // Lấy tất cả task
    completedTasks: state => state.tasks.filter(task => task.completed), // Lấy task đã hoàn thành
    pendingTasks: state => state.tasks.filter(task => !task.completed) // Lấy task chưa hoàn thành
  }
});

export default store;
