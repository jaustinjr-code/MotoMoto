import axios from 'axios';

axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
const instance = axios.create({
    // Dev URL
    baseURL: 'https://localhost:7047',
    // Production URL
    //baseURL: 'http://motomotoca.com:5010'
});

export { instance }