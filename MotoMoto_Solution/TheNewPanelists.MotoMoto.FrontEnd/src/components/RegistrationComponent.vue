<template>
<div id="registrationView">
    <h2 style="font-size: 45px;"><u>Register New Account</u></h2>
    
    <div v-if="!success">
        <div id="requirements">
            <h3>Email</h3>
            <ul>
                <li>Must use a valid email address that you own</li>
            </ul>
            <h3>Passwords</h3>
            <ul>
                <li>Must be a minimum of 8 characters</li>
                <li>Valid characters:</li>
                <ul>
                    <li>Blank spaces</li>
                    <li>A-Z</li>
                    <li>a-z</li>
                    <li>0-9</li>
                    <li>. , @ !</li>
                </ul>
            </ul>
        </div>
        
        <div id="inputsWrapper">
            <div>
                <label>Email:</label>
                <input class="input" type="email" required placeholder="Enter email" v-model="email">
            </div>
            <div>
                <label>Password:</label>
                <input class="input" type="password" required placeholder="Enter password" v-model="password">
            </div>
        </div>
    
        <button @click="RegisterClick">Create Account</button>
    </div>

    <div v-else id="message">
        {{this.message}}
    </div>

</div>
</template>

<script>
import { useCookies } from "vue3-cookies";
import { defineComponent } from "vue";
import {instance} from '../router/RegistrationConnection';

export default defineComponent({
    setup() {
        const { cookies } = useCookies();
        return { cookies };
    },
    data() {
        return{
          email: '',
            password: '',
            message: '',
            validInformation: true,
            alertMessage: '',
            success: false
        }
    },
    methods: {
        Registration: async function(){
            await instance.post('/Registration/Register', null, {params: {email: this.email, password: this.password}}).then((response)=>{
                console.log(`Server replied with: ${response.data}`);
                this.message = response.data.message;
                this.success = response.data.status;
            }).catch((e)=>{
                console.log(e);
                this.message = 'Registration Error';
            });
        },
        ValidateEmail() {
            if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(this.email))
                return (true);

            return (false);
        },
        ValidatePassword() {
            if (/^([a-zA-Z0-9.,@!\s]{8,})+$/.test(this.password))
                return (true);
            return (false);
        },
        RegisterClick() {
            this.alertMessage = '';

            if (!this.ValidateEmail()) {
                this.validInformation = false;

                if (this.email.length == 0)
                    this.alertMessage += "Please enter an email address.";
                else
                    this.alertMessage += "Invalid Email! Enter a valid email address.";
            }
            if (!this.ValidatePassword()) {
                this.validInformation = false;

                if (this.password == 0)
                    this.alertMessage += "\nPlease enter a password.";
                else
                    this.alertMessage += "\nInvalid Password! Enter a valid password matching the requirements.";
            }
            if (!this.validInformation) 
                alert(this.alertMessage);
            else
                this.Registration();
        }
    }
});
</script>

<style scoped>
#registrationView
{
    font-family: Cambria, Cochin, Georgia, Times, 'Times New Roman', serif;
    scroll-behavior: smooth;
    width: 100%;
    height: 100%;
    margin: 0px;
}
#requirements
{
    margin-top: 15px;
    font-size: 16px;
    text-align: left;
    display: inline-block;
}
h3 
{
    font-size: 18px;
    font-weight: bold;
}
button.login
{
    background-color: white;
    color: blue;
}
#inputsWrapper
{
    display: flex;
    justify-content: center;
    align-items: center;
    margin-top: 20px;
    flex-direction: column;
}
label
{
    display: inline-block;
    width: 75px;
    text-align: right;
    font-size: 16px;
}
input
{
    margin: 5px;
    font-size: 14px;
}
button 
{
    white-space: nowrap;
    background-color:green;
    width: fit-content;
    border-width: 1px;
    color: white;
    margin: 20px;
    text-align: center;
    font-size: 16px;
    cursor: pointer;
}
button:hover
{
    color: lightgreen;
}
#message
{
    font-size: 26px;
    display: inline-block;
    margin: 50px;
    padding: 125px;
}
</style>
