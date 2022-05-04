<template>
    <div>
        <h1>Notification Center</h1>
        <select name="notificationFeeds" id="notificationType" @change="fetchData">
            <option value="Upcoming Events">Upcoming Events</option>
            <option value="All Registered Events">All Registered Events</option>
        </select>
        <br>
        <div>
            <h1>{{ notificationType }}</h1>
        </div>
        <table id="registered-events">
            <tr>
                <th>Event Date</th>
                <th>Event Time</th>
                <th>Event</th>
                <th>Event Location</th>
            </tr>
            <tr v-for="event in registeredEventList" :key="event.eventID">
                <td>{{ event.eventDate.split(" ")[0] }}</td>
                <td>{{ event.eventTime }}</td>
                <td>{{ event.eventTitle }}</td>
                <td>{{ event.eventStreetAddress }},<br>{{ event.eventCity }}, 
                    {{ event.eventState }} {{ event.eventZipCode }} {{ event.eventCountry }}</td>
                    <button @click="deleteNotification(event[index].eventID, username)">Delete</button>
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
        // this.deleteNotification(eventID);
    },
    methods: { 
        fetchData() { 
            let params = {username: "ran"}; 
            instance.get('NotificationSystem/GetNotification?username=' + params.username).then((res) =>{
                //console.log(res.data.length); 
            // for(let i = 0; i < res.data.length; i++)
            // {
            //     this.registeredEventList.push({'eventTime' :res.data[i][0], 'eventDate' :res.data[i][1],
            //         'eventStreetAddress' :res.data[i][2], 'eventCity' :res.data[i][3],
            //         'eventState' :res.data[i][4], 'eventCountry' :res.data[i][5],
            //         'eventZipCode' :res.data[i][6], 'eventZipCode' :res.data[i][7]})
                // console.log(res.data.length);
                // if(!this.notes.some(data => data.title === res.data[i]))
                // {
                //     this.notes.push({'title' :res.data[i][0], 'note':res.data[i][1]});
                // }
            //}
            console.log(res);
            console.log(res.data);
            // res.data.array.forEach(event => {
            //     this.registeredEventList.push(event);
            // });
            this.registeredEventList = res.data;
            //registeredEventList = [];
            //this.registeredEventList.push(res.data);
            console.log(this.registeredEventList);
            }).catch((e)=>{
                console.log(e);
            });
        },
        deleteNotification(eventID, username) {
            let params = JSON.stringify({id: eventID, name: username})
            instance.post('NotificationSystem/DeleteNotification?id=', params).then((res) =>{
                console.log(res);
                this.registeredEventList.splice(this.registeredEventList.indexOf(eventID - 1), 1);

            }) 
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