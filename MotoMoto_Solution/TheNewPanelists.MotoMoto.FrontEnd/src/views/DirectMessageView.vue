<template>
  <LogoutComponent/>
  <div class = dm>
    <DirectMessageSideBarComponent @receiver = "getUserClicked"></DirectMessageSideBarComponent>
    <DirectMessageComponent :receiver = "receiver"></DirectMessageComponent>
    <kpi-submission-component viewTitle="Direct Message"></kpi-submission-component>
  </div>
</template>

<script>

import DirectMessageSideBarComponent from "../components/DirectMessageSideBarComponent.vue"
import DirectMessageComponent from '../components/DirectMessageComponent.vue'
import LogoutComponent from "@/components/LogoutComponent.vue";
import KpiSubmissionComponent from "../components/KpiSubmissionComponent.vue"

export default {
  name: 'DirectMessageView',

  components: 
  {
    DirectMessageSideBarComponent,
    DirectMessageComponent,
    LogoutComponent,
    KpiSubmissionComponent
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