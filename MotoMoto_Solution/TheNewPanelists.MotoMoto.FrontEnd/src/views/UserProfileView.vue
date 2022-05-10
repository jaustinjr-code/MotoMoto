<template>
    <div>
        <UserProfileComponent/>
        <kpi-submission-component viewTitle="User Profile"></kpi-submission-component>
    </div>
</template>

<script>
    import UserProfileComponent from '../components/UserProfileComponent.vue'
    import KpiSubmissionComponent from '../components/KpiSubmissionComponent.vue'
    export default {
        name: 'UserProfileView',
        components: 
        {
            UserProfileComponent,
            KpiSubmissionComponent
        },
    }
</script>