<template>
    <div>
        <TabBarComponent/>
        <FeedPostsComponent/>
        <kpi-submission-component viewTitle="Community Board"></kpi-submission-component>
    </div>
</template>

<script>
// import FeedCommunityDropdownComponent from '../../components/CommunityBoardComponents/FeedComponents/FeedCommunityDropdownComponent.vue'
import FeedPostsComponent from '../../components/CommunityBoardComponents/FeedComponents/FeedPostsComponent.vue'
import TabBarComponent from '../../components/TabBarComponent.vue'
import KpiSubmissionComponent from '../../components/KpiSubmissionComponent.vue'

export default {
    components: { 
        // FeedCommunityDropdownComponent, 
        FeedPostsComponent,
        TabBarComponent,
        KpiSubmissionComponent
    },
}
</script>