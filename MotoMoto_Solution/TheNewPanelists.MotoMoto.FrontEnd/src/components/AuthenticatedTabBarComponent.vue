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
    
}
</script>

<style>

</style>