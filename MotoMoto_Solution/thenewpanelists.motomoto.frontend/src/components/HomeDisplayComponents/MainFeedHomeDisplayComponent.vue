<template>
    <div>
        <h1>{{ feedName }}</h1>
        <table id="post-summary" class="homepostlist">
            <tr>
                <th>User</th>
                <th>Title</th>
            </tr>
            <tr v-for="post in postList" :key="post.postId" class="postRow">
                <td class="postUsername">{{ post.postUsername }}</td>
                <td class="postDetails" @click="expandPost(post.postId)" title="Click here for post details">{{ post.postTitle }}</td>
            </tr>
        </table>
    </div>
</template>

<script>
import { useCookies } from 'vue3-cookies'
import { instanceFetch } from '../../router/CommunityBoardConnection'

export default {
    setup() {
        const { cookies } = useCookies();
        return { cookies };
    },
    data() {
        return {
            feedName: "Main Feed",
            postList: []
        }
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
                    console.log(res);
                    if (res.data.output != null) {
                        this.postList = res.data.output.postList;
                    }
                    else 
                        this.postList = []
                    this.feedName = req; 
                })
                .catch((e) => {
                    console.log(e);
                });
        },
        expandPost(req) {
            if(this.cookies.get("username") != null && this.cookies.get("username") != "guest")
                this.$router.push({name: 'postdetails', params: { id: req }});
            else
                this.$router.push('/Login');
        },
    },
    mounted() {
        this.loadFeed(this.feedName);
    }
}
</script>

<style>
.postDetails {
    cursor: pointer;
}

.homepostlist {
    font-size: large;
}

.postRow {
    max-width: fit-content;
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
}

.postUsername {
    width: 20px;
    overflow: hidden;
}
</style>