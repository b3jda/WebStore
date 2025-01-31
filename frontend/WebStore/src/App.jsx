import React from "react";
import {
  BrowserRouter as Router,
  Routes,
  Route,
  useLocation,
} from "react-router-dom";
import Header from "./components/Header";
import Footer from "./components/Footer";
import HomePage from "./pages/HomePage";
import Login from "./pages/Login";
import Register from "./pages/Register";
import SearchResults from "./components/SearchResults"; 
import AdminPage from "./pages/AdminPage";
import ManageProducts from "./pages/ManageProducts";
import ManageOrders from "./pages/ManageOrders";
import ManageUsers from "./pages/ManageUsers";
import CartPage from "./pages/CartPage"; 
import { CartProvider } from "./context/CartContext.jsx";

function App() {
  return (
    <CartProvider>
    <Router>
      <MainLayout>
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />
          <Route path="/search" element={<SearchResults />} />
          <Route path="/admin" element={<AdminPage />} />
          <Route path="/products" element={<ManageProducts />} />
          <Route path="/orders" element={<ManageOrders />} />
          <Route path="/users" element={<ManageUsers />} />
          <Route path="/cart" element={<CartPage />} /> {/* Add Cart Page */}
        </Routes>
      </MainLayout>
    </Router>
    </CartProvider>
  );
}

function MainLayout({ children }) {
  const location = useLocation();

  // Define routes where Header and Footer should not appear
  const hideHeaderFooterRoutes = ["/login", "/register"];
  const shouldHideHeaderFooter = hideHeaderFooterRoutes.includes(
    location.pathname
  );

  return (
    <div className="flex flex-col min-h-screen">
      {!shouldHideHeaderFooter && <Header />}
      <main className="flex-grow">{children}</main>
      {!shouldHideHeaderFooter && <Footer />}
    </div>
    
  );
}

export default App;
