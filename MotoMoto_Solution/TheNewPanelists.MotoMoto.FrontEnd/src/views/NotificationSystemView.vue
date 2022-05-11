<template>
  <NotificationSystemComponent/>
  <kpi-submission-component viewTitle="Notification Center"></kpi-submission-component>
</template>

<script>
import KpiSubmissionComponent from '@/components/KpiSubmissionComponent.vue'
import NotificationSystemComponent from '../components/NotificationSystemComponent.vue'
export default {
  name: 'NotificationSystemView',
  components: 
  {
    NotificationSystemComponent,
    KpiSubmissionComponent
  }
}
</script>

<style>

</style>
    KpiSubmissionComponent