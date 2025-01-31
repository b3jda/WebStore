import React, { useState, useEffect } from "react";
import ProductCard from "../components/ProductCard";
import PriceRangeInput from "../components/PriceRangeInput";
import DiscountFilter from "../components/DiscountFilter";

const API_BASE_URL = "http://localhost:5205/api/Product";

function HomePage() {
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [priceRange, setPriceRange] = useState({ from: 0, to: 1000 });
  const [showDiscounted, setShowDiscounted] = useState(false);
  const [savedScrollPosition, setSavedScrollPosition] = useState(0);

  // Fetch products based on filter state
  useEffect(() => {
    const fetchProducts = async () => {
      setLoading(true);
      try {
        const url = showDiscounted
          ? `${API_BASE_URL}/discounted` // Fetch only discounted products
          : API_BASE_URL; // Fetch all products

        console.log("📡 Fetching from:", url); // Debugging log

        const response = await fetch(url);
        if (!response.ok) {
          throw new Error(`HTTP error! Status: ${response.status}`);
        }

        const data = await response.json();
        if (!Array.isArray(data)) {
          throw new Error("Invalid data format from API");
        }

        setProducts(data);
        setError(null);
      } catch (err) {
        console.error("❌ Fetch error:", err);
        setError(err.message || "Failed to load products");
      } finally {
        setLoading(false);
      }
    };

    fetchProducts();
  }, [showDiscounted]); // Fetch whenever `showDiscounted` changes

  useEffect(() => {
    if (!loading) {
      window.scrollTo({ top: savedScrollPosition, behavior: "instant" });
    }
  }, [products, loading]);

  // Filter products based on price range
  const filteredProducts = products.filter(
    (product) => product.price >= priceRange.from && product.price <= priceRange.to
  );

  if (loading) {
    return (
      <div className="text-center mt-16 text-lg text-gray-600">
        <div className="animate-pulse space-y-4">
          <div className="h-8 bg-gray-200 rounded w-1/4 mx-auto"></div>
          <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-4">
            {[...Array(4)].map((_, i) => (
              <div key={i} className="h-64 bg-gray-200 rounded-lg"></div>
            ))}
          </div>
        </div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="text-center mt-16 space-y-4">
        <div className="text-red-500 text-lg font-medium">⚠️ Error loading products</div>
        <p className="text-gray-600">{error}</p>
        <button
          onClick={() => window.location.reload()}
          className="bg-navy-900 text-gold-500 px-4 py-2 rounded hover:bg-opacity-90"
        >
          Try Again
        </button>
      </div>
    );
  }

  return (
    <div className="bg-gray-50">
      {/* Hero Section */}
      <div className="bg-navy-900 min-h-[50vh] flex flex-col justify-center items-center text-white relative overflow-hidden">
        <div className="text-center px-4 space-y-6">
          <h1 className="text-4xl sm:text-5xl font-light tracking-wide mb-4">
            <span className="block text-6xl sm:text-7xl font-serif mb-4 text-gold-500">
              Simpleté
            </span>
            Premium Essentials
          </h1>

          <div className="mx-auto w-24 border-t border-gold-500/30"></div>

          <p className="text-lg sm:text-xl text-gray-300 max-w-2xl mx-auto leading-relaxed">
            Curated selection of refined products for the discerning individual
          </p>
          <div className="mt-8">
            <button
              className="bg-gold-500 text-navy-900 px-8 py-3 font-medium hover:bg-gold-400 transition-colors duration-200 border-2 border-gold-500/20"
              onClick={() => {
                document.getElementById("product-grid").scrollIntoView({ behavior: "smooth" });
              }}
            >
              Discover Collection
            </button>
          </div>
        </div>
      </div>

      {/* Product Grid Section */}
      <div id="product-grid" className="container mx-auto py-16 px-4">
        <h2 className="text-3xl font-bold text-gray-800 text-center mb-8">Our Collection</h2>

        <div className="container mx-auto p-6">
          {/* Filtering Section */}
          <div className="flex flex-col md:flex-row gap-6 justify-center bg-white/60 backdrop-blur-md p-6 rounded-2xl shadow-lg border border-gray-200">
            <div className="flex flex-col items-center">
              <h3 className="text-lg font-bold text-gray-800 mb-2">Filter by Price</h3>
              <PriceRangeInput min={0} max={1000} step={10} onChange={setPriceRange} />
            </div>

            <div className="flex flex-col items-center">
              <h3 className="text-lg font-bold text-gray-800 mb-2">Discounted Only</h3>
              <DiscountFilter
  showDiscounted={showDiscounted} 
  onToggle={(state) => {
    setSavedScrollPosition(window.scrollY); 
    setShowDiscounted(state);
  }}
/>
            </div>
          </div>

          <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6 mt-8">
            {filteredProducts.length > 0 ? (
              filteredProducts.map((product) => (
                <div key={product.id} className="transition-transform transform hover:-translate-y-2 hover:shadow-xl duration-300">
                  <ProductCard product={product} />
                </div>
              ))
            ) : (
              <p className="text-center text-gray-500 col-span-full text-lg font-semibold">
                ❌ No matching products found.
              </p>
            )}
          </div>
        </div>
      </div>
    </div>
  );
}

export default HomePage;
