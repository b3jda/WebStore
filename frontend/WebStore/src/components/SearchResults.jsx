import React, { useEffect, useState } from "react";
import { useLocation } from "react-router-dom";
import ProductCard from "./ProductCard";

const API_BASE_URL = "http://localhost:5205/api"; // Backend URL

function SearchResults() {
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const location = useLocation();

  // Helper function to extract query parameters
  const getQueryParams = () => {
    const params = new URLSearchParams(location.search);
    const brand = params.get("brand");
    const size = params.get("size");
    const query = params.get("query") || ""; // Fallback if no query
    return { brand, size, query };
  };

  useEffect(() => {
    const fetchSearchResults = async () => {
      const { brand, size, query } = getQueryParams();
      try {
        setLoading(true);

        // Construct the search query
        const searchParams = new URLSearchParams();
        if (query) searchParams.append("query", query);
        if (brand) searchParams.append("brand", brand);
        if (size) searchParams.append("size", size);

        const response = await fetch(`${API_BASE_URL}/Product/search?${searchParams.toString()}`);
        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }

        const data = await response.json();
        setProducts(data);
      } catch (err) {
        console.error("Error fetching search results:", err);
        setError(err.message || "Failed to fetch search results.");
      } finally {
        setLoading(false);
      }
    };

    fetchSearchResults();
  }, [location.search]);

  if (loading) return <p className="text-center mt-4">Loading...</p>;
  if (error) return <p className="text-center mt-4 text-red-500">Failed to fetch search results: {error}</p>;

  return (
    <div className="container mx-auto py-16">
      <h2 className="text-3xl font-bold text-center mb-8">Search Results</h2>
      {products.length > 0 ? (
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-8">
          {products.map((product) => (
            <ProductCard key={product.id} product={product} />
          ))}
        </div>
      ) : (
        <p className="text-center mt-4">No products found for the given criteria.</p>
      )}
    </div>
  );
}

export default SearchResults;
