<template>
  <div class="PersonalizedRecommendationsView">
    <h1><u>Personalized Recommendations</u></h1>
    <h2><u>Register New Account</u></h2>
    
        <div class="Requirements">
            Email
            <ul>
                <li>Must use a valid email address that you own</li>
            </ul>
            Password
            <ul>
                <li>Must be a minimum of 8 characters</li>
                <li>Valid characters:</li>
                <ul>
                    <li>Blank spaces</li>
                    <li>a-z</li>
                    <li>A-Z</li>
                    <li>.,@!</li>
                </ul>
            </ul>
        </div>

        <div class = "email">
            <input type = "email" required placeholder="email" v-model = "email">
        </div>

        <div class = "password">
            <input type = "password" required placeholder="password" v-model= "password"> 
        </div>
        
        <div class = "RegisterButton">
            <button @click="RegisterClick">Register</button>
        </div>

    </div>

</template>

<script>
import { useCookies } from "vue3-cookies";
import { defineComponent } from "vue";
import {instance} from '../router/PersonalizedRecommendationsConnection'

export default defineComponent({
  setup() {
    const { cookies } = useCookies();
    return { cookies };
  },
  data()
  {
      return{
          email: '',
          password: '',
          RegistrationSuccessful:false
      }
  },

  methods: {
        Registration(){
            let params = {email: this.email, password: this.password};
            instance.get('/Api/Register', {params}).then((response)=>{
                console.log(`Server replied with: ${response.data}`);
                if(response.data == true)
                {
                    console.log("inside the method");
                    this.$router.push({path: '/Login'});
                }
                else
                {
                    this.$router.push({path: '/RegistrationView'});
                }
            }).catch((e)=>{
            console.log(e);
            });
        },
        RegisterClick() {
            this.Registration();
        }
  }
})
</script>


<style scoped>

</style>
