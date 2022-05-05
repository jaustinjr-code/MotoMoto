<template>
    <div class = 'button'>
        <button :disabled="disableSave" @click="save" v-if = !isNewNote > save </button>
        <button :disabled="clickedAddNote" @click ="addNote" v-if= isNewNote> addNote </button>
        <button :disabled="clickedDelete"  @click="deleteNote" v-if = !isNewNote>delete</button>
        <button @click="closeNotes"> close </button>
    </div>

  <div class = 'title'>
    <input class = "disabled" :value= noteTitle v-if = !isNewNote placeholder="Title" disabled>
    <input :maxlength="maxLengthTitle" v-model = "titleText" v-if = isNewNote placeholder="Please Enter New Note Title">
  </div>
  

  <div class = 'note'>
      <textarea :maxlength="maxLengthNote" v-model = "noteText" v-if = !isNewNote placeholder="Write Your Note Here"></textarea>
  </div>
</template>

<script>
import {instance} from '../router/noteDashboardConnection'

export default {
    props:['noteTitle', 'isNewNote', 'noteArea'],
    data()
    {
        return{
            noteText: "",
            user: "user1",
            clickedDelete: false,
            clickedAddNote: false,
            disableSave: false,
            maxLengthNote:5000,
            maxLengthTitle: 30
        }
    },
    methods:
    {
        closeNotes()
        {
            this.$emit("NoteClose", true);
        },
        save()
        {
            var user  = "user1"
            let params = {username: user, title: this.noteTitle, notes: this.noteText}
            instance.get('NoteUpdate/UpdateNote', {params}).then((res) => {
                console.log(`Server replied with: ${res.data}`);

            }).catch((e)=>{
                console.log(e);
            });   
        },
        deleteNote()
        {
            this.clickedDelete = true;
            this.disableSave = true;
            let params = {username: "user1", title: this.noteTitle};
            instance.get('NoteDelete/DeleteNote', {params}).then((res) => {
                console.log(`Server replied with: ${res.data}`);

            }).catch((e)=>{
                console.log(e);
            });
        },
        addNote()
        {
            this.clickedAddNote = true;
            //let test = this.user;
            //let test2 = this.noteText;
            if(this.titleText != '' || this.titleText != null)
            {
                let params = {username: "user1", notes: this.titleText};
                instance.get('Note/AddNotes', {params}).then((res) => {
                    console.log(`Server replied with: ${res.data}`);

                }).catch((e)=>{
                    console.log(e);
                });
            }
            else{
                console.log("Empty Title");
            }

        }
    },
    mounted()
    {
        console.log(this.isNewNote);
        console.log(this.noteTitle);
        console.log("NoteArea" + this.noteArea);
        this.noteText = this.noteArea;
    } 

    

}
</script>

<style scoped>
.button
{
    float: right;
    margin-bottom: 10px;
}
.title input
{
    margin: 0px;
    width: 100%;

}
.note
{
    display: flex; 
    flex-flow: column;
    height: 70vh;
}
.note textarea
{
    margin: 0;
    width: 100%; 
    resize: none;
    height: 100%;
    bottom: 0;
    font-family: Comic Sans MS, Comic Sans, cursive;
    font-size: 150%;
    border-radius: 10px;
}
.title input{
    text-align: center;
    font-size: 200%;
    font-weight: bold;
    margin-bottom: 20px;
}
input
{
    border-radius: 10px;
    font-family: Comic Sans MS, Comic Sans, cursive;
    
}
.disabled
{
    border: none;
    background-color: transparent;
    outline: none;
}
</style>