import http from "./Http-coman"
<template>
<<<<<<< HEAD
  <div id="app">
    
=======
  <div class="home">
    <HelloWorld msg="Welcome to MotoMoto!"/>
    <button @click = "goToLogin"> Login </button>
    <LoginComponent/>
    <button @click = "goToEventList"> Event List </button>
    <EventListDashboardComponent/>
    <button @click = "goToRegistration"> Register </button>
    <RegistrationComponent/>
>>>>>>> main
  </div>
</template>

<script>
// @ is an alias to /src


export default {
  name: 'app',
  components: {
<<<<<<< HEAD
=======
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
    }
>>>>>>> main
  }
}
</script>
