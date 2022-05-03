<template>
    <div class="form-row justify-content-center align-items-center">
        <input type="text" class="form-control mb-2" v-model="origin" placeholder="Enter Origin Location">
        <button type="button" class="btn btn-primary mb-2" @click="findOriginLocation">Search</button>
    </div>
    <div class="col-md-6"><h3> Latitude : {{ latitude }}</h3></div>
    <div class="col-md-6"><h3> Longitude : {{ longitude }}</h3></div>
    <div id="map" ref="map"></div>
</template>

<script>
import TabBarComponent from './TabBarComponent.vue'

export default {
    name: 'Map',
    data () {
        return {
            origin: '',
            map: null,
            latitude: '',
            longitude: '',
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
                        // console.log(position.coords.latitude);
                        // console.log(position.coords.longitude);
                    },
                    error => {
                        console.log(error.message);
                    }
                );
            } else {
                console.log("Your browser does not support geolocatoin API");
            }
        }
    },
    mounted() {
        this.map = new window.google.maps.Map(this.$refs["map"], {
            center: {lat: 33.781985, lng: -118.122324},
            zoom: 15
        })
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

<style scoped>
    #map{
        height: 600px;
        background: grey;
    }
</style>