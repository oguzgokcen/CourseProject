import axios from "axios";
import { useState } from "react";
import { useNavigate } from "react-router-dom";


function LoginPage(){
    const[email, setEmail] = useState("");
    const[password, setPassword] = useState("");

    const navigate = useNavigate();

    const handleLogin = async (e) =>{
        e.preventDefault();
        try {
            const response = await axios.post("https://reqres.in/api/login", {
                email,
                password,
            });

            const {token} = response.data;

            localStorage.setItem("jwt", token);

            navigate("/Dashboard");
        } catch (error) {
            if(error.response && error.response.status === 400){
                alert("Invalid credentials");
            }else{
                console.error("Login failed", error);
                alert("An error occured. Plase try again");
            }
            
        }
    }

    return(
        <div>
            <h2>Login</h2>
            <form onSubmit={handleLogin}>
                <input
                    type="email"
                    placeholder="email"
                    value={email}
                    onChange={(e)=>setEmail(e.target.value)}
                ></input>
                <input
                    type="password"
                    placeholder="Password"
                    value={password}
                    onChange={(e)=>setPassword(e.target.value)}
                ></input>
                <button type="submit">Login</button>
            </form>
        </div>
    );
}
export default LoginPage;