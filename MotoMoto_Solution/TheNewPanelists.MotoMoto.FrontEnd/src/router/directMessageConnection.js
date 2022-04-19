import axios from 'axios';

axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
const instance = axios.create({
    //baseURL: 'https://localhost:7071/api/',
    baseURL: 'motomotoca.com:5010'
});

export {instance}