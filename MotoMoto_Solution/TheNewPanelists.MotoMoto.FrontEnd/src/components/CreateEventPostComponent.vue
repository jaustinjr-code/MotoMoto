<template>
    <div class="alert alert-danger" v-show="error">{{ error }}</div>
    <h3>Enter Event Title: </h3>
    <input v-model="eventTitle" placeholder="Event Title"/>
    <h3>Enter Event Time: </h3>
    <input v-model="eventTime" placeholder="Event Time"/>
    <h3>Enter Event Date: </h3>
    <input v-model="eventDate" placeholder="Event Date"/>
    <h3>Enter Event Street Address: </h3>
    <input v-model="eventStreetAddress" placeholder="Event Street Address"/>
    <h3>Enter Event City: </h3>
    <input v-model="eventCity" placeholder="Event City"/>
    <h3>Enter Event State: </h3>
    <input v-model="eventState" placeholder="Event State"/>
    <h3>Enter Event Title: </h3>
    <input v-model="eventCountry" placeholder="Event Country"/>
    <h3>Enter Event ZipCode: </h3>
    <input v-model="eventZipCode" placeholder="Event ZipCode"/>
    <h3><button type="button" class="btn btn-primary mb-2" @click="postEvent()"> Post Event </button></h3>
</template>

<script>
import {instance} from '../router/EventListConnection'
window.axios = require('axios')

export default {
    components: {

    },

    data() {
        return { 
            eventTime: '',
            eventDate: '',
            eventStreetAddress: '',
            eventCity: '',
            eventState: '',
            eventCountry: '',
            eventZipCode: '',
            eventTitle: '',
            error: '',
        };
    },
    methods: {
        postEvent() {
            instance.put('/EventList/CreateEvent', {params: 
                {eventTime: this.eventTime, 
                eventDate: this.eventDate,
                eventStreetAddress: this.eventStreetAddress,
                eventCity: this.eventCity,
                eventState: this.eventState,
                eventCountry: this.eventCountry,
                eventZipCode: this.eventZipCode,
                eventTitle: this.eventTitle
                }})
            .then((response) => {
                window.alert("Server replied with " + response.data);
            }).catch((e)=> {
                this.error = e;
                window.alert(this.error);
            });
        }
    }
}
</script>

<style>

</style>