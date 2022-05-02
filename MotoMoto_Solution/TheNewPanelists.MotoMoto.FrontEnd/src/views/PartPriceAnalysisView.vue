<template>
        <div class = PartAnalysis>
            <PartPriceAnalysisComponent/>
        </div>
</template>

<script>
import PartPriceAnalysisComponent from '../components/PartPriceAnalysisPartsListComponent.vue'

export default {
    name: 'PartPriceAnalysisView',
    components: {
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