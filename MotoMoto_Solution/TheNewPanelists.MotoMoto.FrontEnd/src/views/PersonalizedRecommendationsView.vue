<template>
    <PersonalizedRecommendationsComponent/>
    <LogoutComponent/>
</template>

<script>
    import PersonalizedRecommendationsComponent from '../components/PersonalizedRecommendationsComponent.vue';
    import LogoutComponent from '../components/LogoutComponent.vue'
    export default {
        name: 'PersonalizedRecommendationsView',
        components: 
        {
            PersonalizedRecommendationsComponent,
            LogoutComponent
        }
    }
</script>