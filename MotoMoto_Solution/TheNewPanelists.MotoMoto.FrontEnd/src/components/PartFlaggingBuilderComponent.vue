<template>
    <div id='part-flagging__builder'>
        <h1>Car Builder</h1>
        <div id='builder__car-section' class='builder-section'>
            <h2>Car Selection</h2>
            <div class='builder-single-selection'>
                <label>Select Base Vehicle Make</label>
                <select id='car-make-select' @change='checkCompatibility()'>
                    <option value='honda'>Honda</option>
                </select>
            </div>

            <div>
                <label>Select Base Vehicle Model</label>
                <select id='car-model-select' @change='checkCompatibility()'> 
                    <option value='civic'>Civic</option>
                </select>
            </div>

            <div class='builder-single-selection'>
            <label>Select Base Vehicle Year</label>
                <select id='car-year-select' @change='checkCompatibility()'>
                </select>
            </div>
        </div>

        <div id='builder__part-section' class='builder-section'>
            <h2>Parts Selection</h2>
            <button v-on:click="addPart()">Add A Part</button>
            <div class='builder__single-part-selection' v-for="(inputId, index) in partInputs" :key='inputId'>
                <select class='builder__single-part-selection-selector' @change='checkCompatibility()'>
                    <option value='0'>Compatible Part</option>
                    <option value='1'>Incompatible Part</option>
                </select>
                <button v-on:click='removePart(index)'>Remove Part</button>
                <button v-on:click='flagPart(index)'>Flag Part</button>
            </div>
            
        </div>

        <div id='builder__flag-display' class='builder-section'>
            <h2>Compatibility</h2>
            <p v-if='incompatibleParts.length == 0'>All parts are labeled 'Compatible' With the selected car based on user flags :)</p>
            <p v-else>It appears the following parts are labeled 'Non-Recommended' with the selected car based on user flags: </p>
            <ul>
                <li v-for="(partName) in incompatibleParts" :key='partName'>
                    {{ partName }}
                </li>
            </ul>
        </div>

    </div>
</template>
<script>
import { nextTick, createElementBlock, defineComponent } from "vue";

import axios from 'axios';
import {instance} from '../router/PartFlaggingConnection'

export default {
    data()
    {
        return {
        currentPartCount: 1,
        partInputs: [1],
        incompatibleParts: []
        }
    },
    methods: {
        addPart: function(param) {
            this.currentPartCount += 1
            this.partInputs.push(this.currentPartCount)
            this.checkCompatibility()
        },

        removePart: async function(index) {
            this.partInputs.splice(index,1)
            await nextTick();
            this.checkCompatibility()
        },

        checkCompatibility: async function() {
            let newIncompatibleParts = []
            let carMakeElement = document.getElementById('car-make-select')
            let carMake = carMakeElement.options[carMakeElement.selectedIndex].value

            let carModelElement = document.getElementById('car-model-select')
            let carModel = carModelElement.options[carModelElement.selectedIndex].value

            let carYearElement = document.getElementById('car-year-select')
            let carYear = carYearElement.options[carYearElement.selectedIndex].value

            let partSelections = document.getElementsByClassName('builder__single-part-selection-selector')
            console.log(partSelections)
            for (var partSelectionsIt = 0; partSelectionsIt < partSelections.length; partSelectionsIt++) {
                let selectedElement = partSelections[partSelectionsIt].options[partSelections[partSelectionsIt].selectedIndex]
                let partNum = selectedElement.value
                let partName = selectedElement.innerHTML;

                let params = {partNumber: partNum, carMake: carMake, carModel: carModel, carYear: carYear};
                await instance.get('PartFlagging/IsPossibleIncompatibility', {params}).then((res) => {
                    let isIncompatible = res.data.isPossibleIncompatiblility;
                    if (isIncompatible)
                    {
                        newIncompatibleParts.push(partName);
                        console.log('push')
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
        let yearSelector = document.getElementById('car-year-select')
        for (let yearIterator = 2025; yearIterator >= 1800; --yearIterator)
        {
            let option = document.createElement('option')
            option.value = yearIterator.toString()
            option.innerHTML = yearIterator
            yearSelector.append(option)
        }
        
        
    }
}
</script>

<style>
.builder-section {
    margin-left: 20px;
    margin-bottom: 20px;
    text-align: left;
}
.builder__single-part-selection {
    margin-bottom: 10px;
}
button{
    align-content: right;
}
</style>