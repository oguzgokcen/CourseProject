import React, { useState, useEffect } from 'react';
import { Link } from "react-router-dom";
import { useAuth } from "../context/AuthContext";
import { useCart } from "../context/CartContext";
import { FaSearch, FaShoppingCart, FaUserCircle } from "react-icons/fa";
import { useLocation, useNavigate } from "react-router-dom";
import Popper from './Popper';
import CategoryList from './CategoryList';
import apiClient from '../api/apiClient';
import { endpoints } from '../api/endpoints';

export default function Navbar() {

    const { user, logout } = useAuth();
    const { cart } = useCart();
    const location = useLocation();
    const navigate = useNavigate();
    const [searchQuery, setSearchQuery] = useState('');
    const [categories, setCategories] = useState([]);

    useEffect(() => {
        fetchCategories();
    }, []);

    const fetchCategories = async () => {
        try {
            const response = await apiClient.get(`${endpoints.getAllCategory}`);
            setCategories(response.data.data);
        } catch (error) {
            console.error('Error fetching categories:', error);
        }
    };

    const handleSearch = (e) => {
        e.preventDefault();
        const formattedQuery = searchQuery.trim().replace(/\s+/g, '+');
        navigate(`/search?q=${formattedQuery}`);
        setSearchQuery('');
    };

    const isTeacher = user && user.role === 'Teacher';

    return (
        <nav className="navbar navbar-expand-lg navbar-light bg-light">
            <div className="container-fluid">
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
                            <Popper
                                trigger={
                                    <a className="nav-link" style={{ cursor: 'pointer' }}>
                                        Categories
                                    </a>
                                }
                                content={<CategoryList categories={categories} />}
                            />
                        </li>
                    </ul>

                    {location.pathname == "/profile" || location.pathname == "/login" ? null : (
                        <form className="d-flex me-auto w-50" onSubmit={handleSearch}>
                            <input
                                type="search"
                                placeholder="Search for anything"
                                aria-label="Search"
                                value={searchQuery}
                                onChange={(e) => setSearchQuery(e.target.value)}
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
                                {isTeacher && (
                                    <li className="nav-item">
                                        <a className="nav-link" href="/teacher">
                                            My Courses
                                        </a>
                                    </li>
                                )}
                                <li className="nav-item">
                                    <a className="nav-link" href="/cart" style={{ display: "flex", gap: "5px", alignItems: "center", position: "relative" }}>
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
                                <li className="nav-item dropdown" style={styles.dropdownContainer}>
                                    <a
                                        className="nav-link dropdown-toggle"
                                        href="#"
                                        id="navbarDropdown"
                                        role="button"
                                        data-bs-toggle="dropdown"
                                        aria-expanded="false"
                                        style={styles.dropdownToggle}
                                    >
                                        <FaUserCircle /> {user.name}
                                    </a>
                                    <ul className="dropdown-menu dropdown-menu-end" style={styles.dropdownMenu} aria-labelledby="navbarDropdown">
                                        <li>
                                            <a className="dropdown-item" href="/profile" style={styles.dropdownItem}>
                                                Profile
                                            </a>
                                        </li>
                                        <li>
                                            <a className="dropdown-item" href="/payment-history" style={styles.dropdownItem}>
                                                Payment History
                                            </a>
                                        </li>
                                        <li>
                                            <hr className="dropdown-divider" />
                                        </li>
                                        <li>
                                            <button
                                                className="dropdown-item"
                                                onClick={() => logout()}
                                                style={styles.dropdownItem}
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
                                    <a className="nav-link" href="/signup">
                                        Sign Up
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
    },
    dropdownContainer: {
        position: 'relative'
    },
    dropdownToggle: {
        display: 'flex',
        alignItems: 'center',
        gap: '8px',
        padding: '8px 16px',
        whiteSpace: 'nowrap'
    },
    dropdownMenu: {
        minWidth: '200px',
        padding: '8px 0',
        marginTop: '8px',
        boxShadow: '0 2px 4px rgba(0,0,0,0.08), 0 4px 12px rgba(0,0,0,0.08)'
    },
    dropdownItem: {
        padding: '8px 16px',
        fontSize: '14px',
        color: '#1c1d1f',
        cursor: 'pointer',
        ':hover': {
            backgroundColor: '#f7f9fa'
        }
    }
};