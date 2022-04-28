import { createRouter, createWebHashHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import DirectMessage from '../views/DirectMessageView.vue'
import Login from '../views/LoginView.vue'
import CommunityDashboard from '../views/CommunityView.vue'
import EventList from '../views/EventListView.vue'

const routes = [
  {
    path:'/',
    name: 'HomeView',
    component: HomeView
  },
  {
    path:'/Login',
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
    path: '/EventList',
    name: 'EventList',
    component: EventList
  }
]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router
