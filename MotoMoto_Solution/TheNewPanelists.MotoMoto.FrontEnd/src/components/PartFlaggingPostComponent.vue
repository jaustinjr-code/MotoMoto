<template>
<div id='part-flagging__post'>
    <h1>Build Post</h1>

    <div id='build-car-section' class='post__build-section'>
        <h2>Car Details</h2>
        <div class='car-single-selection'>
            <label>Car Make:</label><span id='car-make' data-value='honda'>Honda</span>
        </div>
        <div class='car-single-selection'>
            <label>Car Model:</label><span id='car-model' data-value='civic'>Civic</span>
        </div>
        <div class='car-single-selection'>
            <label>Car Year:</label><span id='car-year' data-value='2022'>2022</span>
        </div>
    </div>

    <div id='build-parts-section' class='post__build-section'>
        <h2>Parts List</h2>
        <div class='parts-single-selection'>
            <ul class='post-list' id='parts-list'>
                <li value='0'>Compatible Part</li>
                <li value='1'>Incompatible Part</li>
            </ul>
        </div>

    </div>

    <div id='post__flag-display' class='post__build-section'>
            <h2>Compatibility</h2>
            <p v-if='incompatibleParts.length == 0'>All parts are labeled 'Compatible' With the selected car based on user flags :)</p>
            <p v-else>It appears the following parts are labeled 'Non-Recommended' with the selected car based on user flags: </p>
            <button class='part-flag-buttons' v-for="(partName) in incompatibleParts" :key='partName'>
                {{ partName }}
            </button>
    </div>
    <div id='rate-flag'>
        <b-modal></b-modal>
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
        incompatibleParts: []
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

                let params = {partNumber: partNum, carMake: carMake, carModel: carModel, carYear: carYear};
                await instance.get('PartFlagging/IsPossibleIncompatibility', {params}).then((res) => {
                    let isIncompatible = res.data.isPossibleIncompatiblility;
                    if (isIncompatible)
                    {
                        newIncompatibleParts.push(partName);
                    }
                })
            }
            this.incompatibleParts = newIncompatibleParts
        },

        flagPart: async function(index) {
            let carMakeElement = document.getElementById('car-make-select')
            let carMake = carMakeElement.options[carMakeElement.selectedIndex].value

            let carModelElement = document.getElementById('car-model-select')
            let carModel = carModelElement.options[carModelElement.selectedIndex].value

            let carYearElement = document.getElementById('car-year-select')
            let carYear = carYearElement.options[carYearElement.selectedIndex].value

            let partSelections = document.getElementsByClassName('builder__single-part-selection-selector')
            let partNum = partSelections[index].options[partSelections[index].selectedIndex].value

            
            await instance.post('PartFlagging/CreateFlag', null, {
                params: {
                    partNumber: partNum, carMake: carMake, carModel: carModel, carYear: carYear
                    }
                }).then((res) => {
                console.log(res.data)
            })
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
label {
    margin-right: 5px;
}
</style>