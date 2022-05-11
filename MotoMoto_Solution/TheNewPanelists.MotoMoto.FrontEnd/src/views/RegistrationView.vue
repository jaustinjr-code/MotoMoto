<template>
<div>
    <TabBarComponent/>
    <RegistrationComponent/>
</div>
</template>


<script>
import TabBarComponent from '../components/TabBarComponent.vue'
import RegistrationComponent from '../components/RegistrationComponent.vue'

export default {
    name: 'Registration',
    components: {
        RegistrationComponent,
        TabBarComponent
    }
}
</script>