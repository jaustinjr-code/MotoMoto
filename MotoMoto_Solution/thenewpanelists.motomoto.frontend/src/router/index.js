import { createRouter, createWebHashHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import AccountDeletionView from '../views/AccountDeletionView.vue'
import FeedView from '../views/CommunityBoardViews/FeedView.vue'

const routes = [
  {
    path: '/',
    name: 'home',
    component: HomeView
  },
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
    path: '/communityboard',
    name: 'communityboard',
    component: FeedView
  }
]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router
