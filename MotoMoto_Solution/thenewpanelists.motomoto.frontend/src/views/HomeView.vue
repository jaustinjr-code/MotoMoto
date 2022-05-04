import http from "./Http-coman"
<template>
  <div class="home">
    <HelloWorld msg="Welcome to MotoMoto!"/>
    <button @click = "goToLogin"> Login </button>
    <LoginComponent/>
    <button @click = "goToEventList"> Event List </button>
    <EventListDashboardComponent/>
    <button @click = "goToRegistration"> Register </button>
    <RegistrationComponent/>
    <button @click = "goToPersonalizedRecommendations"> Personalized Recommendations </button>
    <PersonalizedRecommendationsComponent/>
    <button @click = "goToParts"> Parts </button>
    <RegistrationComponent/>
    <button @click="goToPartFlaggingBuilder">Part Flagging Builder</button>
    <button @click="goToPartFlaggingPost">Part Flagging Post</button>
    <button @click="goToNotificationSystem"> Notification Center </button>
    <button @click="goToCommunityBoard">Community Board</button>
  </div>
</template>

<script>
// @ is an alias to /src
import HelloWorld from '@/components/HelloWorld.vue'
import router from '../router'

export default {
  name: 'HomeView',
  components: {
    HelloWorld
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
    goToNotificationSystem() {
      this.$router.push('/NotificationSystem')
    },
    goToCommunityBoard(){
      this.$router.push('/communityboard')
    }
  }
}
</script>
