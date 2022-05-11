<template>
    <div class = "modelComponent">
    </div>
</template>

<script>
import { useCookies } from "vue3-cookies";
import { defineComponent } from "vue";

export default defineComponent ({
        setup() {
            const { cookies } = useCookies();
            return { cookies };
        },
        data () {
            return {
            }
        },
})
</script>