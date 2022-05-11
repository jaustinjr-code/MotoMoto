<template>
    <div>
        <!-- Source: https://stackoverflow.com/questions/50982408/vue-js-get-selected-option-on-change -->
        <div>
            <h1 class="postTitle">{{ feedName }}</h1>
            <button @click="createPost()" v-if="feedName != 'Main Feed'">Create Post</button>
        </div>
        <br>
        <select name="community_feeds" id="feedNames" class="feedDropdown" @change="loadFeed($event.target.value)">
            <option value="Main Feed">Main Feed</option>
            <option value="Lowrider">Lowrider</option>
            <option value="Supercar">Supercar</option>
            <option value="European">European</option>
            <option value="American Muscle">American Muscle</option>
            <option value="Exotic">Exotic</option>
            <option value="Japanese">Japanese</option>
            <option value="Is That a Supra?!">Is That a Supra?!</option>
            <option value="Economy">Economy</option>
            <option value="Electric">Electric</option>
            <option value="Sleeper">Sleeper</option>
            <option value="Truck">Truck</option>
            <option value="test">test</option>
        </select>
        <br>
        <br>
        <table id="post-summary" class="postSummary">
            <tr>
                <th>User</th>
                <th>Title</th>
                <th></th>
            </tr>
            <!-- Consider filling in data for a component Post Summary -->
            <!-- Source: https://developer.mozilla.org/en-US/docs/Learn/Tools_and_testing/Client-side_JavaScript_frameworks/Vue_rendering_lists -->
            <tr v-for="post in postList" :key="post.postId" class="postRow">
                <td class="postUsername">{{ post.postUsername }}</td>
                <td class="postDetails" @click="expandPost(post.postId)" title="Click here for post details">{{ post.postTitle }}</td>
                <button @click="upvoteButton(post.postId, post.postTitle)">Upvote</button>
                <!-- Want to pass in current user's username into upvoteButton -->
            </tr>
        </table>
    </div>
</template>

<script>
//import axios from 'axios'
import {instanceFetch, instanceSubmit} from '../../../router/CommunityBoardConnection.js'
//import router from '../../../router/index.js'
import { useCookies } from "vue3-cookies";

export default {
    setup() {
        const { cookies } = useCookies();
        return { cookies };
    },
    methods: {
        loadFeed(req) {
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
                    //window.alert(res.data.responseMessage);
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
                    console.log(e);
                    //window.alert(e);
                });
        },
        // changeFeedName(event) {
        //     console.log(event.target.value);
        //     this.feedName = event.target.value;
        // },
        upvoteButton(id, title) {
            let valid = false;
            if(this.cookies.get("username") != null && this.$cookies.get("username") != "guest")
                valid = true;
            let interactionModel = JSON.stringify({ 
                contentId: id,
                contentTitle: title,
                interactUsername: this.cookies.get("username"),
            });
            // this.postList.forEach(post => {
            //             if (post.postId == req) {
            //                 window.alert(true);

            //             }
            //         })
            if (valid) {
                instanceSubmit.post('/SubmitUpvotePost/SubmitUpvotePost', interactionModel, {
                    headers: {
                        'Content-Type': 'application/json'
                    }
                })
                .then((res) => {
                    console.log(res);
                    //window.alert(res.data.responseMessage + ": " + title);
                    //router.push({path: '/${res.data.postId}'});
                    // Change button to reflect success of Upvote
                })
                .catch((e) => {
                    console.log(e);
                    //window.alert(e);
                });
            }
        },
        expandPost(req) {
            //console.log(req);
            if(this.cookies.get("username") != null && this.cookies.get("username") != "guest")
                this.$router.push({name: 'postdetails', params: { id: req }});
            // this.$router.push({path: '/postdetails/' + req});
        },
        createPost() {
            if(this.cookies.get("username") != null && this.cookies.get("username") != "guest")
                this.$router.push({name: 'createpost', params: { feedName: this.feedName }});
        }
    },
    data() {
        return {
            feedName: "Main Feed",
            postList: []
        }
    },
    mounted() {
        this.loadFeed(this.feedName);
    }
    // props: {
    //     feedName: "Main Feed"
    // }
}
</script>

<style>
.postDetails {
    cursor: pointer;
}

/* td {
    max-width: auto;
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
} */

.postSummary {
    max-width: 50%;
    height: 50%;
    font-size:large;
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
}

.postUsername {
    max-width: fit-content;
}

.feedDropdown {
    size: 100%;
}
</style>