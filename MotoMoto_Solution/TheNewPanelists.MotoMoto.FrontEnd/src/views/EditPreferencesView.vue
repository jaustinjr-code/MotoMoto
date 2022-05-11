<template>
<div>
    <TabBarComponent/>
    <EditPreferencesComponent/>
</div>
</template>

<script>
    import EditPreferencesComponent from '../components/EditPreferencesComponent.vue'
    import TabBarComponent from '../components/TabBarComponent.vue'

    export default {
        name: 'EditPreferences',
        components: {
            EditPreferencesComponent,
            TabBarComponent
        }
    }
</script>