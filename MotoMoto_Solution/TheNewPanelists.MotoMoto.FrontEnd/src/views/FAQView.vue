<template>
<div>
    <TabBarComponent/>
    <h1>Frequently Asked Questions</h1>
    <div class="section">
        <h2>Why can't I register?</h2>
        <ul>
            <li>You may have entered an incorrect email address</li>
            <li>You may have entered a password that does not meet the requirements</li>
            <li>It can be an error on our side (We're sorry!). If it's on our end, check back soon.</li>
        </ul>
    </div>
    <div class="section">
        <h2>Why can't I login?</h2>
        <ul>
            <li>You may have entered the wrong email address</li>
            <li>You may have entered the wrong password</li>
            <li>It can be an error on our side (We're sorry!). If it's on our end, check back soon.</li>
        </ul>
    </div>
    <div class="section">
        <h2>What do I do if I can't remember my login information?</h2>
        <ul>
            <li></li>
        </ul>
    </div>
    <div class="section">
        <h2>What do I do if I can't remember my login information?</h2>
        <ul>
            <li></li>
        </ul>
    </div>
    <div class="section">
        <h2>How many part flags does it take for a part to get the label of "Non-Recommended"?</h2>
        <ul>
            <li>100</li>
        </ul>
    </div>
    <div class="section">
        <h2>Can I downvote a part flag in the car builder?</h2>
        <ul>
            <li>
                No, because user reviews are meant to monitor the validity of the user who
                created the flag. If a user hasn't put a part in a build yet, they have
                no idea whether or not that part is incompatible.
            </li>
        </ul>
    </div>
    <div class="section">
        <h2>Why is there no part flagging for car posts?</h2>
        <ul>
            <li>
                The car posting feature of MotoMoto has not been created yet!
                For this reason, reviewing a flag is not available to a user.
            </li>
        </ul>
    </div>
    <div class="section">
        <h2>How do I compare vehicle parts with each other?</h2>
        <ul>
            <li>
                Each user is able to select up to 2 parts at our current stage but, we hope
                to include more detailed information on vehicle parts but, on the second click
                you should be re-directed to the comparison page
            </li>
        </ul>
    </div>
    <div class="section">
        <h2>What is Parts for?</h2>
        <ul>
            <li>
                Parts is a detailed page where all users can view amazon linked products and 
                view vehicle part price data over time. This is a great way to figure out what
                to shop for and compare prices amongst other competitors sold on amazon.
            </li>
        </ul>
    </div>
</div>
</template>


<script>
import TabBarComponent from '../components/TabBarComponent.vue'

export default {
    name: 'FAQ',
    components: {
        TabBarComponent
    }
}
</script>

<style scoped>
.section
{
    text-align: left;
    margin-top: 25px;
    margin-left: 250px;
}

.section li {
    font-size: 15px;
}
</style>