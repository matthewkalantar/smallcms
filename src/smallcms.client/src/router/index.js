import { createRouter, createWebHistory } from 'vue-router';
import Home from '../views/Home.vue';
import Login from '../views/Login.vue';
import Dashboard from '../views/Dashboard.vue';
import BlogDetails from '../views/BlogDetails.vue';

const routes = [
  { path: '/', name: 'Home', component: Home },
  { path: '/blogposts/:id', name: 'BlogDetails', component: BlogDetails,props: true },
  { path: '/login', name: 'Login', component: Login },
  { path: '/dashboard', name: 'Dashboard', component: Dashboard },
];

const router = createRouter({
  history: createWebHistory('/'),
  routes,
});

export default router;
