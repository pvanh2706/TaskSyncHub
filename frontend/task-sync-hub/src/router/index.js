import { createRouter, createWebHistory } from 'vue-router';
import HelloWorld from '@/components/HelloWorld.vue';
import TaskView from '@/views/TaskView.vue';
import CreateIssueForm from '@/views/CreateIssueForm.vue';

const routes = [
  { path: '/', component: HelloWorld },
  { path: '/task', component: TaskView },
  { path: '/create', component: CreateIssueForm },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
