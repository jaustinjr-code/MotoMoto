<template>
    <div class = logout>
        <button @click="goHome"> Home </button>
        <LogoutComponent/>
    </div>
    <div class = title>
        <h1> Note Dashboard </h1>
    </div>
    <div class = writtenNotes>
        <NoteDashboard v-if = !noteView @success = "noteClicked" @newNote = "newNote"></NoteDashboard>
        <NoteComponent v-if = noteView v-bind:isNewNote = "isNewNote" v-bind:noteTitle = "noteTitle" v-bind:noteArea = "noteArea"  @NoteClose = "closeNotes"></NoteComponent>
    </div>
    <kpi-submission-component viewTitle="Note Dashboard"></kpi-submission-component>
</template>

<script>
import NoteDashboard from "../components/NoteDashboardComponent.vue"
import NoteComponent from "../components/NoteComponent.vue"
import LogoutComponent from "@/components/LogoutComponent.vue"
import KpiSubmissionComponent from "../components/KpiSubmissionComponent.vue"
export default {
    nome: 'NoteDashboardView',
    components: {NoteDashboard, NoteComponent, KpiSubmissionComponent, LogoutComponent},
    data()
    {
        return{
            noteView: false,
            noteTitle: "",
            isNewNote: false,
            noteArea: "",
        }
    },
    methods:
    {
        noteClicked({clicked,title, noteArea})
        {
            this.noteView = clicked;
            this.noteTitle = title;
            this.noteArea = noteArea;
            console.log("parent clicked " + noteArea);
        },
        newNote(clicked)
        {
            this.noteView = clicked;
            this.isNewNote = true;
            this.noteTitle ="";
            this.noteArea = "";
        },
        closeNotes(isClosed)
        {
            this.noteView = !isClosed;
            this.isNewNote = false;
            this.noteTitle = "";
            this.noteArea = "";
        },
        goHome()
        {
            this.$router.push("/");
        }
    }

}
</script>

<style scoped>
.logout
{
  display: inline;
  float: right;
  
}
button
{
    border-radius: 5px;
    margin-left: 5px;
    margin-top: 5px; 
    margin-bottom: 5px; 
    background-color: #555;
    color: white;
    border: none;
}
</style>