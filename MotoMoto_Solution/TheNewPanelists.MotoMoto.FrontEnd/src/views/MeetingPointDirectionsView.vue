<template>
    <button @click = "goToHome"> Home </button>
    <button @click = "directMessage"> Direct Message </button>
    <TabBarComponent/>    
    <div class = MeetingPointDirections></div>
    <h1>Meeting Point Directions</h1>
    <MeetingPointDirectionsComponent/>
</template>

<script>

import MeetingPointDirectionsComponent from "../components/MeetingPointDirectionsComponent.vue"
import TabBarComponent from "../components/TabBarComponent.vue"

export default {
    name: 'MeetingPointDirectionsView',
    components:
    {
        MeetingPointDirectionsComponent,
        TabBarComponent
    },
    methods:
    {
        directMessage()
        {
            this.$router.push({path: '/DM'});
        },
        goToHome(){
            this.$router.push({path: '/'});
        }
    }

}
</script>

<style>

</style>