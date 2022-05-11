import http from "./Http-coman"
<template>
  <div class="home">
    <TabBarComponent/>
    <main-feed-home-display-component/>
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
import MainFeedHomeDisplayComponent from '@/components/HomeDisplayComponents/MainFeedHomeDisplayComponent.vue';

export default {
  name: 'HomeView',
  components: {
    TabBarComponent,
    LogoutComponent,
    MainFeedHomeDisplayComponent
},
mounted: function() {
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
    goToEventList(){
      this.$router.push('/EventList');
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
    goToMeetingPointDirections() {
      this.$router.push('/MeetingPointDirections')
    },
    goToPersonalizedRecommendations(){
      this.$router.push('/PersonalizedRecommendations')
    }
  }
}
</script>
