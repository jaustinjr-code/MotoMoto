import http from "./Http-coman"
<template>
  <div class="home">
    <TabBarComponent/>
    <button @click = "goToEventList"> Event List </button>
    <button @click = "goToCarBuilder"> Car Builder </button>
    <button @click = "goToPersonalizedRecommendations"> Personalized Recommendations </button>
    <button @click="goToPartFlaggingBuilder">Part Flagging Builder</button>
    <button @click="goToPartFlaggingPost">Part Flagging Post</button>
    <button @click="goToNoteDashboard">Note Dashboard</button>
    <button @click="goToDirectMessage">DirectMessage</button>
    <button @click="goToNotificationSystem"> Notification Center </button>
    <button @click="goToCommunityBoard">Community Board</button>
    <button @click="goToMeetingPointDirections">Meeting Point Directions</button>
  </div>
</template>

<script>
// @ is an alias to /src
import HelloWorld from '@/components/HelloWorld.vue'
import TabBarComponent from '../components/TabBarComponent.vue';
import LogoutComponentVue from '../components/LogoutComponent.vue';
import router from '../router'
import { useCookies } from "vue3-cookies";
import LogoutComponent from '../components/LogoutComponent.vue';

export default {
  name: 'HomeView',
  components: {
    TabBarComponent,
    LogoutComponent
},
mounted() {
    if (!this.$cookies.isKey("userId")) {
        this.$cookies.set("userId", -1,"1h")
      }
    if (!this.$cookies.isKey("username")) {
        this.$cookies.set("username", "guest","1hr")
      }
},
  setup() {
    const { cookies } = useCookies();
    return { cookies };
  },

  methods:{
    goToLogin(){
      this.$router.push('/Login');
    },
    goToCarBuilder(){
      this.$router.push('/CarBuilder');
    },
    goToEventList(){
      this.$router.push('/EventList');
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
    goToDirectMessage()
    {
      this.$router.push('/DM');
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
