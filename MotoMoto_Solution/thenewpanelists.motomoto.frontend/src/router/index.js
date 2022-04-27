import { createRouter, createWebHashHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import DirectMessage from '../views/DirectMessageView.vue'
import Login from '../views/LoginView.vue'
import CommunityDashboard from '../views/CommunityView.vue'
import PartFlaggingBuilder from '../views/PartFlaggingBuilderView.vue'
import PartFlaggingPost from '../views/PartFlaggingPostView.vue'


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
  },
  {
    path: '/PartFlaggingBuilder',
    name: 'PartFlaggingCarBuilder',
    component: PartFlaggingBuilder
  },
  {
    path: '/PartFlaggingPost',
    name: 'PartFlaggingPost',
    component: PartFlaggingPost
  }

]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router
