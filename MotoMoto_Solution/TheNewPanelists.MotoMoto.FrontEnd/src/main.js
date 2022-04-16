import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import VueCookies from 'vue3-cookies'

import 'bootstrap/dist/css/bootstrap.min.css'

createApp(App).use(router).use(VueCookies, {
    expireTimes: "1d",
    path: "/",
    domain: "",
    secure: true,
    sameSite: "None"
}).use(store).mount('#app')



