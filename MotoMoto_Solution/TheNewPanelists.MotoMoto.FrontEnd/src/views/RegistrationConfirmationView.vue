<template>
<div>
    <TabBarComponent/>
    <RegistrationConfirmationComponent/>
</div>
</template>

<script>
    import TabBarComponent from '../components/TabBarComponent.vue'
    import RegistrationConfirmationComponent from '../components/RegistrationConfirmationComponent.vue'
    
    export default {
        name: 'RegistrationConfirmation',
        components: 
        {
            RegistrationConfirmationComponent,
            TabBarComponent
        }
    }
</script>