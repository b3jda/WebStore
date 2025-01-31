import React, { useState } from "react";
import { useCart } from "../context/CartContext.jsx";
import useAuth from "../hooks/useAuth";
import { FaTrash, FaPlus, FaMinus, FaCheckCircle } from "react-icons/fa";

// Reuse image logic from ProductCard
import tshirtImage from "../assets/tshirt.jpg";
import trousersImage from "../assets/trousers.jpg";
import sneakersImage from "../assets/uggs.jpg";
import jacketImage from "../assets/jacket.jpg";
import sneakerssImage from "../assets/sneakers.jpg";

function CartPage() {
  const { cart, removeFromCart, addToCart, decreaseQuantity, clearCart } = useCart();
  const { getUserId } = useAuth();
  const [isPlacingOrder, setIsPlacingOrder] = useState(false); // Loading state
  const [orderPlaced, setOrderPlaced] = useState(false); // Success state

  // Function to get image based on product name
  const getImage = (name) => {
    if (name.toLowerCase().includes("tshirt")) return tshirtImage;
    if (name.toLowerCase().includes("trousers")) return trousersImage;
    if (name.toLowerCase().includes("uggs")) return sneakersImage;
    if (name.toLowerCase().includes("sneakers")) return sneakerssImage;
    return jacketImage;
  };

  // Calculate the total price of all items in the cart
  const totalPrice = cart.reduce((total, product) => total + product.price * product.quantity, 0);

  // Function to handle placing an order
  const handleOrder = async () => {
    const userId = getUserId(); // Dynamically fetch user ID
    const token = localStorage.getItem("token"); // Retrieve the token from localStorage

    if (!userId) {
      alert("User is not authenticated!");
      return;
    }

    setIsPlacingOrder(true); // Show loading state

    const orderData = {
      orderDate: new Date().toISOString(),
      userId, // Use the dynamically fetched user ID
      orderItems: cart.map((product) => ({
        productId: product.id,
        quantity: product.quantity,
        unitPrice: product.price,
      })),
    };

    try {
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

      // Clear the cart after successful order placement
      clearCart();
      setIsPlacingOrder(false);
      setOrderPlaced(true); // Show success message

      // Hide success message after 3 seconds
      setTimeout(() => setOrderPlaced(false), 3000);
    } catch (err) {
      alert(`Order failed: ${err.message}`);
      setIsPlacingOrder(false);
    }
  };

  return (
    <div className="container mx-auto py-10 px-4 sm:px-6">
      <h2 className="text-3xl font-bold text-center mb-8 text-gray-800 bg-gradient-to-r from-teal-500 to-blue-600 bg-clip-text text-transparent">
        Your Shopping Cart
      </h2>

      {cart.length === 0 ? (
        <p className="text-center text-gray-500">Your cart is empty.</p>
      ) : (
        <>
          <div className="grid gap-6 max-w-3xl mx-auto">
            {cart.map((product) => (
              <div
                key={product.id}
                className="flex items-center bg-white p-6 rounded-lg shadow-lg hover:shadow-xl transition-shadow"
              >
                {/* Product Image */}
                <img
                  src={getImage(product.name)}
                  alt={product.name}
                  className="w-24 h-24 object-cover rounded-lg mr-6 border border-gray-200"
                />

                {/* Product Details */}
                <div className="flex-grow">
                  <h3 className="text-xl font-semibold text-gray-800">{product.name}</h3>
                  <p className="text-gray-600 mt-1">Price: {`$${(product.price * product.quantity).toFixed(2)}`}</p>
                  <p className="text-gray-500 text-sm">Quantity: {product.quantity}</p>
                </div>

                {/* Quantity Controls */}
                <div className="flex items-center space-x-4">
                  <button
                    className="bg-gray-100 p-2 rounded-lg hover:bg-gray-200 transition-colors"
                    onClick={() => decreaseQuantity(product.id)} // Decrease quantity
                  >
                    <FaMinus size={16} className="text-gray-700" />
                  </button>
                  <span className="text-lg font-semibold text-gray-800">{product.quantity}</span>
                  <button
                    className="bg-gray-100 p-2 rounded-lg hover:bg-gray-200 transition-colors"
                    onClick={() => addToCart(product)} // Increase quantity
                  >
                    <FaPlus size={16} className="text-gray-700" />
                  </button>
                </div>

                {/* Remove Button */}
                <button
                  className="ml-6 bg-red-500 text-white px-4 py-2 rounded-lg hover:bg-red-600 transition-colors flex items-center"
                  onClick={() => removeFromCart(product.id)}
                >
                  <FaTrash size={16} className="mr-2" />
                  Remove
                </button>
              </div>
            ))}
          </div>

          {/* Invoice Section */}
          <div className="max-w-3xl mx-auto mt-8 bg-white p-6 rounded-lg shadow-lg">
            <h3 className="text-2xl font-bold text-gray-800 mb-4">Order Summary</h3>
            <div className="space-y-4">
              {cart.map((product) => (
                <div key={product.id} className="flex justify-between">
                  <p className="text-gray-700">
                    {product.name} (x{product.quantity})
                  </p>
                  <p className="text-gray-700">${(product.price * product.quantity).toFixed(2)}</p>
                </div>
              ))}
              <div className="border-t border-gray-200 pt-4">
                <div className="flex justify-between">
                  <p className="text-lg font-semibold text-gray-800">Total</p>
                  <p className="text-lg font-semibold text-gray-800">${totalPrice.toFixed(2)}</p>
                </div>
              </div>
            </div>
          </div>

          {/* Place Order Button */}
          <div className="text-center mt-10">
          <button
  className="bg-gold-500 text-navy-900 px-8 py-3 rounded-lg 
            hover:bg-gold-600 hover:shadow-lg 
            transition-all duration-200 
            transform hover:scale-[1.02] 
            text-lg font-semibold 
            flex items-center justify-center mx-auto
            disabled:bg-gold-700 disabled:cursor-not-allowed"
  onClick={handleOrder}
  disabled={isPlacingOrder}
>
  {isPlacingOrder ? (
    <>
      <svg 
        className="animate-spin h-5 w-5 mr-3 text-navy-900" 
        xmlns="http://www.w3.org/2000/svg" 
        fill="none" 
        viewBox="0 0 24 24"
      >
        <circle cx="12" cy="12" r="10" stroke="currentColor" strokeWidth="4" className="opacity-25"></circle>
        <path fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"></path>
      </svg>
      Processing...
    </>
  ) : (
    "Place Order"
  )}
</button>
          </div>
        </>
      )}

      {/* Success Message */}
      {orderPlaced && (
        <div className="fixed top-4 right-4 bg-green-500 text-white px-6 py-3 rounded-lg shadow-lg flex items-center animate-fade-in">
          <FaCheckCircle className="mr-2" />
          Order placed successfully!
        </div>
      )}
    </div>
  );
}

export default CartPage;