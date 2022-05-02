<template>
    <div>
        <TabBarComponent/>
        <PostCreationComponent/>
    </div>
</template>

<script>
import PostCreationComponent from '../../components/CommunityBoardComponents/PostComponents/PostCreationComponent.vue'
import TabBarComponent from '../../components/TabBarComponent.vue'

export default {
    components: { 
        PostCreationComponent,
        TabBarComponent
    },
}
</script>