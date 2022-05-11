import axios from 'axios';

// Use multiple instances because there's multiple services
// Source: https://stackoverflow.com/questions/47477594/how-to-use-2-instances-of-axios-with-different-baseurl-in-the-same-app-vue-js
axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
const instanceFetch = axios.create({
    // Dev URL
    // baseURL: 'https://localhost:7047',
    // Production URL
    baseURL: 'http://motomotoca.com:5050'
    // baseURL: 'http://motomotoca.com:5051'
});

const instanceSubmit = axios.create({
    // Dev URL
    // baseURL: 'https://localhost:7050',
    // Production URL
    baseURL: 'http://motomotoca.com:5051'
});

export { instanceFetch, instanceSubmit }