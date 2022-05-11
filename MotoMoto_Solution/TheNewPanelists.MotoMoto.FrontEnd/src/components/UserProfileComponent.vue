<template>
  <div class="UserProfileView" style="font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;">
    <TabBarComponent/>
    <span class="profile-header">
      <div class="ProfileImageHeader">
          <span>
              <div class="ProfileImage" v-if="isImage(profile.profileImagePath) === true">
                <img src="{{profile.profileImagePath}}" class="user_profile_image" alt="user_profile_image">
              </div>
              <div class="ProfileImage" v-else>
                  <img src="https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_960_720.png" class="user_profile_image" alt="user_profile_image">
              </div>
          </span>
      </div>
      <div class="ProfileUsernameHeader">
          <div class="profile-username" v-if="profile.username != null">
              <h3 class="profile-username-header">{{profile.username}}</h3>
          </div>
          <div class="profile-username" v-else>
          </div>
      </div>
      <span class="profile-edit">
        <button class="edit-profile-button" v-on:click="EditProfile()">Edit Profile</button>
        <LogoutComponentVue/>
      </span>
    </span>
    <div class="ProfileDescription">
        <body id="profileBod">
            <h2 class="profileTitle">About Me</h2>
            <span class="profileDescriptionInline" v-if="profile.profileDescription != null">
                <p class="profileDescriptiontext">{{profile.profileDescription}}</p>
            </span>
            <span class="profileDescriptionInline" v-else>
                <p class="profileDescriptiontext">No Profile Description</p>
            </span>
        </body>
    </div>
    <div class="userPostsDiv">
        <h3 class="userPostTitle">User Posts</h3>
        <table class="userPosts">
            <thead class="postThead">
                <tr class="postItems">
                    <td class="postTitles" >Post Title</td>
                    <td class="postTitles" >Feed Name</td>
                    <td class="postTitles" >Post Description</td>
                    <td class="postTitles" >Post Date</td>
                </tr>
            </thead>
            <thead v-if="{profilePosts} != null">
                <tr class="postItems" v-for="(profilePost) in paginatedDataPost()" :key=profilePost>
                    <td class="postTitles"><router-link :to="{name: 'postdetails', params: {id: profilePost.postId}}">
                        {{profilePost.postTitle}}
                    </router-link></td>
                    <td class="postTitles">{{profilePost.feedName}}</td>
                    <td class="postTitles">{{profilePost.postDescription.slice(0,25)}}</td>
                    <td class="postTitles">{{profilePost.submitUTC.slice(0,10)}}</td>
                </tr>
            </thead>
            <thead v-else>
                <h2 class="no-posts">No Posts Created</h2>
            </thead>
        </table>
        <div class="pageButtons">
            <button class="buttonLeft" @click="prevPagePost()">Prev</button>
            <button class="buttonRight" @click="nextPagePost()">Next</button>
            <footer>
                <p>{{displayPageNumberPost()}} of {{pageCountPost()}}</p>
            </footer>
        </div>
    </div>
    <div class="upvotedPostsDiv">
        <h3 class="upvotedPostTitle">Upvoted Posts</h3>
        <table class="upvotedPosts">
            <thead class="postItems">
                <tr class="postTitles">
                    <td class="upvoteTitle">Author</td>
                    <td class="upvoteTitle">Post Title</td>
                    <td class="upvoteTitle">Feed Name</td>
                    <td class="upvoteTitle">Post Description</td>
                    <td class="upvoteTitle">Post Date</td>
                </tr>
            </thead>
            <thead v-if="{profilePosts} != null">
                <tr class="postItems" v-for="(profilePost) in paginatedDataUpvo()" :key=profilePost>
                    <td class="upvoteTitle">{{profilePost["postUsername"]}}</td>
                    <td class="upvoteTitle"><router-link :to="{name: 'postdetails', params: {id: profilePost.postId}}">
                        {{profilePost["postTitle"]}}
                    </router-link></td>
                    <td class="upvoteTitle">{{profilePost["feedName"]}}</td>
                    <td class="upvoteTitle">{{profilePost["postDescription"].slice(0,25)}}</td>
                    <td class="upvoteTitle">{{profilePost["submitTime"].slice(0,10)}}</td>
                </tr>
            </thead>
        </table>
        <div class="pageButtons">
            <button class="buttonLeft" @click="prevPageUpvo()">Prev</button>
            <button class="buttonRight" @click="nextPageUpvo()">Next</button>
            <footer>
                <p>{{displayPageNumberUpvo()}} of {{pageCountUpvo()}}</p>
            </footer>
        </div>
    </div>
    <h2 style="font-size: 22px" class = "header"><i>Preferences</i></h2>

    <div class = "preferencesContainer">
        <table class="prefTable">
          <th class = "title">Countries Followed</th>
          <tbody class="prefBody">
            <tr class="prefTr">
              <th class="prefTh">Country</th>
            </tr>
            <tr v-for="record in followedCountries" :key=record.country class="prefTr">
              <td>{{record.country}}</td>
            </tr>
          </tbody>
        </table>  
        <table class="prefTable">
          <th class = "title">Makes Followed</th>
          <tbody class="prefBody">
            <tr class="prefTr">
              <th class="prefTh">Make</th>
            </tr>
            <tr v-for="record in followedMakes" :key=record.make>
              <td class="prefTd">{{record.make}}</td>
            </tr>
          </tbody>
        </table>        
        <table class="prefTable">
          <th class = "title">Models Followed</th>
          <tbody class="prefBody">
            <tr class="prefTr">
              <th class="prefTh">Make</th>
              <th class="prefTh">Model</th>
            </tr>
            <tr v-for="record in followedModels" :key=record.model class="prefTr">
              <td class="prefTd">{{record.make}}</td>
              <td class="prefTd">{{record.model}}</td>
            </tr>
          </tbody>
        </table>
    </div>
    <div class = "editButton">
      <button @click="$router.push('/UserProfile/EditPreferences')">Edit Preferences</button>
    </div>
  </div>
