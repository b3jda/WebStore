import React, { useState, useEffect } from "react";

function ManageOrders({ user }) {
  const [orders, setOrders] = useState([]);
  const [orderStatus, setOrderStatus] = useState({ orderId: "", status: "" });

  const fetchOrders = async () => {
    try {
      const token = localStorage.getItem("token");
      const response = await fetch("http://localhost:5205/api/Order", {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      const data = await response.json();
      setOrders(data);
    } catch (err) {
      console.error("Error fetching orders:", err);
    }
  };

  const handleChangeOrderStatus = async (e) => {
    e.preventDefault();
    try {
      const token = localStorage.getItem("token");
      const response = await fetch(
        `http://localhost:5205/api/Order/${orderStatus.orderId}/status`,
        {
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
          },
          body: JSON.stringify(parseInt(orderStatus.status)),
        }
      );

      if (response.ok) {
        alert("Order status updated successfully!");
        fetchOrders();
      } else {
        throw new Error("Failed to update order status.");
      }
    } catch (err) {
      console.error(err.message);
    }
  };

  useEffect(() => {
    fetchOrders();
  }, []);

  if (user?.roles.includes("Admin")) {
    // Admin: Can view and change the status of orders
    return (
      <div>
        <h2 className="text-2xl font-bold mb-4">Manage Orders</h2>
        <ul>
          {orders.map((order) => (
            <li key={order.id}>
              Order ID: {order.id}, Status: {order.status}, Total Price:{" "}
              {order.totalPrice}
            </li>
          ))}
        </ul>
        <form onSubmit={handleChangeOrderStatus}>
          <input
            type="text"
            placeholder="Order ID"
            value={orderStatus.orderId}
            onChange={(e) =>
              setOrderStatus({ ...orderStatus, orderId: e.target.value })
            }
            className="border p-2 rounded mb-2"
            required
          />
          <select
            value={orderStatus.status}
            onChange={(e) =>
              setOrderStatus({ ...orderStatus, status: e.target.value })
            }
            className="border p-2 rounded mb-2"
            required
          >
            <option value="" disabled>
              Select Status
            </option>
            <option value="0">Pending</option>
            <option value="1">Processing</option>
            <option value="2">Shipped</option>
            <option value="3">Delivered</option>
            <option value="4">Cancelled</option>
            <option value="5">Completed</option>
          </select>
          <button className="bg-teal-500 text-white px-4 py-2 rounded hover:bg-teal-700">
            Update Status
          </button>
        </form>
      </div>
    );
  } else if (user?.roles.includes("AdvancedUser")) {
    // AdvancedUser: Can only view the list of orders
    return (
      <div>
        <h2 className="text-2xl font-bold mb-4">View Orders</h2>
        <ul>
          {orders.map((order) => (
            <li key={order.id}>
              Order ID: {order.id}, Status: {order.status}, Total Price:{" "}
              {order.totalPrice}
            </li>
          ))}
        </ul>
      </div>
    );
  } else {
    // Neither Admin nor AdvancedUser: No permission
    return <div>You do not have permission to view this page.</div>;
  }
}

export default ManageOrders;
