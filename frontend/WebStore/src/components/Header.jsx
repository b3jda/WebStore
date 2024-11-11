import React, { useState } from "react";
import { useNavigate, Link } from "react-router-dom";
import useAuth from "../hooks/useAuth";

function Header() {
  const { user, logout } = useAuth(); // Use authentication context
  const [searchQuery, setSearchQuery] = useState("");
  const [brand, setBrand] = useState("");
  const [size, setSize] = useState("");
  const [isHovered, setIsHovered] = useState(false); // For hover effect
  const navigate = useNavigate();

  const handleSearch = () => {
    const params = new URLSearchParams();
    if (searchQuery) params.append("query", searchQuery);
    if (brand) params.append("brand", brand);
    if (size) params.append("size", size);

    navigate(`/search?${params.toString()}`);
  };

  const handleLogout = () => {
    logout();
    navigate("/login");
  };

  return (
    <header className="bg-teal-500 p-4 text-white relative">
      <div className="container mx-auto flex justify-between items-center">
        {/* Logo */}
        <Link to="/" className="text-2xl font-bold text-white">
          WEBSTORE
        </Link>

        {/* Search Bar */}
        <div
          className="relative w-full max-w-md"
          onMouseEnter={() => setIsHovered(true)}
          onMouseLeave={() => setIsHovered(false)}
        >
          <input
            type="text"
            placeholder="Search for products..."
            className="w-full py-2 px-4 rounded-md text-gray-800 shadow-md focus:outline-none"
            value={searchQuery}
            onChange={(e) => setSearchQuery(e.target.value)}
          />

          {/* Dropdown for additional filters */}
          {isHovered && (
            <div className="absolute top-full left-0 w-full bg-white text-gray-800 shadow-lg rounded-md mt-2 p-4 z-10">
              <div className="mb-3">
                <label className="block text-sm font-medium">Brand</label>
                <select
                  className="w-full p-2 rounded-md border border-gray-300"
                  value={brand}
                  onChange={(e) => setBrand(e.target.value)}
                >
                  <option value="">Select Brand</option>
                  <option value="Uggs">Uggs</option>
                  <option value="Nike">Nike</option>
                  <option value="TommyHilfiger">TommyHilfiger</option>
                  <option value="Zara">Zara</option>
                </select>
              </div>
              <div className="mb-3">
                <label className="block text-sm font-medium">Size</label>
                <select
                  className="w-full p-2 rounded-md border border-gray-300"
                  value={size}
                  onChange={(e) => setSize(e.target.value)}
                >
                  <option value="">Select Size</option>
                  <option value="36">36</option>
                  <option value="38">38</option>
                  <option value="S">S</option>
                  <option value="M">M</option>
                  <option value="L">L</option>
                  <option value="XL">XL</option>
                </select>
              </div>
              <button
                className="w-full bg-teal-500 text-white py-2 rounded-md"
                onClick={handleSearch}
              >
                Search
              </button>
            </div>
          )}
        </div>

        {/* User Actions */}
        <div className="flex items-center space-x-4">
          {user ? (
            <>
              <p className="font-medium">
                Welcome, <span className="font-bold">{user.email}</span>
              </p>
              <button
                onClick={handleLogout}
                className="bg-white text-teal-500 px-4 py-2 rounded-md hover:bg-gray-100"
              >
                Logout
              </button>
            </>
          ) : (
            <>
              <Link
                to="/login"
                className="bg-white text-teal-500 px-4 py-2 rounded-md hover:bg-gray-100"
              >
                Login
              </Link>
              <Link
                to="/register"
                className="bg-white text-teal-500 px-4 py-2 rounded-md hover:bg-gray-100"
              >
                Register
              </Link>
            </>
          )}
        </div>
      </div>
    </header>
  );
}

export default Header;