</template>

<script>
import { useCookies } from "vue3-cookies";
import { defineComponent } from "vue";
import {PersonalizedRecsApi} from '../router/PersonalizedRecommendationsConnection';
import {Profile} from '../router/ProfileConnection';
import TabBarComponent from '../components/TabBarComponent';
import LogoutComponentVue from "./LogoutComponent.vue";

export default defineComponent({
  components: {
    TabBarComponent,
    LogoutComponentVue,
  },
  setup() {
    const { cookies } = useCookies();
    return { cookies };
  },
  data () {
    return {
      maxPagesPost: 0,
      maxPagesUpvo: 0,
      pageNumberPost: 0,
      pageNumberUpvo: 0, 
      profile: [],
      profilePosts: [],
      profileUpvotedPosts: [],
      followedCountries: [],
      followedMakes: [],
      followedModels: [],
      hasPreferences: false
    }
  },
  props: {
    profilePosts: {
      type:Array,
      required:true
    },
    sizePost:{
        type:Number,
        required:false,
        default: 5
    },
    profileUpvotedPosts: {
      type:Array,
      required:true
    },
    sizeUpvo:{
        type:Number,
        required:false,
        default: 5
    },
  },
  mounted()
  {
        this.GetProfleDetails();
        this.GetUserPosts();
        this.GetUserUpvotedPosts();
  },
  methods: {
    GetProfleDetails: async function() {
        let params = {username: this.$cookies.get("username")}
        await Profile.get('/ProfileRetrieval/Profile', {params}).then((response) =>{
            this.profile = response.data;
            this.$cookies.set("userId", response.data.userId,"1hr")
            console.log(response.data);
        })
    },
    GetUserPosts: async function() {
        let params = {username: this.$cookies.get("username")}
        await Profile.get('/ProfileRetrieval/GetPosts', {params}).then((response) => {
            console.log(`Server replied with ${response.data}`),
            this.profilePosts = response.data["userPosts"];
            console.log(response.data);
        }).catch((e)=>{
            console.log(e)
        })
    },
    GetUserUpvotedPosts: async function() {
        await Profile.get('/ProfileRetrieval/ProfileUpvotePosts', {params: {username: this.$cookies.get("username")}}).then((response) => {
            console.log(`Server replied with ${response.data}`),
            this.profileUpvotedPosts = response.data["upVotedPosts"];
            console.log(response.data);
        }).catch((e)=>{
            console.log(e)
        })
    },
    GetPreferences: async function() {
        await PersonalizedRecsApi.get('/Preferences/Retrieve', {params: {userId: this.$cookies.get("userId")}}).then((response) => {
            console.log(`Server replied with: ${response.data}`),
            this.followedCountries = response.data.followedCountries, 
            this.followedMakes = response.data.followedMakes, 
            this.followedModels = response.data.followedModels
        }).catch((e)=>{
            console.log(e);})
    },
    isImage: function(url) {
      return /\.(jpg|jpeg|png|webp|avif|gif|svg)$/.test(url);
    },
    UpdateProfileDescription: async function(newDescription) {
        await Profile.put('/ProfileUpdate/DescriptionUpdate', {params: {username: this.$cookies.get("username"), description: newDescription}}).then((response) => {
            console.log(`Server replied with ${resposne.data}`);
        }).catch((e)=> {
            console.log(e);
        });
    },
    GetCookieUsername: function() {
        return this.$cookies.get("username");
    },
    EditProfile: function() {
        this.$router.push('/EditProfile')
    },
    pageCountPost() {
        let l = this.profilePosts.length,
        s = this.sizePost;
        this.maxPagesPost = Math.ceil(l/s);
        // if (this.maxPagesPost <= 1) {
            
        // }
        console.log(this.maxPagesPost);
        return this.maxPagesPost;
    },
    paginatedDataPost() {
        const start = this.pageNumberPost * this.sizePost,
        end = start + this.sizePost;
        return this.profilePosts.slice(start, end);
    },
    displayPageNumberPost() {
        if (this.maxPagesPost === 0) {
          return 0;
        }
        return this.pageNumberPost+1;
    },
    nextPagePost: function() {
        if (this.pageNumberPost < this.pageCountPost()-1) {
            this.pageNumberPost++;
            console.log(this.pageNumberPost);
        }
    },
    prevPagePost: function() {
        if (this.pageNumberPost > 0) {
            this.pageNumberPost--;
            console.log(this.pageNumberPost);
        }
    },
    pageCountUpvo() {
        let l = this.profileUpvotedPosts.length,
        s = this.sizeUpvo;
        this.maxPagesUpvo = Math.ceil(l/s);
        // if (this.maxPagesPost <= 1) {
            
        // }
        console.log(this.maxPagesUpvo);
        return this.maxPagesUpvo;
    },
    paginatedDataUpvo() {
        const start = this.pageNumberUpvo * this.sizeUpvo,
        end = start + this.sizeUpvo;
        return this.profileUpvotedPosts.slice(start, end);
    },
    displayPageNumberUpvo() {
        if (this.maxPagesUpvo === 0) {
          return 0;
        }
        return this.pageNumberUpvo+1;
    },
    nextPageUpvo: function() {
        if (this.pageNumberUpvo < this.pageCountUpvo()-1) {
            this.pageNumberUpvo++;
            console.log(this.pageNumberUpvo);
        }
    },
    prevPageUpvo: function() {
        if (this.pageNumberUpvo > 0) {
            this.pageNumberUpvo--;
            console.log(this.pageNumberUpvo);
        }
    },
  },
  created: function () {
      if (this.$cookies.get("userId") != "guest") {
          this.GetPreferences();
      }
      else {
          this.$router.push('/login');
      }
  },
})
</script>

