import { createRouter, createWebHashHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import DirectMessage from '../views/DirectMessageView.vue'
import Login from '../views/LoginView.vue'
import CommunityDashboard from '../views/CommunityView.vue'
import PartFlaggingBuilder from '../views/PartFlaggingBuilderView.vue'
import PartFlaggingPost from '../views/PartFlaggingPostView.vue'
import EventList from '../views/EventListView.vue'
import Registration from '../views/RegistrationView.vue'
import PersonalizedRecommendations from '../views/PersonalizedRecommendationsView.vue'
import PartPriceAnalysis from '../views/PartPriceAnalysisView.vue'

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
    path: '/PartFlaggingBuilder',
    name: 'PartFlaggingCarBuilder',
    component: PartFlaggingBuilder
  },
  {
    path: '/PartFlaggingPost',
    name: 'PartFlaggingPost',
    component: PartFlaggingPost
  },
  {
    path: '/EventList',
    name: 'EventList',
    component: EventList
  },
  {
    path: '/Registration',
    name: 'Registration',
    component: Registration
  },
  {
    path: '/PersonalizedRecommendations',
    name: 'PersonalizedRecommendations',
    component: PersonalizedRecommendations
  },
  {
    path: '/Parts',
    name: 'PartPriceAnalysis',
    component: PartPriceAnalysis
  }
]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router
