<template>
<!-- Container for CarBuilder Component -->
<div id='car_builder'>
    <h1>Car Builder</h1>

    <!-- Section for selecting which car is the base of the build -->
    <div id='car_type' class='choose-car-type'>
        <label>Choose Car Make</label>
        <select id='car-make-select' @change='handleCarMake($event)'>
            <option value="">--Please choose an option--</option>
            <option v-bind:value='make' v-for='make in carTypeList' >
                {{ make }}
            </option>
        </select>
    </div>
    <div>
        <label>Choose Car Model</label>
        <select id='car-model-select' @change='handleCarModel($event)'>
            <option value="">--Please choose an option--</option>
            <option value='civic'>Civic</option>
            <option value='toyota'>Camry</option>
        </select>
    </div>
    <div class='builder-single-selection'>
        <label>Choose Car Year</label>
        <option value="">--Please choose an option--</option>
        <select id='car-year-select' @change='handleCarYear($event)'>
        </select>
    </div>

    <button @click='createCarBuild'> Submit </button> 


    <!-- Section for selecting parts to put in vehicle build 
    <div id='car_part' class='modify-car'>
        <label>Modify Car</label>
        <select id='car-part-select' @change='modifyCarBuild()'>
            <option value='honda'>OEM</option>
            <option value='toyota'>Aftermarket</option>
        </select>
    </div>
    -->

</div>
</template>
<script>
import { nextTick } from "vue";

import axios from 'axios';
import {instance} from '../router/CarBuilderConnection'

export default {
    data()
    {
        return{
            carTypeList: ["Honda", "Toyota"],
            make: "",
            model: "",
            year: "",
            partNumber: "",
            type: ""
        }
    },
    methods: {
        GetCarTypes(){
            let car_type = {make: "Honda", model: "Accord", year: "2015"};
            instance.get("CarBuilder/GetCarTypes", car_type).then((res) => 
            {console.log(res);
            this.carTypeList = res.data;
            console.log(this.carTypeList);
            }).catch((e) => {
                console.log(e);
            });
        }
        // GetCarParts(){
        // let car_part = {partNumber: "", type: ""};
        // instance.get("CarBuilder/GetCarTypes", car_type).then((res) => 
        // {console.log(res);
        // this.carTypeList = res.data;
        // console.log(this.carTypeList);
        // }).catch((e) => {
        //     console.log(e);
        // });
        // }
        // createCarBuild(){
            
        // },
        // handleCarMake(a){
        //     debugger
        // },
        // handleCarModel(){},
        // handleCarYear(){}
    },

    //Initializes year selector for car selection
    mounted() {

        this.GetCarTypes();


        let yearSelector = document.getElementById('car-year-select')
        for (let yearIterator = 2022; yearIterator >= 1950; --yearIterator)
        {
            let option = document.createElement('option')
            option.value = yearIterator.toString()
            option.innerHTML = yearIterator
            yearSelector.append(option)
        }
    }
}

</script>