<template>
    <button @click = "goToHome"> Home </button>
    <button @click = "directMessage"> Direct Message </button>
    <TabBarComponent/>    
    <div class = MeetingPointDirections></div>
    <h1>Meeting Point Directions</h1>
    <MeetingPointDirectionsComponent/>
    <kpi-submission-component viewTitle="Event List"></kpi-submission-component>
</template>

<script>

import MeetingPointDirectionsComponent from "../components/MeetingPointDirectionsComponent.vue"
import TabBarComponent from "../components/TabBarComponent.vue"
import KpiSubmissionComponent from "../components/KpiSubmissionComponent.vue"

export default {
    name: 'MeetingPointDirectionsView',
    components:
    {
        MeetingPointDirectionsComponent,
        TabBarComponent,
        KpiSubmissionComponent
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