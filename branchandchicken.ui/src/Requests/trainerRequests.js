import axios from 'axios';
const baseUrl = 'https://localhost:44392/api';

const getAllTrainers = () => new Promise((resolve, reject) => {
    axios.get(`${baseUrl}/trainers`)
        .then(result => resolve(result.data))
        .catch(err => reject(err));
});

export default getAllTrainers;
