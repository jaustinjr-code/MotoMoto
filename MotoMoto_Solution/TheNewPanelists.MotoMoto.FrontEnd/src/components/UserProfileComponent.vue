<template>
    <div class="EditPreferencesView">

        <div class = "EditButton">
            <button @click="EditPreferencesClick">Edit Preferences</button>
        </div>
    </div>
</template>

<script>
/*import { useCookies } from "vue3-cookies";*/
import { defineComponent } from "vue";

export default defineComponent({
  /*setup() {
    const { cookies } = useCookies();
    return { cookies };
  },*/

  methods: {
        EditPreferencesClick() {
            this.$router.push({path: '/EditPreferences'});
        }
  }
})
</script>


<style scoped>

</style>
