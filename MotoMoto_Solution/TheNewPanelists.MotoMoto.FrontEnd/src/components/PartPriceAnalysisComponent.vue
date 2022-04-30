<template>
<body>
    <div id='part-list'>
        <h1>Vehicle Parts</h1>
        <div id='part_selection-list' class="part-selection">
            <label>Select Part Category:   </label>
            <select id='part-category-select' @change="getCategoryID()">
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
                <table>
                    <thead>
                        <tr class="titles">
                            <td><input type="submit" value="Compare"></td>
                            <td>Part Name</td>
                            <td>Part Rating</td>
                            <td>Rating Count</td>
                            <td>Current Price</td>
                            <td>Product Link</td>
                        </tr>
                        <tr class="PartListings" v-for="part in paginatedData()" :key=part>
                            <td class="checkBox"><input type="checkbox"></td>
                            <td class="partName">{{part['partName']}}</td>
                            <td class="partRati">{{part['rating']}}</td>
                            <td class="ratCount">{{part['ratingCount']}}</td>
                            <td class="curPrice">${{part['currentPrice']}}.00</td>
                            <td><a class="proURL" :href="part['productURL']">Link</a></td>
                        </tr>
                    </thead>
                </table>
                <button @click="prevPage()">Previous</button>
                <button @click="nextPage()">Next</button>
            </div>
        </div>
    </div>
    </body>
</template>

<script>
import {instance} from '../router/PartPriceAnalysisConnection'

export default {

    data()
    //data attributes in classes
    {
        return {
            pageNumber: 0,
            categoryID: 0,
            categoryName: '',
            parts: [],
            comparedParts: []
        }
    },
    props:{
        parts:{
        type:Array,
        required:true
        },
        size:{
        type:Number,
        required:false,
        default: 10
        }
    },
    methods: {
        retrievePartInformation: async function()
        {
            this.parts = []
            let params = {_categoryID: this.categoryID};
            await instance.get('PartPriceAnalysisRetrieval', {params}).then((res) => {
                this.parts = res.data["partList"]
                this.categoryName = res.data["categoryName"]
                console.log(this.parts)
            })
        },
        getCategoryID: function()
        {
            let selector = document.getElementById('part-category-select')
            let partNum = selector.options[selector.selectedIndex].value
            this.categoryID = parseInt(partNum)
            console.log(this.categoryID)  
            this.retrievePartInformation()
        },
        nextPage: function() {
            this.pageNumber++;
        },
        prevPage: function() {
            this.pageNumber--;
        },
        pageCount: function() {
            let l = this.parts.length,
                s = this.size;
            return Math.ceil(l/s);
        },
        paginatedData: function() {
            const start = this.pageNumber * this.size,
            end = start + this.size;
            return this.parts.slice(start, end);
        },
        
    },
}
</script>

<style>
body {
    padding-bottom: 20px;
}

table {
    margin-left: auto;
    margin-right: auto;
}
.titles {
  overflow: auto;
  max-width: 100%;
  background:
    linear-gradient(to right, white 30%, rgba(255,255,255,0)),
    linear-gradient(to right, rgba(255,255,255,0), white 70%) 0 100%,
    radial-gradient(farthest-side at 0% 50%, rgba(0,0,0,.2), rgba(0,0,0,0)),
    radial-gradient(farthest-side at 100% 50%, rgba(0,0,0,.2), rgba(0,0,0,0)) 0 100%;
  background-repeat: no-repeat;
  background-color: white;
  background-size: 40px 100%, 40px 100%, 14px 100%, 14px 100%;
  background-position: 0 0, 100%, 0 0, 100%;
  background-attachment: local, local, scroll, scroll;
}
</style>

