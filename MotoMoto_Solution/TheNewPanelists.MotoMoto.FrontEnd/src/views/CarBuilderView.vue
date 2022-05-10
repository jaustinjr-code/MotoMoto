<template>
  <CarBuilderComponent/>
  <kpi-submission-component viewTitle="Car Builder"></kpi-submission-component>
</template>

<script>
import KpiSubmissionComponent from '@/components/KpiSubmissionComponent.vue'
import CarBuilderComponent from '../components/CarBuilderComponent.vue'
export default {
  name: 'CarBuilderView',
  components: 
  {
    CarBuilderComponent,
    KpiSubmissionComponent
  }
}
</script>

<style>
</style>
    KpiSubmissionComponent