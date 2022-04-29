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
            <button @click="RegisterClick">Register</button>
        </div>

    </div>

</template>

<script>
/*import { useCookies } from "vue3-cookies";*/
import { defineComponent } from "vue";
import {instance} from '../router/RegistrationConnection'

export default defineComponent({
  /*setup() {
    const { cookies } = useCookies();
    return { cookies };
  },*/

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
.RegistrationView
{
    padding-top: 25px;
    scroll-behavior: smooth;
    overflow-y: scroll;
    position:fixed;
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

button.submit
{
    float:left;
}

button
{
    border-radius: 5px;
    color: #333;
    border: 1px solid #eee;
    padding: 8px;
    margin: 30px 0 10px 0;
    text-decoration: none;
    text-align: center;

}

input
{
    border-radius: 5px;
    text-decoration: none;
}
</style>
