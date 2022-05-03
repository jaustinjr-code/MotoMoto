<template>
    <div>  </div>
    <div id="map" ref="map"></div>
</template>

<script>
import TabBarComponent from './TabBarComponent.vue'

export default {
    name: 'map',
    data: () => ({
        map: null
    }),
    methods: {

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
    methods:{

    },
    components: {
        TabBarComponent
    },
    methods: {

    }
}
</script>

<style scoped>
    #map{
        height: 600px;
        background: grey;
    }
</style>