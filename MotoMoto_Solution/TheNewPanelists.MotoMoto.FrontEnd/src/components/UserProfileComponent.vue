<template>
  <div class="UserProfileView" style="font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;">
    <h2 style="font-size: 22px" class = "header"><i>User Preferences</i></h2>

    <div class = "preferencesContainer">
      <div v-if="preferences.length > 0">
        <table v-if="followedCountries.length > 0">
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

        <table v-if="followedMakes.length > 0">
          <th class = "title">Makes Followed</th>
          <tbody>
            <tr>
              <th>Country</th>
              <th>Make</th>
            </tr>
            <tr v-for="record in followedMakes" :key=record.make>
              <td>{{record.country}}</td>
              <td>{{record.make}}</td>
            </tr>
          </tbody>
        </table>        

        <table v-if="followedModels > 0">
          <th class = "title">Models Followed</th>
          <tbody>
            <tr>
              <th>Country</th>
              <th>Make</th>
              <th>Model</th>
            </tr>
            <tr v-for="record in followedModels" :key=record.model>
              <td>{{record.country}}</td>
              <td>{{record.make}}</td>
              <td>{{record.model}}</td>
            </tr>
          </tbody>
        </table> 
      </div>

      <div v-else class="noPreferences"> 
          <b>You have no preferences!</b><br><br>
           <p>
             Please select edit preferences to follow specific<br>
             makes, models, or countries of origin
          </p>
      </div>
    </div>

    <div class = "editButton">
      <button @click="$router.push('/UserProfile/EditPreferences')">Edit Preferences</button>
    </div>
  </div>
</template>

<script>
import { useCookies } from "vue3-cookies";
import { defineComponent } from "vue";
import {instance} from '../router/PreferencesManagerConnection'

export default defineComponent({
  setup() {
    const { cookies } = useCookies();
    return { cookies };
  },
  data () {
    return {
      userId: 0,
      followedCountries: [],
      followedMakes: [],
      followedModels: [],
      preferences: []
    }
  },
  created () {
  },
  methods: {

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
  text-align: left;
  white-space: nowrap;
  padding-bottom: 10px;
}
th
{
  padding-left: 5px;
  padding-bottom: 5px;
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
