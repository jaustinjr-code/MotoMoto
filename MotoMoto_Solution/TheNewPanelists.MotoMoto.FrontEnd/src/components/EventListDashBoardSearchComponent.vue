<template>
    <div class = "Search">
        <input type="text" v-model="search" placeholder="Search">
    </div>
</template>

<script>
export default {
    data(){
        return{
            events: [],
            search: ''
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