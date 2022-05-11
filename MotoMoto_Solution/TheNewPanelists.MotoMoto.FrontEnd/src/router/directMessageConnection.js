import axios from 'axios';

axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
axios.defaults.headers.common['Access-Control-Allow-Methods'] = 'DELETE, POST, GET, OPTIONS';
axios.defaults.headers.common['Access-Control-Allow-Headers'] = 'Content-Type, Authorization, X-Requested-With';
const instance = axios.create({
    baseURL: 'https://localhost:7071/api/'
    // baseURL: 'http://motomotoca.com:5010'
});
export {instance}
