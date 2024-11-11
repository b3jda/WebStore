import { useState } from "react";

function useAuth() {
  // Initialize the user state from localStorage
  const [user, setUser] = useState(() => {
    const savedUser = localStorage.getItem("user");
    return savedUser ? JSON.parse(savedUser) : null;
  });

  // Initialize the token state from localStorage
  const [token, setToken] = useState(() => {
    return localStorage.getItem("token") || null;
  });

  const login = async (email, password) => {
    try {
      const response = await fetch("http://localhost:5205/api/auth/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ email, password }),
      });

      if (!response.ok) {
        const errorData = await response.json().catch(() => null);
        throw new Error(errorData?.message || "Login failed.");
      }

      const data = await response.json();

      // Ensure the user object includes the id
      if (!data.userId) {
        throw new Error("User ID is missing in the login response.");
      }

      // Ensure roles are present in the response
      if (!data.roles || !Array.isArray(data.roles)) {
        throw new Error("Roles are missing in the login response.");
      }

      // Save user info and token in localStorage
      localStorage.setItem("user", JSON.stringify(data));
      localStorage.setItem("token", data.token);

      // Set user and token in state
      setUser(data);
      setToken(data.token);

      return data; // Return the full user data
    } catch (error) {
      console.error("Login error:", error.message);
      throw error;
    }
  };

  const logout = () => {
    setUser(null);
    setToken(null);
    localStorage.removeItem("user");
    localStorage.removeItem("token");
  };

  // Utility to retrieve the userId
  const getUserId = () => {
    return user?.userId || null;
  };

  // Utility to retrieve the token
  const getToken = () => token;

  // Utility to check if user has a specific role
  const hasRole = (role) => {
    return user?.roles?.includes(role);
  };

  return { user, token, login, logout, getUserId, getToken, hasRole };
}

export default useAuth;
