import { createApp } from 'vue';    // Thêm createApp
import App from './App.vue';    // Thêm App.vue
import ElementPlus from 'element-plus';    // Thêm Element Plus
import 'element-plus/dist/index.css';    // Thêm Element Plus
import 'bootstrap/dist/css/bootstrap.min.css';    // Thêm bootstrap
import router from './router';    // Thêm router
import store from './store';    // Thêm store

const app = createApp(App);    // Tạo ứng dụng
app.use(ElementPlus);    // Sử dụng Element Plus
app.use(router);    // Sử dụng router
app.use(store);    // Sử dụng store
app.mount('#app');    // Mount ứng dụng
