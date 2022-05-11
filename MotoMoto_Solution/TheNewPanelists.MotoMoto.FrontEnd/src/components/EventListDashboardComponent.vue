<template>
    <button type="button" class="btn btn-primary mb-2" @click="goToCreateEventPost"> Create Event </button>
    <div class = "Feed">
        <button type="button" class="btn btn-primary mb-2" @click="goToEventAccountVerification"> Find Event Accounts to Review </button>
        <div class="table-responsive">
            <table class="table table-striped">
                <thead>
                    <tr>
                        <td>Event ID</td>
                        <!-- <th>Street Address</th> -->
                        <td>City</td>
                        <!-- <th>State</th> -->
                        <td>Event Time</td>
                        <td>Event Date</td>
                        <td>Registered Users</td>
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

                    <!-- <MeetingPointDirectionsComponent :idData="this.id"/> -->
                    <td><button type="button" class="btn btn-primary mb-2" @click="goToMeetingPointDirections(id)"> Find Directions </button></td>   
                </tr>
            </tbody>
            </table>
        </div>
    </div>
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
            // .then(response => console.log(response.data))
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
            this.$router.push({ name: 'MeetingPointDirections', params: {data: this.id+1 } });
            // this.$router.push('/MeetingPointDirections')
        },
        goToCreateEventPost() {
            this.$router.push({name: 'CreateEventPost'});
        },
        goToEventAccountVerification() {
            this.$router.push({name: 'EventAccountVerification'});
        }
    }
}
</script>

<style>

</style>