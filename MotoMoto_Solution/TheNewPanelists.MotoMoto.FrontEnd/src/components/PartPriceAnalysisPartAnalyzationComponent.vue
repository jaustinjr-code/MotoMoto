<template>
    
</template>

<script>
import {instance} from '../router/PartPriceAnalysisConnection'
export default {
    data()
    {
        return {
            part = []
        }
    },
    methods: {
        RetrievePartInformation: async function()
        {
            this.part = []
            let params = {_partID: this.partID}
            await instance.get('PartPriceAnalysisEvaluation/Evaluate', {params}).then((res) =>{
                this.part = res.data;
            })
        }
        
    }
}
</script>

<style>

</style>