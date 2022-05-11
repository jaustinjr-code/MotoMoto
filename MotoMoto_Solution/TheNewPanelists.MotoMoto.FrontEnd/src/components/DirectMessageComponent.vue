<template>
    <div class="chatBox">
        <h1 class = username>{{receiver}}</h1>
        <div class = "viewMessages" v-for="message in messages" :key="message">
            <div v-if="this.sender == message.sender" class="right">
                <span class = "message"> {{message.messages}} </span>
            </div>
            <div v-else class="left">
                <span class = "message">{{message.messages}}</span>
            </div>
        </div>
        <div class = "chat">
            <textarea class = "textArea" v-model = "message" :maxlength="1000"></textarea>
            <button class = "sendMessage" @click = "addMessage"> Send </button>

        </div>
    </div>

</template>

<script>
import { useCookies } from "vue3-cookies";
import {instance} from '../router/directMessageConnection'
export default {
    props: ['receiver'],
    setup()
    {
        const { cookies } = useCookies();
        return { cookies };
    },
    name: 'DirectMessage',
    data()
    {
        return {
            sender: '',
            messages:[],
            message: '',
            sMessage: '',
            timer: ''   
        }
    },
    watch: 
    {
        receiver:function()
        {
           this.messages = [];
           this.getMessages();
        }
    },
    created()
    {
        this.getMessages();
        this.timer = setInterval(this.getMessages, 5000);
    },
    methods:
    {
        getMessages()
        {
            console.log("DM receiver: " + this.receiver);
            this.sender = this.$cookies.get('username');
            let params = {sender: this.sender, receiver: this.receiver};
            instance.get('DirectMessage/GetMessage', {params}).then((res) => {
            console.log(`Server replied with: ${res.data}`);
            
            for(let i = 0; i<res.data.length; i++)
            {

                if(!this.messages.some(data => data.time === res.data[i][2]))
                {

                    this.messages.push({'sender' :res.data[i][0], 'messages' :res.data[i][1], 'time' :res.data[i][2]});
                }
            }
            
            }).catch((e)=>{
                console.log(e);
            });   
        },
        addMessage()
        {

            let params = {sender: this.$cookies.get('username'), receiver: this.receiver, message: this.message};
            //let params = {sender: this.$cookies.get('username'), receiver:this.receiver, message:this.sMessage}
            instance.put('DirectMessage/SendMessage', params).then((res) => {
                console.log(`Server replied with: ${res.data}`);
                this.getMessages();

            }).catch((e)=>{
                console.log(e);
            });   
        }
    },
    beforeUnmount()
    {
        clearInterval(this.timer);
    }
}
</script>

<style>
.username
{
    width: 100vh;
}
.chatBox
{
    width:100vh;
    padding-left: 10px;
    padding-bottom: 10px;
}
.chat
{
    width: 100vh;
    bottom:0;
    position:absolute;

}

.left
{
    text-align: left;
    color:white;

}
.left .message
{
    background:rgb(0, 75, 73);
}
.right
{
    text-align: right;
    color:black;

}
.message
{
    padding-left: 5px;
    padding-right: 5px;
    padding-top: 5px;
    padding-bottom: 5px;
    text-align: center;
    border:1px solid rgb(0, 75, 73);;
    border-radius:10px;
    max-width: 40vh;
    display:inline-block;
}
.textArea
{
    width: 90vh;

}
</style>