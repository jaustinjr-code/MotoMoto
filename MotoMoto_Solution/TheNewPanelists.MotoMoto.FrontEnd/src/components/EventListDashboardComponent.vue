<template>
    <div class = "Feed">
        <h1>Feed</h1> 
        <div class="table-responsive">
            <table class="table table-striped">
                <thead>
                    <tr>
                        <th>Event ID</th>
                        <!-- <th>Street Address</th> -->
                        <th>City</th>
                        <!-- <th>State</th> -->
                        <th>Event Time</th>
                        <th>Event Date</th>
                        <th>Registered Users</th>
                        <th>Expand</th>
                    </tr>
                </thead>
                <tbody>
                    <tr
                    v-for="(event, id) in events"
                    :key="event.eventID">
                    <td>{{ event.eventID }}</td>
                    <!-- <td>{{ event.eventStreetAddress }}</td> -->
                    <td>{{ event.eventCity }}</td>
                    <!-- <td>{{ event.eventState }}</td> -->
                    <td>{{ event.eventTime }}</td>
                    <td>{{ event.eventDate }}</td>
                    <td>{{ event.registeredUsers}}</td>
                    <td><button type="button" class="btn btn-primary mb-2"> Details </button></td>
                    <td><button type="button" class="btn btn-primary mb-2" @click="goToMeetingPointDirections(id)"> Find Directions </button></td>   
                </tr>
            </tbody>
            </table>
        </div>
    </div>
    <!-- <MeetingPointDirectionsComponent :eventID="this.id"/> -->
</template>

<script>
import {instance} from '../router/EventListConnection'
import MeetingPointDirectionsComponent from '../components/MeetingPointDirectionsComponent.vue'
window.axios = require('axios')

export default {
    components: {
        MeetingPointDirectionsComponent,
    },
    mounted: function() {
        instance.get('EventList/GetEvents')
            .then(response => this.events = response.data)
            .catch(error => console.log(error))
            .finally(() => console.log('Data loading complete.'))
    },
    data() {
        return { 
            events: [],
            id: 0,
        };
    },
    methods: {
        goToMeetingPointDirections(id) {
            // var rowId = this.events.eventID;
            this.id = id;
            console.log(this.id);
            console.log(this.events);
            this.$router.push('/MeetingPointDirections')
        }
    }
}
</script>

<style>

</style>