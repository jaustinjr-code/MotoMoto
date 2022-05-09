<template>
  <div class="RegistrationView">
    <h1>MotoMoto</h1>
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
            <button @click="RegisterClick">Create Account</button>
            <button @click="$router.push('/')">Back Home</button>
        </div>

    </div>

</template>

<script>
import { useCookies } from "vue3-cookies";
import { defineComponent } from "vue";
import {instance} from '../router/RegistrationConnection'
import { instanceSubmit } from '../router/CommunityBoardConnection.js'

export default defineComponent({
  setup() {
    const { cookies } = useCookies();
    return { cookies };
  },
  data() {
      return{
          email: '',
          password: '',
      }
  },
  methods: {
        Registration: async function(){
            await instance.post('/Registration/Register', null, {params: {email: this.email, password: this.password}}).then((response)=>{
                console.log(`Server replied with: ${response.data}`);
                if (response.status == 200) {
                    let params = JSON.stringify({ metric: 1 });
                    instanceSubmit.post('SubmitKpi/SubmitRegistrationKpiMetric', params, {
                            headers: {
                                'Content-Type': 'application/json; charset=utf-8'
                            }
                        })
                        .then(res => {
                            console.log(res);
                        })
                        .catch(err => {
                            console.log(err);
                        });
                }
                this.$router.push({path: '/Login'});
            }).catch((e)=>{
                console.log(e);
                this.$router.push({path: '/Registration'});
            });
        },
        RegisterClick() {
            this.Registration();
        }
  }
})
</script>


<style scoped>
.RegistrationView
{
    padding-top: 25px;
    scroll-behavior: smooth;
    overflow-y: scroll;
    position: fixed;
    width: 100%;
    height: 100%;
    margin: 0px;
}

h1
{
    font-family: "Copperplate", "Papyrus";
    display: inline;
    font-size: 50px;
    border-style: solid;
    border-width: .05ch;
    background-color: aliceblue;
    padding-left: 20px;
    padding-right: 20px;
}

h2
{
    padding-top: 75px;
    font-size: 45px;
    font-family: 'Times New Roman', Times, serif;
}

.Requirements
{
    font-size: 14px;
    text-align: left;
    display: inline-block;
    padding-top: 30px;
    padding-right: 10px;
    padding-left: 10px;
    padding-bottom: 3%;
    border-radius: 20px;
}

.password
{
    padding-bottom: 20px;
}

.RegisterButton
{
    padding-bottom: 20px;
}

button 
{
    white-space: nowrap;
    background-color:lightgrey;
    width: fit-content;
    border: none;
    margin: 10px 5px;
    color: black;
    text-align: center;
    text-decoration: none;
    display: inline-block;
    font-size: 16px;
    cursor: pointer;
    
}
button:hover
{
    color: red;
}

input
{
    border-radius: 5px;
    text-decoration: none;
}
</style>
