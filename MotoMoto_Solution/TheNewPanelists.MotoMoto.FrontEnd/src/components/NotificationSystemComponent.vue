<template>
    <div>
        <div class = "popUp" v-if="this.$cookies.get('username') == 'guest' || this.$cookies.get('username') == null"> 
                {{ popUpWindow() }} 
                <h2 style="popUp">Please login before accessing the Notification Center.</h2>
        </div>
        <div v-else>
            <h1>Notification Center</h1>
            <h2>Upcoming Events</h2>
            <br />
            <p>current user: {{this.$cookies.get("username")}}</p>
            <br><br>
            <select name="notifications" id="notificationType" @change="fetchNotifications($value)">
                <option value="Upcoming Events">Upcoming Events</option>
                <option value="All Events">All Registered Events</option>
            </select>
            <br><br>
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
                </tr> 
            </table>
        </div>
    </div>
</template>

<script>
import {instance} from '../router/NotificationSystemConnection'
import { useCookies } from "vue3-cookies"

export default {
    setup() {
        const { cookies } = useCookies();
        return { cookies };
    },
    data() { 
        return {
            registeredEventList: []
        }
    },
    mounted() { 
        this.fetchNotifications(); 
    },
    methods: { 
        fetchNotifications() { 
            let params = {
                username: this.$cookies.get("username"),
                notificationType: notificationType.value
            }
            console.log(params)
            instance.post('/NotificationSystem/GetRegisteredEventDetails', params, {
                headers: {
                    'Content-Type': 'application/json'
                }
            }).then((res) =>{
   
            console.log(res);
            console.log(res.data);
       
            this.registeredEventList = res.data;
     
            console.log(this.registeredEventList);
            }).catch((e)=>{
                console.log(e);
            });
        },
        popUpWindow() {
            window.alert("Login in to access the notification center.");
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


<style scope>

.Search
{
    padding-top: 100px;
    padding-bottom: 100px;
    width: 100%;
    overflow: hidden;
}
.popUp
{
    color: red;
}
</style>