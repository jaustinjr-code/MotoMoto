import { createRouter, createWebHashHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import AccountDeletionView from '../views/AccountDeletionView.vue'
import FeedView from '../views/CommunityBoardViews/FeedView.vue'
import CreatePostView from '../views/CommunityBoardViews/CreatePostView.vue'
import PostDetails from '../views/CommunityBoardViews/PostDetails.vue'
import DirectMessage from '../views/DirectMessageView.vue'
import Login from '../views/LoginView.vue'
import PartFlaggingBuilder from '../views/PartFlaggingBuilderView.vue'
import PartFlaggingPost from '../views/PartFlaggingPostView.vue'
import CarBuilder from '../views/CarBuilderView.vue'
import EventList from '../views/EventListView.vue'
import Registration from '../views/RegistrationView.vue'
import RegistrationConfirmation from '../views/RegistrationConfirmationView.vue'
import PartPriceAnalysis from '../views/PartPriceAnalysisView.vue'
import PartPriceDetails from '../views/PartPriceDetailsView.vue'
import PartComparison from '../views/PartPriceAnalysisComparisonView.vue'
import UserProfile from '../views/UserProfileView.vue'
import EditPreferences from '../views/EditPreferencesView'
import MeetingPointDirections from '../views/MeetingPointDirectionsView.vue'
import NoteDashboardView from '../views/NoteDashboardView.vue'
import NotificationSystem from '../views/NotificationSystemView'
import PersonalizedRecommendations from '../views/PersonalizedRecommendationsView.vue'
import UsageAnalysisDashboard from '../views/UsageAnalysisDashboardView.vue'
import EditProfile from '../views/EditProfileView.vue'
import CreateEventPost from '../views/CreateEventPostView.vue'
import AboutView from '../views/AboutView.vue'
import FAQ from '../views/FAQView.vue'

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
    path:'/FAQ',
    name: 'FAQ',
    component: FAQ
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
  },
  {
    path: '/createpost/:feedName',
    name: 'createpost',
    //meta: {guest: false}, // Attempt at restricting direct access
    component: CreatePostView,
    props: true,
  },
  {
    // path: '/postdetails', // Lets the id to show up in url
    path: '/postdetails/:id', // Lets the id to show up in url
    name: 'postdetails',
    //meta: {guest: false}, // Attempt at restricting direct access
    component: PostDetails,
    props: true,
  },
  {
    path: '/DM',
    name: 'DirectMessage',
    component: DirectMessage
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
    path: '/CarBuilder',
    name: 'CarBuilder',
    component: CarBuilder
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
    path: '/Registration/Confirmation/:',
    name: 'RegistrationConfirmation',
    component: RegistrationConfirmation
  },
  {
    path: '/Parts',
    name: 'PartPriceAnalysis',
    component: PartPriceAnalysis
  },
  {
    path: '/PartDetails/:id',
    name: 'PartPriceDetails',
    component: PartPriceDetails
  },
  {
    path: '/PartComaprison/PartOne/:id1/PartTwo/:id2',
    name: 'PartComparison',
    component: PartComparison
  },
  {
    path: '/UserProfile/:username',
    name: 'UserProfile',
    component: UserProfile,
  },
  {
    path: '/UserProfile/EditPreferences',
    name: 'EditPreferences',
    component: EditPreferences
  },
  {
    path: '/MeetingPointDirections',
    name: 'MeetingPointDirections',
    component: MeetingPointDirections
  },
  {
    path: '/noteDashboard',
    name: 'noteDashabord',
    component: NoteDashboardView
  },
  {
    path: '/NotificationSystem',
    name: 'NotificationSystem',
    component: NotificationSystem
  },
  {
    path: '/PersonalizedRecommendations',
    name: 'PersonalizedRecommendations',
    component: PersonalizedRecommendations
  },
  {
    path: '/UsageAnalysisDashboard',
    name: 'UsageAnalysisDashboard',
    component: UsageAnalysisDashboard
  },
  {
    path: '/EditProfile',
    name: 'EditProfile',
    component: EditProfile
  },
  {
    path: '/CreateEventPost',
    name: 'CreateEventPost',
    component: CreateEventPost
  },
  {
    path: '/About',
    name: 'About',
    component: AboutView
  }
]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router
