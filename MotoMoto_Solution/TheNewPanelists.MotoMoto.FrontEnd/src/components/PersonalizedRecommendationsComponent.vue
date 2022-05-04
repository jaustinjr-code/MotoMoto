<template>
    <div class = "main">
        <div class = "top">
            <h1 class = "header">Personalized Recommendations</h1>
            <div class = "menunav">
                <button @click = "$router.push('/')">Home</button>
                <button @click = "$router.push('/DM')">Direct Messages</button>
                <button @click = "$router.push('/CommunityDashboard')">Community Board</button>
                <button @click = "$router.push('/UserProfile')">Preferences</button>
            </div>      
        </div>

        <div class = "container">
            <div class = "component">
                <h2 class = "header"><i>Our Recommendations</i></h2>
                <PersonalizedRecommendationsDefaultComponent/>
            </div>

            <div class = "component" v-if="followedMakes.length == 0">
                <h2 class = "header"><i>Followed Makes</i></h2>
                <PersonalizedRecommendationsMakesComponent/>
            </div>
                
            <div class = "component" v-if="followedModels.length == 0">
                <h2 class = "header"><i>Specific Models</i></h2>
                <PersonalizedRecommendationsModelsComponent/>
            </div>
        </div>
    </div>
</template>

<script>
    import { useCookies } from "vue3-cookies";
    import { defineComponent } from "vue";
    import {instance} from '../router/PreferencesManagerConnection';

    export default defineComponent ({
        data () {
            return {
                followedMakes: [],
                followedModels: []
            }
        },
        methods: {
            GetPreferences: async function() {
            if (this.$cookies.get("userId") != "guest") {
                userId = this.$cookies.get("userId")

                if(!this.$cookies.get("preferencesChecked"))
                {
                    await instance.get('/Preferences/Retrieve', {params: {userId: this.$cookies.get("userId")}}).then((response)=>{
                        console.log(`Server replied with: ${response.data}`),
                        this.$cookies.set("preferencesChecked", true, "1h"),
                        this.$cookies.set("preferences", [response.data.followedCountries, response.data.followedModels, response.data.followedMakes], "1h");
                    }).catch((e)=>{
                        console.log(e);})
                }
                else
                {
                    this.followedCountries = (this.$cookies.get("preferences"))[0],
                    this.followedMakes = (this.$cookies.get("preferences"))[1],
                    this.followedModels = (this.$cookies.get("preferences"))[2];
                }
            }
            else
            {
                this.$router.push('/login');
            }
            }
        },
        created () {
            this.GetPreferences()
        }
    })
</script>

<style scoped>

.top
{
    font-family:Cambria, Cochin, Georgia, Times, 'Times New Roman', serif;
    margin: auto;
    white-space: nowrap;
	text-align: center; 
    height: 100px;
    width: 600px;
    background-color: aliceblue;
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