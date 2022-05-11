import axios from 'axios';

axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
axios.defaults.headers.common['Access-Control-Allow-Methods'] = 'DELETE, POST, GET, OPTIONS';
axios.defaults.headers.common['Access-Control-Allow-Headers'] = 'Content-Type, Authorization, X-Requested-With';
const instance = axios.create({
    baseURL: 'http://localhost:44333/api/'
    //baseURL: 'http://motomotoca.com:5070/'
});

const instanceSubmit = axios.create({
    baseURL: 'http://localhost:44333/api/'
    //baseURL: 'http://motomotoca.com:5070/'
});

const flaggingInstance = axios.create({
    // baseURL: 'https://localhost:7285/api/'
    baseURL: 'http://motomotoca.com:5555/api/'
});

export {instance, instanceSubmit, flaggingInstance}