import axios from 'axios';

axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
const instance = axios.create({
    // baseURL: 'https://localhost:44335/'//EventList/GetEvents' // Used for testing in local machine

    baseURL: 'http://motomotoca.com:5020' // Used for connecting to Web API in backend
});

export {instance}