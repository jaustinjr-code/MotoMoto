<template>
<div class = "main">
    <h1 class = "header"><u>Personalized Recommendations</u></h1>
            <div class = "component">
                <h2 class = "header">Our Recommendations</h2>
                <PersonalizedRecommendationsDefaultComponent/>
            </div>

            <div class = "component" v-if="followedMakes.length == 0">
                <h2 class = "header">Followed Makes</h2>
                <PersonalizedRecommendationsMakesComponent/>
            </div>
                
            <div class = "component" v-if="followedModels.length == 0">
                <h2 class = "header">Specific Models</h2>
                <PersonalizedRecommendationsModelsComponent/>
            </div>
</div>
</template>

<script>
    import PersonalizedRecommendationsDefaultComponent from '../components/PersonalizedRecommendationsDefaultComponent.vue';
    import PersonalizedRecommendationsMakesComponent from '../components/PersonalizedRecommendationsMakesComponent.vue';
    import PersonalizedRecommendationsModelsComponent from '../components/PersonalizedRecommendationsModelsComponent.vue';

    export default {
        name: 'PersonalizedRecommendationsView',
        components: 
        {
            PersonalizedRecommendationsDefaultComponent,
            PersonalizedRecommendationsMakesComponent,
            PersonalizedRecommendationsModelsComponent
        },
        data () {
            return {
                followedMakes: [],
                followedModels: []
            }
        },
        methods () {

        }
    }
</script>

<style scoped>

h1.header
{
    font-family:Cambria, Cochin, Georgia, Times, 'Times New Roman', serif;
    font-size: 40px;
    padding-bottom: 100px;
    color: darkslateblue;
}
.component
{
    border-style: hidden;
    border-color: black;
    padding-bottom: 150px;
    border-width: 0.25px;
    overflow-x: scroll;
}
h2.header
{
    font-family:Cambria, Cochin, Georgia, Times, 'Times New Roman', serif;
    font-size: 24px;
}
</style>