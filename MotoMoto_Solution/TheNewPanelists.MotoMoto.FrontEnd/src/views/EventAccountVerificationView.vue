<template>
    <TabBarComponent/>    
    <div class = Verified Event Accounts></div>
    <h1>Verified Event Accounts</h1>
    <EventAccountVerificationComponent/>
</template>

<script>

import EventAccountVerificationComponent from "../components/EventAccountVerificationComponent.vue"
import TabBarComponent from "../components/TabBarComponent.vue"

export default {
    name: 'EventAccountVerificationView',
    components:
    {
        EventAccountVerificationComponent,
        TabBarComponent
    },
}
</script>

<style>

</style>