<template>
  <button @click = "Logout">Logout</button>
</template>

<script>
import { useCookies } from "vue3-cookies";
<<<<<<< HEAD
=======

>>>>>>> main
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
            this.$router.push({path: '/'});
        }
    }
}
</script>

<style>
button{
    align-content: right;
}
</style>