<template>
    <div class = EventList></div>
    <h1>Event List Feed</h1>
    <button @click = "goToHome"> Home </button>
    <button @click = "directMessage"> Direct Message </button>
    <EventListDashboardSearchComponent/>    
    <EventListDashboardFilterComponent/>
    <EventListDashboardComponent/>
</template>

<script>

import EventListDashboardComponent from "../components/EventListDashboardComponent.vue";
import EventListDashboardSearchComponent from "../components/EventListDashBoardSearchComponent.vue"
import EventListDashboardFilterComponent from "../components/EventListDashboardFilterComponent.vue"
import LogoutComponent from "../components/LogoutComponent.vue";

export default {
    name: 'EventListView',
    components:
    {
        EventListDashboardSearchComponent,
        EventListDashboardFilterComponent,
        EventListDashboardComponent,
        LogoutComponent
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