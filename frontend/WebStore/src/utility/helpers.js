// src/utils/helpers.js

// Format prices to two decimal places with currency
export function formatPrice(price) {
  return `$${price.toFixed(2)}`;
}

// Capitalize the first letter of a string
export function capitalize(str) {
  if (!str) return "";
  return str.charAt(0).toUpperCase() + str.slice(1);
}

// Validate an email address
export function isValidEmail(email) {
  const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  return regex.test(email);
}

// Format date to a readable string
export function formatDate(dateString) {
  const options = { year: "numeric", month: "long", day: "numeric" };
  return new Date(dateString).toLocaleDateString(undefined, options);
}
