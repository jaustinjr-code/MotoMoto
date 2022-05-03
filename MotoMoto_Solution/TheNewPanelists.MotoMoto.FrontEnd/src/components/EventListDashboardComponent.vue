<template>
    <div class = "Feed">
        <h1>Feed</h1> 
        <div class="table-responsive">
            <table class="table table-striped">
                <thead>
                    <tr>
                        <th>Event ID</th>
                        <th>Event Location</th>
                        <th>Event Time</th>
                        <th>Event Date</th>
                        <th>Registered Users</th>
                        <th>Expand</th>
                    </tr>
                </thead>
                <tbody>
                    <tr
                    v-for="event in events"
                    :key="event.eventID">
                    <td>{{ event.eventID }}</td>
                    <td>{{ event.eventLocation }}</td>
                    <td>{{ event.eventTime }}</td>
                    <td>{{ event.eventDate }}</td>
                    <td>{{ event.registeredUsers}}</td>
                    <td>
                        <b-button variant="default">Details</b-button>
                        <b-button variant="default">Find Directions</b-button>
                    </td>
                </tr>
            </tbody>
            </table>
        </div>
    </div>
</template>

<script>
import {instance} from '../router/EventListConnection'
window.axios = require('axios')

export default {
    mounted: function() {
        instance.get('EventList/GetEvents')
            .then(response => this.events = response.data)
            .catch(error => console.log(error))
            .finally(() => console.log('Data loading complete.'))
    },
    data() {
        return { 
            events: [],
        };
    }
}
</script>

<style>
</style>