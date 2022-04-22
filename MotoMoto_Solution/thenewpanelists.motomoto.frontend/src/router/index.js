import { createRouter, createWebHashHistory } from 'vue-router'
import AccountDeletionView from '../views/AccountDeletionView.vue'
import LoggedOutView from '../views/LoggedOutView.vue'
import LoginView from '../views/LoginView.vue'

const routes = [
  {
    path: '/about',
    name: 'about',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: function () {
      return import(/* webpackChunkName: "about" */ '../views/AboutView.vue')
    }
  },
  {
    path: '/accountdelete',
    name: 'accountdelete',
    component: AccountDeletionView
  },
  {
    path: '/',
    name: 'homedefault',
    component: LoggedOutView
  },
  {
    path: '/login',
    name: 'login',
    component: LoginView
  }
]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router
