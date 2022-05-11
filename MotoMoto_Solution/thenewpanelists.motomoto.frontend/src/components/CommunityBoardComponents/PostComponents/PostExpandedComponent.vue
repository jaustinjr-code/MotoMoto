<template>
    <div>
        <div id="title-section">
            <h2>{{this.postUsername}}</h2>
            <h1>{{this.postTitle}}</h1>
        </div>
        <ul id="post-content">
            <p>{{this.postDescription}}</p>
            <br>
            <button @click="UpvotePostButton(this.postId, this.postTitle)">Upvote</button>
            <p>{{this.upvoteCount}} Upvotes</p>
        </ul>
        <table id="comment-section">
            <tr>
                <th>User</th>
                <th>Description</th>
                <th></th>
            </tr>
            <tr v-for="comment in commentList" :key="comment.commentId" >
                <td>{{ comment.commentUsername }}</td>
                <td>{{ comment.commentDescription }}</td>
                <!-- commentUsername defaults to existing user right now -->
                <button @click="UpvoteCommentButton(comment.commentId, comment.postId)">Upvote</button>
            </tr>
        </table>
        <br>
        <br>
        <form @submit="Validate">
            <textarea id="commentInput" cols="100" rows="10" placeholder="Input Comment Here"></textarea>
            <br>
            <button>Submit</button>
        </form>
    </div>
</template>

<script>
import {instanceFetch, instanceSubmit} from  '../../../router/CommunityBoardConnection.js'
import { useCookies } from "vue3-cookies";

export default {
    props: [
        'id'
    ],
    setup() {
        const { cookies } = useCookies();
        return { cookies };
    },
    data() {
        return {
            postId: this.id,
            postTitle: "",
            postUsername: "",
            postDescription: "",
            imageList: [],
            commentList: [],
            upvoteCount: 0,
            btnPostUp: "Upvote",
            comment: ""
        }
    },
    methods: {
        FetchDetails() {
            //let param = JSON.stringify({ postId: parseInt(this.postId) });
            //console.log(param);
            instanceFetch.get('FetchPostDetails/FetchPostDetails?postId=' + this.postId
            // instanceFetch.get('FetchPostDetails/FetchPostDetails', param, {
                // ,{headers: {
                //     'Content-Type': 'application/json; charset=utf-8'
                // }}
            )
                .then(res => {
                    console.log(res);
                    //window.alert(res.data.responseMessage);

                    if(res.status == 200 && res.data.output != null) {
                        this.postId = res.data.output.postId;
                        this.postTitle = res.data.output.postTitle;
                        this.postUsername = res.data.output.postUsername;
                        this.postDescription = res.data.output.postDescription;
                        this.commentList = res.data.output.commentList;
                        this.upvoteCount = res.data.output.upvoteCount;
                    }
                })
                .catch(e => {
                    window.alert(e);
                });
        },
        Validate(event) {
            let valid = false;
            var input = event.target.elements.commentInput.value;

            let bounds = {
                commentMin: 1,
                commentMax: 1000
            }
            let inLen = input.length;
            if (bounds.commentMin <= inLen && inLen <= bounds.commentMax)
                valid = true;
            
            if(valid) {
                let p = {
                    postID: parseInt(this.postId),
                    postUser: this.$cookies.get("username"),
                    postDescription: input
                }
                let params = JSON.stringify(p);
                this.SubmitComment(params);
            }
            else
                window.alert("Invalid Input") 
        },
        SubmitComment(params) {
            instanceSubmit.post("/SubmitComment/SubmitComment", params, {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            })
                .then(res => {
                    console.log(res);
                    //window.alert(res.data.responseMessage);

                    // Refreshes the page so you can see the new comment
                    if (res.status == 200)
                        location.reload();
                })
                .catch(e => {
                    console.log(e);
                    //window.alert(e);
                })
        },
        UpvoteCommentButton(cid, pid) {
            let params = JSON.stringify({
                contentId: parseInt(cid),
                postId: parseInt(pid),
                interactUsername: this.$cookies.get("username")
            });
            //console.log(params);
            instanceSubmit.post('/SubmitUpvoteComment/SubmitUpvoteComment', params, {
                headers: {
                    'Content-Type': 'application/json'
                }
            })
                .then(res => {
                    console.log(res);
                    window.alert(res.data.responseMessage);
                    // Use in case you display upvotes for comments
                    //if(res.status == 200)
                    //location.reload();
                })
                .catch(e => {
                    window.alert(e);
                })
        },
        UpvotePostButton(id, title) {
            let interactionModel = JSON.stringify({ 
                contentId: id,
                contentTitle: title,
                interactUsername: this.$cookies.get("username")
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
                    //window.alert(res.data.responseMessage + ": " + title);
                    //router.push({path: '/${res.data.postId}'});
                    // Change button to reflect success of Upvote
                    if(res.status == 200)
                        location.reload();
                })
                .catch((e) => {
                    console.log(e);
                    //window.alert(e);
                });
        }
    },
    mounted() { 
        this.FetchDetails();
    }
}
</script>

<style>
ul#title-section h1 {
    display: inline;
    text-align:right;
    float:right;
    margin-right: 150px;
}

ul#title-section h2 {
    display: inline;
    text-align:left;
    float:left;
    margin-left: 150px;
}

</style>