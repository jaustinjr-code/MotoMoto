<template>
  <div class = sidebar>
      <div class= users  v-for="item in items" :key="item" > 
            <buttons @click="getUserClicked(item.username)">
                {{item.username}}
            </buttons>    
      </div>
      <h1>this.user</h1>

  </div>
</template>

<script>
import {instance} from '../router/directMessageConnection'
export default {
    name: 'DirectMessageSideBar',
    data() {
        return {
        items: [{ username: 'Foo' }, { username: 'Bar' }],
        username: 'ran',
        user:'User'
        }

    },
    created()
    {
        let params = {sender: this.username};
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
        getUserClicked(username)
        {
            
        }
    }

}
</script>

<style>
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