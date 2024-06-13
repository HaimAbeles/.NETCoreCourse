import React, { useEffect, useState } from 'react';
import { getHeader, logout } from '../api/auth';
import { useAuth } from '../context/AuthContext';
import { useNavigate } from 'react-router-dom';
import Chat from './Chat';

const Home: React.FC = () => {
    const [header, setHeader] = useState('');
    const { setIsAuthenticated } = useAuth();
    const navigate = useNavigate();

    useEffect(() => {
        fetchHeader();
    }, []);

    const fetchHeader = async () => {
        try {
            const response = await getHeader();
            setHeader(response.data);
        } catch (error) {
            console.error('Error fetching header:', error);
        }
    };

    const handleLogout = async () => {
        await logout();
        setIsAuthenticated(false);
        navigate('/');
    }

    return <>
    <h1>{header}</h1>
    <Chat />
    <button onClick={handleLogout}>התנתקות</button>
    </>;
};

export default Home;
