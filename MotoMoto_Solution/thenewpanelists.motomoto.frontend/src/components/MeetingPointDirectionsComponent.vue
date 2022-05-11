<template>
    <!-- Origin Location -->
    <div class="form-row justify-content-center align-items-center">
        <div class="alert alert-danger" v-show="error">{{ error }}</div>
        <input type="text" class="form-control mb-2" v-model="origin" id="origin" placeholder="Enter Origin Location">
        <button type="button" class="btn btn-primary mb-2" @click="findOriginLocation">Use Current Location</button>
    </div>
    
    <div class="col-md-6"><h3> Latitude of Origin : {{ latitude }}</h3></div>
    <div class="col-md-6"><h3> Longitude of Origin : {{ longitude }}</h3></div>
    
    <!-- Event Location -->
    <div class="form-row justify-content-center align-items-center">
        <div class="row">
            <h3> Selected Event Location : {{ this.location }}</h3>
                <input type="text" class="form-control mb-2" v-model="destination" id="destination" placeholder="Enter Selected Event Location">
            <div class="col-md-6"><h3> Latitude of Event Location : {{ eventLatitude }}</h3></div>
            <div class="col-md-6"><h3> Longitude of Event Location : {{ eventLongitude }}</h3></div>
        </div>
    </div>

    <!-- Mode of travel dropdown -->
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
    
    <div class="row">
        <div class="col-md-6"><h3> Distance to Event Location : {{ distance }}</h3></div>
        <div class="col-md-6"><h3> Duration to Event Location : {{ duration }}</h3></div>
    </div>
    <div id="map" ref="map"></div>

</template>

<script>
import TabBarComponent from './TabBarComponent.vue'
import {instance} from '../router/MeetingPointDirectionsConnection'
window.axios = require('axios')

export default {
    name: 'Map',
    props: ['id'],
    data () {
        return {
            map: null,
            origin: '',
            destination: '',
            latitude: '',
            longitude: '',
            eventLatitude: '',
            eventLongitude: '',
            location: '',
            eventID: 0,
            eventStreetAddress: '',
            eventCity: '',
            eventState: '',
            eventCountry: '',
            eventZipCode: '',
            error: '',
            distance: '',
            duration: '',
        }
    },
    methods: {

        findOriginLocation() {
            // Prompt user if allow to use current location
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition( // True: Get coordinates of user
                    position => {
                        this.latitude = position.coords.latitude;
                        this.longitude = position.coords.longitude;
                        this.getAddressFromCoords(this.latitude, this.longitude); // Reverse geolocate coordinates
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

        // Reverse geocode the coordinates to find address
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

        // Fetch event details information within the datastore using eventID
        fetchInformation() {
            // instance.get('MeetingPointDirections/GetEventLocation', {params: { eventID: 1 } })
            instance.get('MeetingPointDirections/GetEventLocation?eventID=' + this.eventID )

            // Determine if the response is valid and set the data
            .then(response => {
                    if(response.status == 200) { 
                        this.eventStreetAddress = response.data[0].eventStreetAddress;                        
                        this.eventCity = response.data[0].eventCity;                        
                        this.eventState = response.data[0].eventState;                        
                        this.eventCountry = response.data[0].eventCountry;
                        this.eventZipCode = response.data[0].eventZipCode;                    
                        this.location = this.eventStreetAddress + " " + this.eventCity 
                            + " " + this.eventState + " " + this.eventZipCode;
                    }
                })
            .catch(error => this.error = error)
            .finally(() => window.alert("Loaded event location for eventID "))
        },

        // Method to autocomplete the origin and destination search bars
        initMap() {

            // Create autocomplete text input for origin and get the address and coordinates
            var autocomplete = new window.google.maps.places.Autocomplete(
                document.getElementById("origin"),
            )
            autocomplete.addListener("place_changed", () => {
                var place = autocomplete.getPlace();
                this.origin = place.formatted_address; 
                this.latitude = place.geometry.location.lat();
                this.longitude = place.geometry.location.lng();
            })

            // Create autocomplete text input for destination and get the address and coordinates
            var autocomplete2 = new window.google.maps.places.Autocomplete(
                document.getElementById("destination"),
            )
            autocomplete2.addListener("place_changed", () => {
                var place2 = autocomplete2.getPlace();
                this.destination = place2.formatted_address;
                this.eventLatitude = place2.geometry.location.lat();
                this.eventLongitude = place2.geometry.location.lng();
            })
        },

        // Find the directions after the user presses the button
        findDirections() {

            var directionsRenderer = new google.maps.DirectionsRenderer();
            var directionsService = new google.maps.DirectionsService();

            // Create a map which will be used to display the directions
            this.map = new window.google.maps.Map(document.getElementById("map"), {
                zoom: 15,
                center: {lat: this.latitude, lng: this.longitude},
            })
            directionsRenderer.setMap(this.map);
            
            // Display directions with current options
            this.displayDirections(directionsRenderer, directionsService);

            // If the user changes the mode then display the directions with the new mode
            document.getElementById("mode").addEventListener("change", () =>{ 
                this.displayDirections(directionsRenderer, directionsService);
            })
        },

        // Will display the directions from origin to destination
        displayDirections(directionsRenderer, directionsService) {
            
            var selectedMode = document.getElementById("mode").value;

            // Determine the route from origin to location using desired mode of travel
            directionsService.route({
                origin: new google.maps.LatLng(this.latitude, this.longitude),
                destination: new google.maps.LatLng(this.eventLatitude, this.eventLongitude),
                travelMode: google.maps.TravelMode[selectedMode],
                provideRouteAlternatives: true,

            })
            .then(response => {
                directionsRenderer.setDirections(response)
                this.duration = response.routes[0].legs[0].duration.text;
                this.distance = response.routes[0].legs[0].distance.text;
            })
            .catch(error => {
                window.alert("Directions request failed due to " + error)
            })
        }
    },

    created() {
        // Get the eventID passed from the EventList page
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