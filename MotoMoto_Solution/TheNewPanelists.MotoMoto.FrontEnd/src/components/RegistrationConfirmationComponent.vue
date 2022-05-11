<template>
    <div id="confirmationview">
    <h1 class="response">{{this.message}}</h1>
        <div class="content">
            <div class="success" v-if="success">
                <h2 style="font-size: 20px;">You can change your username or password under the user profile section.</h2>
       
                <div style="margin-top: 40px;">
                    <router-link to="/Login"><a class="login">Login</a></router-link>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import { useCookies } from "vue3-cookies";
import { defineComponent } from "vue";
import { instance } from '../router/RegistrationConnection';

export default defineComponent({
    setup() {
        const { cookies } = useCookies();
        return { cookies };
    },
    data() {
        return{
            email: '',
            registrationId: '',
            message: '',
            success: false
        }
    },
    methods: {
        Confirmation: async function(){
            await instance.post('/Registration/Confirmation', null, 
            {params: {email: this.email , registrationId: this.registrationId}}).then((response)=>{
                console.log(`Server replied with: ${response.data}`);
                this.message = response.data.message;
                if(response.data.status == true) {
                    this.success = true;
                }
            }).catch((e)=>{
                console.log(e);
                this.message = e
                });
        },
    },
    created: function() {

        this.email = this.$route.query.email;
        this.registrationId = this.$route.query.registrationID;

        if((this.email === undefined) || (this.registrationId === undefined))
        {
            alert("Missing confirmation details. Redirecting to the home page.");
            this.$router.push('/');
        }
        else    
            this.Confirmation();
    }
})
</script>

<style scoped>
#confirmationview
{
    width: 100%;
    height: 100%;
    margin: 0px;
    padding: 20px;
}
.content
{
    font-family: Cambria, Cochin, Georgia, Times, 'Times New Roman', serif;
    display: inline-block;
    width: 500px;
}
h1.response 
{
    margin-top: 5px;
    font-family: Cambria, Cochin, Georgia, Times, 'Times New Roman', serif;
    font-size: 24px;
}
a.login 
{
    white-space: nowrap;
    background-color:green;
    width: fit-content;
    border-width: 1px;
    color: white;
    text-align: center;
    font-size: 16px;
    cursor: pointer;
    padding: 5px;
}
a.login:hover
{
    color: lightgreen;
}
</style>