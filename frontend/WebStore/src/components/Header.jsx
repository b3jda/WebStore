import React, { useState } from "react";
import { useNavigate, Link } from "react-router-dom";
import useAuth from "../hooks/useAuth";
import { useCart } from "../context/CartContext.jsx";
import { FaShoppingCart, FaUserCircle, FaSearch } from "react-icons/fa"; // Icons

function Header() {
  const { user, logout } = useAuth();
  const { cart } = useCart();
  const [brand, setBrand] = useState("");
  const [size, setSize] = useState("");
  const navigate = useNavigate();

  const handleSearch = () => {
    const params = new URLSearchParams();
    if (brand.trim()) params.set("brand", brand.trim());
    if (size.trim()) params.set("size", size.trim());
    navigate(`/search?${params.toString()}`);
  };

  const handleLogout = () => {
    logout();
    navigate("/login");
  };

  return (
    <header className="bg-gray-800 shadow-md py-2">
      <div className="container mx-auto flex justify-between items-center px-6">
        
        {/* Left Section: Logo & Cart */}
        <div className="flex items-center space-x-6">
          {/* Logo */}
          <Link to="/" className="text-2xl font-extrabold text-white tracking-wide">
          Simpleté
          </Link>

          {/* Cart */}
          <Link to="/cart" className="relative flex items-center text-white text-lg">
            <FaShoppingCart size={22} className="hover:text-gray-300 transition" />
            {cart.length > 0 && (
              <span className="bg-red-500 text-white text-xs font-bold rounded-full px-2 absolute -top-2 -right-3">
                {cart.length}
              </span>
            )}
          </Link>
        </div>



        {/* Center: Filter Bar */}
        <div className="flex items-center space-x-4 bg-gray-700 p-2 rounded-md shadow-md">
          {/* Brand Filter */}
          <div>
            <label className="block text-xs font-semibold text-gray-300">Brand</label>
            <select
              className="w-32 p-1 rounded-md border border-gray-600 bg-gray-800 text-white focus:ring-2 focus:ring-blue-400"
              value={brand}
              onChange={(e) => setBrand(e.target.value)}
            >
              <option value="">All Brands</option>
              <option value="Uggs">Uggs</option>
              <option value="Nike">Nike</option>
              <option value="TommyHilfiger">TommyHilfiger</option>
              <option value="Zara">Zara</option>
            </select>
          </div>

          {/* Size Filter */}
          <div>
            <label className="block text-xs font-semibold text-gray-300">Size</label>
            <select
              className="w-32 p-1 rounded-md border border-gray-600 bg-gray-800 text-white focus:ring-2 focus:ring-blue-400"
              value={size}
              onChange={(e) => setSize(e.target.value)}
            >
              <option value="">All Sizes</option>
              <option value="36">36</option>
              <option value="38">38</option>
              <option value="S">S</option>
              <option value="M">M</option>
              <option value="L">L</option>
              <option value="XL">XL</option>
            </select>
          </div>

          {/* Search Button */}
          <button
            onClick={handleSearch}
            className="flex items-center bg-blue-500 text-white px-4 py-1 rounded-md hover:bg-blue-600 transition relative top-[7px]"
          >
            <FaSearch className="mr-1 text-white" /> Search
          </button>
        </div>

        {/* Right Section: User Info & Logout */}
        <div className="flex items-center space-x-4">
          {user ? (
            <div className="flex items-center space-x-2">
              <FaUserCircle size={20} className="text-white" />
              <p className="text-white text-sm relative top-[7px]">{user.email}</p> {/* Adjusted alignment */}
              <button
                onClick={handleLogout}
                className="bg-red-500 text-white px-3 py-1 rounded-md hover:bg-red-600 transition text-sm"
              >
                Logout
              </button>
            </div>
          ) : (
            <>
              <Link to="/login" className="bg-white text-gray-800 px-3 py-1 rounded-md hover:bg-gray-200 transition text-sm">
                Login
              </Link>
              <Link to="/register" className="bg-white text-gray-800 px-3 py-1 rounded-md hover:bg-gray-200 transition text-sm">
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
