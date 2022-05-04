<template>
    <div>
        <PersonalizedRecommendationsComponent/>
        <PersonalizedRecommendationsDefaultComponent/>
        <PersonalizedRecommendationsMakesComponent/>
        <PersonalizedRecommendationsModelsComponent/>
    </div>
</template>

<script>
    import PersonalizedRecommendationsComponent from '../components/PersonalizedRecommendationsComponent.vue'
    import PersonalizedRecommendationsDefaultComponent from '../components/PersonalizedRecommendationsDefaultComponent.vue';
    import PersonalizedRecommendationsMakesComponent from '../components/PersonalizedRecommendationsMakesComponent.vue';
    import PersonalizedRecommendationsModelsComponent from '../components/PersonalizedRecommendationsModelsComponent.vue';

    export default {
        name: 'PersonalizedRecommendationsView',
        components: {
            PersonalizedRecommendationsComponent,
            PersonalizedRecommendationsDefaultComponent,
            PersonalizedRecommendationsMakesComponent,
            PersonalizedRecommendationsModelsComponent
        }
    }
</script>
