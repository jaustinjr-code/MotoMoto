<template>
    <div class="edit-profile">
        <TabBarComponent/>
        <h2>Edit Profile</h2>
        <div class="insert">
            <div class="profile-description">
                <div class="description-title">
                    <p id="edit">New Profile Description</p>
                </div>
                <textarea class="motoTextArea" v-model="description" placeholder="Edit Description"/>
                <div class="centerBtn">
                    <button class="submitDescription" v-on:click="updateProfileDescription({description})">Edit Description</button>
                </div>

                <p class="inputValues" v-text="charactersRemaining(description.length)"></p>
            </div>
        </div>
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
            max: 160,
            username: "",
            profileImage: "",
            description: "",
        }
    },
    mounted() {
        this.getLoginCredential();
    },
    methods : {
        updateProfileDescription: async function(newProfDescription) {
            this.username = this.$cookies.get("username")
            let params = {username: this.username, newDescription: newProfDescription.description};
            if (newProfDescription.description > 160 || newProfDescription.description <= 0)
                return null;
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
        countMaxChars: function(description) {
            const charCount = 160
            let value = charCount - description.length
            document.getElementById("charCount").innerHTML = value+' characters';
        },
        charactersRemaining: function(countValue) {
            let value = "Characters Remaining " + (this.max - countValue).toString();
            return value;
        }
    }
}
</script>

<style>
.centerBtn {
    display: flex;
    justify-content: center;
    align-items: center;
}
.submitDescription {
    display: block;
    margin: center;
    padding-top: 0.5%;
    padding-bottom: 0.5%;
}
.inputValues {
    padding-left : 2.5%;
}
.motoTextArea {
    width: 50%;
    height: 80px;
    padding: 12px 20px;
    box-sizing: border-box;
    border: 2px solid #ccc;
    border-radius: 4px;
    background-color: #f8f8f8;
    font-size: 16px;
    resize: none;
}
.profile-description {
    padding-bottom: 20px;
}
</style>