import React, { useState } from "react";
import ManageProducts from "./ManageProducts";
import ManageUsers from "./ManageUsers";
import ManageOrders from "./ManageOrders";
import ManageReports from "./ManageReports";
import useAuth from "../hooks/useAuth";

function AdminPage() {
  const [activeTab, setActiveTab] = useState("products");
  const { user } = useAuth();

  return (
    <div className="flex h-screen">
      {/* Sidebar */}
      <div className="w-1/4 bg-teal-600 text-white flex flex-col justify-center items-center">
        <ul className="space-y-8 text-lg font-semibold">
          <li
            className={`cursor-pointer ${
              activeTab === "products" ? "text-teal-200" : "text-white"
            }`}
            onClick={() => setActiveTab("products")}
          >
            Manage Products
          </li>
          <li
            className={`cursor-pointer ${
              activeTab === "orders" ? "text-teal-200" : "text-white"
            }`}
            onClick={() => setActiveTab("orders")}
          >
            Manage Orders
          </li>
          <li
            className={`cursor-pointer ${
              activeTab === "users" ? "text-teal-200" : "text-white"
            }`}
            onClick={() => setActiveTab("users")}
          >
            Manage Users
          </li>
          <li
            className={`cursor-pointer ${
              activeTab === "reports" ? "text-teal-200" : "text-white"
            }`}
            onClick={() => setActiveTab("reports")}
          >
            Manage Reports
          </li>
        </ul>
      </div>

      {/* Main Content */}
      <div className="w-3/4 p-8 overflow-y-auto">
        {activeTab === "products" && <ManageProducts />}
        {activeTab === "orders" && <ManageOrders user={user} />}
        {activeTab === "users" && <ManageUsers />}
        {activeTab === "reports" && <ManageReports />}
      </div>
    </div>
  );
}

export default AdminPage;
