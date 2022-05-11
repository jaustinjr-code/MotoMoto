<template>

    <div class = "verifiedAccounts">
        <h3> Ratings and Reviews for selected event account </h3>
        <div class="table-responsive">
            <table class="table table-striped">
                <thead>
                    <tr>
                        <td>Event Account Username</td>
                        <td>Rating</td>
                        <td>Review</td>
                    </tr>
                </thead>
                    <tbody>
                        <tr
                            v-for="accnt in eventAccounts"
                            :key="accnt.username">
                            <td>{{ accnt.username }}</td>
                            <td>{{ accnt.rating }}</td>
                            <td>{{ accnt.review }}</td>
                        </tr>
                </tbody>
            </table>
        </div>
    </div>

    <div class="alert alert-danger" v-show="error">{{ error }}</div>
    <h3>Enter Rating: </h3>
    <input v-model="eventTitle" placeholder="Rating"/>
    <h3>Enter Review: </h3>
    <input v-model="eventTime" placeholder="Review"/>
    <h3><button type="button" class="btn btn-primary mb-2" @click="postReview()"> Post Review </button></h3>
</template>

<script>
import TabBarComponent from './TabBarComponent.vue'
import {instance} from '../router/EventListConnection'
window.axios = require('axios')

export default {
    components: {

    },
    props: ['user'],
    data() {
        return { 
            username: '',
            rating: null,
            review: '',
            error: '',
        };
    },
    methods: {
        postEvent() {
            let params = {username: this.username, rating: this.rating, review: this.review}
            console.log(params);
            instance.get('EventList/CreateEvent', {params})
            .then((response) => {
                window.alert("Review has successfully been posted... Returning back to event account verifictaion page...");
                this.$router.push({name: 'EventAccountVerification'});
            }).catch((e)=> {
                this.error = e;
                window.alert(this.error);
            });
        }
    },
    created() {
        this.username = this.$route.params.data; 
    },
    components: {
        TabBarComponent,
    },
}
</script>

<style>

</style>