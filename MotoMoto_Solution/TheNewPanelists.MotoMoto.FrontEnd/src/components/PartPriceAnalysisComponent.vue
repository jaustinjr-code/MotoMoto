<template>
    <div id='part-list'>
        <h1>Vehicle Parts</h1>
        <div id='part_selection-list' class="part-selection">
            <label>Select Part Category: </label>
            <select id='part-category-select' @change="GetCategoryID()">
                <option value='N'>None</option>
                <option value='0'>Alternator</option>
                <option value='1'>Brake Pads</option>
                <option value='2'>Brake Rotor</option>
                <option value='3'>Cylinder Head</option>
                <option value='4'>Engine Block</option>
                <option value='5'>Exhaust Manifold</option>
                <option value='6'>Muffler</option>
                <option value='7'>Oil Filter</option>
                <option value='8'>Radiator</option>
                <option value='10'>Spark Plug</option>
                <option value='11'>Timing Belt</option>
                <option value='12'>Turbo</option>
                <option value='13'>Water Pump</option>
            </select>
            <div>
                <ul>
                    <li v-for="part in parts" :key=part>
                        {{part['partName']}}     {{part['rating']}}
                    </li>
                </ul>
            </div>
        </div>
        
    </div>
</template>

<script>
import {instance} from '../router/PartPriceAnalysisConnection'
export default {
    data()
    //data attributes in classes
    {
        return {
            categoryID: 0,
            categoryName: '',
            parts: []
        }
    },
    methods: {
        RetrievePartInformation: async function()
        {
            this.parts = []
            let params = {_categoryID: this.categoryID};
            await instance.get('PartPriceAnalysisRetrieval', {params}).then((res) => {
                this.parts = res.data["partList"]
                this.categoryName = res.data["categoryName"]
                console.log(this.parts)
            })
        },
        GetCategoryID: function()
        {
            let selector = document.getElementById('part-category-select')
            let partNum = selector.options[selector.selectedIndex].value
            this.categoryID = parseInt(partNum)
            console.log(this.categoryID)  
            this.RetrievePartInformation()
        },
    },
}
</script>

<style>
.part-selection {
    font-size: 20px;
}
</style>
