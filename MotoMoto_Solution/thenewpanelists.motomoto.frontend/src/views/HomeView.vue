import http from "./Http-coman"
<template>
  <div class="home">
    <TabBarComponent/>
    <button @click = "goToEventList"> Event List </button>
    <button @click = "goToRegistration"> Register </button>
    <button @click = "goToPersonalizedRecommendations"> Personalized Recommendations </button>
    <button @click="goToPartFlaggingBuilder">Part Flagging Builder</button>
    <button @click="goToPartFlaggingPost">Part Flagging Post</button>
    <button @click="goToNotificationSystem"> Notification Center </button>
    <LogoutComponent/>
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
  setup() {
    const { cookies } = useCookies();
    return { cookies };
  },
  created() {
      if (!this.$cookies.isKey("userId")) {
        this.$cookies.set("userId", -1,"1h")
      }
      if (!this.$cookies.isKey("username")) {
        this.$cookies.set("username", "guest","1hr")
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
    goToNotificationSystem() {
      this.$router.push('/NotificationSystem')
    },
    goToCommunityBoard(){
      this.$router.push('/communityboard')
    },
    goToPersonalizedRecommendations(){
      this.$router.push('/PersonalizedRecommendations')
    }
  }
}
</script>
