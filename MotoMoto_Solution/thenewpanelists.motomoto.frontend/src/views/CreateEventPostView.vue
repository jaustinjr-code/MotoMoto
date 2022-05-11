<template>
    <TabBarComponent/>    
    <div class = Create Event Post></div>
    <h1>Create Event Post</h1>
    <CreateEventPostComponent/>
</template>

<script>

import CreateEventPostComponent from "../components/CreateEventPostComponent.vue"
import TabBarComponent from "../components/TabBarComponent.vue"

export default {
    name: 'CreateEventPostView',
    components:
    {
        CreateEventPostComponent,
        TabBarComponent
    },
}
</script>

<style>

</style>