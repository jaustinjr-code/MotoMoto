import axios from 'axios';

axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
const instance = axios.create({
    baseURL: 'https://api.nhtsa.gov/products/'
});

export {instance}