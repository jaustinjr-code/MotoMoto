<template>
<div id='part-flagging__post'>
    <h1>Build Post</h1>

    <div id='build-car-section' class='post__build-section'>
        <h2>Car Details</h2>
        <div class='car-single-selection'>
            <label class='flagging-post-label'>Car Make:</label><span id='car-make' data-value='honda'>Honda</span>
        </div>
        <div class='car-single-selection'>
            <label class='flagging-post-label'>Car Model:</label><span id='car-model' data-value='civic'>Civic</span>
        </div>
        <div class='car-single-selection'>
            <label class='flagging-post-label'>Car Year:</label><span id='car-year' data-value='2022'>2022</span>
        </div>
    </div>

    <div id='build-parts-section' class='post__build-section'>
        <h2>Parts List</h2>
        <div class='parts-single-selection'>
            <ul class='post-list' id='parts-list'>
                <div class='parts-list-single-entry'>
                    <li class='parts-list-item' value='0'>Compatible Part</li><button v-on:click='flagNewPart(0)'>Flag Part</button>
                </div>
                <div class='parts-list-single-entry'>
                    <li class='parts-list-item' value='1'>Incompatible Part</li><button v-on:click='flagNewPart(1)'>Flag Part</button>
                </div>
            </ul>
        </div>

    </div>

    <div id='post__flag-display' class='post__build-section'>
            <h2>Compatibility</h2>
            <p v-if='incompatibleParts.length == 0'>All parts are labeled 'Compatible' With the selected car based on user flags :)</p>
            <p v-else>It appears the following parts are labeled 'Non-Recommended' with the selected car based on user flags: </p>
            <div  v-for="(part, index) in incompatibleParts" :key='part'>
                <button v-on:click="SelectFlag(index)" class='part-flag-buttons'>
                    {{ part['partName'] }}
                </button>
                <div v-if='openPart == index'>
                    <h3> Review Flag </h3>
                    <button v-on:click="Upvote(index)">
                        Upvote
                    </button>
                    <button v-on:click="Downvote(index)" v-for="(partName, index) in incompatibleParts" :key='partName'>
                        Downvote
                    </button>
                </div>
            </div>
    </div>
</div>
</template>
<script>
import { createElementBlock, defineComponent } from "vue";
import axios from 'axios';
import {instance} from '../router/PartFlaggingConnection'

export default {
    data()
    {
        return {
        incompatibleParts: [],
        openPart: null
        }
    },
    methods: {
        checkCompatibility: async function() {
            let newIncompatibleParts = []
            let carMakeElement = document.getElementById('car-make')
            let carMake = carMakeElement.dataset.value

            let carModelElement = document.getElementById('car-model')
            let carModel = carModelElement.dataset.value

            let carYearElement = document.getElementById('car-year')
            let carYear = carYearElement.dataset.value

            let partList = document.getElementById('parts-list').getElementsByTagName('li')
            for (var partListIt = 0; partListIt < partList.length; partListIt++) {
                let currentPart = partList[partListIt]
                let partNum = currentPart.value
                let partName = currentPart.innerHTML

                let params = {partNum: partNum, carMake: carMake, carModel: carModel, carYear: carYear};
                await instance.get('PartFlagging/IsPossibleIncompatibility', {params}).then((res) => {
                    let isIncompatible = res.data.isPossibleIncompatiblility;
                    if (isIncompatible)
                    {
                        newIncompatibleParts.push({'partName': partName, 'partNum': partNum});
                    }
                })
            }
            this.incompatibleParts = newIncompatibleParts
        },

        flagNewPart: async function(partNum) {
            let carMakeElement = document.getElementById('car-make')
            let carMake = carMakeElement.dataset.value

            let carModelElement = document.getElementById('car-model')
            let carModel = carModelElement.dataset.value

            let carYearElement = document.getElementById('car-year')
            let carYear = carYearElement.dataset.value

            await instance.post('PartFlagging/CreateFlag', null, {
                params: {
                    partNum: partNum, carMake: carMake, carModel: carModel, carYear: carYear
                    }
                }).then((res) => {
                console.log(res.data)
            })
        },

        flagPart: async function(index) {
            let carMakeElement = document.getElementById('car-make')
            let carMake = carMakeElement.dataset.value

            let carModelElement = document.getElementById('car-model')
            let carModel = carModelElement.dataset.value

            let carYearElement = document.getElementById('car-year')
            let carYear = carYearElement.dataset.value

            let partNum = this.incompatibleParts[index]['partNum']

            await instance.post('PartFlagging/CreateFlag', null, {
                params: {
                    partNum: partNum, carMake: carMake, carModel: carModel, carYear: carYear
                    }
                }).then((res) => {
                console.log(res.data)
            })
        },

        decrementFlagCount: async function(index) {
            let carMakeElement = document.getElementById('car-make')
            let carMake = carMakeElement.dataset.value

            let carModelElement = document.getElementById('car-model')
            let carModel = carModelElement.dataset.value

            let carYearElement = document.getElementById('car-year')
            let carYear = carYearElement.dataset.value

            let partNum = this.incompatibleParts[index]['partNum']

            await instance.post('PartFlagging/DecrementFlagCount', null, {
                params: {
                    partNum: partNum, carMake: carMake, carModel: carModel, carYear: carYear
                    }
                }).then((res) => {
                console.log(res.data)
            })
        },

        SelectFlag: function(index) {
            this.openPart = index
        },

        UnSelectFlag: function() {
            this.openPart = null
        },

        Upvote: async function(index){
            this.flagPart(index)
            this.UnSelectFlag()
        },

        Downvote: async function(index) {
            this.decrementFlagCount(index)
            this.UnSelectFlag()
        }
    },
    mounted() {   
        this.checkCompatibility()
    }
}
</script>

<style>
.post__build-section
{
    text-align: left;
    margin-left: 20px;
    margin-bottom: 20px;
}
.post-list  { 
    list-style-type: none;
    padding: 0;
}

.part-flag-buttons {
    display: block;
    margin-bottom: 10px;
    background-color: transparent;
    border: 1px solid black;
}
.flagging-post-label {
    margin-right: 5px;
}


.parts-list-item {
    display: inline-block
}

</style>