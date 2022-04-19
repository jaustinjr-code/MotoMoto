import { createRouter, createWebHashHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import DirectMessage from '../views/DirectMessageView.vue'
import Login from '../views/LoginView.vue'
import CommunityDashboard from '../views/CommunityView.vue'


const routes = [
  {
    path:'/',
    name: 'Login',
    component: Login
  },
  {
    path: '/DM',
    name: 'DirectMessage',
    component: DirectMessage
  },
  {
    path: '/CommunityDashboard',
    name: 'CommunityDashboard',
    component: CommunityDashboard
  }

]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router
