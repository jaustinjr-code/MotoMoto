<template>
  <PartFlaggingPostComponent/>
  <kpi-submission-component viewTitle="Part Flagging"></kpi-submission-component>
</template>

<script>
import KpiSubmissionComponent from '@/components/KpiSubmissionComponent.vue'
import PartFlaggingPostComponent from '../components/PartFlaggingPostComponent.vue'
export default {
  name: 'PartFlaggingPostView',
  components: 
  {
    PartFlaggingPostComponent,
    KpiSubmissionComponent
  }
}
</script>

<style>

</style>