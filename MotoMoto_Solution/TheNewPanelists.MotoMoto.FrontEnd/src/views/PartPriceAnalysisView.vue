<template>
        <div class = PartAnalysis></div>
        <TabBarComponent/>
        <PartPriceAnalysisComponent/>
</template>

<script>
import PartPriceAnalysisComponent from '../components/PartPriceAnalysisComponent.vue'
import TabBarComponent from '../components/TabBarComponent.vue'
export default {
    name: 'PartPriceAnalysisView',
    components:
    {
        TabBarComponent,
        PartPriceAnalysisComponent
    },
    methods:
    {
        directMessage()
        {
            this.$router.push({path: '/'});
        },
        goToHome(){
            this.$router.push({path: '/'});
        }
    }
}
</script>

<style>

</style>