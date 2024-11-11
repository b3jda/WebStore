import { useState, useEffect } from "react";

const API_BASE_URL = "http://localhost:5205/api"; // Backend URL

const useFetch = (endpoint, options = {}) => {
  const [data, setData] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        setLoading(true);
        const token = localStorage.getItem("token"); // Token for authenticated requests
        const response = await fetch(`${API_BASE_URL}${endpoint}`, {
          headers: {
            "Content-Type": "application/json",
            ...(token && { Authorization: `Bearer ${token}` }),
            ...options.headers,
          },
          ...options,
        });

        if (!response.ok) {
          const errorData = await response.json();
          throw new Error(errorData?.message || `HTTP Error ${response.status}`);
        }

        const result = await response.json();
        setData(result);
      } catch (err) {
        console.error("Error fetching data:", err);
        setError(err.message || "Something went wrong.");
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [endpoint, options]);

  return { data, loading, error };
};

export default useFetch;
