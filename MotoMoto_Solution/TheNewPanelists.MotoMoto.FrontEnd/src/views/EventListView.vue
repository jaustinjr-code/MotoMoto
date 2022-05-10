<template>
    <TabBarComponent/>
    <div class = EventList></div>
    <h1>Event List Feed</h1>
    <EventListDashboardSearchComponent/>    
    <EventListDashboardFilterComponent/>
    <EventListDashboardComponent/>
</template>

<script>

import EventListDashboardComponent from "../components/EventListDashboardComponent.vue";
import EventListDashboardSearchComponent from "../components/EventListDashBoardSearchComponent.vue"
import EventListDashboardFilterComponent from "../components/EventListDashboardFilterComponent.vue"
import TabBarComponent from "../components/TabBarComponent.vue"
import LogoutComponent from "../components/LogoutComponent.vue";

export default {
    name: 'EventListView',
    components:
    {
        EventListDashboardSearchComponent,
        EventListDashboardFilterComponent,
        EventListDashboardComponent,
        LogoutComponent,
        TabBarComponent,
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