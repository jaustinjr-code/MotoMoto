<template>
  <div class="loginView">
    <TabBarComponent/>
    <div class="login">
        <h4 class="LoginString">Login</h4>
        <div class="username">
            <p class="unString">Username:</p>
            <input class="unInput" type = "usersname" required placeholder="username" v-model = "username">
        </div>
        <div class = "password">
            <p class="pwString">Password:</p>
            <input class="pwInput" type = "password" required placeholder="password" v-model= "password">
        </div>
        <span class = "loginRegisterButton">
            <button class="submit" v-on:click="loginClick">Submit</button>
            <button class="registration" v-on:click = "goToRegistration"> Register </button>
        </span>
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
                    this.$router.push({path: '/'});
                }
            }).catch((e)=>{
                console.log(e);
            });
        },
        loginClick() {
            this.login();
        },
        goToRegistration(){
            this.$router.push('/Registration')
        },
    }
})
</script>

<style scoped>
.loginView {
    top:0px;
    position:fixed;
    width: 100%;
    height: 100%;
    margin: 0px;   
}
h1 {
    padding-top: 5%;
    padding-bottom: 10%;
    font-family: "Copperplate", "Papyrus";
}
.login {
    border-style: solid;
    display: inline-block;
    padding-right: 10px;
    padding-left: 10px;
    padding-bottom: 3%;
    border-radius: 20px;
}
.LoginString {
    color: rgb(0, 75, 73);
	text-align: center;
    font-size: 25px;
	font-family: "Copperplate";
    padding-top: 3%;
    padding-bottom: 3%;
}
.password {
    padding-bottom: 1px;
}
.loginButton {
    padding-bottom: 10px;
}
button.signUp {
    color: #333;
    border: 1px solid #eee;
    padding: 8px;
    margin: 20px 0 10px 0;
}
.unInput {
    font-size: 15px;
    margin-left: 10px;
}
.pwInput {
    font-size: 15px;
    margin-left: 10px;
}
button {
    border-radius: 5px;
    color: #333;
    border: 1px solid #eee;
    padding: 8px;
    margin: 25px 0 10px 0;
    text-decoration: none;
    text-align: center;
}
input {
    border-radius: 5px;
    text-decoration: none;
}
unString {
    padding-right: 3%;
    font-size: 15px;
}
pwString {
    padding-right: 3%;
    font-size: 15px;
}
#inputField {
    font-size: 15px;
}
p {
    display: inline-block;
}
.submit {
    margin-right: 10px;
    font-size: 15px;
}
.registration {
    margin-left: 10px;
    font-size: 15px;
}
</style>