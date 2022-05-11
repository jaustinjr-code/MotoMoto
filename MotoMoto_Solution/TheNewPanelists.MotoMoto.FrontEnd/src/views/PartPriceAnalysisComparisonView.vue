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
    <div class="priceTrendGraph">
            <canvas id="partTrendGraph" width="585" height="450"></canvas>
    </div>
    <table>
        <thead>
            <tr>
                <td>Part Name</td>
                <td class="titles" v-for="(date) in partDates" :key=date>{{date.slice(0,10)}}</td>
            </tr>
        </thead>
        <thead>
            <tr>
                <td>{{partNameOne.slice(0,35)}}</td>
                <td class="titles" v-for="(partP1) in partHistory1" :key=partP1>${{partP1}}</td>
            </tr>
        </thead>
        <thead>
            <tr>
                <td>{{partNameTwo.slice(0,35)}}</td>
                <td class="titles" v-for="(partP2) in partHistory2" :key=partP2>${{partP2}}</td>
            </tr>
        </thead>
    </table>
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
            partDates: [],
            partNameOne: "",
            partNameTwo: "",
            partHistory1: [],
            partHistory2: [],
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
                this.assignPartNames();
                this.assignPartHistoryDate();
            })
        },
        assignPartNames: function() {
            this.partNameOne= this.comparisonParts[0].partName
            this.partNameTwo= this.comparisonParts[1].partName
        },
        assignPartHistoryDate: function() {
            this.partDates = this.comparisonParts[0].historicalDate
            this.partHistory1 = this.comparisonParts[0].histroicalListingPrice
            this.partHistory2 = this.comparisonParts[1].histroicalListingPrice
            this.drawGraph();
        },
        determineLargerGraph: function() {
            let max = 0;
            let list = 0;
            for (let i = 0; i < this.partHistory1; i++) {
                if (this.partHistory1[i] > max)
                {
                    max = 1;
                    list = 1;
                } else if (this.partHistory2[i] > max) {
                    max = 2;
                    list = 2;
                }
            }
            switch(list) {
                case 1:
                    break;
                case 2:
                    temp = this.partHistory1;
                    this.partHistory1 = this.partHistory2;
                    this.partHistory2 = temp;
                    break;
            }
        },
        drawGraph: function() {
            let maxOne = Math.max(this.partHistory1);
            let maxTwo = Math.max(this.partHistory2);
            if (maxOne > maxTwo) {
                var dataArr = this.partHistory1;
                var dataArr2 = this.partHistory2;
            } else {
                var dataArr2 = this.partHistory1;
                var dataArr = this.partHistory2;
            }
            var arrayLen = dataArr.length;
            var arrayLen2 = dataArr2.length;
            var canvas = document.getElementById("partTrendGraph");  
            var context = canvas.getContext("2d"); 
            
        
            var graphTop = 25;  
            var graphBot = 375;  
            var graphLef = 25;  
            var graphRight = 475;  
            var graphHei = 350;    
        
            var largest = 0;  
            for( var i = 0; i < arrayLen; i++ ){  
                if( dataArr[i] > largest){  
                    largest = dataArr[i];  
                }  
            }  
            context.clearRect(0, 0, 500, 500);  
            context.font = "16px Arial";  
        
            context.beginPath();  
            context.moveTo(graphLef, graphBot);  
            context.lineTo(graphRight, graphBot);  
            context.lineTo(graphRight, graphTop);  
            context.stroke();  
            
            context.beginPath();  
            context.strokeStyle = "black";  
            context.moveTo(graphLef, graphTop);  
            context.lineTo(graphRight, graphTop);

            context.beginPath();  
            context.moveTo(graphLef, (graphHei) / 4 * 3 + graphTop);  
            context.lineTo(graphRight, (graphHei) / 4 * 3 + graphTop);

            context.fillText(largest / 4, graphRight + 15, (graphHei) / 4 * 3 + graphTop);  
            context.stroke();    
            context.beginPath();  
            
            context.moveTo(graphLef, (graphHei) / 2 + graphTop );  
            context.lineTo(graphRight, (graphHei) / 2 + graphTop );  

            context.fillText(largest / 2, graphRight + 15, (graphHei) / 2 + graphTop);  
            context.stroke();  
        
            context.beginPath();  
            context.moveTo(graphLef, (graphHei) / 4 + graphTop);  
            context.lineTo(graphRight, (graphHei) / 4 + graphTop );   
            context.fillText(largest / 4 * 3, graphRight + 15, (graphHei) / 4 + graphTop);  
            context.stroke();

            context.fillText("Months", graphRight / 2.2, graphBot + 50);  
            context.fillText("Cost", graphRight + 70, graphHei / 1.8);  
        
            context.beginPath();  
            context.lineJoin = "round";  
            context.strokeStyle = "blue";  
        
            context.moveTo(graphLef, (graphHei - dataArr[0] / largest * graphHei) + graphTop);  
            context.fillText("1", 15, graphBot + 25);  
            for(var i = 1; i < arrayLen; i++){  
                context.lineTo( graphRight / arrayLen * i + graphLef, (graphHei - dataArr[i] / largest * graphHei) + graphTop);   
                context.fillText((i + 1), graphRight / arrayLen * i, graphBot + 25); 
            }

            context.stroke(); 
            context.beginPath();
             context.lineJoin = "round";  
            context.strokeStyle = "red";  
        
            context.moveTo(graphLef, (graphHei - dataArr2[0] / largest * graphHei) + graphTop);   
            for(var i = 1; i < arrayLen; i++){  
                context.lineTo( graphRight / arrayLen2 * i + graphLef, (graphHei - dataArr2[i] / largest * graphHei) + graphTop);   
            }
            context.stroke(); 
        },
    },
}
</script>

<style>
.partComp {
    border-collapse: collapse;
}
p.priceDiff {
    font: 25px;
}
h6.priceDiff {
    padding-top: 10px;
    padding-bottom: 25px;
}
div.priceTrendGraph {
    padding-top: 20px;
}
.line-chart {
    list-style: none;
    margin: 0;
    padding: 0;
}
.priceTrendGraph {
    padding-left: 50px;
}
.data-point {
    background-color: white;
    border: 2px solid lightblue;
    border-radius: 50%;
    height: 12px;
    position: absolute;
    width: 12px;
}
</style>
