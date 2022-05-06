import axios from 'axios';

axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
const instance = axios.create({
    // baseURL: 'https://localhost:50841'//MeetingPointDirections/GetEventLocation' // Used for testing in local machine
    // https://localhost:50841/GetEventLocation?eventID=?

    baseURL: 'http://motomotoca.com:5021' // Used for connecting to Web API in backend
});

export {instance}