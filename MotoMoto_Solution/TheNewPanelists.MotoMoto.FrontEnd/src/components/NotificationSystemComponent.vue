<template>
    <div>
        <h1>Notification Center</h1>
        <h3>Registered Events</h3>
        <h1>{{notification}}</h1>
        <!-- <button>Subscribed Accounts</button> -->
        <br>
        <table id="registered-events">
            <tr>
                <th>Event Date</th>
                <th>Event</th>
            </tr>
            <tr v-for="event in registeredEventList" :key="event.eventID">
                <td>{{ event.eventDate}}</td>
                <td @click="ExpandEventDetail(event.eventID)">{{ event}}</td>

            </tr>
        </table>
    </div>
</template>

<script>
import {instance} from '../router/NotificationSystemConnection'

export default {
    data() { 
        return {
            registeredEventList: []
        }
    },
    mounted() { 
        this.fetchData(); 
    },
    methods: { 
        fetchData() { 
            let params = {username: "user118"}; 
            instance.get('NotificationSystem/GetRegisteredEventDetails?username=' + params.username).then((res) =>{
            for(let i = 0; i < res.data.length; i++)
            {
                this.registeredEventList.push({'eventTime' :res.data[i][0], 'eventDate' :res.data[i][1],
                    'eventStreetAddress' :res.data[i][2], 'eventCity' :res.data[i][3],
                    'eventState' :res.data[i][4], 'eventCountry' :res.data[i][5],
                    'eventZipCode' :res.data[i][6], 'eventZipCode' :res.data[i][7]})
                // console.log(res.data.length);
                // if(!this.notes.some(data => data.title === res.data[i]))
                // {

                //     this.notes.push({'title' :res.data[i][0], 'note':res.data[i][1]});
                // }
            }
            }).catch((e)=>{
                console.log(e);
            });
        }
    }
}   
</script>

<style>

.Search
{
    padding-top: 100px;
    padding-bottom: 100px;
    width: 100%;
    overflow: hidden;
}
</style>