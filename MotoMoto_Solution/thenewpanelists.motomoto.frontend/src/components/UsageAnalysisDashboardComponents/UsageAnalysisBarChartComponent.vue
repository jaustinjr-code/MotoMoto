<template>
  <h1>{{this.analytic}}</h1>
  <Bar :chart-data="this.chartData"/>
</template>

<script>
import { useCookies } from 'vue3-cookies'
import { instanceFetch } from '../../router/CommunityBoardConnection.js'
import { Bar } from 'vue-chartjs'
import Chart from 'chart.js/auto' // Imports any necessary chart js plugins

export default {
    props: ['id'],
    components: {
        Bar,
    },
    setup() {
        const { cookies } = useCookies();
        return { cookies };
    },
    data() {
        return {
            analytic: this.id,
            labels: [],
            x_data: [],
            y_data: [],
            chartData: {}
        }
    },
    methods: {
        fetchChartData() {
            let reqUrl = 'FetchBarChart/';
            // prop might not be accessed without data member
            if (this.analytic == 'view_display')
                reqUrl += 'FetchViewDisplayAnalytic';
            else if (this.analytic == 'view_duration')
                reqUrl += 'FetchViewDurationAnalytic';
            else if (this.analytic == 'feed')
                reqUrl += 'FetchFeedPostAnalytic';
            
            let params = JSON.stringify({
                username: this.cookies.get('username')
            })
            instanceFetch.post(reqUrl, params, { 
                headers: {
                    'Content-Type': 'application/json'
                }
            }).then((res) => {
                console.log(res);
                if (res.data.isComplete == false) {
                    window.alert("Invalid Request");
                    this.$router.replace('/');
                }
                this.x_data = [];
                this.y_data = [];
                res.data.output.metricList.forEach(metric => {
                    this.x_data.push(metric.xData);
                    this.y_data.push(metric.yData);
                });
                // this.x_data = Array.from(this.x_data.values())
                let x = [...this.x_data.values()];
                // this.y_data = Array.from(this.y_data.values())
                let y = [...this.y_data.values()];
                
                //console.log(x)
                //console.log(y)
                this.chartData.labels = x
                this.chartData.datasets = [{
                    label: "First",
                    data: y,
                    backgroundColor: ["gold", "silver", "brown", "orange", "navy"],
                    }];
            }).catch((err) => {
                console.log(err);
            });
        }
    },
    mounted() {
        this.fetchChartData();
        // console.log(this.id)
        // console.log(this.analytic)
    }
}
</script>

<style>

</style>