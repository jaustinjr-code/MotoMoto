<template>
  <div class="UserProfileView" style="font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;">
    <h2 style="font-size: 22px" class = "header"><i>User Preferences</i></h2>

    <div class = "preferencesContainer">

        <table>
          <th class = "title">Countries Followed</th>
          <tbody>
            <tr>
              <th>Country</th>
            </tr>
            <tr v-for="record in followedCountries" :key=record.country>
              <td>{{record.country}}</td>
            </tr>
          </tbody>
        </table>  

        <table>
          <th class = "title">Makes Followed</th>
          <tbody>
            <tr>
              <th>Make</th>
            </tr>
            <tr v-for="record in followedMakes" :key=record.make>
              <td>{{record.make}}</td>
            </tr>
          </tbody>
        </table>        

        <table>
          <th class = "title">Models Followed</th>
          <tbody>
            <tr>
              <th>Make</th>
              <th>Model</th>
            </tr>
            <tr v-for="record in followedModels" :key=record.model>
              <td>{{record.make}}</td>
              <td>{{record.model}}</td>
            </tr>
          </tbody>
        </table> 

    </div>

    <div class = "editButton">
      <button @click="$router.push('/UserProfile/EditPreferences')">Edit Preferences</button>
    </div>
  </div>
</template>

<script>
import { useCookies } from "vue3-cookies";
import { defineComponent } from "vue";
import {PersonalizedRecsApi} from '../router/PersonalizedRecommendationsConnection';


export default defineComponent({
  setup() {
    const { cookies } = useCookies();
    return { cookies };
  },
  data () {
    return {
      followedCountries: [],
      followedMakes: [],
      followedModels: [],
      hasPreferences: false
    }
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
    }
  },
  created: function () {
      if (this.$cookies.get("userId") != "guest") {
          this.GetPreferences();
      }
      else {
          this.$router.push('/login');
      }
  }
})
</script>

<style scoped>
.preferencesContainer
{
  display: flexbox;
  margin: auto;
  width: 500px;
  height: 500px;
  border: 3px solid grey;
  overflow-y: scroll;
}
Button
{
  margin-top: 5px;
  text-align: center;
  background-color: darkslateblue;
  color: white;
}
button:hover
{
  color: blue;
  background-color: lightgrey;
}
table
{
  table-layout: fixed;
  font-size: 16px;
  border-bottom: 5px solid grey;
  width: 100%;
  margin-top: 10px;
}
.title
{
  font-family: Cambria, Cochin, Georgia, Times, 'Times New Roman', serif;
  font-size: 20px;
  color: black;
  text-align: left;
  white-space: nowrap;
  padding-bottom: 10px;
  background-color: white;
}
th
{
  padding-left: 5px;
  padding-bottom: 5px;
  background-color:dimgray;
}
td,tr
{
  border-bottom: 1px solid grey;
  text-align: left;
  font-size: 12px;
  padding: 8px;
}
tr:nth-child(even) 
{
  background-color: #dddddd;
}
.noPreferences
{
  top: 30%;
  position: relative;
  font-size: 18px;
}
</style>
