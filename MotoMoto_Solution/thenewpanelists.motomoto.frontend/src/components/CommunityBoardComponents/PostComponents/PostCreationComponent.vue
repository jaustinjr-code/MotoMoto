<template>
    <div>
        <h1>Creating a Post in {{this.feedName}}</h1>
        <form @submit="validate">
                <input id="title" type="text" placeholder="Title" required />
                <br>
                <textarea id="description" placeholder="Description" rows="5" cols="80" required ></textarea>
                <br>
                <!-- <input id="images" type="file" name="filename" multiple disabled /> -->
                <!-- <br> -->
                <button>Submit</button>
        </form>
        <ul id="button-selection">
            <button>Upload Car Build</button>
            <button @click="discardPost()">Discard</button>
        </ul>
    </div>
</template>

<script>
import {instanceSubmit} from '../../../router/CommunityBoardConnection.js'
import { useCookies } from "vue3-cookies";

export default {
    props: [
        'feedName'
    ],
    setup() {
        const { cookies } = useCookies();
        return { cookies };
    },
    data() {
        return {
            feed: this.feedName
        }
    },
    methods: {
        validate(event) {
            console.log(event);
            let valid = false;
            var title = event.target.elements.title.value;
            var description = event.target.elements.description.value;
            //var images = event.target.elements.images.value;

            let bounds = {
                titleMin: 15,
                titleMax: 75,
                descriptionMin: 1,
                descriptionMax: 1500,
                imageMax: 1024
            }
            let tLen = title.length;
            let dLen = description.length;

            if ((bounds.titleMin <= tLen && tLen <= bounds.titleMax) &&
                (bounds.descriptionMin <= dLen && dLen <= bounds.descriptionMax))
                valid = true;

            // images.array.forEach(img => {
            //    // Source: https://stackoverflow.com/questions/16002412/check-file-extension-and-alert-user-if-isnt-image-file
            //     if (!img.name.match(/.(jpg|jpeg|png|gif)$/i))
            //         valid = false;
            //     // Check for image sizes too
            //     //window.alert("Invalid Image")
            // });

            if(valid) {
                let p =  {
                    postTitle: title,
                    contentType: this.feed,
                    postUser: this.$cookies.get("username"), // Temporary user until Authentication works
                    postDescription: description,
                    //imageList: images
                }
                //console.log(p)
                let params = JSON.stringify(p);
                //console.log(params)
                this.submitPost(params);
            } else
                window.alert("Invalid Input")            
        },
        submitPost(postModel) {
            instanceSubmit.post('/SubmitPost/SubmitPost', postModel, {
                headers: {
                    'Content-Type': 'application/json'
                }
            })
                .then(res => {
                    console.log(res);
                    //window.alert(res.data.responseMessage);
                    if (res.status == '200' && res.data.isSuccess === true) {
                        this.$router.push('/communityboard');
                    }
                    // else {
                        // Alert user that their operation did not work
                    // }
                })
                .catch(e => {
                    console.log(e);
                    //window.alert(e);
                    this.$router.push('/communityboard');
                });
        },
        discardPost() {
            this.$router.push('/communityboard');
        }
    }
}
</script>

<style>

</style>