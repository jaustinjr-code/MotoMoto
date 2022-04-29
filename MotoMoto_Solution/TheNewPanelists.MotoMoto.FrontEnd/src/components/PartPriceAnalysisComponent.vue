<template>
    <div id='part-list'>
        <h1>Vehicle Parts</h1>
        <div id='part_selection-list' class="part-selection">
        </div>
    </div>
</template>

<script>
import axios from 'axios';
import {instance} from '../router/partPriceAnalysisConnection'
export default {
    data()
    {
        return {
            partName: "",
            part: [],
            partPrice: 0,
        }
    },
    methods: {
        RetrievePartInformation: async function()
        {
            let categoryID = document.getElementById('part-selection')
            
            let params = {_categoryID: categoryID};
            await instance.get('PartPriceAnalysisRetrieval/Categorial', {params}).then((res) => {
                let partData = res.data.partList;
            })
        },
    },
}
</script>

<style>

</style>
