import React from "react";

function Footer() {
  return (
    <footer className="bg-gradient-to-t from-gray-900 via-gray-800 to-gray-700 text-white mt-auto">
      <div className="container mx-auto py-8 text-center">
        {/* Melting Effect */}
        <div className="w-full h-2 bg-gradient-to-r from-teal-400 via-blue-500 to-purple-600 rounded-t-lg mb-4"></div>

        {/* Content */}
        <p className="text-sm">
          &copy; {new Date().getFullYear()}{" "}
          <span className="font-bold">WebStore</span>. All rights reserved.
        </p>
        <p className="text-sm mt-2">
          Made by{" "}
          <a
            href="#"
            className="text-teal-400 hover:text-teal-300 hover:underline transition-colors"
          >
            B.M
          </a>
        </p>

        {/* Social Icons */}
      </div>
    </footer>
  );
}

export default Footer;
