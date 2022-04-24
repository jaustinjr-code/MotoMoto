<template>
    <div>
        <button @click="getCookie">Delete Account</button>
    </div>
</template>

<script>
import { useCookies } from "vue3-cookies";

export default {
    name: 'AccountDelete',
    props: {
        username: String
    },
    methods:{
        setup() {
            const { cookies } = useCookies();
            return { cookies };
        },
        getCookie(){
            const _username = this.$cookies.get("username");
            console.log(_username);
            username = _username
        }
    }
}
</script>