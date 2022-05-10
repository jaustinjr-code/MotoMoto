<template>
  <div class = logout>
    <button @click="goHomePage">Home</button>
    <LogoutComponent/>
  </div>
  <div class = dm>
    <DirectMessageSideBarComponent @receiver = "getUserClicked"></DirectMessageSideBarComponent>
    <DirectMessageComponent :receiver = "receiver"></DirectMessageComponent>
  </div>
</template>

<script>

import DirectMessageSideBarComponent from "../components/DirectMessageSideBarComponent.vue"
import DirectMessageComponent from '../components/DirectMessageComponent.vue'
import LogoutComponent from "@/components/LogoutComponent.vue";


export default {
  name: 'DirectMessageView',

  components: 
  {
    DirectMessageSideBarComponent,
    DirectMessageComponent,
    LogoutComponent
},
  data(){
    return{
      receiver: '',
    }
  },
  methods:
  {
    getUserClicked(receiver)
    {
      this.receiver = receiver;
    },
    goHomePage()
    {
      this.$router.push('/');
    }
  }
}
</script>

<style scoped>
.dm
{
  display:flex;
  min-height: 100vh;
}
.logout
{
  display: inline;
  float: right;
  
}
button
{
    border-radius: 5px;
    margin-left: 5px;
    margin-top: 5px; 
    margin-bottom: 5px; 
    background-color: #555;
    color: white;
    border: none;
}

</style>