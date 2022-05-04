<template>
    <div>
        <EditPreferencesComponent/>
        <router-view></router-view>
    </div>
</template>

<script>
    import EditPreferencesComponent from '../components/EditPreferencesComponent.vue'
    export default {
        name: 'EditPreferencesView',
        components: {
            EditPreferencesComponent,
        }
    }
</script>