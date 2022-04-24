<template>
    <div class = "Search">
        <input type="text" v-model="search" placeholder="Zip Code Search">
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