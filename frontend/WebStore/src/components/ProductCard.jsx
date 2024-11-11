import React, { useEffect, useState } from "react";
import useAuth from "../hooks/useAuth"; // Import useAuth for user details

// Import local images from the assets folder
import tshirtImage from "../assets/tshirt.jpg";
import trousersImage from "../assets/trousers.jpg";
import sneakersImage from "../assets/uggs.jpg";
import jacketImage from "../assets/jacket.jpg";
import sneakerssImage from "../assets/sneakers.jpg";

function ProductCard({ product, onUpdateProduct }) {
  const { getUserId } = useAuth(); // Get the user ID dynamically
  const [currentQuantity, setCurrentQuantity] = useState(product.quantity); // State for current quantity
  const [loading, setLoading] = useState(false); // Loading state for order action
  const [error, setError] = useState(null); // Error state

  // Dynamically determine the image based on product name
  const getImage = (name) => {
    if (name.toLowerCase().includes("tshirt")) return tshirtImage;
    if (name.toLowerCase().includes("trousers")) return trousersImage;
    if (name.toLowerCase().includes("uggs")) return sneakersImage;
    if (name.toLowerCase().includes("sneakers")) return sneakerssImage;
    return jacketImage; // Default to TShirt image if no match
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
        setCurrentQuantity(data.currentQuantity); // Update current quantity
      } catch (err) {
        setError(err.message);
      }
    };

    fetchProductQuantity();
  }, [product.id]);

  const handleOrder = async () => {
    const userId = getUserId(); // Dynamically fetch user ID
    const token = localStorage.getItem("token"); // Retrieve the token from localStorage
  
    if (!userId) {
      alert("User is not authenticated!");
      return;
    }
  
    setLoading(true); // Set loading to true during the process
    setError(null); // Reset error state
  
    const orderData = {
      orderDate: new Date().toISOString(),
      userId, // Use the dynamically fetched user ID
      orderItems: [
        {
          productId: product.id,
          quantity: 1, // Fixed quantity for this example
          unitPrice: product.price,
        },
      ],
    };
  
    try {
      // Step 1: Place the order
      const response = await fetch("http://localhost:5205/api/Order", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`, // Add Bearer token for authentication
        },
        body: JSON.stringify(orderData),
      });
  
      if (!response.ok) {
        throw new Error("Failed to place order.");
      }
  
      // Step 2: Fetch the updated quantity from the backend
      const quantityResponse = await fetch(
        `http://localhost:5205/api/Product/${product.id}/quantity`
      );
  
      if (!quantityResponse.ok) {
        throw new Error("Failed to fetch updated product quantity.");
      }
  
      const updatedQuantityData = await quantityResponse.json();
  
      // Step 3: Update the current quantity in the UI
      setCurrentQuantity(updatedQuantityData.currentQuantity);
  
      // Notify the parent component (if needed)
      if (onUpdateProduct) {
        onUpdateProduct(product.id, {
          ...product,
          quantity: updatedQuantityData.currentQuantity, // Use updated quantity
        });
      }
  
      alert("Order placed successfully!");
    } catch (err) {
      setError(err.message);
      alert(`Order failed: ${err.message}`);
    } finally {
      setLoading(false); // Reset loading state
    }
  };
  

  return (
    <div className="bg-white rounded-lg shadow-md overflow-hidden hover:shadow-lg transition-shadow">
      {/* Product Image */}
      <img
        src={getImage(product.name)} // Dynamically select the image
        alt={product.name}
        className="w-full h-48 object-cover"
      />

      {/* Product Details */}
      <div className="p-4">
        <h3 className="text-lg font-bold text-gray-800">{product.name}</h3>
        <p className="text-gray-600 mt-1">Price: ${product.price.toFixed(2)}</p>

        {/* Additional Details */}
        <p className="text-sm text-gray-500 mt-1">Brand: {product.brandName}</p>
        <p className="text-sm text-gray-500 mt-1">Category: {product.categoryName}</p>
        <p className="text-sm text-gray-500 mt-1">Size: {product.sizeName}</p>
        <p className="text-sm text-gray-500 mt-1">Gender: {product.genderName}</p>
        <p className="text-sm text-gray-500 mt-1">
          Quantity: {currentQuantity}{" "}
          {error && <span className="text-red-500">({error})</span>}
        </p>

        {/* Order Button */}
        <button
          className="mt-4 w-full bg-teal-500 text-white py-2 rounded-md hover:bg-teal-600 disabled:opacity-50"
          onClick={handleOrder}
          disabled={currentQuantity <= 0 || loading} // Disable button if quantity is 0 or loading
        >
          {loading ? "Placing Order..." : currentQuantity > 0 ? "Order" : "Out of Stock"}
        </button>
      </div>
    </div>
  );
}

export default ProductCard;
