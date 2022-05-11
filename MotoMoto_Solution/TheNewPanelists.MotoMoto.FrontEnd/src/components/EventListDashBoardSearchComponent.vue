<template>
    <div class = "Search">
        <h1> Search </h1>
        <input type="text" v-model="search" placeholder="Search">
        <div v-for="event in events" :key="event.eventID" class="single-event"></div>
    </div>
</template>

<script>
import EventListDashboardComponent from '../components/EventListDashboardComponent.vue'

export default {
    data(){
        return{
            events: EventListDashboardComponent.events,
            search: ''
        }
    },
    computed: {
        filteredEvents: function(){
            return events.filter((event) => {return event.eventLocation.match(this.search)})
        }
    }
}

</script>

<style>

.Search
{
    padding-top: 50px;
    padding-bottom: 50px;
    width: 100%;
    overflow: hidden;
}
</style>