axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
axios.defaults.headers.common['Access-Control-Allow-Methods'] = 'DELETE, POST, GET, OPTIONS';
axios.defaults.headers.common['Access-Control-Allow-Headers'] = 'Content-Type, Authorization, X-Requested-With';

import axios from 'axios';

const instance = axios.create({
    baseURL: 'https://localhost:7006/Api'
    //baseURL: 'http://localhost:5006'
});

export {instance}