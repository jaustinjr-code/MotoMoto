<template>
    <TabBarComponent />
    <!-- Source: https://stackoverflow.com/questions/32913226/auto-refresh-page-every-30-seconds -->
    <meta http-equiv="refresh" content="60">
    <!-- <UsageAnalysisBarChartComponent :id='view_display' /> -->
    <usage-analysis-bar-chart-component id="view_display"></usage-analysis-bar-chart-component>
    <!-- <UsageAnalysisBarChartComponent :id='view_duration' /> -->
    <usage-analysis-bar-chart-component id="view_duration"></usage-analysis-bar-chart-component>
    <!-- <UsageAnalysisBarChartComponent :id='feed' /> -->
    <usage-analysis-bar-chart-component id="feed"></usage-analysis-bar-chart-component>
    <!-- <UsageAnalysisTrendChartComponent :id='login' /> -->
    <usage-analysis-trend-chart-component id="login"></usage-analysis-trend-chart-component>
    <!-- <UsageAnalysisTrendChartComponent :id='registration' /> -->
    <usage-analysis-trend-chart-component id="registration"></usage-analysis-trend-chart-component>
    <!-- <UsageAnalysisTrendChartComponent :id='event' /> -->
    <usage-analysis-trend-chart-component id="event"></usage-analysis-trend-chart-component>
</template>

<script>
import TabBarComponent from '../components/TabBarComponent.vue'
import UsageAnalysisBarChartComponent from '../components/UsageAnalysisDashboardComponents/UsageAnalysisBarChartComponent.vue'
import UsageAnalysisTrendChartComponent from '../components/UsageAnalysisDashboardComponents/UsageAnalysisTrendChartComponent.vue'
export default {
    components: {
        TabBarComponent,
        UsageAnalysisBarChartComponent,
        UsageAnalysisTrendChartComponent,
    }
}
</script>
