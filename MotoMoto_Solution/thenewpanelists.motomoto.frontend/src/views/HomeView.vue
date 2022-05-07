import http from "./Http-coman"
<template>
  <div class="home">
    <HelloWorld msg="Welcome to MotoMoto!"/>
    <button @click = "goToLogin"> Login </button>
    <LoginComponent/>
    <button @click = "goToEventList"> Event List </button>
    <CarBuilderComponent/>
    <button @click = "goToCarBuilder"> Car Builder </button>
    <EventListDashboardComponent/>
    <button @click = "goToRegistration"> Register </button>
    <RegistrationComponent/>
    <button @click = "goToPersonalizedRecommendations"> Personalized Recommendations </button>
    <RegistrationComponent/>
    <button @click="goToPartFlaggingBuilder">Part Flagging Builder</button>
    <button @click="goToPartFlaggingPost">Part Flagging Post</button>
  </div>
</template>

<script>
// @ is an alias to /src
import HelloWorld from '@/components/HelloWorld.vue'

export default {
  name: 'HomeView',
  components: {
    HelloWorld
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
    goToRegistration(){
      this.$router.push('/Registration')
    },
    goToPersonalizedRecommendations(){
      this.$router.push('/PersonalizedRecommendations')
    },
    goToPartFlaggingBuilder(){
      this.$router.push('/PartFlaggingBuilder')
    },
    goToPartFlaggingPost(){
      this.$router.push('/PartFlaggingPost')
    }
  }
}
</script>
