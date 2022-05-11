<template>
    <div>
        <button :disabled="noteCapacityExceeded" @click="newNote">New Note</button>
        <select v-model="selected" @change="getUpdateNotes()">
            <option disabled value="">Please select one</option>
            <option>By Date: Ascending Order</option>
            <option>By Date: Descending Order</option>
            <option>Alphabetical: Ascending Order</option>
            <option>Alphabetical: Descending Order</option>
        </select>
    </div>
    <div class="userNotes" ref="noteList" v-for="item in notes" :key = "item">
      <button @click="notesClicked(item.title, item.note)">{{item.title}}</button>
    </div>
      
</template>

<script>
import {instance} from '../router/noteDashboardConnection'
import { useCookies } from "vue3-cookies";
export default {
    setup() {
    const { cookies } = useCookies();
    return { cookies };
    },
    data()
    {
        return{
            notes: [],
            user: "",
            noteCapacityExceeded:false
        }
    },
    methods: 
    {
        notesClicked(noteTitle, noteM)
        {
            console.log("clicked Note area: " + noteM);
            this.$emit('success', {clicked: true, title: noteTitle, noteArea: noteM})
        },
        newNote()
        {
            this.$emit('newNote', true)
        },
        getNotes()
        {
            //this.user = "user1"
            let option = "";
            if(typeof this.selected === 'undefined')
            {
                option = "none";
            }
            else{
                option = this.selected;
            }
            let params = {username: this.$cookies.get("username"), option: option};
            instance.get('NoteDashboard/GetNotes', {params}).then((res) =>{
            console.log(`Server replied with: ${res.data}`);
            for(let i = 0; i < res.data.length; i++)
            {
                if(res.data.length >= 100)
                {
                    this.noteCapacityExceeded = true;
                }
                if(!this.notes.some(data => data.title === res.data[i][0]))
               {
                   
                    this.notes.push({'title' :res.data[i][0], 'note':res.data[i][1]});
                }
            }
            }).catch((e)=>{
                console.log(e);
            });   
        },
        getUpdateNotes()
        {
            this.notes = [];
            this.getNotes();
        }

    },
    beforeMount()
    {
        this.getNotes();
    },

}
</script>

<style scoped>
.userNotes
{
    display:grid;
    grid-auto-rows: 1fr 1fr 1fr;
    grid-template-columns: 1fr 1fr 1fr;

}
.userNotes button
{
    border-radius: 10px;
    margin-top: 10px;
    margin-bottom: 10px;
    margin-left: 5px;
    margin-right: 5px;
    font-size: 20px;
    padding: 10px;
    text-align: center;
    font-family: Comic Sans MS, Comic Sans, cursive;
    background: whitesmoke
}
</style>