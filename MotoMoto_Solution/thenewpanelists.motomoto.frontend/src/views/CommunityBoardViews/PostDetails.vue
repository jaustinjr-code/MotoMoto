<template>
    <div>
        <TabBarComponent/>
        <PostExpandedComponent />
    </div>
</template>

<script>
import PostExpandedComponent from '../../components/CommunityBoardComponents/PostComponents/PostExpandedComponent.vue'
import TabBarComponent from '../../components/TabBarComponent.vue'

export default ({
    components: { 
        PostExpandedComponent,
        TabBarComponent
    },
})
</script>
