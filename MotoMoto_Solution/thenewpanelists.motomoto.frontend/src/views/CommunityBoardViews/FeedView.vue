<template>
    <div>
        <TabBarComponent/>
        <FeedPostsComponent/>
    </div>
</template>

<script>
// import FeedCommunityDropdownComponent from '../../components/CommunityBoardComponents/FeedComponents/FeedCommunityDropdownComponent.vue'
import FeedPostsComponent from '../../components/CommunityBoardComponents/FeedComponents/FeedPostsComponent.vue'
import TabBarComponent from '../../components/TabBarComponent.vue'

export default {
    components: { 
        // FeedCommunityDropdownComponent, 
        FeedPostsComponent,
        TabBarComponent
    },
}
</script>