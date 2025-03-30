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
