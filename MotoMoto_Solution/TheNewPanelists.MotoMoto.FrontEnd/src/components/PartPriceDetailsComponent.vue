<template>
    <div>
        <h1>Part View</h1>
        <div class="body">
        </div>
    </div>
</template>

<script>

import {instance} from '../router/PartPriceAnalysisConnection'
export default {
    name: 'PartPriceDetails',
    data()
    {
        return {
            part: [],
            partID: 0,
        }
    },
    methods: {
        RetrievePartInformation: async function()
        {
            this.part = []
            let params = {_partID: this.partID}
            await instance.get('PartPriceAnalysisEvaluation/Evaluate', {params}).then((res) =>{
                this.part = res.data;
                console.log(res.data);
            })
        }
    }
}
</script>

<style>

</style>