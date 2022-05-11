<template>
    <div class = EventList></div>
    <h1>Event List Feed</h1>
    <button @click = "goToHome"> Home </button>
    <button @click = "directMessage"> Direct Message </button>
    <EventListDashboardSearchComponent/>    
    <EventListDashboardFilterComponent/>
    <EventListDashboardComponent/>
    <kpi-submission-component viewTitle="Event List"></kpi-submission-component>
</template>

<script>

import EventListDashboardComponent from "../components/EventListDashboardComponent.vue";
import EventListDashboardSearchComponent from "../components/EventListDashBoardSearchComponent.vue"
import EventListDashboardFilterComponent from "../components/EventListDashboardFilterComponent.vue"
import LogoutComponent from "../components/LogoutComponent.vue";
import KpiSubmissionComponent from "../components/KpiSubmissionComponent.vue"

export default {
    name: 'EventListView',
    components:
    {
        EventListDashboardSearchComponent,
        EventListDashboardFilterComponent,
        EventListDashboardComponent,
        LogoutComponent,
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