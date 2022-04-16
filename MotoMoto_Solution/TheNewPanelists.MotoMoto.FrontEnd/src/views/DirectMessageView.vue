<template>
  <div class = dm>
    <DirectMessageSideBarComponent/>
    <DirectMessageComponent/>
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