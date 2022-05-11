<template>
    <TabBarComponent/>
    <div class = EventList></div>
    <h1>Event List Feed</h1>
    <EventListDashboardSearchComponent/>    
    <EventListDashboardFilterComponent/>
    <EventListDashboardComponent/>
    <kpi-submission-component viewTitle="Event List"></kpi-submission-component>
</template>

<script>

import EventListDashboardComponent from "../components/EventListDashboardComponent.vue";
import EventListDashboardSearchComponent from "../components/EventListDashBoardSearchComponent.vue"
import EventListDashboardFilterComponent from "../components/EventListDashboardFilterComponent.vue"
import TabBarComponent from "../components/TabBarComponent.vue"
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