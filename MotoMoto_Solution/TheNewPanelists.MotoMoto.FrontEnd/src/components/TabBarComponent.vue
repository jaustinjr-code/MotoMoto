<template>
<div>
	<body>
		  <router-link to="/"><h1 class="title" v-on:onclick="home">MotoMoto</h1></router-link>
		<nav v-bind:class="active" v-on:click.prevent>
			<router-link to="/carbuilder"><a class="carbuilder" v-on:click="makeActive('carbuilder')">Car Builder</a></router-link>
			<router-link to="/parts"><a class="projects" v-on:click="makeActive('projects')">Vehicle Parts</a></router-link>
			<router-link to="/communityboard"><a class="communityboard" v-on:click="makeActive('communityboard')">Community Board</a></router-link>
			<router-link to=""><a class="contact" v-on:click="makeActive('contact')">Contact</a></router-link>
			<router-link to="/login"><a class="login" v-on:click="makeAcive('login')">Login</a></router-link>
		</nav>
	</body>
</div>
</template>

<script>
export default {
	name: 'TabBarComponent'
}
</script>
<style>

h1, h1:visited, h1:hover, h1:active {
  text-decoration: none;
  font: 30px;
  color: rgb(0, 75, 73);
  text-align: center;
  padding: 20px 0px 0px 0px;
  font-family: "Copperplate", "Papyrus";
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

nav{
  list-style-type: none;
	margin:0;
  padding: 0;
	overflow: hidden;
  background-color:rgb(0, 75, 73);
}

nav a {
	display:inline;
  float: left;
	padding: 10px 10px;
	color:#fff !important;
	font-weight:bold;
	font-size:10px;
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
	font-size:14px;
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