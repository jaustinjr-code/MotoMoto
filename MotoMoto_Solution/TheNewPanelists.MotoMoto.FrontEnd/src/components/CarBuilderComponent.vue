<template>
<!-- Container for CarBuilder Component -->
<div id='car_builder' class="car-builder-page">
    <h1>Car Builder</h1>

    <!-- Section for selecting which car is the base of the build -->
    <div id='car_type' class='choose-car-type'>
        <label>Choose Car Make</label>
        <select id='car-make-select' v-model="carID" @change='GetMake($event)'>
            <option v-bind:value='carType.carID' v-for="(carType, index) in carTypeList" :key='index'>
                {{ carType.make }}
            </option>
        </select>
    </div>
    
    <div>
        <label>Choose Car Model</label>
        <select id='car-model-select' v-model="model" @change='GetModel($event)'>
            <!-- <option value="">--Please choose an option--</option>
            <option value="F150">F150</option>
            <option value="M5">M5</option>
            <option value="Accord">Accord</option>
            <option value="Corolla">Corolla</option>
            <option value="CLA250">CLA250</option>
            <option value="Q5">Q5</option>
            <option value="CTS">CTS</option>
            <option value="Silverado">Silverado</option>
            <option value="Rogue">Rogue</option>
            <option value="Wrangler">Wrangler</option>
            <option value="Model Y">Model Y</option>
            <option value="911">911</option>
            <option value="Golf">Golf</option> -->
            <option v-bind:value='carType.model' v-for="(carType, index) in carTypeList" :key='index'>
                {{ carType.model }}
            </option>
        </select>
    </div>

    <div class='builder-single-selection' @change='GetYear($event)'>
        <label>Choose Car Year</label>
        <select id='car-year-select' >
        </select>
    </div>

    <div id="car-modify-part" class="choose-car-part">
        <!-- <label>Modify Car with These Parts</label> -->
        <!-- <button v-on:click="isHidden = true">Submit ></button> -->
        <button @click='CreateCarType()'> Submit ></button> 
        <h1 v-if="isHidden">Modify Your Car!</h1>
    </div>

    <div id='car_part' class='modify-car' v-if="modifyCar">
        <label>Modify Car</label>
        <select id='car-part-select' v-model='partID' @change='GetPart($event)'>
            <!-- <option value="">--Please choose an option--</option> -->
            <option v-bind:value='carModify.partID' v-for="(carModify, index) in carPartList" :key='index'>
                {{ carModify.partNumber }} 
                <!-- {{ carModify.type }} -->
            </option>
        </select>
        <img src="../assets/carBuilderCar.jpg" alt="Car Camaro" width="1200" height="1000">
        <button @click="UpdateCar()"> Save Car </button>
        <h1>{{saveCar}}}</h1>
    </div>



</div>
</template>
<script>
import { nextTick } from "vue";

import axios from 'axios';
import {instance} from '../router/CarBuilderConnection'
import { instanceSubmit } from "../router/CarBuilderConnection";

export default {
    data()
    {
        return{
            partName: "",
            carModel: "",
            carYear: "",
            isHidden: false,
            carTypeList: [],
            carPartList: [],
            userSavedCarsList: [],
            make: "",
            model: "",
            year: "",
            partNumber: "",
            type: "",
            modifyCar: false,
            username: "ran",
            //username: this.$cookies.get("username"),
            carID: "",
            partID: "",
            saveCar: ""
        }
    },
    methods: {
        async GetCarTypes(){
            //debugger;
            //let car_type = {make: "Honda", model: "Accord", year: "2015"};
            //Instance is not descriptive enough: ajax client would be better
            await instance.get("CarBuilder/GetCarTypes").then((res) => 
            {console.log(res);
            this.carTypeList = res.data;
            console.log(this.carTypeList); //better to create your own LOG SERVICE
            }).catch((e) => {
                console.log(e);
            });
        },
        async GetCarPart(){
            await instance.get("CarBuilder/GetCarPart").then((res) =>
            {console.log(res);
            this.carPartList = res.data;
            console.log(this.carPartList);
            }).catch((e) => {
                console.log(e);
            });
        },
        CreateCarType(make, model, year){
            this.isHidden = true;
            this.modifyCar = true;
            console.log(this.carID);
            // let carTypeModel = JSON.stringify({ "make": make, "model": model, "year": year });
            // instanceSubmit.post("CarBuilder/CreateCarType", carTypeModel, {
            //     headers: {
            //         'Content-Type': 'application/json'
            //     }
            // }).then((res) => {
            //     console.log("Car succesfully created!");
            //     this.make = make;
            //     this.model = model;
            //     this.year = year;
            // });
        },
        UpdateCar(){
            let updateCarModel = JSON.stringify({ "carID": this.carID, "partID": this.partID, "username": this.username });
            instanceSubmit.post("CarBuilder/UpdateCar", updateCarModel, {
                headers: {
                    'Content-Type': 'application/json'
                }
            }).then((res) => {
                console.log("Car succesfully saved!");
                this.saveCar = "Car Has Successfully Saved <3";
                // this.make = make;
                // this.model = model;
                // this.year = year;
            });
        },
        GetModifiedCarBuilds(){
            instance.get("CarBuilder/CarBuild", this.username).then((res) =>
            {console.log(res);
            this.userSavedCarsList = res.data;
            console.log(this.userSavedCarsList);
            }).catch((e) => {
                console.log(e);
            });
        },
        GetMake(e){
            this.carID = e.target.value;
            //var name = e.target.options[e.target.options.selectedIndex].text;
        },
        GetModel(e){
            this.carModel= e.target.value;
        },
        GetYear(e){
            this.carYear = e.target.value;
        },
        GetPart(e){
            this.partID = e.target.value;
        }
    },
    mounted() {

        this.GetCarTypes();
        this.GetCarPart();
        //this.CreateCarType();
        //this.GetModifiedCarBuilds();

        //Initializes year selector for car selection
        let yearSelector = document.getElementById('car-year-select')
        for (let yearIterator = 2022; yearIterator >= 1950; --yearIterator) //get current year to update year
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
.car-builder-page {
    margin-left: 20px;
    margin-bottom: 20px;
    text-align: left;
}
</style>