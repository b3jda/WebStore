import React, { useState, useEffect } from "react";
import useAuth from "../hooks/useAuth";

function ManageProducts() {
  const { hasRole } = useAuth(); // Check roles using useAuth
  const [products, setProducts] = useState([]);
  const [newProduct, setNewProduct] = useState({
    name: "",
    description: "",
    price: 0,
    quantity: 0,
    categoryName: "",
    brandName: "",
    sizeName: "",
    genderName: "",
    colorName: "",
  });
  const [discount, setDiscount] = useState({ productId: "", percentage: 0 });
  const [removeDiscount, setRemoveDiscount] = useState({ productId: "" }); // Added for remove discount


  // Fetch all products
  const fetchProducts = async () => {
    try {
      const response = await fetch("http://localhost:5205/api/Product");
      const data = await response.json();
      setProducts(data);
    } catch (err) {
      console.error("Error fetching products:", err);
    }
  };

  // Add a new product
  const handleAddProduct = async (e) => {
    e.preventDefault();
    try {
      const token = localStorage.getItem("token"); // Retrieve the token from localStorage
      const response = await fetch("http://localhost:5205/api/Product", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`, // Include the Bearer token
        },
        body: JSON.stringify(newProduct),
      });
      if (response.ok) {
        alert("Product added successfully!");
        fetchProducts();
        setNewProduct({
          name: "",
          description: "",
          price: 0,
          quantity: 0,
          categoryName: "",
          brandName: "",
          sizeName: "",
          genderName: "",
          colorName: "",
        });
      } else {
        throw new Error("Failed to add product.");
      }
    } catch (err) {
      console.error(err.message);
    }
  };

  // Apply a discount to a product
  const handleApplyDiscount = async (e) => {
    e.preventDefault();
    try {
      const token = localStorage.getItem("token"); // Retrieve the token from localStorage
      const response = await fetch(
        `http://localhost:5205/api/Product/apply-discount/${discount.productId}`,
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`, // Include the Bearer token
          },
          body: JSON.stringify(discount.percentage), // Send the discount percentage as a number
        }
      );
      if (response.ok) {
        alert("Discount applied successfully!");
        fetchProducts(); // Refresh the product list
      } else {
        const error = await response.json();
        throw new Error(error.message || "Failed to apply discount.");
      }
    } catch (err) {
      console.error(err.message);
      alert(err.message);
    }
  };

  // 🔥 Remove discount from a product
  const handleRemoveDiscount = async (e) => {
    e.preventDefault();
    try {
      const token = localStorage.getItem("token"); // Retrieve the token from localStorage
      const response = await fetch(
        `http://localhost:5205/api/Product/remove-discount/${removeDiscount.productId}`,
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`, // Include the Bearer token
          },
        }
      );
      if (response.ok) {
        alert("Discount removed successfully!");
        fetchProducts(); // Refresh the product list
      } else {
        const error = await response.json();
        throw new Error(error.message || "Failed to remove discount.");
      }
    } catch (err) {
      console.error(err.message);
      alert(err.message);
    }
  };

  // Fetch products on component load
  useEffect(() => {
    fetchProducts();
  }, []);

  // Render permission message if not Admin or AdvancedUser
  if (!hasRole("Admin") && !hasRole("AdvancedUser")) {
    return <p className="text-red-500 font-bold">You do not have permission to view this page.</p>;
  }

  return (
    <div>
      <h2 className="text-2xl font-bold mb-4">Manage Products</h2>

      {/* Add Product Form */}
      {(hasRole("Admin") || hasRole("AdvancedUser")) && (
        <form onSubmit={handleAddProduct} className="mb-8">
          <h3 className="font-bold mb-2">Add Product</h3>
          <div className="flex flex-col space-y-2">
            <input
              type="text"
              placeholder="Name"
              value={newProduct.name}
              onChange={(e) =>
                setNewProduct({ ...newProduct, name: e.target.value })
              }
              className="border p-2 rounded"
              required
            />
            <textarea
              placeholder="Description"
              value={newProduct.description}
              onChange={(e) =>
                setNewProduct({ ...newProduct, description: e.target.value })
              }
              className="border p-2 rounded"
              required
            />
            <input
              type="number"
              placeholder="Price"
              value={newProduct.price}
              onChange={(e) =>
                setNewProduct({
                  ...newProduct,
                  price: parseFloat(e.target.value),
                })
              }
              className="border p-2 rounded"
              required
            />
            <input
              type="number"
              placeholder="Quantity"
              value={newProduct.quantity}
              onChange={(e) =>
                setNewProduct({
                  ...newProduct,
                  quantity: parseInt(e.target.value),
                })
              }
              className="border p-2 rounded"
              required
            />
            <input
              type="text"
              placeholder="Category Name"
              value={newProduct.categoryName}
              onChange={(e) =>
                setNewProduct({
                  ...newProduct,
                  categoryName: e.target.value,
                })
              }
              className="border p-2 rounded"
            />
            <input
              type="text"
              placeholder="Brand Name"
              value={newProduct.brandName}
              onChange={(e) =>
                setNewProduct({ ...newProduct, brandName: e.target.value })
              }
              className="border p-2 rounded"
            />
            <input
              type="text"
              placeholder="Size Name"
              value={newProduct.sizeName}
              onChange={(e) =>
                setNewProduct({ ...newProduct, sizeName: e.target.value })
              }
              className="border p-2 rounded"
            />
            <input
              type="text"
              placeholder="Gender Name"
              value={newProduct.genderName}
              onChange={(e) =>
                setNewProduct({ ...newProduct, genderName: e.target.value })
              }
              className="border p-2 rounded"
            />
            <input
              type="text"
              placeholder="Color Name"
              value={newProduct.colorName}
              onChange={(e) =>
                setNewProduct({ ...newProduct, colorName: e.target.value })
              }
              className="border p-2 rounded"
            />
            <button className="bg-teal-500 text-white px-4 py-2 rounded hover:bg-teal-700">
              Add Product
            </button>
          </div>
        </form>
      )}

      {/* Apply Discount Form */}
      {hasRole("Admin") && (
        <form onSubmit={handleApplyDiscount} className="mb-8">
          <h3 className="font-bold mb-2">Apply Discount</h3>
          <div className="flex flex-col space-y-2">
            <input
              type="text"
              placeholder="Product ID"
              value={discount.productId}
              onChange={(e) =>
                setDiscount({ ...discount, productId: e.target.value })
              }
              className="border p-2 rounded"
              required
            />
            <input
              type="number"
              placeholder="Discount Percentage"
              value={discount.percentage}
              onChange={(e) =>
                setDiscount({
                  ...discount,
                  percentage: parseFloat(e.target.value),
                })
              }
              className="border p-2 rounded"
              required
            />
            <button className="bg-teal-500 text-white px-4 py-2 rounded hover:bg-teal-700">
              Apply Discount
            </button>
          </div>
        </form>
      )}
      {/* 🚀 Remove Discount Form */}
      {hasRole("Admin") && (
        <form onSubmit={handleRemoveDiscount} className="mb-8">
          <h3 className="font-bold mb-2">Remove Discount</h3>
          <div className="flex flex-col space-y-2">
            <input
              type="text"
              placeholder="Product ID"
              value={removeDiscount.productId}
              onChange={(e) =>
                setRemoveDiscount({ ...removeDiscount, productId: e.target.value })
              }
              className="border p-2 rounded"
              required
            />
            <button className="bg-red-500 text-white px-4 py-2 rounded hover:bg-red-700">
              Remove Discount
            </button>
          </div>
        </form>
      )}
    </div>
  );
}

export default ManageProducts;
