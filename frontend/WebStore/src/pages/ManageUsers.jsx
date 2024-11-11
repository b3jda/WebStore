import React, { useState, useEffect } from "react";
import useAuth from "../hooks/useAuth";

function ManageUsers() {
  const [users, setUsers] = useState([]);
  const { user, hasRole, getToken } = useAuth();

  // Fetch all users
  const fetchUsers = async () => {
    try {
      const token = getToken();
      const response = await fetch("http://localhost:5205/api/User", {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      if (response.ok) {
        const data = await response.json();
        setUsers(data);
      } else {
        throw new Error("Failed to fetch users.");
      }
    } catch (err) {
      console.error("Error fetching users:", err.message);
    }
  };

  // Delete a user by ID
  const handleDeleteUser = async (userId) => {
    try {
      const token = getToken();
      const response = await fetch(`http://localhost:5205/api/User/${userId}`, {
        method: "DELETE",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      if (response.ok) {
        alert("User deleted successfully!");
        fetchUsers(); // Refresh user list after deletion
      } else {
        throw new Error("Failed to delete user.");
      }
    } catch (err) {
      console.error("Error deleting user:", err.message);
    }
  };

  // Fetch users on component mount
  useEffect(() => {
    if (hasRole("Admin")) {
      fetchUsers();
    }
  }, [hasRole]);

  if (!user) {
    return <div>Loading user data...</div>;
  }

  if (!hasRole("Admin")) {
    return <div>You do not have permission to view this page.</div>;
  }

  return (
    <div>
      <h2 className="text-2xl font-bold mb-4">Manage Users</h2>
      <ul className="list-disc pl-5">
        {users.map((user) => (
          <li key={user.id} className="mb-2 flex justify-between items-center">
            <span>
              ID: {user.id}, Email: {user.userName}, Name: {user.firstName} {user.lastName}
            </span>
            <button
              className="bg-red-500 text-white px-4 py-1 rounded hover:bg-red-700"
              onClick={() => handleDeleteUser(user.id)}
            >
              Delete
            </button>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default ManageUsers;
