### Cài đặt frontend
Khởi tạo project:
```bash
vue create task-sync-hub
```

Cài đặt các dependencies:
```bash
npm install element-plus axios bootstrap
npm install vue-router@4

```

### Cấu trúc thư mục
```bash
/frontend
│── /public                 # Chứa favicon, index.html
│── /src
│   ├── /assets             # Chứa hình ảnh, CSS chung
│   ├── /components         # Chứa các UI component
│   │   ├── TaskList.vue
│   │   ├── TaskItem.vue
│   │   ├── TaskForm.vue
│   ├── /views              # Chứa các trang chính
│   │   ├── HomeView.vue
│   │   ├── TaskView.vue
│   ├── /router             # Chứa file định tuyến (Vue Router)
│   │   ├── index.js
│   ├── /store              # Quản lý state (Pinia/Vuex)
│   │   ├── taskStore.js
│   ├── /services           # Xử lý API (Axios)
│   │   ├── taskService.js
│   │   ├── jiraService.js
│   ├── /styles             # Chứa file CSS/SCSS tùy chỉnh
│   │   ├── global.css
│   ├── App.vue             # File chính của ứng dụng
│   ├── main.js             # Điểm khởi chạy ứng dụng
│── package.json            # Quản lý dependencies
│── vite.config.js          # Cấu hình Vite
```
#### Sửa file main.js
```javascript
    import { createApp } from 'vue';
    import App from './App.vue';
    import ElementPlus from 'element-plus';
    import 'element-plus/dist/index.css';
    import 'bootstrap/dist/css/bootstrap.min.css';
    import router from './router';

    const app = createApp(App);
    app.use(ElementPlus);
    app.use(router);
    app.mount('#app');
```
### Thêm thư mục router => router/index.js
```javascript
    import { createRouter, createWebHistory } from 'vue-router';
    import HelloWorld from '@/components/HelloWorld.vue';
    import TaskView from '@/views/TaskView.vue';

    const routes = [
        { path: '/', component: HelloWorld },
        { path: '/task', component: TaskView },
    ];

    const router = createRouter({
        history: createWebHistory(),
        routes,
    });

    export default router;
```
### Sửa template file App.vue
```javascript
    <template>
        <el-menu mode="horizontal" router>
            <el-menu-item index="/">Trang chủ</el-menu-item>
            <el-menu-item index="/task">Công việc</el-menu-item>
        </el-menu>
        <router-view></router-view>
    </template>
```
### Cài vuex
```javascript
    npm install vuex@next
```
### Sửa file main.js
```javascript
    import { createStore } from 'vuex';
```
### Sửa file store/index.js
```javascript
    import { createStore } from 'vuex';

// Định nghĩa dữ liệu mặc định cho danh sách task
const store = createStore({
  state() {
    return {
      tasks: [
        { id: 1, title: 'Làm báo cáo', completed: false },
        { id: 2, title: 'Họp team', completed: true },
        { id: 3, title: 'Triển khai dự án', completed: false }
      ]
    };
  },
  mutations: {
    // Thêm task mới
    addTask(state, task) {
      state.tasks.push(task);
    },
    // Xóa task
    removeTask(state, taskId) {
      state.tasks = state.tasks.filter(task => task.id !== taskId);
    },
    // Cập nhật trạng thái hoàn thành
    toggleTaskStatus(state, taskId) {
      const task = state.tasks.find(task => task.id === taskId);
      if (task) {
        task.completed = !task.completed;
      }
    }
  },
  actions: {
    addTask({ commit }, task) {
      commit('addTask', task);
    },
    removeTask({ commit }, taskId) {
      commit('removeTask', taskId);
    },
    toggleTaskStatus({ commit }, taskId) {
      commit('toggleTaskStatus', taskId);
    }
  },
  getters: {
    allTasks: state => state.tasks,
    completedTasks: state => state.tasks.filter(task => task.completed),
    pendingTasks: state => state.tasks.filter(task => !task.completed)
  }
});

export default store;

```
### Sửa file main.js    
```javascript
   import store from './store'; 
   app.use(store)
```
