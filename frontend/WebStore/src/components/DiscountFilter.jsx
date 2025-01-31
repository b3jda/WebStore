import React, { useState, useEffect } from "react";

const DiscountFilter = ({ onToggle, showDiscounted }) => {
  const [isDiscounted, setIsDiscounted] = useState(showDiscounted);

  // Sync internal state with prop
  useEffect(() => {
    setIsDiscounted(showDiscounted);
  }, [showDiscounted]);

  const handleToggle = () => {
    const newState = !isDiscounted;
    setIsDiscounted(newState);
    if (onToggle) onToggle(newState); // Pass state to parent
  };

  return (
    <div className="w-full p-5 bg-white/60 backdrop-blur-md rounded-xl shadow-lg border border-gray-200">
      {/* Title */}
      <h3 className="text-lg font-bold text-gray-800 text-center mb-4">
        Show Only Discounted Products
      </h3>

      {/* Toggle Switch */}
      <div className="flex justify-center items-center">
        <span className="text-gray-700 font-medium">All Products</span>

        {/* Toggle Button */}
        <button
          onClick={handleToggle}
          className={`mx-3 w-14 h-8 flex items-center bg-gray-300 rounded-full p-1 transition duration-300 
            ${isDiscounted ? "bg-green-500" : "bg-gray-300"}`}
        >
          <div
            className={`w-6 h-6 bg-white rounded-full shadow-md transform transition duration-300 
              ${isDiscounted ? "translate-x-6" : "translate-x-0"}`}
          ></div>
        </button>

        <span className="text-gray-700 font-medium">Discounted</span>
      </div>
    </div>
  );
};

export default DiscountFilter;