<template>
  <h1>{{this.id}}</h1>
  <Line :chart-data="this.chartData" :key="reloadKey" />
</template>

<script>
import { useCookies } from 'vue3-cookies'
import { instanceFetch } from '../../router/CommunityBoardConnection.js'
import { Line } from 'vue-chartjs'
import Chart from 'chart.js/auto'

export default {
    props: ['id'],
    components: {
        Line,
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
            chartData: {},
            reloadKey: 0,
            reloadId: 0
        }
    },
    methods: {
        fetchChartData() {
            let reqUrl = 'FetchTrendChart/';
            // prop might not be accessed without data member
            if (this.analytic == 'login')
                reqUrl += 'FetchLoginAnalytic';
            else if (this.analytic == 'registration')
                reqUrl += 'FetchRegistrationAnalytic';
            else if (this.analytic == 'event')
                reqUrl += 'FetchEventAnalytic';
            let params = JSON.stringify({
                username: this.cookies.get('username')
            })
            instanceFetch.post(reqUrl, params, { 
                headers: {
                    'Content-Type': 'application/json'
                }
            }).then((res) => {
                console.log(res);

                res.data.output.metricList.forEach(metric => {
                    this.x_data.push(metric.xData)
                    this.y_data.push(metric.yData)
                })
                let x = [...this.x_data.values()];
                // this.y_data = Array.from(this.y_data.values())
                let y = [...this.y_data.values()];

                console.log(x)
                console.log(y)

                this.chartData.labels = x;
                this.chartData.datasets = [{
                    label: res.data.output.xTitle,
                    data: y,
                    fill: true,
                    borderColor: 'rgb(75, 192, 192)'
                }]
                // this.chartData.options = {
                //     scales: {
                //         y: [{
                //         scaleLabel: {
                //             display: true,
                //             labelString: 'probability'
                //         }
                //         }]
                //     }     
                // }
                ++this.reloadKey;
            }).catch((err) => {
                console.log(err);
            })
        }
    },
    mounted() {
        this.fetchChartData();
        this.reloadId = window.setInterval(() => {
            this.fetchChartData();
        }, 60000)
    },
    unmounted() {
        window.clearInterval(this.reloadId);
    }
}
</script>

<style>

</style>