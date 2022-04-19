<template>
  <button @click = "directMessage"> Direct Message </button>
  <LogoutComponent/>
</template>

<script>
import LogoutComponent from "../components/LogoutComponent.vue";

export default {
    components:
    {
        LogoutComponent
    },
    methods:
    {
        directMessage()
        {
            this.$router.push({path: '/DM'});
        }
    }

}
</script>

<style>

</style>