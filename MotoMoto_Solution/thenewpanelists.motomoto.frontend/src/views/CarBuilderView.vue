<template>
    <div>
      <TabBarComponent/>
      <CarBuilderComponent/>
    </div>
  <kpi-submission-component viewTitle="Car Builder"></kpi-submission-component>
</template>

<script>
import KpiSubmissionComponent from '@/components/KpiSubmissionComponent.vue'
import CarBuilderComponent from '../components/CarBuilderComponent.vue'
import TabBarComponent from '../components/TabBarComponent.vue'
export default {
  name: 'CarBuilderView',
  components: 
  {
    TabBarComponent,
    CarBuilderComponent,
    KpiSubmissionComponent
  }
}
</script>

<style>

</style>
    KpiSubmissionComponent
