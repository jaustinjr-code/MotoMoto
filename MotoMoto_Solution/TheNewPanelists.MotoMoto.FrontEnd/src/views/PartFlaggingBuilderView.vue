<template>
  <PartFlaggingBuilderComponent/>
  <kpi-submission-component viewTitle="Part Flagging"></kpi-submission-component>
</template>

<script>
import KpiSubmissionComponent from '@/components/KpiSubmissionComponent.vue'
import PartFlaggingBuilderComponent from '../components/PartFlaggingBuilderComponent.vue'
export default {
  name: 'PartFlaggingBuilderView',
  components: 
  {
    PartFlaggingBuilderComponent,
    KpiSubmissionComponent
  }
}
</script>

    KpiSubmissionComponent
<style>

</style>