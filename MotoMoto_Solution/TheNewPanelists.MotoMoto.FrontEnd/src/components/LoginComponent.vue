<template>
  <div class="loginView">
    <h1>MotoMoto</h1>
    <div class="login">
        <h4>Login</h4>
        <div class="username">
            <p>Username: </p>
            <input type = "usersname" required placeholder="username" v-model = "username">
        </div>
        <div class = "password">
            <p>Password: </p>
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
<<<<<<< HEAD
//import {instance} from '../router/directMessageConnection'
=======
import {instance} from '../router/directMessageConnection'

>>>>>>> main
export default defineComponent({
  setup() {
    const { cookies } = useCookies();
    return { cookies };
  },
<<<<<<< HEAD
=======

>>>>>>> main
  data()
  {
      return{
          username: '',
          password: '',
          loginSuccessful:false
      }
  },
<<<<<<< HEAD
=======

>>>>>>> main
  methods: {
        login(){
            let params = {username: this.username, password: this.password};
            instance.get('Login/Login', {params}).then((res)=>{
                console.log(`Server replied with: ${res.data}`);
                if(res.data == true)
                {
                    this.$cookies.set("username", this.username, "1d");
                    console.log("inside the method");
                    this.$router.push({path: '/CommunityDashboard'});
                }
            }).catch((e)=>{
            console.log(e);
            });
        },
        loginClick() {
            this.login();
        }
<<<<<<< HEAD
  }
=======

  }


>>>>>>> main
})
</script>


<style scoped>
.loginView
{
    top:0px;
    position:fixed;
<<<<<<< HEAD
    width: 100%;
    height: 100%;
    margin: 0px;
}
h1{
    padding-top: 1%;
    padding-bottom: 10%;
    text-align: center;
=======
    background: rgb(102,153,204);
    width: 100%;
    height: 100%;
    margin: 0px;
    


    
}

h1{
    padding-top: 5%;
    padding-bottom: 10%;
>>>>>>> main
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
<<<<<<< HEAD
=======

>>>>>>> main
.password
{
    padding-bottom: 20px;
}
<<<<<<< HEAD
=======

>>>>>>> main
.loginButton
{
    padding-bottom: 20px;
}
<<<<<<< HEAD
=======

>>>>>>> main
button.submit
{
    float:left;
}
<<<<<<< HEAD
=======

>>>>>>> main
button.signUp
{
    float:right;
}
<<<<<<< HEAD
=======

>>>>>>> main
button
{
    border-radius: 5px;
    color: #333;
    border: 1px solid #eee;
    padding: 8px;
    margin: 30px 0 10px 0;
    text-decoration: none;
    text-align: center;
<<<<<<< HEAD
}
=======

}

>>>>>>> main
input
{
    border-radius: 5px;
    text-decoration: none;
}
<<<<<<< HEAD
=======

>>>>>>> main
p
{
    
    display: inline-block;
}
<<<<<<< HEAD
</style>
=======
</style>
>>>>>>> main
