<template>
  <div class = "sidebar">
      <button class = "messageRequest" @click="() => TogglePopup('buttonTrigger')">Message Request</button>
      <div class = "search">
          <input class = "input"  v-model = "newChatUser">
          <button @click = "createNewChat">search</button>
          </div>    
      <div class= "users"  v-for="item in items" :key="item" > 
            <buttons @click = "getUserClicked(item.username)">
                {{item.username}}
            </buttons>    
      </div>
  </div>    
    <Popup v-if="popupTrigger.buttonTrigger" :TogglePopup="() => TogglePopup('buttonTrigger')">
        <h2>My popup</h2>
        <div class="requestOption" v-for="request in requestList" :key="request">
            {{request}}
            <button @click="acceptRequest(request)">Accept</button>
            <button @click="declineRequest(request)">Decline</button>
        </div>
    </Popup>
</template>

<script>
import {instance} from '../router/directMessageConnection'
import { useCookies } from "vue3-cookies";
import Popup from '../components/PopupComponent.vue'
import {ref} from 'vue'

export default {
    setup() {
        const { cookies } = useCookies();
        
        const popupTrigger = ref({
            buttonTrigger: false
        });
		const TogglePopup = (trigger) => {
			popupTrigger.value[trigger] = !popupTrigger.value[trigger]
		}        
        return { cookies, Popup, popupTrigger,TogglePopup };
    },
    name: 'DirectMessageSideBar',
    data() {
        return {
            user: '',  
            items: [],
            timer: '',
            requestList: []
        }
    
    },
    components:
    {
        Popup
    },
    created()
    {
        this.getMessageHistory();
        this.timer = setInterval(this.getMessageHistory, 5000);
        this.getRequestList();

    },
    methods:
    {
        getMessageHistory()
        {
            this.user = this.$cookies.get("username")
            let params = {sender: this.user};
            instance.get('DirectMessageHistory/GetMessageHistory', {params}).then((res) =>{
            console.log(`Server replied with: ${res.data}`);
            for(let i = 0; i < res.data.length; i++)
            {
                if(!this.items.some(data => data.username === res.data[i]))
                {
                    this.items.push({'username' :res.data[i]});
                }
            }
            //console.log(JSON.stringify(this.items));
            }).catch((e)=>{
                console.log(e);
            });
        },
        getUserClicked(receiverUsername)
        {
            this.$emit('receiver', receiverUsername);
        },
        createNewChat()
        {
            console.log("This.newchat user" + this.newChatUser);
            let params = {sender: this.$cookies.get('username'), receiver: this.newChatUser};
            instance.put('DirectMessageHistory/CreateNewChat', params).then((res) => {
                console.log(`Server replied with: ${res.data}`);
                this.getMessageHistory();

            }).catch((e)=>{
                console.log(e);
            });   
        },
        getRequestList()
        {
            this.user = this.$cookies.get("username")
            let params = {currentUser: this.user};
            instance.get('MessageRequest/GetRequest', {params}).then((res) =>{
            console.log(`Server replied with: ${res.data}`);
            for(let i = 0; i < res.data.length; i++)
            {
                if(!this.requestList.some(data => data.time === res.data[i]))
                {
                    this.requestList.push(res.data[i]);
                }
            }
            }).catch((e)=>{
                console.log(e);
            });
        },
        acceptRequest(sender)
        {
            let params = {sender: sender, receiver: this.$cookies.get("username")};
            instance.put('MessageRequest/AcceptRequest', params).then((res) => {
                console.log(`Server replied with: ${res.data}`);
                const filtersList = this.requestList.filter(element => element !== sender)
                this.requestList=filtersList;

            }).catch((e)=>{
                console.log(e);
            });   
        },
        declineRequest(sender)
        {
           let params = {sender: sender, receiver: this.$cookies.get("username")};
           instance.delete('MessageRequest/DeclineRequest', {params}).then((res) => {
                console.log(`Server replied with: ${res.data}`);
                const filtersList = this.requestList.filter(element => element !== sender)
                this.requestList=filtersList;
            }).catch((e)=>{
                console.log(e);
            });              
        }
    },
    beforeDestroy()
    {
        clearInterval(this.timer);
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