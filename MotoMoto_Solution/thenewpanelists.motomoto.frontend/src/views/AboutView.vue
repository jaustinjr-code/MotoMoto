<template>
  <div class="about">
    <TabBarComponent/>
    <div class='about__content'>
      <h1>About</h1>
      <a href='https://github.com/bobdidley/MotoMoto'><img width='50' height='50' src='https://upload.wikimedia.org/wikipedia/commons/9/91/Octicons-mark-github.svg' /></a>
      <p class='about-us__text'>
      MotoMoto is a motor vehicle-centric application that promotes community, education, and products 
      for every car enthusiast. Through MotoMoto we allow car lovers to generate part lists for any car build they could dream,
      create posts discussing their builds, along with so many other amazing features! If you like cars, take a look around. We're sure
      you will want to stay a while!
      </p>
    </div>  
  </div>
</template>

<style>
.about-us__text {
  display: block;
  margin-top: 20px;
  text-align: left;
}

.about__content {
  margin-left: 25%;
  margin-right: 25%;
}

</style>

<script>
import TabBarComponent from '../components/TabBarComponent.vue'
export default {
  name: 'AboutView',
  components: 
  {
    TabBarComponent,
  }
}
</script>


