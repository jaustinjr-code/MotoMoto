<template>
<div>
	<body class="tab-bar-body">
		  <router-link class="toHome" to="/"><h1 class="title" v-on:onclick="home">MotoMoto</h1></router-link>
		<nav class="navBar" v-bind:class="active" v-on:click.prevent>
			<router-link to="/CarBuilder"><a class="carbuilder">Car Builder</a></router-link>
			<router-link to="/parts"><a class="projects">Vehicle Parts</a></router-link>
			<router-link to="/eventlist"><a class="eventlist">Event List</a></router-link>
			<router-link to="/communityboard"><a class="communityboard">Community Board</a></router-link>
			<router-link to="/About"><a class="about">About</a></router-link>
			<div v-if="loggedIn() === false">
				<router-link to="/login"><a class="login">Login / Register</a></router-link>
			</div>
			<div v-else>
				<router-link :to="{name: 'UserProfile', params: { username: getLoginCredential()}}"><a class="profile">Profile</a></router-link>
			</div>
		</nav>
	</body>
</div>
</template>

<script>
import { useCookies } from "vue3-cookies";

export default {
	name: 'TabBarComponent',
	data() {
		return {
			userId: this.$cookies.get("userId"),
			username: this.$cookies.get("username"),
		}
	},
	// mounted() {
	// 	// this.getLoginCredential();
	// },
	methods: {
		loggedIn: function () {
			if (this.$cookies.get("username") === "guest" || this.$cookies.get("username") === null)
			{
				return false;
			}
			return true;
		},
		getLoginCredential: function () {
			this.username = this.$cookies.get("username")
			return this.username;
		},
		makeActive(paths) {
			this.$router.push({path: paths});
		}
	},
}
</script>
<style>
.toHome {
	text-decoration: none !important;
}
.title {
	text-decoration: none;
	font-size: 50px;
	font-weight: bold;
	color: rgb(0, 75, 73);
	text-align: center;
	padding: 20px 0px 0px 0px;
	font-family: "Copperplate";
}

body{
	padding-top: 10px;
	padding-bottom: 10px;
	font:15px/1.3 'Open Sans', sans-serif;
	color: #5e5b64;
	text-align:center;
}

a, a:visited {
	outline:none;
	color:#389dc1;
}

section, footer, header, aside, nav{
	display: block;
}

nav {
  	list-style-type: none;
	margin:0;
	padding: 0;
	overflow: hidden;
  	background-color:rgb(0, 75, 73);
}

.navBar a {
	display:inline;
  	float: left;
	padding: 13px 13px;
	color:#fff !important;
	font-weight:bold;
	font-size:12px;
	text-decoration:none !important;
	line-height:1;
	text-transform: uppercase;
	background-color:transparent;
	-webkit-transition:background-color 0.25s;
	-moz-transition:background-color 0.25s;
	transition:background-color 0.25s;
}

nav a:first-child{
	border-radius:2px 0 0 2px;
}

nav a:last-child{
  float: right;
	border-radius:0 2px 2px 0;
}

nav.home .home,
nav.projects .projects,
nav.services .services,
nav.contact .contact{
	background-color:#e35885;
}

p {
	display:inline;
	font-size:15px;
	font-weight:bold;
	color:#7d9098;
}

p b{
  display: inline;
  color:#ffffff;
	display:block;
	padding:5px 10px;
	background-color:#c4d7e0;
	border-radius:2px;
	text-transform:uppercase;
	font-size:18px;
}

</style>