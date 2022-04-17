<template>
  <div class = dm>
    <DirectMessageSideBarComponent @receiver = "getUserClicked"></DirectMessageSideBarComponent>
    <DirectMessageComponent :receiver = "receiver"></DirectMessageComponent>
  </div>
</template>

<script>

import DirectMessageSideBarComponent from "../components/DirectMessageSideBarComponent.vue"
import DirectMessageComponent from '../components/DirectMessageComponent.vue'

export default {
  name: 'DirectMessageView',
  components: 
  {
    DirectMessageSideBarComponent,
    DirectMessageComponent
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
    }
  }
}
</script>

<style>
.dm
{
  display:flex;
  min-height: 100vh;
}
</style>