<template>
    <div class = "verifiedAccounts">
        <h3> Verified Event Accounts </h3>
        <div class="table-responsive">
            <table class="table table-striped">
                <thead>
                    <tr>
                        <td>Event Account Username</td>
                    </tr>
                </thead>
                    <tbody>
                        <tr
                            v-for="accnt in eventAccounts"
                            :key="accnt.username">
                            <td>{{ accnt.username }}</td>
                            <td><button type="button" class="btn btn-primary mb-2" @click="goToCreateReview(accnt.username)"> Rate This Event Account </button></td>
                        </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>

<script>
import {instance} from '../router/EventListConnection'
import CreateEventAccountReviewComponent from '../components/CreateEventAccountReviewComponent.vue'
window.axios = require('axios')

export default {
    components: {
        CreateEventAccountReviewComponent,
    },
    data() {
        return { 
            eventAccounts: [],
        };
    },
    mounted: function() {
    instance.get('EventList/GetAllEventAccounts')
        .then(response => this.eventAccounts = response.data)
        // .then(response => console.log(response.data))
        .catch(error => console.log(error))
        .finally(() => console.log('Data loading complete.'))
    },  
    methods: {
        goToCreateReview(user) {
            console.log(user);
            this.$router.push({ name: 'CreateEventAccountReview', params: {data: user}}); 
        },
    }
}
</script>

<style>

</style>