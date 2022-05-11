<template>
    <div>
        <TabBarComponent/>
        <MainComponent/>
        <kpi-submission-component viewTitle="Personalized Recommendations"></kpi-submission-component>
    </div>
</template>

<script>
    import MainComponent from '../components/PersonalizedRecommendationsComponents/MainComponent.vue'
    import TabBarComponent from '../components/TabBarComponent.vue'
    import KpiSubmissionComponent from '../components/KpiSubmissionComponent.vue'

    export default {
        name: 'PersonalizedRecommendationsView',
        components: {
            MainComponent,
            KpiSubmissionComponent,
            TabBarComponent
        }
    }
</script>
