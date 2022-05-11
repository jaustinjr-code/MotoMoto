<template>
    <div class = "PersonalizedRecommendationsComponent">
        <div class = "top">
            <h1 class = "header">Personalized Recommendations</h1>    
        </div>

        <div class = "container">
            <div class = "component">
                <h2 class = "header"><i>Our Recommendations</i></h2>
                <PersonalizedRecommendationsDefaultComponent/>
            </div>

            <div class = "component" v-if="followedCountries.length > 0">
                <h2 class = "header"><i>Followed Countries</i></h2>
                <PersonalizedRecommendationsCountriesComponent/>
            </div>
            <div class = "component" v-if="followedMakes.length > 0">
                <h2 class = "header"><i>Followed Makes</i></h2>
                <PersonalizedRecommendationsMakesComponent/>
            </div>
                
            <div class = "component" v-if="followedModels.length > 0">
                <h2 class = "header"><i>Specific Models</i></h2>
                <PersonalizedRecommendationsModelsComponent/>
            </div>

            <button style="margin-top: 20px;" @click = "GoToPreferences()">Preferences</button>
        </div>
    </div>
</template>

<script>
    import { useCookies } from "vue3-cookies";
    import { defineComponent } from "vue";
    import DefaultComponent from '../../components/PersonalizedRecommendationsComponents/DefaultComponent.vue';
    import CountriesComponent from '../PersonalizedRecommendationsComponents/CountriesComponent.vue';
    import MakesComponent from '../PersonalizedRecommendationsComponents/MakesComponent.vue';
    import ModelsComponent from '../PersonalizedRecommendationsComponents/ModelsComponent.vue';

    export default defineComponent ({
        setup() {
            const { cookies } = useCookies();
            return { cookies };
        },
        data () {
            return {
                username: '',
                followedCountries: '',
                followedMakes: '',
                followedModels: ''
            }
        },
        components: {
            DefaultComponent,
            CountriesComponent,
            MakesComponent,
            ModelsComponent,
        },
        methods: {
            GetPreferences: async function() {
                await PersonalizedRecsApi.get('/Preferences/Retrieve', {params: {userId: this.$cookies.get("userId")}}).then((response)=>{
                    console.log(`Server replied with: ${response.data}`),
                    this.followedCountries = response.data.followedCountries, 
                    this.followedMakes = response.data.followedMakes, 
                    this.followedModels = response.data.followedModels
                }).catch((e)=>{
                    console.log(e);})
            },
            GoToPreferences: async function() {
                this.$router.push({
                    name: 'UserProfile',
                    params: {
                        username: this.username
                    }
                });
            }
        },
        mounted: function () {
            if (this.$cookies.get("userId") == "guest") {
                this.$router.push('/login');
            }
            else {
                this.username = this.$cookies.get("username");
                this.GetPreferences();
            }
        }
    })
</script>

<style scoped>
.PersonalizedRecommendationsComponent
{
    font-family: Cambria, Cochin, Georgia, Times, 'Times New Roman', serif;
}
.top
{
    margin: auto;
    white-space: nowrap;
	text-align: center; 
    height: 78px;
    width: 600px;
    border-style: solid;
    border-width: .05ch;
}
h1.header
{
    font-size: 40px;
    color: darkslateblue;
}

button 
{
    white-space: nowrap;
    background-color: lightgray;
    width: fit-content;
    border: solid;
    border-color: black;
    border-width: 0.5px;
    color: black;
    text-align: center;
    font-size: 16px;
    cursor: pointer;
    
}
button:hover
{
    background-color:lightgrey;
    color: red;
}
.container
{
    padding-top: 75px;
}
.component
{
    text-align: left;
    padding-bottom: 125px;
    overflow-x: scroll;
}
h2.header
{
    font-family:Cambria, Cochin, Georgia, Times, 'Times New Roman', serif;
    font-size: 24px;
}
</style>