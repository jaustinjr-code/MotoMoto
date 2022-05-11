<template>
  <div class="loginView">
    <TabBarComponent/>
    <div class="login">
        <h4 class="LoginString">Login</h4>
        <div class="username">
            <p class="unString">Username:    </p>
            <input type = "usersname" required placeholder="username" v-model = "username">
        </div>
        <div class = "password">
            <p class="pwString">Password:    </p>
            <input type = "password" required placeholder="password" v-model= "password"> 
        </div>
        <div class = "loginButton">
            <button class = "submit" @click="loginClick">Submit</button>
        </div>
    </div>
  </div>
</template>

<script>
import { useCookies } from "vue3-cookies";
import { defineComponent } from "vue";
import {instance} from '../router/directMessageConnection'
import { instanceSubmit } from '../router/CommunityBoardConnection.js'
import TabBarComponent from "../components/TabBarComponent.vue";

export default defineComponent({
    setup() {
        const { cookies } = useCookies();
        return { cookies };
    },
    components: {
        TabBarComponent
    },
    data()
    {
        return{
            username: '',
            password: '',
            loginSuccessful:false
        }
    },
    methods: {
        login() {
            let params = {username: this.username, password: this.password};
            instance.get('Login/Login', {params}).then((res)=>{
                console.log(`Server replied with: ${res.data}`);
                if(res.data == true)
                {
                    this.$cookies.set("username", this.username, "1d");
                    console.log("inside the method");

                    if(res.status == 200) {
                        let params = JSON.stringify({ metric: 1 })
                        instanceSubmit.post('SubmitKpi/SubmitLoginKpiMetric', params, {
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
                    //this.$router.push({path: '/CommunityDashboard'});
                    this.$router.push({path: '/'});
                }
            }).catch((e)=>{
                console.log(e);
            });
        },
        loginClick() {
            this.login();
        }
    }
})
</script>


<style scoped>
.loginView
{
    top:0px;
    position:fixed;
    width: 100%;
    height: 100%;
    margin: 0px;   
}

h1{
    padding-top: 5%;
    padding-bottom: 10%;
    font-family: "Copperplate", "Papyrus";
}
.login
{
    border-style: solid;
    display: inline-block;
    padding-right: 10px;
    padding-left: 10px;
    padding-bottom: 3%;
    border-radius: 20px;
}
.LoginString {
    padding-top: 3%;
    padding-bottom: 3%;
}
.password
{
    padding-bottom: 20px;
}

.loginButton
{
    padding-bottom: 20px;
}
button.signUp
{
    float:right;
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
unString 
{
    padding-right: 3%;
}
pwString
{
    padding-right: 3%;
}
p
{
    
    display: inline-block;
}
</style>
