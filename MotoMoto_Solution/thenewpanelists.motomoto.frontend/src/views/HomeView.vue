import http from "./Http-coman"
<template>
  <div class="home">
    <HelloWorld msg="Welcome to MotoMoto!"/>
    <button @click = "goToLogin"> Login </button>
    <LoginComponent/>
    <button @click = "goToEventList"> Event List </button>
    <EventListDashboardComponent/>
    <button @click = "goToRegistration"> Register </button>
    <button @click = "goToPersonalizedRecommendations"> Personalized Recommendations </button>
    <button @click = "goToParts"> Parts </button>
    <button @click="goToPartFlaggingBuilder">Part Flagging Builder</button>
    <button @click="goToPartFlaggingPost">Part Flagging Post</button>
    <button @click="goToNoteDashboard">Note Dashboard</button>
    <button @click="goToNotificationSystem"> Notification Center </button>
    <button @click="goToCommunityBoard">Community Board</button>
    <button @click="goToMeetingPointDirections">Meeting Point Directions</button>
  </div>
</template>

<script>
// @ is an alias to /src
import HelloWorld from '@/components/HelloWorld.vue'
import router from '../router'
import { useCookies } from "vue3-cookies";

export default {
  name: 'HomeView',
  components: {
    HelloWorld
  },
  setup() {
    const { cookies } = useCookies();
    return { cookies };
  },
  created() {
      if (!this.$cookies.isKey("userId")) {
        this.$cookies.set("userId", "guest","1h")
      }
  },
  methods:{
    goToLogin(){
      this.$router.push('/Login');
    },
    goToEventList(){
      this.$router.push('/EventList');
    },
    goToRegistration(){
      this.$router.push('/Registration')
    },
    goToParts(){
      this.$router.push('/parts')
    },
    goToPartFlaggingBuilder(){
      this.$router.push('/PartFlaggingBuilder')
    },
    goToPartFlaggingPost(){
      this.$router.push('/PartFlaggingPost')
    },
    goToNoteDashboard()
    {
      this.$router.push('/noteDashboard');
	  },
    goToNotificationSystem() {
      this.$router.push('/NotificationSystem')
    },
    goToCommunityBoard(){
      this.$router.push('/communityboard')
    },
    goToMeetingPointDirections() {
      this.$router.push('/MeetingPointDirections')
    },
    goToPersonalizedRecommendations(){
      this.$router.push('/PersonalizedRecommendations')
    }
  }
}
</script>
