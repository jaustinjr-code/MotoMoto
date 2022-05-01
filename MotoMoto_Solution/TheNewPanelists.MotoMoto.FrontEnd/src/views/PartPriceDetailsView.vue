<template>
<div>
    <router-link to="/"><h1 class="title" v-on:onclick="home">MotoMoto</h1></router-link>
    <TabBarComponent/>
    <h5 class="partTitle">Part Details</h5>
    <ul>
        <li class="partName">{{part.partName}}</li>
        <li class="partPrice">{{part.currentPrice}}</li>
    </ul>
    <div>
        <canvas class="testCanvas" id="partTrendGraph" width="585" height="450"></canvas>
    </div>
</div>
</template>

<script>
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
        TabBarComponent
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
                this.partDates.push(this.partHistory[i].dateTime)
                this.partPricesOverTime.push(this.partHistory[i].productPrice)
            }
            const d = new Date();
            let text = d.toDateString();
            this.partDates.push(text);
            this.partPricesOverTime.push(this.part.currentPrice);
            this.drawGraph()
        },
        //tabular function << not sure if we are able to use the js library for graphs >>
        drawGraph: function() {
            var dataArr = this.partPricesOverTime
            var canvas = document.getElementById( "partTrendGraph" );  
            var context = canvas.getContext( "2d" );  
        
            var GRAPH_TOP = 25;  
            var GRAPH_BOTTOM = 375;  
            var GRAPH_LEFT = 25;  
            var graphRight = 475;  
        
            var graphHei = 350;  
            var GRAPH_WIDTH = 450;  
        
            var arrayLen = dataArr.length;  
        
            var largest = 0;  
            for( var i = 0; i < arrayLen; i++ ){  
                if( dataArr[i] > largest ){  
                    largest = dataArr[i];  
                }  
            }  
        
            context.clearRect( 0, 0, 500, 500 );  
            context.font = "16px Arial";  
        
            context.beginPath();  
            context.moveTo( GRAPH_LEFT, GRAPH_BOTTOM );  
            context.lineTo( graphRight, GRAPH_BOTTOM );  
            context.lineTo( graphRight, GRAPH_TOP );  
            context.stroke();  
            
            context.beginPath();  
            context.strokeStyle = "#BBB";  
            context.moveTo( GRAPH_LEFT, GRAPH_TOP );  
            context.lineTo( graphRight, GRAPH_TOP );  

            context.fillText( largest, graphRight + 15, GRAPH_TOP);  
            context.stroke();  
      
            context.beginPath();  
            context.moveTo( GRAPH_LEFT, (graphHei) / 4 * 3 + GRAPH_TOP );  
            context.lineTo( graphRight, (graphHei) / 4 * 3 + GRAPH_TOP );  

            context.fillText( largest / 4, graphRight + 15, (graphHei) / 4 * 3 + GRAPH_TOP);  
            context.stroke();    
            context.beginPath();  
            
            context.moveTo( GRAPH_LEFT, (graphHei) / 2 + GRAPH_TOP );  
            context.lineTo( graphRight, (graphHei) / 2 + GRAPH_TOP );  

            context.fillText( largest / 2, graphRight + 15, (graphHei) / 2 + GRAPH_TOP);  
            context.stroke();  
        
            context.beginPath();  
            context.moveTo( GRAPH_LEFT, (graphHei) / 4 + GRAPH_TOP );  
            context.lineTo( graphRight, (graphHei) / 4 + GRAPH_TOP );   
            context.fillText( largest / 4 * 3, graphRight + 15, (graphHei) / 4 + GRAPH_TOP);  
            context.stroke();  
        
            context.fillText( "Months", graphRight / 2.2, GRAPH_BOTTOM + 50);  
            context.fillText( "Cost", graphRight + 70, graphHei / 1.8);  
        
            context.beginPath();  
            context.lineJoin = "round";  
            context.strokeStyle = "black";  
        
            context.moveTo( GRAPH_LEFT, (graphHei - dataArr[0] / largest * graphHei) + GRAPH_TOP );  
            context.fillText( "1", 15, GRAPH_BOTTOM + 25);  
            for( var i = 1; i < arrayLen; i++ ){  
                context.lineTo( graphRight / arrayLen * i + GRAPH_LEFT, (graphHei - dataArr[i] / largest * graphHei) + GRAPH_TOP );  
                context.fillText( ( i + 1 ), graphRight / arrayLen * i, GRAPH_BOTTOM + 25);  
            }  
            context.stroke();  
        }   
    },
}
</script>

<style>
.partTitle {
    padding-top: 10px;
    padding-bottom: 10px;
    font: 15px;
    color: black;
}
li {
    align-items: inherit;
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

.line-chart {
    list-style: none;
    margin: 0;
    padding: 0;
}

.data-point {
    background-color: white;
    border: 2px solid lightblue;
    border-radius: 50%;
    height: 12px;
    position: absolute;
    width: 12px;
}
.testCanvas {
    border: 1px solid #000000;
}
</style>

