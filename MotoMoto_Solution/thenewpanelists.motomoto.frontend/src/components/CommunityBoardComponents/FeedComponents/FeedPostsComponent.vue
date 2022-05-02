<template>
    <div>
        <!-- Source: https://stackoverflow.com/questions/50982408/vue-js-get-selected-option-on-change -->
        <select name="community_feeds" id="feedNames" @change="LoadFeed($event.target.value)">
            <option value="test">test</option>
            <option value="Main Feed">Main Feed</option>
            <option value="Supercar">Supercar</option>
        </select>
        <div>
            <h1>{{ feedName }}</h1>
            <button @click="CreatePost()" v-if="feedName != 'Main Feed'">Create Post</button>
        </div>
        <ul id="post-summary">
            <!-- Consider filling in data for a component Post Summary -->
            <!-- Source: https://developer.mozilla.org/en-US/docs/Learn/Tools_and_testing/Client-side_JavaScript_frameworks/Vue_rendering_lists -->
            <div v-for="post in postList" :key="post.postId" >
                <li>{{ post.postUsername }}</li>
                <li @click="ExpandPost(post.postId)">{{ post.postTitle }}</li>
                <button @click="UpvoteButton(post.postId, post.postTitle)">Upvote</button>
                <!-- Want to pass in current user's username into UpvoteButton -->
            </div>
        </ul>
    </div>
</template>

<script>
//import axios from 'axios'
import {instanceFetch, instanceSubmit} from '../../../router/CommunityBoardConnection.js'
//import router from '../../../router/index.js'
export default {
    methods: {
        LoadFeed(req) {
            // Syntax Error?
            //console.log(req);
            let feedModel = JSON.stringify({ "feedName": req });
            //console.log(instanceFetch.getUri());
            //console.log(feedModel);
            instanceFetch.post('/FetchFeed/FetchFeed', feedModel, {
                headers: {
                    'Content-Type': 'application/json'
                }
            })
                .then((res) => {
                    //window.alert(res.status);
                    console.log(res);
                    window.alert(res.data.responseMessage);
                    //window.alert(res.data.postId);
                    if (res.data.output != null) {
                        this.postList = res.data.output.postList;
                        //this.postList.forEach(post => {
                        //console.log(post.postId);
                        //console.log(post.postTitle);
                        //});
                    }
                    else 
                        this.postList = []
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
        UpvoteButton(id, title) {
            let interactionModel = JSON.stringify({ 
                contentId: id,
                contentTitle: title,
                interactUsername: 'ran'
            });
            // this.postList.forEach(post => {
            //             if (post.postId == req) {
            //                 window.alert(true);

            //             }
            //         })
            instanceSubmit.post('/SubmitUpvotePost/SubmitUpvotePost', interactionModel, {
                headers: {
                    'Content-Type': 'application/json'
                }
            })
                .then((res) => {
                    console.log(res);
                    window.alert(res.data.responseMessage + ": " + title);
                    //router.push({path: '/${res.data.postId}'});
                    // Change button to reflect success of Upvote
                })
                .catch((e) => {
                    window.alert(e);
                });
        },
        ExpandPost(req) {
            console.log(req);
            this.$router.push({name: 'postdetails', params: { id: req }});
            // this.$router.push({path: '/postdetails/' + req});
        },
        CreatePost() {
            this.$router.push({name: 'createpost', params: { feedName: this.feedName }});
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
    padding: 20px;
}
</style>