<style scoped>
.user_profile_image
{
    height: 100px;
    width: 100px;
    border-radius: 50%;
}
.profileDescriptiontext
{
    text-align: center;
}
.userPosts
{
    text-align: center;
    border-collapse: collapse;
}
.upvotedPosts
{
    padding-bottom: 10px;
    border-collapse: collapse;
    margin-left: auto;
    margin-right: auto;
}
.upvoteTitle 
{
    padding-left: 10px;
}
.preferencesContainer
{
  display: flexbox;
  margin: auto;
  width: 500px;
  height: 500px;
  border: 3px solid grey;
  overflow-y: scroll;
}
Button
{
  margin-top: 5px;
  text-align: center;
  background-color: rgb(0, 75, 73);
  color: white;
}
button:hover
{
  color: black;
  background-color: lightgrey;
}
.userPostsDiv
{
    padding-bottom: 2.5%;
}
.upvotedPostsDiv
{
    padding-bottom: 2.5%;
}
.prefTable
{
  table-layout: fixed;
  font-size: 16px;
  border-bottom: 5px solid grey;
  width: 100%;
  margin-top: 10px;
}
.title
{
  font-family: Cambria, Cochin, Georgia, Times, 'Times New Roman', serif;
  font-size: 20px;
  color: black;
  text-align: left;
  white-space: nowrap;
  padding-bottom: 10px;
  background-color: white;
}
.prefTh
{
  padding-left: 5px;
  padding-bottom: 5px;
  background-color:dimgray;
}
.prefTd, .prefTr
{
  border-bottom: 1px solid grey;
  text-align: left;
  font-size: 12px;
  padding: 8px;
}
tr:nth-child(even) 
{
  background-color: #dddddd;
}
.noPreferences
{
  top: 30%;
  position: relative;
  font-size: 18px;
}
.ProfileUsernameHeader
{
  padding-top: 2.5%;
}
.edit-profile-button 
{
  margin-right: 10px;
  width: 100px;
  height: 30px;
}
.postItems td.upvoteTitle,
.postItems td.postTitles
{
  font-size: 15px;
}

</style>