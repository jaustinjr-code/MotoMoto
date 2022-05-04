<template>
    <div>
        <TabBarComponent/>
        <PostExpandedComponent :id='id' />
    </div>
</template>

<script>
import PostExpandedComponent from '../../components/CommunityBoardComponents/PostComponents/PostExpandedComponent.vue'
import TabBarComponent from '../../components/TabBarComponent.vue'

export default {
    components: { 
        PostExpandedComponent,
        TabBarComponent
    },
    props: {
        id: String
    },
}
</script>
