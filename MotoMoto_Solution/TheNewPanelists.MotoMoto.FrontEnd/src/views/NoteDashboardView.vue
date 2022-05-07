<template>
    <div class = title>
        <h1> Note Dashboard </h1>
    </div>
    <div class = writtenNotes>
        <NoteDashboard v-if = !noteView @success = "noteClicked" @newNote = "newNote"></NoteDashboard>
        <NoteComponent v-if = noteView v-bind:isNewNote = "isNewNote" v-bind:noteTitle = "noteTitle" v-bind:noteArea = "noteArea"  @NoteClose = "closeNotes"></NoteComponent>
    </div>
</template>

<script>
import NoteDashboard from "../components/NoteDashboardComponent.vue"
import NoteComponent from "../components/NoteComponent.vue"
export default {
    nome: 'NoteDashboardView',
    components: {NoteDashboard, NoteComponent},
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
        }    
    }

}
</script>

<style>

</style>