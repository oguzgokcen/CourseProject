import { createContext, useState,useContext } from "react";
import { useNavigate } from "react-router-dom";
import { jwtDecode } from "jwt-decode";

const AuthContext = createContext();

export const useAuth = () => useContext(AuthContext);

export const AuthProvider = ({children}) => {

    const [user, setUser] = useState(() => {
        const storedUser = localStorage.getItem("user");
        return storedUser ? JSON.parse(storedUser) : null;
    });
    const navigate = useNavigate();

    const login = (token) => {
        if(token){
            localStorage.setItem("token", token);
            const decoded = jwtDecode(token);
            localStorage.setItem("user", JSON.stringify(decoded));
            setUser(decoded);
        }
    }

    const logout = () => {
        setUser(null);
        localStorage.removeItem("token");
        localStorage.removeItem("user");
        navigate("/login");
    }

    return(
        <AuthContext.Provider value={{user, login, logout}}>
            {children}
        </AuthContext.Provider>
    );
}
