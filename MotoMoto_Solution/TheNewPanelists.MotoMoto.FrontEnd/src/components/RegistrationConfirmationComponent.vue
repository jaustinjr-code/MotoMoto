<template>
    <div class="RegistrationView">
        <h1>MotoMoto</h1>
        {{this.message}}
        <div v-if="success">
        <button @click="this.$router.push('login')">Go to Login</button>
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
            message: '',
            success: false
        }
    },
    methods: {
        Confirmation: async function(){
            await instance.post('/Registration/Confirmation', null, 
            {params: {email: this.$route.params.email , registrationId: this.$route.params.registrationId}}).then((response)=>{
                console.log(`Server replied with: ${response.data}`);
                this.message = response.data.message;
                if(response.data.status == true)
                    this.success = true;
            }).catch((e)=>{
                console.log(e);
                this.message = 'Confirmation Error'
                });
        },
    },
    created: function() {
        this.Confirmation();
    }
})
</script>