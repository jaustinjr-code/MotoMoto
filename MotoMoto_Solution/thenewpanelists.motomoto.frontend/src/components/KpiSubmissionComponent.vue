<script>
import { instanceSubmit } from '../router/CommunityBoardConnection.js'

export default {
    props: ['viewTitle'],
    data() {
        return {
            startDuration: 0,
            endDuration: 0,
        }
    },
    methods: {
        submitViewDisplayKpi() {
            // subType, title, metric
            // subType { 'displayTotal' 'durationAvg' }
            // title { current view group }
            // metric { int }
            let params = JSON.stringify({
                subType: 'display',
                title: this.viewTitle,
                metric: 1
            });
            instanceSubmit.post('SubmitKpi/SubmitViewKpiMetric', params, {
                headers: {
                    'Content-Type': 'application/json'
                }
                })
                .then(res => {
                    console.log(res);
                })
                .catch(err => {
                    console.log(err);
                });
        },
        submitViewDurationKpi() {
            let params = JSON.stringify({
                subType: 'duration',
                title: this.viewTitle,
                metric: Math.floor(this.endDuration - this.startDuration),
            });
            instanceSubmit.post('SubmitKpi/SubmitViewKpiMetric', params, {
                headers: {
                    'Content-Type': 'application/json'
                }
                })
                .then(res => {
                    console.log(res);
                })
                .catch(err => {
                    console.log(err);
                });
        }
    },
    mounted() {
        this.startDuration = new Date().getTime() / 1000;
        console.log(this.startDuration);
        this.submitViewDisplayKpi();
    },
    unmounted() {
        this.endDuration = new Date().getTime() / 1000;
        console.log(this.endDuration);
        this.submitViewDurationKpi();
    }
}
</script>