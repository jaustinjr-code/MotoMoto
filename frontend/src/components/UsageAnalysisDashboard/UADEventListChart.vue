<template>
<div>
  <h3>Event List</h3>
  <button @click="pressed">Press</button>
  <p>{{ table }}</p>
  </div>
</template>

<script>
export default {
  methods: {
    pressed: function() {
      // console.log("NOo")
            fetch("https://localhost:7197/UsageAnalysisDashboard/GetAnalytics")
            .then((response) => response.json())
            .then((d) => {
              this.table = d;
            });
    }
  },
  data() {
      return {
          table: "Event Data"
      }
  }
}
</script>

<style>

</style>