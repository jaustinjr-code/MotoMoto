<template>
    <!-- Container for part flagging builder component -->
    <div id='part-flagging__builder'>
        <h1>Car Builder</h1>
        <div id='builder__car-section' class='builder-section'>
            <h2>Car Selection</h2>
            <div class='builder-single-selection'>
                <label>Select Base Vehicle Make</label>
                <select id='car-make-select' @change='checkCompatibility()'>
                    <option value='honda'>Honda</option>
                    <option value='toyota'>Toyota</option>
                </select>
            </div>

            <!-- Section for selecting which car is the base of the build -->
            <div>
                <label>Select Base Vehicle Model</label>
                <select id='car-model-select' @change='checkCompatibility()'> 
                    <option value='civic'>Civic</option>
                    <option value='toyota'>Camry</option>
                </select>
            </div>

            <div class='builder-single-selection'>
            <label>Select Base Vehicle Year</label>
                <select id='car-year-select' @change='checkCompatibility()'>
                </select>
            </div>
        </div>

        <!-- Section for selecting parts to put in vehicle build -->
        <div id='builder__part-section' class='builder-section'>
            <h2>Parts Selection</h2>
            <button class='flagging-builder-button' v-on:click="addPart()">Add A Part</button>
            <div class='builder__single-part-selection' v-for="(inputId, index) in partInputs" :key='inputId'>
                <select class='builder__single-part-selection-selector' @change='checkCompatibility()'>
                    <option value='0'>Compatible Part</option>
                    <option value='1'>Incompatible Part</option>
                </select>
                <button class='flagging-builder-button' v-on:click='removePart(index)'>Remove Part</button>
                <button class='flagging-builder-button' v-on:click='flagPart(index)'>Flag Part</button>
            </div>
            
        </div>

        <!-- Section for displaying part compatible and non-recommended flags. -->
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
import { nextTick } from "vue";

import axios from 'axios';
import {instance} from '../router/PartFlaggingConnection'

export default {
    data()
    {
        return {
        // The number of parts that have been added to the build, serves as unique key to idenitfy part
        currentPartCount: 1,

        // List of part keys, used for generating part elements in DOM
        partInputs: [1],

        //List of parts that are flagged as non-recommended
        incompatibleParts: []
        }
    },
    methods: {
        // Adds a part to the build
        addPart: function(param) {
            this.currentPartCount += 1
            this.partInputs.push(this.currentPartCount)
            this.checkCompatibility()
        },

        // Removes a part from the build
        removePart: async function(index) {
            this.partInputs.splice(index,1)
            await nextTick();
            this.checkCompatibility()
        },

        //Checks for incompatible parts in the build
        checkCompatibility: async function() {
            let newIncompatibleParts = []
            let carMakeElement = document.getElementById('car-make-select')
            let carMake = carMakeElement.options[carMakeElement.selectedIndex].value

            let carModelElement = document.getElementById('car-model-select')
            let carModel = carModelElement.options[carModelElement.selectedIndex].value

            let carYearElement = document.getElementById('car-year-select')
            let carYear = carYearElement.options[carYearElement.selectedIndex].value

            let partSelections = document.getElementsByClassName('builder__single-part-selection-selector')
            
            //Loop through parts, retrieve compatibility from WebAPI, and update incompatible parts list
            for (var partSelectionsIt = 0; partSelectionsIt < partSelections.length; partSelectionsIt++) {
                let selectedElement = partSelections[partSelectionsIt].options[partSelections[partSelectionsIt].selectedIndex]
                let partNum = selectedElement.value
                let partName = selectedElement.innerHTML;

                let params = {partNum: partNum, carMake: carMake, carModel: carModel, carYear: carYear};
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

        //Increments the count for selected flag in the flagging database
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
                    partNum: partNum, carMake: carMake, carModel: carModel, carYear: carYear
                    }
                }).then((res) => {
                console.log(res.data)
            })
        }
    },
    //Initializes year selector for car selection
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
.flagging-builder-button{
    align-content: right;
}
</style>