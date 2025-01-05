import { useAuth } from "../context/AuthContext";
import { Navigate } from "react-router-dom";
const TeacherProtectedRoute = ({children}) => {
    const {user} = useAuth();
    if(user.role === 'Teacher'){
        return children;
    }
    return <Navigate to="/unauthorized"/>;
}

export default TeacherProtectedRoute;