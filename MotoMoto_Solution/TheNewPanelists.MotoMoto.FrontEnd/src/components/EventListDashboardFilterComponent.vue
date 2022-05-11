<template>
    <!-- <div class = Filter></div> -->
    <div class="row">
        <div class="Filters">
            <h1>Filters</h1>
                <input type="checkbox" value="upvotes" v-model="selected">Upvotes
                <input type="checkbox" value="carType" v-model="selected">Car Type
                <input type="checkbox" value="newest" v-model="selected">Newest
                <input type="checkbox" value="oldest" v-model="selected">Oldest
        </div>
    </div>
</template>

<script>
export default {

}

</script>

<style>
.Filters {
    padding-bottom: 50px;
}
</style>