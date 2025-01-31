import React from "react";
import { FaGithub, FaLinkedin, FaEnvelope } from "react-icons/fa";

function Footer() {
  return (
    <footer className="bg-navy-900 text-gray-100 mt-auto">
      {/* Melting Effect Border (matches header gradient) */}
      <div className="w-full h-1 bg-gradient-to-r from-teal-400 via-blue-500 to-purple-600 animate-pulse"></div>

      <div className="container mx-auto py-8 px-4">
        <div className="grid grid-cols-1 md:grid-cols-3 gap-8 mb-8">
          {/* Brand Section */}
          <div className="text-center md:text-left">
            <h3 className="text-xl font-bold mb-4 text-gold-500">Simpleté</h3>
            <p className="text-sm text-gray-400">
              Where minimalism meets sophistication
            </p>
          </div>

          {/* Quick Links */}
          <div className="text-center">
            <h4 className="font-semibold mb-4 text-gold-500">Navigation</h4>
            <ul className="space-y-2">
              <li><a href="#" className="hover:text-gold-500 transition-colors text-gray-300">Collections</a></li>
              <li><a href="#" className="hover:text-gold-500 transition-colors text-gray-300">About Us</a></li>
              <li><a href="#" className="hover:text-gold-500 transition-colors text-gray-300">Contact</a></li>
            </ul>
          </div>

          {/* Social Links */}
          <div className="text-center md:text-right">
            <h4 className="font-semibold mb-4 text-gold-500">Connect</h4>
            <div className="flex justify-center md:justify-end space-x-4">
              <a href="#" className="p-2 hover:text-gold-500 text-gray-300 transition-colors">
                <FaGithub size={24} />
              </a>
              <a href="#" className="p-2 hover:text-gold-500 text-gray-300 transition-colors">
                <FaLinkedin size={24} />
              </a>
              <a href="#" className="p-2 hover:text-gold-500 text-gray-300 transition-colors">
                <FaEnvelope size={24} />
              </a>
            </div>
          </div>
        </div>

        {/* Copyright Section */}
        <div className="border-t border-navy-700 pt-6 text-center">
          <p className="text-sm text-gray-400">
            &copy; {new Date().getFullYear()} Simpleté. All rights reserved.
            <br />
            Crafted by{" "}
            <a
              href="#"
              className="text-gold-500 hover:text-gold-400 transition-colors"
            >
              B.M
            </a>
          </p>
        </div>
      </div>
    </footer>
  );
}

export default Footer;