import { createContext, useState,useContext } from "react";
import { useNavigate } from "react-router-dom";
import { jwtDecode } from "jwt-decode";

const AuthContext = createContext();

export const useAuth = () => useContext(AuthContext);

export const AuthProvider = ({children}) => {

    const [user, setUser] = useState(() => {
        const storedUser = localStorage.getItem("user");
        if (storedUser) {
            const parsedUser = JSON.parse(storedUser);
            return {
                ...parsedUser,
                role: parsedUser['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']
            };
        }
        return null;
    });
    const navigate = useNavigate();

    const login = (token) => {
        if(token){
            localStorage.setItem("token", token.accessToken);
            const decoded = jwtDecode(token.accessToken);
            const userWithRole = {
                ...decoded,
                role: decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']
            };
            localStorage.setItem("user", JSON.stringify(decoded));
            setUser(userWithRole);
            localStorage.setItem("refreshToken", token.refreshToken);
        }
    }

    const logout = () => {
        setUser(null);
        localStorage.removeItem("token");
        localStorage.removeItem("refreshToken");
        localStorage.removeItem("user");
        navigate("/login");
    }

    return(
        <AuthContext.Provider value={{user, login, logout}}>
            {children}
        </AuthContext.Provider>
    );
}
