import axios from 'axios';

const baseUrl = 'http://localhost:36196';

export const login = async (name: string, password: string) => {
    return await axios.post(
        `${baseUrl}/api/users/login`,
        { name, password },
        { withCredentials: true }
    );
};

export const getHeader = async () => {
    return await axios.post(`${baseUrl}/api/home/getheader`, { withCredentials: true });
};

export const logout = async () => {
    return await axios.get(`${baseUrl}/api/users/logout`, { withCredentials: true });
}
