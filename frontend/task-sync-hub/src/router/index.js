import { createRouter, createWebHistory } from 'vue-router';
import HelloWorld from '@/components/HelloWorld.vue';
import TaskView from '@/views/TaskView.vue';
import CreateIssueForm from '@/views/CreateIssueForm.vue';
import ConfigComponent from '@/views/ConfigComponent.vue';

const routes = [
  { path: '/', component: HelloWorld },
  { path: '/task', component: TaskView },
  { path: '/create', component: CreateIssueForm },
  { path: '/config', component: ConfigComponent },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
