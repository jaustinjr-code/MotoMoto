<template>
    <div>
        <!-- Source: https://stackoverflow.com/questions/50982408/vue-js-get-selected-option-on-change -->
        <select name="community_feeds" id="feedNames" @change="LoadFeed($event.target.value)">
            <option value="test">test</option>
            <option value="Main Feed">Main Feed</option>
            <option value="Supercar">Supercar</option>
        </select>
        <h1>{{ feedName }}</h1>
        <ul id="post-summary">
            <!-- Consider filling in data for a component Post Summary -->
            <!-- Source: https://developer.mozilla.org/en-US/docs/Learn/Tools_and_testing/Client-side_JavaScript_frameworks/Vue_rendering_lists -->
            <div v-for="post in postList" :key="post.postId">
                <li>{{ post.postUser }}</li>
                <li>{{ post.postTitle }}</li>
                <button @click="UpvoteButton(post.postId)">Upvote</button>
            </div>
        </ul>
    </div>
</template>

<script>
//import axios from 'axios'
import {instance} from '../../../router/FeedConnection.js'
import PostSummaryComponent from '../PostComponents/PostSummaryComponent.vue'
//import router from '../../../router/index.js'
export default {
    components: {
        PostSummaryComponent,
    },
    methods: {
        LoadFeed(req) {
            // Syntax Error?
            console.log(req);
            let feedModel = JSON.stringify({ "feedName": req });
            console.log(instance.getUri());
            console.log(feedModel);
            instance.post('/FetchFeed/FetchFeed', feedModel, {
                headers: {
                    'Content-Type': 'application/json'
                }
            })
                .then((res) => {
                    //window.alert(res.status);
                    console.log(res);
                    window.alert(res.data.responseMessage);
                    //window.alert(res.data.postId);
                    this.postList = res.data.output.postList;
                    this.postList.forEach(post => {
                        console.log(post.postId);
                        console.log(post.postTitle);
                    });
                    //this.feedName = res.data.feedName; // Use when request works
                    this.feedName = req; 
                })
                .catch((e) => {
                    window.alert(e);
                });
        },
        // changeFeedName(event) {
        //     console.log(event.target.value);
        //     this.feedName = event.target.value;
        // },
        UpvoteButton(req) {
            // let params = { postId: req };
            this.postList.forEach(post => {
                        if (post.postId == req) window.alert(true);
                    })
            // instance.get(url='/FetchPost/FetchPostDetails', { params })
            //     .then((res) => {
            //         this.postList.forEach(post => {
            //             if (post.postId == res) window.alert(true);
            //         })
            //         window.alert(res.data.message);
            //         //router.push({path: '/${res.data.postId}'});
            //     })
            //     .catch((e) => {
            //         window.alert(e);
            //     })
        }
    },
    data() {
        return {
            feedName: "test",
            postList: []
        }
    },
    mounted() {
        this.LoadFeed(this.feedName);
    }
    // props: {
    //     feedName: "Main Feed"
    // }
}
</script>

<style>
ul#post-summary {
    border:1px solid #000;
    border-color: black;
    margin: 50px;
}
ul#post-summary li {
    display: inline;
}
li {
    padding: 20px
}
</style>