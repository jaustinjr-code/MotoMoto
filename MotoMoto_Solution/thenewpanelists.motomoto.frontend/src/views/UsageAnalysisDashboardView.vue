<template>
    <TabBarComponent />
    <!-- Source: https://stackoverflow.com/questions/32913226/auto-refresh-page-every-30-seconds -->
    <!-- Refreshes when outside of route -->
    <!-- <meta http-equiv="refresh" content="60"> -->
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

// Refresh alternatives
// setTimeout(function(){
//     location = '#/UsageAnalysisDashboard'
//   },5000)

// window.setInterval('refresh()', 5000);     // Call a function every 10000 milliseconds (OR 10 seconds).
// // Refresh or reload page.
// function refresh() {
//     window.location.reload();
// }
</script>