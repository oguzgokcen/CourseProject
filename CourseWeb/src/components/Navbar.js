import { Link } from "react-router-dom";
import { useAuth } from "../context/AuthContext";
import { FaSearch, FaShoppingCart, FaUserCircle } from "react-icons/fa";

export default function Navbar() {

    const { user, logout } = useAuth();

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

                {/* Navbar content */}
                <div className="collapse navbar-collapse" id="navbarNav">
                    <ul className="navbar-nav me-auto">
                        {/* Categories Link */}
                        <li className="nav-item">
                            <a className="nav-link" href="/categories">
                                Categories
                            </a>
                        </li>
                    </ul>

                    {/* Search Bar */}
                    <form className="d-flex me-auto w-50">
                        <input
                            className="form-control"
                            type="search"
                            placeholder="Search for anything"
                            aria-label="Search"
                        />
                        <button className="btn btn-outline-secondary ms-2" type="submit">
                            <FaSearch />
                        </button>
                    </form>

                    {/* Right Menu */}
                    <ul className="navbar-nav ms-auto">
                        <li className="nav-item">
                            <a className="nav-link" href="/my-learning">
                                My Learning
                            </a>
                        </li>
                        <li className="nav-item">
                            <a className="nav-link" href="/teach">
                                Teach
                            </a>
                        </li>
                        <li className="nav-item">
                            <a className="nav-link" href="/cart">
                                <FaShoppingCart />
                            </a>
                        </li>

                        {/* User Profile Dropdown */}
                        {user ? (
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
                        ) : (
                            <li className="nav-item">
                                <a className="nav-link" href="/login">
                                    Login
                                </a>
                            </li>
                        )}
                    </ul>
                </div>
            </div>
        </nav>
    );
}