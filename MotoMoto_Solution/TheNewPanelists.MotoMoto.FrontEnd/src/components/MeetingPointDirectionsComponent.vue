<template>
    <div class="form-row justify-content-center align-items-center">
        <div class="alert alert-danger" v-show="error">{{ error }}</div>
        <input type="text" class="form-control mb-2" v-model="origin" id="autocomplete" placeholder="Enter Origin Location">
        <button type="button" class="btn btn-primary mb-2" @click="findOriginLocation">Search</button>
    </div>
    <div class="col-md-6"><h3> Latitude : {{ latitude }}</h3></div>
    <div class="col-md-6"><h3> Longitude : {{ longitude }}</h3></div>

    <div class="form-row justify-content-center align-items-center">
        <div class="col-md-6"><h3> Location : {{ this.location }}</h3></div>
    </div>

    
    <!-- <ul>
        <li v-for="(value, index) in id" 
        :key="index" >
        {{value}} 
        </li> 
    </ul> -->
    <div id="map" ref="map"></div>
</template>

<script>
import TabBarComponent from './TabBarComponent.vue'
import EventListDashboardComponent from './EventListDashboardComponent.vue'
import {instance} from '../router/MeetingPointDirectionsConnection'
window.axios = require('axios')

export default {
    name: 'Map',
    props: ['id'],
    data () {
        return {
            map: null,
            origin: '',
            latitude: '',
            longitude: '',
            error: '',
            location: '',
            eventID: 0,
            eventStreetAddress: '',
            eventCity: '',
            eventState: '',
            eventCountry: '',
            eventZipCode: '',
        }
    },
    methods: {
        findOriginLocation() {
            // Determine if browser is supported by geolocation api
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(
                    position => {
                        this.latitude = position.coords.latitude;
                        this.longitude = position.coords.longitude;
                        this.getAddressFromCoords(this.latitude, this.longitude);
                        this.showOriginLocationOnMap(this.latitude, this.longitude);
                        // console.log(position.coords.latitude);
                        // console.log(position.coords.longitude);
                    },
                    error => {
                        this.error = error.message + " PLEASE TYPE YOUR ADDRESS MANUALLY...";
                        // console.log(error.message);
                    }
                );
            } else {
                this.error = "BROWSER DOES NOT SUPPORT GEOLOCATION API..."
                // console.log("Your browser does not support geolocatoin API");
            }
        },
        getAddressFromCoords(latitude, longitude) {
            var geocoder = new window.google.maps.Geocoder();

            // const input = document.getElementById("latlng").value;
            // const latlngStr = input.split(",", 2);
            // const latlng = {
            //     lat: parseFloat(latlngStr[0]),
            //     long: parseFloat(latlngStr[1]),
            // };

            geocoder.geocode({ location: new google.maps.LatLng(latitude, longitude) })
            .then((response) => {
                if(response.results[0]) {
                    this.origin = response.results[0].formatted_address;
                    // console.log(response.results[0].formatted_address);
                }
                else {
                    this.error = response.error_message;
                    // console.log(response.error_message);
                }
            })
            .catch(error => {
                this.error = error;
                // console.log(error.message);
            })
        },
        //     axios.get("https://maps.googleapis.com/maps/api/geocode/json?latlng="
        //     + lat + "," + long + "&key=AIzaSyDWiig_4EKjtfZjDf49AEbYReRb3EwLBRs")
        //     .then(response => {
        //         if(response.data.error.message) {
        //             console.log(response.data.error.message);
        //         }
        //         else {
        //             console.log(response.data.results[0].formatted_address);
        //         }
        //     })
        //     .catch(error => {
        //         console.log(error.message);
        //     })
        // }

        showOriginLocationOnMap(latitude, longitude) {
            // Create map object
            this.map = new window.google.maps.Map(this.$refs["map"], {
                center: {lat: latitude, lng: longitude},
                zoom: 15
            })

            // Create origin marker on map
            new window.google.maps.Marker({
                position: {lat: latitude, lng: longitude},
                map: this.map,
            })
        },

        fetchInformation() {
            instance.get('MeetingPointDirections/GetEventLocation', {params: { eventID: 2 } })
            // instance.get('MeetingPointDirections/GetEventLocation?eventID=' + this.eventID )

            .then(response => {

                    if(response.status == 200) {
                        // this.location = response.data[0].eventStreetAddress;
                        // console.log(this.location);
                        this.eventStreetAddress = response.data[0].eventStreetAddress;
                        console.log(this.eventStreetAddress);
                        
                        this.eventCity = response.data[0].eventCity;
                        console.log(this.eventCity);
                        
                        this.eventState = response.data[0].eventState;
                        console.log(this.eventState);
                        
                        this.eventCountry = response.data[0].eventCountry;
                        console.log(this.eventCountry);
                        
                        this.eventZipCode = response.data[0].eventZipCode;
                        console.log(this.eventZipCode);
                    
                        this.location = this.eventStreetAddress + ", " + this.eventCity + ", " + this.eventState + " " + this.eventZipCode;
                        console.log(this.location);
                    }
                })
            .catch(error => console.log(error))
            .finally(() => console.log('DATA LOADING COMPLETE...'))
        },
    },
    created() {
        this.eventID = this.$route.params.data;
        // console.log(this.eventID);
    },
    mounted() {
        var autocomplete = new window.google.maps.places.Autocomplete(
            document.getElementById("autocomplete"),
        )
        autocomplete.addListener("place_changed", () => {
            var place = autocomplete.getPlace();
            console.log(place);
            this.latitude = place.geometry.location.lat();
            this.longitude = place.geometry.location.lng();
            this.showOriginLocationOnMap(place.geometry.location.lat(), place.geometry.location.lng());
        })
        this.fetchInformation();
        // instance.get('MeetingPointDirections/GetEventLocation', {params: { eventID: 2 } })
        //     // .then(response => this.location = response.data)
        //     // .then(response => console.log(this.location))
        //     // .then(console.log(this.location))

        //     .then(response => {
        //     //         // console.log(response.data);

        //             if(response.status == 200) {
        //                 this.location = response.data;
        //                 console.log(this.location);
                                            
        //                 // this.eventStreetAddress = this.location.eventStreetAddress;
        //                 // console.log(this.eventStreetAddress);
        //                 // this.eventStreetAddress = response.data.eventCity;
        //                 // console.log(response.data.eventStreetAddress);
        //                 // this.eventCity = response.data.eventCity;
        //                 // this.eventState = response.data.eventState;
        //                 // this.eventCountry = response.data.eventCountry;
        //                 // this.eventZipCode = response.data.eventZipCode;
        //             }
        //         })
        //     //     // .catch(e => {
        //     //     //     window.alert(e);
        //     //     // });
        //     .catch(error => console.log(error))
        //     .finally(() => console.log('DATA LOADING COMPLETE...'))

        // console.log(this.location);

        // this.map = new window.google.maps.Map(this.$refs["map"], {
        //     center: {lat: 33.781985, lng: -118.122324},
        //     zoom: 15
        // })
        // this.map = new window.google.maps.Map(document.getElementById('map'), {
        //     center: {lat: 33.781985, lng: -118.122324},
        //     zoom: 15
        // })
    },
    components: {
        TabBarComponent
    },
}
</script>

<style>
    #map{
        height: 600px;
        width: 800px;
        margin-left: auto;
        margin-right: auto;
        background: grey;
    }

    .pac-item{
        padding: 10px;
        font-size: 16px;
        cursor: pointer;
    }

    .pac-item:hover {
        background-color: grey;
    }

    .pac-item-query{
        font-size: 16px;
    }

</style>