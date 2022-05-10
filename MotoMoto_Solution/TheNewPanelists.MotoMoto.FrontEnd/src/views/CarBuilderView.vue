<template>
    <div>
      <TabBarComponent/>
      <CarBuilderComponent/>
    </div>
</template>

<script>
import CarBuilderComponent from '../components/CarBuilderComponent.vue'
import TabBarComponent from '../components/TabBarComponent.vue'
export default {
  name: 'CarBuilderView',
  components: 
  {
    TabBarComponent,
    CarBuilderComponent
  }
}
</script>

<style>

</style>