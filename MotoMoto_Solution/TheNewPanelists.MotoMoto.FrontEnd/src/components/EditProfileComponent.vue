<template>
    <div class="edit-profile">
        <TabBarComponent/>
        <h2>Edit Profile</h2>
        <span class="insert">
            <div class="profile-description">
                <p id="edit">New Profile Description</p>
                <input v-model="description" placeholder="Edit Description">
                <button class="submitDescription" v-on:click="updateProfileDescription({description})">Edit Description</button>
            </div>
            <div class="profile-image">
                <p id="edit">Profile Image: </p>
                <input v-model="image" placeholder="Image URL">
                <button class="submitDescription" v-on:click="updateProfileImage(image)">Edit Image URL</button>
            </div>
        </span>
    </div>
</template>

<script>
import {instance} from '../router/ProfileConnection';
import TabBarComponent from "../components/TabBarComponent.vue";

export default {
    components: {
        TabBarComponent
    },
    data() {
        return {
            username: "",
            profileImage: "",
            description: "",
        }
    },
    mounted() {
        this.getLoginCredential();
    },
    methods : {
        updateProfileDescription: async function(_description) {
            this.username = this.$cookies.get("username")
            let params = {username: this.username, newDescription: _description.description};
            await instance.get('/ProfileUpdate/DescriptionUpdate', {params}).then((response) =>{
                this.profile = response.data;
            })
        },
        updateProfileImage: async function(_url) {
            let params = {username: this.username, newURL: _url.image}
            await instance.get('/ProfileUpdate/ImageUpdate', {params}).then((response) =>{
                this.profile = response.data;
                console.log(response.data);
            })
        },
        updateProfileUsername: async function(_username) {
            let params = {username: this.$cookies.get("username"), newUsername: _username.username}
            await instance.get('/ProfileUpdate/UsernameUpdate', {params}).then((response) =>{
                this.profile = response.data;
                console.log(response.data);
            })
        },
        updateProfileStatus: async function(_status) {
            let params = {username: this.$cookies.get("username"), status: _status.status}
            await instance.get('/ProfileUpdate/StatusUpdate', {username: this.$cookies.get("username"), newUsername: _username}).then((response) =>{
                this.profile = response.data;
                console.log(response.data);
            })
        },
        getLoginCredential: function () {
			this.username = this.$cookies.get("username")
            console.log(this.username);
		},
    }
}
</script>

<style>
.edit {
    padding-left: 1.5%;
}
</style>