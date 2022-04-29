<template>
    <div>
        <feed-community-dropdown-component></feed-community-dropdown-component>
        <FeedPostsComponent/>
    </div>
</template>

<script>
import FeedCommunityDropdownComponent from '../../components/CommunityBoardComponents/FeedComponents/FeedCommunityDropdownComponent.vue'
import FeedPostsComponent from '../../components/CommunityBoardComponents/FeedComponents/FeedPostsComponent.vue'
export default {
    components: { 
        FeedCommunityDropdownComponent, 
        FeedPostsComponent
    },
}
</script>