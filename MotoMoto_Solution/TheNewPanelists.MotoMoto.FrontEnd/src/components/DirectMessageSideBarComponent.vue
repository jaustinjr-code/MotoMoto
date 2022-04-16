<template>
  <div class = "sidebar">
      <div class = "search">
          <input class = "input">
          <button>search</button>
          </div>    
      <div class= "users"  v-for="item in items" :key="item" > 
            <buttons>
                {{item.username}}
            </buttons>    
      </div>

  </div>
</template>

<script>
import {instance} from '../router/directMessageConnection'
import { useCookies } from "vue3-cookies";
export default {
    setup() {
        const { cookies } = useCookies();
        return { cookies };
    },
    name: 'DirectMessageSideBar',
    data() {
        return {
        user: '',  
        items: []

    }

    },
    created()
    {
        this.user = this.$cookies.get('username');
        let params = {sender: this.user};
        instance.get('DirectMessageHistory/GetMessageHistory', {params}).then((res) =>{
            console.log(`Server replied with: ${res.data}`);
            console.log(JSON.stringify(this.items));
            for(let i = 0; i < res.data.length; i++)
            {
                this.items.push({'username' :res.data[i]});
            }
            //console.log(JSON.stringify(this.items));
        }).catch((e)=>{
            console.log(e);
         });
    },
    methods: 
    {

    }

}
</script>

<style>
template
{
    background-color: blue;
}
.sidebar
{
  min-height: 100vh;
  width: 25%;
  height:fit-content;
  overflow: hidden;
  background-color: slategray;
  
}
.users{
  background-color: #04AA6D; /* Green background */
  border: 1px solid green; /* Green border */
  color: white; /* White text */
  padding: 10px 24px; /* Some padding */
  cursor: pointer; /* Pointer/hand icon */

  display: block; /* Make the buttons appear below each other */
}
</style>