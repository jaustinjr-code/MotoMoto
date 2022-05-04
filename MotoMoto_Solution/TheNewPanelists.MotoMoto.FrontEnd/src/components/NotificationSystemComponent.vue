<template>
    <div>
        <h1>Notification Center</h1>
        <h3>Registered Events</h3>
        <!-- <button>Subscribed Accounts</button> -->
        <br>
        <table id="registered-events">
            <tr>
                <th>Event Date</th>
                <th>Event</th>
            </tr>
            <tr v-for="event in tests" :key="event.id">
                <td>{{ event.first }}</td>
                <td>{{ event.second }}</td>
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
    },
    methods: { 
        fetchData() { 
            let params = {username: "user118"}; 
            instance.get('NotificationSystem/GetRegisteredEventDetails?username=' + params.username).then((res) =>{
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