<template>
  <div class = "EditPreferences">
    <h2 style="font-size: 30px;">Edit Preferences</h2>
        <div class ="countries">
          <h1 class>Country of Origin:</h1>
          <div class = "selections" v-for="country in countries" :key=country.country>
            <input type="checkbox" id={{country.country}} name={{country.country}} value="country" :checked="this.selectedCountries.includes('country.country')">
            <label for={{country.country}}>{{country.country}}</label>
          </div>
        </div>

        <div class = "makes">
          <h1>Makes:</h1>
          <div class = "selections" v-for="make in makes" :key=make.make>
            <input type="checkbox" id={{make.make}} name={{make.make}} value="make" :checked="this.selectedMakes.includes('makes.make')">
            <label for={{make.make}}>{{make.make}}</label>
          </div>
        </div>

        <div class = "models">
          <h1>Models:</h1>
          <h2 style="text-align: left; font-size: 16px; padding-left: 5px;">Add Model</h2>
          
          <label for="make" />Make:
          <select v-model="makeOption" id="makeOption">
            <option v-for="make in makes" :key=make.make value='make.make'>{{make.make}}</option>
          </select>

          <label style="padding-left: 15px;" for="model" />Model:
          <select name="model" id="model">
            <option v-for="model in modelOptions" :key=model.model value=''>
            <div v-if="this.modelOptions.length > 0">Select Model First</div>
            <div v-else>{{modelOptions.model}}</div>
            </option>
          </select>

          <table>
            <tbody>
              <tr>
                <th>Make</th>
                <th>Model</th>
              </tr>
              <tr v-for="model in selectedModels" :key="model.model">
                <td>{{model.model}}</td>
                <td></td>
              </tr>
            </tbody>
          </table>
        </div> 

        <button style="font-size: 14px;" type="submit" name="submit">Submit</button>
  </div>
</template>

<script>
import { useCookies } from "vue3-cookies";
import { defineComponent } from "vue";
import {VPICApi} from '../router/PersonalizedRecommendationsConnection';
import {PersonalizedRecsApi} from '../router/PersonalizedRecommendationsConnection';


export default defineComponent({
  el: '#makeOption',
  setup() {
    const { cookies } = useCookies();
    return { cookies };
  },
  data () {
    return {
      countries: [{country: 'United States'}, {country: 'Japan'}, {country: 'Germany'}, {country: 'Italy'}, {country: 'United Kingdom'}, 
                  {country: 'China'}, {country: 'South Korea'}, {country: 'Sweden'}],
      makes: [{make: 'Acura'}, {make: 'Alfa Romeo'}, {make: 'Aston Martin'}, {make: 'Audi'}, {make: 'Bentley'}, {make: 'BMW'}, 
              {make: 'Buick'}, {make: 'Cadillac'}, {make: 'Chevrolet'}, {make: 'Chrysler'}, {make: 'Dodge'}, {make: 'Ferrari'}, 
              {make: 'FIAT'}, {make: "Ford"}, {make: "Genesis"}, {make: "GMC"}, {make: "Honda"}, {make: "Hyundai"}, {make: "INFINITI"},
              {make: 'Jaguar'}, {make: "Jeep"}, {make: "Kia"}, {make: "Lamborghini"}, {make: "Land Rover"}, {make: "Lexus"}, 
              {make: 'Lincoln'}, {make: "Lotus"}, {make: "Maserati"}, {make: "MAZDA"}, {make: "McLaren"}, {make: "Mercedes-Benz"},
              {make: 'MINI'}, {make: "Mitsubishi"}, {make: "Nissan"}, {make: "Plymouth"}, {make: "Porsche"}, {make: "RAM"}, 
              {make: 'Rolls-Royce'}, {make: "Subaru"}, {make: "Suzuki"}, {make: "Tesla"}, {make: "Toyota"}, {make: "Volkswagen"},
              {make: 'Volvo'}],
      makeOption: '',
      modelOptions: [],
      selectedCountries: [],
      selectedMakes: [],
      selectedModels: []
    }
  },
  watch: {
    modelOptions: function(makeOption) {
      this.modelOptions = GetModelsByMake(makeOption)
    }
  },
  methods: {
      GetModelsByMake: async function(make) {
        await VPICApi.get('/Vehicles/GetModelsForMake/' + make, null, {params: {format: 'json'}}).then((response)=>{
          console.log(`Server replied with: ${response.data}`);
          selectedModels = response.data.results;
        }).catch((e)=>{
          console.log(e);
          })
      },
      GetPreferences: async function() {
            await PersonalizedRecsApi.get('/Preferences/Retrieve', {params: {userId: this.$cookies.get("userId")}}).then((response)=>{
                console.log(`Server replied with: ${response.data}`),
                this.selectedCountries = response.data.followedCountries, 
                this.selectedMakes = response.data.followedMakes, 
                this.selectedModels = response.data.followedModels
            }).catch((e)=>{
                console.log(e);})
      }
  },
  created: function(){
      if (this.$cookies.get("userId") != "guest") {
        this.GetPreferences()
      }
      else {
        this.$router.push('/login')
      }
  }
})
</script>

<style scoped>
.EditPreferences
{
  display: inline-block;
  font-family: Cambria, Cochin, Georgia, Times, 'Times New Roman', serif;
}
.countries
{
  width: 750px;
  height: 100px;
  margin: 20px;
  border: solid 1px;
}
.makes
{
  width: 750px;
  height: 300px;
  margin: 20px;
  border: solid 1px;
}
.models
{
  margin: 20px;
  width: 750px;
  height: fit-content;
  border: solid 1px;
  text-align: left;
}
.selections
{
  float: left;
  padding: 10px;
}
.select
{
  width: 150px;
}
label
{
  padding-left: 5px;
  color:black;
}
h1
{
  border-bottom: solid 1px;
  font-weight: bold;
  text-decoration: underline;
  padding-top: 3px;
  padding-bottom: 2px;
  padding-left: 5px;
  background-color: lightgrey;
  font-size: 25px;
  text-align: left;
  color: darkslateblue;
}
table
{
  table-layout: fixed;
  font-size: 16px;
  border-bottom: 1px solid grey;
  width: 100%;
  margin-top: 10px;
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
button
{
  margin-top: 5px;
  text-align: center;
  background-color: rgb(0, 75, 73);
  color: white;
}
button:hover
{
  color: black;
  background-color: lightgrey;
}
</style>