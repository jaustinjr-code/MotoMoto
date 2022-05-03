<template>
    <div>
        <h1>Notification Center</h1>
        <button @click="display">Registered Events</button>
        <button>Subscribed Accounts</button>
        <h2>CurrentTime()</h2>
    </div>
</template>

<script>
import {instance} from '../router/NotificationSystemConnection'

export default {
    data() {
        return
    },
    mounted() {
        fetch()
    }
}   
</script>

<style>

.Search
{
    padding-top: 100px;
    padding-bottom: 100px;
    width: 100%;
    overflow: hidden;
}
</style>