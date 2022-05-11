<template>
    <div id = "defaultComponent">
    </div>
</template>

<script>
import { useCookies } from "vue3-cookies";
import { defineComponent } from "vue";
import instance from '../../router/PersonalizedRecommendationsConnection'

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