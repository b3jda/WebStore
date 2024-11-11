import React, { useState, useEffect } from "react";
import ProductCard from "../components/ProductCard";
import { fetchData } from "../utility/api"; 

function HomePage() {
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const loadProducts = async () => {
      try {
        // Ensure the endpoint is correct
        const response = await fetch("http://localhost:5205/api/Product");
        if (!response.ok) {
          throw new Error("Failed to fetch products.");
        }
        const data = await response.json();
        setProducts(data);
      } catch (err) {
        console.error(err);
        setError("Failed to load products.");
      } finally {
        setLoading(false);
      }
    };

    loadProducts();
  }, []);

  const handleUpdateProduct = (productId, updatedProduct) => {
    setProducts((prevProducts) =>
      prevProducts.map((p) => (p.id === productId ? updatedProduct : p))
    );
  };

  if (loading) {
    return (
      <p className="text-center mt-16 text-lg text-gray-600">Loading products...</p>
    );
  }

  if (error) {
    return <p className="text-center mt-16 text-lg text-red-500">{error}</p>;
  }

  return (
    <div className="bg-gray-50">
      {/* Hero Section */}
      <div className="bg-gradient-to-r from-teal-500 to-blue-600 min-h-[50vh] flex flex-col justify-center items-center text-white relative overflow-hidden">
        <h1 className="text-6xl font-extrabold tracking-wider drop-shadow-lg mb-4 z-10">
          Welcome to WebStore
        </h1>
        <p className="text-2xl italic max-w-3xl text-center z-10">
          "Experience the art of shopping, where every item is crafted for your
          lifestyle."
        </p>
      </div>

    
    {/* Product Grid Section */}
<div className="container mx-auto py-16 px-4">
  <h2 className="text-4xl font-bold text-gray-800 text-center mb-8">
    Top Picks You’ll Love. Shop Now!
  </h2>
  {products.length === 0 ? (
    <p className="text-center text-gray-600">
      No products available right now. Please check back later!
    </p>
  ) : (
    <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-8">
      {products.map((product) => (
        <div
          key={product.id}
          className="transform transition duration-300 hover:scale-105 hover:shadow-lg"
        >
          <ProductCard
            product={product}
            onUpdateProduct={handleUpdateProduct}
          />
        </div>
      ))}
    </div>
  )}
</div>

    </div>
  );
}

export default HomePage;
