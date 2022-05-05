<template>
  <div class = "EditPreferences">
    <form>

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
          <select name="make" id="make">
            <option v-for="make in makes" :key=make.make value='make.make'>{{make.make}}</option>
          </select>

          <label style="padding-left: 15px;" for="model" />Model:
          <select name="model" id="model">
            <option v-for="model in this.GetModelsByMake()" :key=model.model value=''>Select Make First</option>
          </select>

          <table>
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
    </form>
  </div>
</template>

<script>
import { useCookies } from "vue3-cookies";
import { defineComponent } from "vue";
import {VPICApi} from '../router/PersonalizedRecommendationsConnection';
import {PersonalizedRecsApi} from '../router/PersonalizedRecommendationsConnection';


export default defineComponent({
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
      selectedCountries: [],
      selectedMakes: [],
      selectedModels: []
    }
  },
  methods: {
      GetModelsByMake: async function(make) {
        await VPICApi.get('/Vehicles/GetModelsForMake/' + make, null, {params: {format: 'json'}}).then((response)=>{
          console.log(`Server replied with: ${response.data}`);
          return(data.results);
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
  font-family: Cambria, Cochin, Georgia, Times, 'Times New Roman', serif;
}
.countries
{
  width: 450px;
  height: 150px;
  margin: 20px;
  border: solid 1px;
}
.makes
{
  width: 750px;
  height: 325px;
  margin: 20px;
  border: solid 1px;
}
.models
{
  margin: 20px;
  width: 500px;
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
  font-weight: bold;
  text-decoration: underline;
  padding-top: 8px;
  padding-bottom: 5px;
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
</style>