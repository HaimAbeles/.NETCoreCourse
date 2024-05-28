import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { login } from '../api/auth';
import { useAuth } from '../context/AuthContext';

const Login: React.FC = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const navigate = useNavigate();
    const { setIsAuthenticated, isAuthenticated } = useAuth();

    useEffect(() => {
        if (isAuthenticated)
            navigate('/');
    }, [isAuthenticated]);

    
    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            await login(username, password);
            setIsAuthenticated(true);
        } catch (error) {
            alert('Invalid credentials');
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <div>
                <label>
                    Username:
                    <input type="text" value={username} onChange={(e) => setUsername(e.target.value)} />
                </label>
            </div>
            <div>
                <label>
                    Password:
                    <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} />
                </label>
            </div>
            <button type="submit">Login</button>
        </form>
    );
};

export default Login;
