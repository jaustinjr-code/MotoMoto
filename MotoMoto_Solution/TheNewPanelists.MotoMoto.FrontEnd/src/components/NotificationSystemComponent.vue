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
            <tr v-for="event, index in registeredEventList" :key="event">
                <td>{{ event[index].eventDate.split(" ")[0] }}</td>
                <td>{{ event[index].eventTime }}</td>
                <td>{{ event[index].eventTitle }}</td>
                <td>{{ event[index].eventStreetAddress }},<br>{{ event[index].eventCity }}, 
                    {{ event[index].eventState }} {{ event[index].eventZipCode }} {{ event[index].eventCountry }}</td>
                    <td><button @click="deleteNotification(event[index].eventID, username)">Delete</button></td>
            </tr>
            <!-- <tr v-for="name in tests" :key="name.id">
                <td>{{ name.first }}</td>
                <td>{{ name. second }}</td>
                <td>{{ name.third }}</td>
            </tr> -->
        </table>
    </div>
</template>

<script>
import {instance} from '../router/NotificationSystemConnection'
export default {
    data() { 
        return {
            registeredEventList: [],
            tests: [{first: 'naeun', second: 'yu', thrid: 'joon'}, 
                    {first: 'this', second: 'is', third: 'last'}]
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
            this.registeredEventList.push(res.data);
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