<template>
    <div class="RegistrationView">
        <h1>MotoMoto</h1>
        {{this.message}}
        <div v-if="success">
        <button @click="this.$router.push('/login')">Go to Login</button>
        </div>
    </div>
</template>

<script>
import { useCookies } from "vue3-cookies";
import { defineComponent } from "vue";
import { instance } from '../router/RegistrationConnection'

export default defineComponent({
    setup() {
        const { cookies } = useCookies();
        return { cookies };
    },
    data() {
        return{
            email: '',
            registrationId: '',
            message: '',
            success: false
        }
    },
    methods: {
        Confirmation: async function(){
            await instance.post('/Registration/Confirmation', null, 
            {params: {email: this.email , registrationId: this.registrationId}}).then((response)=>{
                console.log(`Server replied with: ${response.data}`);
                this.message = response.data.message;
                if(response.data.status == true)
                    this.success = true;
            }).catch((e)=>{
                console.log(e);
                this.message = e
                });
        },
    },
    created: function() {
        this.email = this.$route.query.email;
        this.registrationId = this.$route.query.registrationID;
        this.Confirmation();
    }
})
</script>