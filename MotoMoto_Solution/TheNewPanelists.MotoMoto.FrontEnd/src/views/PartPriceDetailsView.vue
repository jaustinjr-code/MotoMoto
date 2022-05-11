<template>
<div>
    <TabBarComponent/>
    <kpi-submission-component viewTitle="Part Price Analysis"></kpi-submission-component>
    <h5 class="partTitle">Part Details</h5>
    <table class="partDetails" align="center">
        <thead>
            <tr class="partTitle">
                <div class="nameNprice">
                    <td>{{part.partName}}</td>
                    <td>Current Price Point: ${{part.currentPrice}}.00</td>
                </div>
            </tr>
        </thead>
    </table>
    <a class="proURL" :href="part.productURL">Link to Product</a>
    <div class="priceTrendGraph">
        <h5 class="partHistoryTitle">Part History Past 6 Months</h5>
        <div class="priceTrendGraph">
            <canvas id="partTrendGraph" width="585" height="450"></canvas>
        </div>
        <div class="partPriceHistory">
            <ul>
                <table>
                    <thead>
                        <tr class="titles">
                            <td class="titleString">Index</td>
                            <td class="titleString">Date</td>
                            <td class="titleString">Price Point</td>
                        </tr>
                    </thead>
                    <thead>
                        <tr class="values" v-for="(partHist, Index) in partHistory" :key=partHist>
                            <td class="titleString">{{Index}}</td>
                            <td class="titleString">{{partHist.dateTime.slice(0,10)}}</td>
                            <td class="titleString">${{partHist.productPrice}}.00</td>
                        </tr>
                    </thead>
                </table>
            </ul>
        </div>
        <table class="ratingDetails">
            <thead>
                <tr class="ratingTitle">
                    <div class="rating">
                        <td class="titleString">Rating: {{part.rating}}</td>
                        <td class="titleString">Number of Reviews: {{part.ratingCount}}</td>
                    </div>
                </tr>
            </thead>
        </table>
    </div>
    <div class="partPriceHistory">
        <ul>
            <table>
                <thead>
                    <tr class="titles">
                        <td>Index</td>
                        <td>Date</td>
                        <td>Price Point</td>
                    </tr>
                </thead>
                <thead>
                    <tr class="values" v-for="(partHist, Index) in partHistory" :key=partHist>
                        <td>{{Index}}</td>
                        <td>{{partHist.dateTime.slice(0,10)}}</td>
                        <td>${{partHist.productPrice}}.00</td>
                    </tr>
                </thead>
            </table>
        </ul>
    </div>
    <table class="ratingDetails">
        <thead>
            <tr class="ratingTitle">
                <div class="rating">
                    <td class="partRating">Rating: {{part.rating}}</td>
                    <td class="partratingCount">Number of Reviews: {{part.ratingCount}}</td>
                </div>
            </tr>
        </thead>
    </table>
</div>
</template>

<script>
import KpiSubmissionComponent from '@/components/KpiSubmissionComponent.vue'
import TabBarComponent from '../components/TabBarComponent.vue'
import {instance} from '../router/PartPriceAnalysisConnection'
export default {
    data() {
        return {
            part: {},
            partID: 0,
            partHistory: [],
            partPricesOverTime: [],
            partDates: [],
        }
    },
    components: {
        TabBarComponent,
        KpiSubmissionComponent
    },
    props: ["baseURL", "parts"],
    beforeMount() {
        this.partPricesOverTime = [];
        this.partDates = []
    },
    mounted() {
        this.partID = this.$route.params.id;
        this.retrievePartInformation();
    },
    methods: {
        retrievePartInformation: async function()
        {
            console.log(this.partID)
            let params = {_partID: this.partID}
            await instance.get('PartPriceAnalysisEvaluation/Evaluate', {params}).then((res) =>{
                this.part = res.data;
                this.partHistory = res.data.historicalPrices;
                this.setPartPricesOverTime()
            })
        },
        setPartPricesOverTime: function() {
            for (let i = 0; i < this.partHistory.length; i++) {
                this.partDates.push(this.partHistory[i].dateTime.toDateString)
                this.partPricesOverTime.push(this.partHistory[i].productPrice)
            }
            const d = new Date();
            let text = d.toDateString();
            this.partDates.push(text);
            this.partPricesOverTime.push(this.part.currentPrice);
            this.drawGraph()
        },
        drawGraph: function() {
            var dataArr = this.partPricesOverTime;
            var arrayLen = dataArr.length;
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
            for(var i = 1; i < arrayLen; i++) {  
                context.lineTo( graphRight / arrayLen * i + graphLef, (graphHei - dataArr[i] / largest * graphHei) + graphTop);  
                context.fillText((i + 1), graphRight / arrayLen * i, graphBot + 25);  
            }  
            context.stroke();  
        },
    },
}
</script>

<style>
a.proURL {
  background-color: #000000;
  color: white;
  padding: 12px 20px;
  text-align: center;
  text-decoration: none;
  display: inline-block;
}

h2.partHistoryTitle {
    font: 20px;
}
.partTitle {
    padding-top: 10px;
    padding-bottom: 10px;
    font: 15px;
    color: black;
}
.css-chart {
    border-bottom: 1px solid;
    border-left: 1px solid;
    height: var(--widget-size);
    margin: 1em;
    padding: 0;
    position: relative;
    width: var(--widget-size);
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
.partTrendGraph {
    border: 1px solid #000000;
}
.partDetails {
    margin-left: auto;
    margin-right: auto;   
}
</style>