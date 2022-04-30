<template>
<div>
    <router-link to="/"><h1 class="title" v-on:onclick="home">MotoMoto</h1></router-link>
    <TabBarComponent/>
    <h5 class="partTitle">Part Details</h5>
    <ul>
        <li class="partName">{{part.partName}}</li>
    </ul>
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
        }
    },
    components: {
        TabBarComponent
    },
    props: ["baseURL", "parts"],
    mounted() {
        this.partID = this.$route.params.id;
        this.retrievePartInformation()
    },
    methods: {
        retrievePartInformation: async function()
        {
            console.log(this.partID)
            let params = {_partID: this.partID}
            await instance.get('PartPriceAnalysisEvaluation/Evaluate', {params}).then((res) =>{
                this.part = res.data;
            })
        },
        backToPartsList: function()
        {
            
        },
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
</style>

