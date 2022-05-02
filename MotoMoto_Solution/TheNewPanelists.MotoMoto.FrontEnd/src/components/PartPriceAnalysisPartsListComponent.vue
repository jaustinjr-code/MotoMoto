<template>
<body>
    <div id='part-list'>
        <TabBarComponent/>
        <div id='part_selection-list' class="part-selection">
            <h4 class="title">Parts List</h4> 
            <label class='selectString'>Select Part Category:</label>
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
                            <td>Comparison Selection</td>
                            <td>Part Name</td>
                            <td>Part Rating</td>
                            <td>Rating Count</td>
                            <td>Current Price</td>
                            <td>Product Link</td>
                        </tr>
                        <tr class="PartListings" v-for="(part) in paginatedData()" :key=part>
                            <td class="checkBox">
                                <input type="checkbox" name="parts" v-model="checkedParts" :value="part.partID" @change="logProductsSelected(checkedParts)">
                            </td>
                            <td class="partName">
                            <router-link class="productDescr" :to="{name: 'PartPriceDetails', params: {id: part.partID}}">
                                {{part.partName}}
                            </router-link>
                            </td>
                            <td class="partRati">{{part.rating}}</td>
                            <td class="ratCount">{{part.ratingCount}}</td>
                            <td class="curPrice">${{part.currentPrice}}.00</td>
                            <td><a class="proURL" :href="part.productURL">Link</a></td>
                        </tr>
                    </thead>
                </table>
                
                <div class="pageButtons">
                    <button class="buttonLeft" @click="prevPage()">Prev</button>
                    <button class="buttonRight" @click="nextPage()">Next</button>
                    <footer>
                        <p>{{displayPageNumber()}} of {{pageCount()}}</p>
                    </footer>
                </div>
            </div>
        </div>
    </div>
    </body>
</template>

<script>
import TabBarComponent from './TabBarComponent.vue'
import {instance} from '../router/PartPriceAnalysisConnection'

export default {

    data()
    //data attributes in classes
    {
        return {
            pageNumber: 0,
            maxPages: 0,
            categoryID: 0,
            categoryName: '',
            parts: [],
            checkedParts: [],
            returnComparison: []
        }
    },
    components: {
        TabBarComponent
    },
    props: {
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
        retrievePartInformation: async function() {
            this.parts = []
            this.pageNumber = 0
            let params = {_categoryID: this.categoryID};
            await instance.get('PartPriceAnalysisRetrieval', {params}).then((res) => {
                this.parts = res.data["partList"]
                this.categoryName = res.data["categoryName"]
            })
        },
        logProductsSelected: function() {
            if(this.checkedParts.length == 2) {
                this.$router.push({name: 'PartComparison', params: {id1: this.checkedParts[0], id2: this.checkedParts[1]}})
            }
        },
        getCategoryID: function(part) {
            let selector = document.getElementById('part-category-select')
            let partNum = selector.options[selector.selectedIndex].value
            this.categoryID = parseInt(partNum) 
            this.retrievePartInformation()
        },
        logStuff: function() {
            this.returnComparison = this.checkedParts
        },
        goToProduct: function() {
            this.$router.push({name: 'PartPriceDetails', params: {id : part.partID}})
            //this.$router.push({name: 'parts/partID', params: { partID: part['partID']}})
        },
        nextPage: function() {
            if (this.pageNumber < this.pageCount()-1)
            {
                this.pageNumber++;
                console.log(this.pageNumber);
            }
        },
        prevPage: function() {
            if (this.pageNumber > 0)
            {
                this.pageNumber--;
                console.log(this.pageNumber);
            }
        },
        pageCount: function() {
            let l = this.parts.length,
                s = this.size;
            this.maxPages = Math.ceil(l/s);
            return this.maxPages;
        },
        paginatedData: function() {
            const start = this.pageNumber * this.size,
            end = start + this.size;
            return this.parts.slice(start, end);
        },
        displayPageNumber: function()
        {
            if (this.maxPages === 0) {
                return 0;
            }
            return this.pageNumber+1;
        }
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
.selectString {
    margin-right: 10px;
}
.buttonLeft {
    display: inline-block;
    padding: 2px 12px;
    background-color: rgb(9, 189, 144);
    text-decoration: white;
    margin-left: 2px;
}
.buttonRight {
    display: inline-block;
    padding: 2px 10px;
    background-color: rgb(9, 189, 144);
    text-decoration: white;
    margin-left: 5px;
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
.submitButton {
    display: inline-block;
    padding: 2px 12px;
    background-color: rgb(9, 189, 144);
    text-decoration: white;
}
.buttonSubmit {
    text-align: left;
    padding-left: 50px;
}
</style>

