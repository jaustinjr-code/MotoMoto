<template>
<div>
    <TabBarComponent/>
    <h5>Part Price Comparison</h5>
    <table class="partComp">
        <thead>
            <tr class="titles">
                <div class="partNameCol">
                    <td class="partN">Part Name</td>
                </div>
                <td class="partR">Part Rating</td>
                <td class="ratCou">Rating Count</td>
                <td class="currPri">Current Price</td>
                <td class="proLink">Product Link</td>
            </tr>
        </thead>
        <thead>
            <tr class="Items" v-for="(part) in parts.comparisonParts" :key=part>
                <div class="partNameCol">
                    <td>{{part.partName}}</td>
                </div>
                <td class="partN">{{part.rating}}</td>
                <td class="ratCou">{{part.ratingCount}}</td>
                <td class="currPri">${{part.currentPrice}}</td>
                <td><a class="proURL" :href="part.productURL">Link</a></td>
            </tr>
        </thead>
    </table>
    <h6 class="priceDiff" v-for="price in parts.currentPriceDifference" :key=price>Current Price Difference: ${{price}}.00</h6>
    <h5>Part Price Comparison Over Time</h5>
    <!-- <table>
        <thead>
            <tr class="titles" v-for="(part) in comparisonParts" :key=part>
                <tr class="history" v-for="history in part.historicalPrices" :key=history>
                    <td>{{history.dateTime.slice(0,10)}}</td>
                </tr>
            </tr>
        </thead>
    </table> -->
</div>
    
</template>

<script>
import PopulatedTables from '../components/PopulatedTableComponent.vue'
import TabBarComponent from '../components/TabBarComponent.vue'
import {instance} from '../router/PartPriceAnalysisConnection'

export default {
    data() {
        return {
            parts: {},
            partIDOne: 0,
            partIDTwo: 0,
            curPriceDiff: 0,
            comparisonParts: [],
            partPrices: [],
        }
    },
    components: {
        TabBarComponent,
        PopulatedTables
    },
    mounted() {
        this.partIDOne = this.$route.params.id1;
        this.partIDTwo = this.$route.params.id2;
        this.retrievePartInformation();

    },
    methods: {
        retrievePartInformation: async function() {
            let params = {_partIdOne: this.partIDOne, _partIdTwo: this.partIDTwo};

            await instance.get('PartPriceAnalysisEvaluation/compareParts', {params}).then((res) =>{
                this.parts = res.data;
                this.comparisonParts = res.data['comparisonParts'];
            })
        },
        mergePartHistoryForDisplay: function() {
            displayDict = {}
            for (let i = 0; i < this.comparisonParts.historicalPrices.length; i++) {
                if ((this.comparisonParts.historicalPrices[i].dateTime in displayDict) === false) {
                    displayDict[this.comparisonParts.historicalPrices] = [];
                }
            }
            // for (let i = 0; i < this.comparisonParts.historicalPrices.length; i++) {
            //     for (let j = 0; j < displayDict.length; j++) {
            //         if (this.comparisonParts.historicalPrices[i].dateTime === displayDict)
            //     } 
            // }
            // return (displayDict);
        },
    },
}
</script>

<style>
p.priceDiff {
    font: 25px;
}
h6.priceDiff {
    padding-top: 10px;
    padding-bottom: 25px;
}
</style>

dictionary = {
    dates = {date: [], date: [], date[]}
}