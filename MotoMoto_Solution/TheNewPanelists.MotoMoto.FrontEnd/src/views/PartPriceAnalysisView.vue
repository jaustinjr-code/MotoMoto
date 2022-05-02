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
th {
    background-color:green;
    Color:white;
}
th, td {
    width:200px;
    text-align:center;
    border:1px solid black;
    padding:5px
}
.geeks {
    border-right:hidden;
}
.gfg {
    border-collapse:separate;
    border-spacing:0 15px;
}
</style>