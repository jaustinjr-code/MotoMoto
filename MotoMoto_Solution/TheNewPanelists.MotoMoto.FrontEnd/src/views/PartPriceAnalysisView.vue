<template>
        <div class = PartAnalysis>
            <PartPriceAnalysisComponent/>
            <kpi-submission-component viewTitle="Part Price Analysis"></kpi-submission-component>
        </div>
</template>

<script>
import KpiSubmissionComponent from '@/components/KpiSubmissionComponent.vue';
import PartPriceAnalysisComponent from '../components/PartPriceAnalysisPartsListComponent.vue'

export default {
    name: 'PartPriceAnalysisView',
    components: {
        PartPriceAnalysisComponent,
        KpiSubmissionComponent
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
th {
    background-color:green;
    Color:white;
}
th, td {
    width:200px;
    text-align:center;
    padding:5px;
    border-collapse: collapse;
}
</style>