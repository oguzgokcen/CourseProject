import { Link } from "react-router-dom";
import { useAuth } from "../context/AuthContext";
import { useCart } from "../context/CartContext";
import { FaSearch, FaShoppingCart, FaUserCircle } from "react-icons/fa";
import { useLocation } from "react-router-dom";

export default function Navbar() {

    const { user, logout } = useAuth();
    const { cart } = useCart();
    const location = useLocation();

    return (
        <nav className="navbar navbar-expand-lg navbar-light bg-light">
            <div className="container-fluid">
                {/* Logo */}
                <a className="navbar-brand" href="/">
                    Udemy
                </a>
                <button
                    className="navbar-toggler"
                    type="button"
                    data-bs-toggle="collapse"
                    data-bs-target="#navbarNav"
                    aria-controls="navbarNav"
                    aria-expanded="false"
                    aria-label="Toggle navigation"
                >
                    <span className="navbar-toggler-icon"></span>
                </button>

                <div className="collapse navbar-collapse" id="navbarNav">
                    <ul className="navbar-nav me-auto">
                        <li className="nav-item">
                            <a className="nav-link" href="/categories">
                                Categories
                            </a>
                        </li>
                    </ul>

                    {location.pathname == "/profile" || location.pathname == "/login" ? null : (
                        <form className="d-flex me-auto w-50">
                            <input
                                type="search"
                                placeholder="Search for anything"
                                aria-label="Search"
                            />
                            <button className="btn btn-outline-secondary ms-2" type="submit">
                                <FaSearch />
                            </button>
                        </form>
                    )}

                    {/* Right Menu */}
                    <ul className="navbar-nav ms-auto">
                        {user ? (
                            <>
                            <li className="nav-item">
                                <a className="nav-link" href="/my-learning">
                                    My Learning
                                </a>
                            </li>
                            <li className="nav-item">
                                <a className="nav-link" href="/cart" style={{display: "flex", gap: "5px", alignItems: "center", position: "relative" }}>
                                    My Cart
                                    <div style={{ position: "relative" }}>
                                        <FaShoppingCart />
                                        {user && cart.length > 0 && (
                                            <span style={styles.cartBadge}>
                                                {cart.length}
                                            </span>
                                        )}
                                    </div>
                                </a>
                            </li>
                            <li className="nav-item dropdown">
                                <a
                                    className="nav-link dropdown-toggle"
                                    href="#"
                                    id="navbarDropdown"
                                    role="button"
                                    data-bs-toggle="dropdown"
                                    aria-expanded="false"
                                >
                                    <FaUserCircle /> {user.name}
                                </a>
                                <ul className="dropdown-menu" aria-labelledby="navbarDropdown">
                                    <li>
                                        <a className="dropdown-item" href="/profile">
                                            Profile
                                        </a>
                                    </li>
                                    <li>
                                        <a className="dropdown-item" href="/settings">
                                            Settings
                                        </a>
                                    </li>
                                    <li>
                                        <hr className="dropdown-divider" />
                                    </li>
                                    <li>
                                        <button
                                            className="dropdown-item"
                                            onClick={() => logout()}
                                        >
                                            Logout
                                        </button>
                                    </li>
                                    </ul>
                                </li>
                            </>
                        ) : (
                            <>
                            <li className="nav-item">
                                <a className="nav-link" href="/login">
                                    Login
                                </a>
                            </li>
                            <li className="nav-item">
                                <a className="nav-link" href="/register">
                                    Register
                                </a>
                            </li>
                            </>
                        )}
                    </ul>
                </div>
            </div>
        </nav>
    );
}

const styles = {
    cartBadge: {
        position: "absolute",
        top: "-8px",
        right: "-8px",
        backgroundColor: "#a435f0",
        color: "white",
        borderRadius: "50%",
        padding: "2px 6px",
        fontSize: "10px",
        fontWeight: "bold",
        minWidth: "16px",
        textAlign: "center"
    }
};