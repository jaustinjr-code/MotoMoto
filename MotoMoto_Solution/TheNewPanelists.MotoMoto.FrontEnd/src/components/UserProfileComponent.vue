<template>
  <div class="UserProfileView" style="font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;">
    <TabBarComponent/>
    <span class="profile-header">
      <div class="ProfileImageHeader">
          <div class="ProfileImage" v-if="isImage(profile.profileImagePath)">
              <img src="{{profile.profileImagePath}}" class="user_profile_image" alt="user_profile_image">
          </div>
          <div class="ProfileImage" v-else>
              <img src="https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_960_720.png" class="user_profile_image" alt="user_profile_image">
          </div>
      </div>
      <div class="ProfileUsernameHeader">
          <div class="profile-username" v-if="profile.username != null">
              <h3 class="profile-username-header">{{profile.username}}</h3>
          </div>
          <div class="profile-username" v-else>
              <h3 class="profile-username-header">This is an easter egg and should not be seen</h3>
          </div>
      </div>
    </span>
    <div class="ProfileDescription">
        <body id="profileBod">
            <h2 class="profileTitle">About Me</h2>
            <span class="profileDescriptionInline" v-if="profile.profileDescription != null">
                <p class="profileDescriptiontext">{{profile.profileDescription}}</p>
                <button class="updateDescription">Change About Me</button>
            </span>
            <span class="profileDescriptionInline" v-else>
                <p class="profileDescriptiontext">No Profile Description</p>
                <button class="updateDescription">Change About Me</button>
            </span>
        </body>
    </div>
    <div class="userPostsDiv">
        <h3 class="userPostTitle">User Posts</h3>
        <table class="userPosts">
            <thead class="postThead">
                <tr class="postTitles">
                    <td>Post Title</td>
                    <td>Feed Name</td>
                    <td>Post Description</td>
                    <td>Post Date</td>
                </tr>
            </thead>
            <thead v-if="{profilePosts} != null">
                <tr class="postItems" v-for="(profilePost) in profilePosts" :key=profilePost>
                    <td class="postTitle">
                        <router-link class="postTitle" :to="{name: 'postdetails', params: {id: profilePost.postId}}">
                          {{profilePost.title}}
                        </router-link>
                    </td>
                    <td class="postTitle">{{profilePost.postTitle}}</td>
                    <td class="feedName">{{profilePost.feedName}}</td>
                    <td class="postDescription">{{profilePost.postDescription}}</td>
                    <td class="submitUTC">{{profilePost.submitUTC}}</td>
                </tr>
            </thead>
            <thead v-else>
                <h2 class="no-posts">No Posts Created</h2>
            </thead>
        </table>
    </div>
    <div class="upvotedPostsDiv">
        <h3 class="upvotedPostTitle">Upvoted Posts</h3>
        <table class="upvotedPosts">
            <thead class="upvotedPosts">
                <tr class="postTitles">
                    <td class="upvoteTitle">Author</td>
                    <td class="upvoteTitle">Post Title</td>
                    <td class="upvoteTitle">Feed Name</td>
                    <td class="upvoteTitle">Post Description</td>
                    <td class="upvoteTitle">Post Date</td>
                </tr>
            </thead>
        </table>
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

export default defineComponent({
  components: {
    TabBarComponent,
  },
  setup() {
    const { cookies } = useCookies();
    return { cookies };
  },
  data () {
    return {
      profile: [],
      profilePosts: [],
      profileUpvotedPosts: [],
      followedCountries: [],
      followedMakes: [],
      followedModels: [],
      hasPreferences: false
    }
  },
  methods: {
    GetProfleDetails: async function() {
        await Profile.get('/ProfileRetrieval/Profile', {params: { username: this.$cookies.get("username")}}).then((response) =>{
            console.log(`Server replied with: ${response.data}`),
            this.profile = response.data;
        }).catch((e)=>{
            console.log(e)
        });
    },
    GetUserPosts: async function() {
        await Profile.get('/ProfileRetrieval/GetPosts', {params: {username: this.$cookies.get("username")}}).then((response) => {
            console.log(`Server replied with ${response.data}`),
            this.profilePosts = response.data["userPosts"];
        }).catch((e)=>{
            console.log(e)
        })
    },
    GetUserUpvotedPosts: async function() {
        await Profile.get('/ProfileRetrieval/ProfileUpvotePosts', {params: {username: this.$cookies.get("username")}}).then((response) => {
            console.log(`Server replied with ${response.data}`),
            this.profileUpvotedPosts = response.data["upVotedPosts"];
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
.profileTitle 
{
    font-size: 20px;
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
.profileDescriptiontext 
{
  padding-right: 3.5%;
}
.ProfileUsernameHeader
{
  padding-top: 2.5%;
}
</style>
