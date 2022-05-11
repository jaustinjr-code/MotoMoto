<template>
    <TabBarComponent/>    
    <div class = Create Event Account Review></div>
    <h1>Selected Event Account Details</h1>
    <CreateEventAccountReviewComponent/>
</template>

<script>

import CreateEventAccountReviewComponent from "../components/CreateEventAccountReviewComponent.vue"
import TabBarComponent from "../components/TabBarComponent.vue"

export default {
    name: 'CreateEventAccountReview',
    components:
    {
        CreateEventAccountReviewComponent,
        TabBarComponent
    },
}
</script>

<style>

</style>