<template>
    <div>
        <PersonalizedRecommendationsComponent/>
        <kpi-submission-component viewTitle="Personalized Recommendations"></kpi-submission-component>
    </div>
</template>

<script>
    import PersonalizedRecommendationsComponent from '../components/PersonalizedRecommendationsComponent.vue'
    import KpiSubmissionComponent from '../components/KpiSubmissionComponent.vue'

    export default {
        name: 'PersonalizedRecommendationsView',
        components: {
            PersonalizedRecommendationsComponent,
            KpiSubmissionComponent
        }
    }
</script>
