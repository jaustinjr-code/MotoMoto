<template>
    <div class="form-row justify-content-center align-items-center">
        <div class="alert alert-danger" v-show="error">{{ error }}</div>
        <input type="text" class="form-control mb-2" v-model="origin" id="autocomplete" placeholder="Enter Origin Location">
        <button type="button" class="btn btn-primary mb-2" @click="findOriginLocation">Use Current Location</button>
    </div>
    <!-- <div class="row"> -->
        <div class="col-md-6"><h3> Latitude of Origin : {{ latitude }}</h3></div>
        <div class="col-md-6"><h3> Longitude of Origin : {{ longitude }}</h3></div>
    <!-- </div> -->

    <div class="form-row justify-content-center align-items-center">
        <div class="row">
            <h3> Selected Event Location : {{ this.location }}</h3>
                <input type="text" class="form-control mb-2" v-model="destination" id="autocomplete2" placeholder="Enter Selected Event Location">
            <div class="col-md-6"><h3> Latitude of Event Location : {{ eventLatitude }}</h3></div>
            <div class="col-md-6"><h3> Longitude of Event Location : {{ eventLongitude }}</h3></div>
        </div>
    </div>

    <div class="form-row justify-content-center align-items-center">
        <div class="row">
            <h3> Mode of Travel: </h3>
            <select id="mode">
                <option value="DRIVING">Driving</option>
                <option value="WALKING">Walking</option>
                <option value="BICYCLING">Bicycling</option>
                <option value="TRANSIT">Transit</option>
            </select>
        </div>
    </div>
    
    <button type="button" class="btn btn-primary mb-2" @click="findDirections">Find Directions</button>

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
            eventLatitude: '',
            eventLongitude: '',
            error: '',
            location: '',
            eventID: 0,
            eventStreetAddress: '',
            eventCity: '',
            eventState: '',
            eventCountry: '',
            eventZipCode: '',
            destination: '',
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
                        // this.showOriginLocationOnMap(this.latitude, this.longitude);
                    },
                    error => {
                        window.alert(error.message + " REDIRECTING TO EVENT LIST PAGE...");
                        this.$router.push('/EventList')
                    }
                );
            } else {
                this.error = "BROWSER DOES NOT SUPPORT GEOLOCATION API..."
            }
        },
        getAddressFromCoords(latitude, longitude) {
            var geocoder = new window.google.maps.Geocoder();

            geocoder.geocode({ location: new google.maps.LatLng(latitude, longitude) })
            .then((response) => {
                if(response.results[0]) {
                    this.origin = response.results[0].formatted_address;
                }
                else {
                    this.error = response.error_message;
                }
            })
            .catch(error => {
                this.error = error;
            })
        },

        showOriginLocationOnMap(latitude, longitude) {

            // // Create map object
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
                        this.eventStreetAddress = response.data[0].eventStreetAddress;                        
                        this.eventCity = response.data[0].eventCity;                        
                        this.eventState = response.data[0].eventState;                        
                        this.eventCountry = response.data[0].eventCountry;
                        this.eventZipCode = response.data[0].eventZipCode;                    
                        this.location = this.eventStreetAddress + " " + this.eventCity + " " + this.eventState;// + " " + this.eventZipCode;
                    }
                })
            .catch(error => console.log(error))
            .finally(() => console.log('DATA LOADING COMPLETE...'))
        },

        initMap() {

            var autocomplete = new window.google.maps.places.Autocomplete(
                document.getElementById("autocomplete"),
            )
            autocomplete.addListener("place_changed", () => {
                var place = autocomplete.getPlace();
                // console.log(place);
                this.origin = place.formatted_address; // MIGHT NOT NEED IF ORIGIN TEXT BOX GETS UPDATED ON CLICK
                this.latitude = place.geometry.location.lat();
                this.longitude = place.geometry.location.lng();
                this.showOriginLocationOnMap(place.geometry.location.lat(), place.geometry.location.lng());
            })

            var autocomplete2 = new window.google.maps.places.Autocomplete(
                document.getElementById("autocomplete2"),
            )
            autocomplete2.addListener("place_changed", () => {
                var place2 = autocomplete2.getPlace();
                this.destination = place2.formatted_address;
                this.eventLatitude = place2.geometry.location.lat();
                this.eventLongitude = place2.geometry.location.lng();
            })
        },
        findDirections() {

            var directionsRenderer = new google.maps.DirectionsRenderer();
            var directionsService = new google.maps.DirectionsService();

            this.map = new window.google.maps.Map(document.getElementById("map"), {
                zoom: 15,
                center: {lat: this.latitude, lng: this.longitude},
            })
            directionsRenderer.setMap(this.map);
            this.displayDirections(directionsRenderer, directionsService);
            document.getElementById("mode").addEventListener("change", () =>{
                this.displayDirections(directionsRenderer, directionsService);
            })
        },

        displayDirections(directionsRenderer, directionsService) {
            const selectedMode = document.getElementById("mode").value;
            // console.log(document.getElementById("autocomplete").value);
            directionsService.route({
                origin: new google.maps.LatLng(this.latitude, this.longitude),
                destination: new google.maps.LatLng(this.eventLatitude, this.eventLongitude),
                travelMode: google.maps.TravelMode[selectedMode],
                provideRouteAlternatives: true,

            })
            .then(response => {
                directionsRenderer.setDirections(response);
            })
            .catch(error => {
                window.alert("Directions request failed due to " + error)
            })
        }
    },
    created() {
        this.eventID = this.$route.params.data;
    },
    mounted() {

        this.fetchInformation();

        this.initMap();

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

    h3{
        padding-top: 25px;
    }

</style>