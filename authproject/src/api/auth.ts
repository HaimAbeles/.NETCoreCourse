import axios from 'axios';

const baseUrl = 'http://localhost:36196';

export const login = async (username: string, password: string) => {
    return await axios.post(
        `${baseUrl}/api/users/login`,
        { username, password },
        { withCredentials: true }
    );
};

export const getHeader = async () => {
    return await axios.get(`${baseUrl}/api/home/getheader`, { withCredentials: true });
};

export const logout = async () => {
    return await axios.get(`${baseUrl}/api/users/logout`, { withCredentials: true });
}
