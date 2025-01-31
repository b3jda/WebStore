import React, { useEffect, useState } from "react";
import useAuth from "../hooks/useAuth";
import { useCart } from "../context/CartContext.jsx";

// Import local images from the assets folder
import tshirtImage from "../assets/tshirt.jpg";
import trousersImage from "../assets/trousers.jpg";
import sneakersImage from "../assets/uggs.jpg";
import jacketImage from "../assets/jacket.jpg";
import sneakerssImage from "../assets/sneakers.jpg";

function ProductCard({ product, onUpdateProduct }) {
  const { addToCart } = useCart();
  const [currentQuantity, setCurrentQuantity] = useState(product.quantity);
  const [isAdded, setIsAdded] = useState(false);
  const [error, setError] = useState(null);

  // Dynamically determine the image based on product name
  const getImage = (name) => {
    if (name.toLowerCase().includes("tshirt")) return tshirtImage;
    if (name.toLowerCase().includes("trousers")) return trousersImage;
    if (name.toLowerCase().includes("uggs")) return sneakersImage;
    if (name.toLowerCase().includes("sneakers")) return sneakerssImage;
    return jacketImage;
  };

  // Fetch updated quantity when the component loads
  useEffect(() => {
    const fetchProductQuantity = async () => {
      try {
        const response = await fetch(
          `http://localhost:5205/api/Product/${product.id}/quantity`
        );
        if (!response.ok) {
          throw new Error(`Failed to fetch product quantity for ID: ${product.id}`);
        }
        const data = await response.json();
        setCurrentQuantity(data.currentQuantity);
      } catch (err) {
        setError(err.message);
      }
    };

    fetchProductQuantity();
  }, [product.id]);

  const handleAddToCart = () => {
    addToCart(product);
    setIsAdded(true);
    setTimeout(() => setIsAdded(false), 1500);
  };

  return (
    <div className="bg-white rounded-lg shadow-md overflow-hidden hover:shadow-lg transition-shadow relative">
      {/* Discount Ribbon */}
      {product.isDiscounted && (
        <div className="absolute top-0 right-0 bg-red-600 text-white text-sm font-semibold px-4 py-2 transform rotate-45 translate-x-8 -translate-y-2 shadow-lg">
          -{product.discountPercentage}%
        </div>
      )}

      {/* Product Image */}
      <div className="relative">
        <img
          src={getImage(product.name)}
          alt={product.name}
          className="w-full h-48 object-cover"
        />
        {product.isDiscounted && (
          <div className="absolute top-0 left-0 bg-red-600 text-white text-xs font-semibold px-3 py-1 rounded-br-lg">
          DISCOUNTED  -{product.discountPercentage}%
        </div>
        )}
      </div>
      {/* Product Details */}
      <div className="p-4">
        <h3 className="text-lg font-bold text-gray-800">{product.name}</h3>
        
        {/* Additional Details */}
        <div className="space-y-1 mt-2">
          <p className="text-sm text-gray-500">Brand: {product.brandName}</p>
          <p className="text-sm text-gray-500">Category: {product.categoryName}</p>
          <p className="text-sm text-gray-500">Size: {product.sizeName}</p>
          <p className="text-sm text-gray-500">Gender: {product.genderName}</p>
          <p className="text-sm text-gray-500">
            Quantity: {currentQuantity}
            {error && <span className="text-red-500"> ({error})</span>}
          </p>
          <p className="text-sm font-bold text-gray-800">Price: {product.price}</p>
        </div>

        {/* Add to Cart Button */}
        <button
          className={`mt-4 w-full py-2 rounded-md transition-colors duration-200 ${
            isAdded ? "bg-gold-600" : "bg-gold-500 hover:bg-gold-600"
          } text-navy-900 font-medium`}
          onClick={handleAddToCart}
        >
          {isAdded ? (
            <span className="flex items-center justify-center">
              <svg 
                xmlns="http://www.w3.org/2000/svg" 
                className="h-5 w-5 mr-2" 
                viewBox="0 0 20 20" 
                fill="currentColor"
              >
                <path fillRule="evenodd" d="M16.707 5.293a1 1 0 010 1.414l-8 8a1 1 0 01-1.414 0l-4-4a1 1 0 011.414-1.414L8 12.586l7.293-7.293a1 1 0 011.414 0z" clipRule="evenodd" />
              </svg>
              Added to Cart
            </span>
          ) : (
            "Add to Cart"
          )}
        </button>
      </div>
      
    </div>
  );
}

export default ProductCard;