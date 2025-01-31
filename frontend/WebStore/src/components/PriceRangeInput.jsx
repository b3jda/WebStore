import React, { useState } from "react";

const PriceRangeInput = ({ min = 0, max = 1000, step = 1, onChange }) => {
  const [values, setValues] = useState({ from: min, to: max });

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    const newValues = { ...values, [name]: value };
    setValues(newValues);
    if (onChange) onChange(newValues);
  };

  return (
    <div className="w-full p-5 bg-white/60 backdrop-blur-md rounded-xl shadow-lg border border-gray-200">
      
      {/* Title */}
      <h3 className="text-lg font-bold text-gray-800 text-center mb-4">
        Select Price Range
      </h3>

      {/* Input Fields */}
      <div className="flex items-center justify-between gap-4">
        
        {/* From Input */}
        <div className="flex flex-col items-center w-1/2">
          <label className="text-sm font-medium text-gray-700 mb-1">From:</label>
          <input
            type="number"
            name="from"
            value={values.from}
            onChange={handleInputChange}
            min={min}
            max={values.to}
            step={step}
            className="p-3 text-lg font-semibold text-gray-800 bg-white rounded-xl border-2 border-transparent 
            shadow-md focus:border-blue-500 focus:ring-2 focus:ring-blue-300 transition-all w-full text-center"
          />
        </div>

        {/* To Input */}
        <div className="flex flex-col items-center w-1/2">
          <label className="text-sm font-medium text-gray-700 mb-1">To:</label>
          <input
            type="number"
            name="to"
            value={values.to}
            onChange={handleInputChange}
            min={values.from}
            max={max}
            step={step}
            className="p-3 text-lg font-semibold text-gray-800 bg-white rounded-xl border-2 border-transparent 
            shadow-md focus:border-blue-500 focus:ring-2 focus:ring-blue-300 transition-all w-full text-center"
          />
        </div>
      </div>

      {/* Price Display */}
      <div className="mt-5 text-center text-gray-700 text-lg font-semibold">
        <span className="px-4 py-2 bg-gray-100 rounded-lg shadow">
          ${values.from} - ${values.to}
        </span>
      </div>
    </div>
  );
};

export default PriceRangeInput;
