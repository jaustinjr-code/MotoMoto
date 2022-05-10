<template>
  <button v-on:click="Logout()">Logout</button>
</template>

<script>
import { useCookies } from "vue3-cookies";

export default {
    setup() {
        const { cookies } = useCookies();
        return { cookies };
    },
    methods:
    {
        Logout()
        {
            this.$cookies.remove("username");
            this.$cookies.remove("userId");
            this.$router.push({path: '/'});
            window.location.reload();
        }
    }
}
</script>

<style scoped>
button{
    align-content: right;
    border-radius: 5px;
    margin-left: 5px;
    margin-top: 5px; 
    margin-bottom: 5px; 
    background-color: #555;
    color: white;
    border: none;
}
</style>