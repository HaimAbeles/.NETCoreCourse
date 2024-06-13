import { useEffect, useState } from "react";
import { onReceiveMessage, sendMessage, start } from "../signalRService";
import './Chat.css';

type Message = {
    user: string;
    message: string;
}

export default function Chat() {
    const [message, setMessage] = useState<string>("");
    const [messages, setMessages] = useState<Message[]>([]);

    const onNewMessages = (user: string, message: string) => {
        debugger
        setMessages((prevMessages: any) => {
            return [...prevMessages, { user, message }]
        })
    }

    useEffect(() => {
        start();
        onReceiveMessage(onNewMessages);
    }, []);

    const handleSendMessage = () => {
        debugger
        sendMessage(message);
        setMessage("");
    };

    return (
        <div className="App">
            <input
                type="text"
                placeholder="Message"
                value={message}
                onChange={e => setMessage(e.target.value)}
            />
            <button onClick={handleSendMessage}>Send</button>
            <ul>
                {messages.map((msg, index) => (
                    <li key={index} className={`message ${sessionStorage.getItem('userName') == msg.user ? 'currentUser' : ''}`}>
                        {msg.message}
                    </li>
                ))}
            </ul>
        </div>
    );
}