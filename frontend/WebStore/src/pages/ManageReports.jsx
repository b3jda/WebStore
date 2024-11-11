import React, { useState } from "react";
import useAuth from "../hooks/useAuth";

function ManageReports() {
  const { hasRole, getToken } = useAuth();
  const [reportData, setReportData] = useState(null);
  const [error, setError] = useState(null);

  const fetchReport = async (endpoint) => {
    try {
      setError(null); // Reset error state
      const token = getToken();
      const response = await fetch(endpoint, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      if (!response.ok) {
        throw new Error("Failed to fetch report.");
      }
      const data = await response.json();
      setReportData(data);
    } catch (err) {
      setError(err.message);
      console.error("Error fetching report:", err);
    }
  };

  const handleDailyReport = () => {
    const dateTime = new Date().toISOString(); 
    const endpoint = `http://localhost:5205/api/Report/daily?date=${dateTime}`;
    fetchReport(endpoint);
  };

  const handleMonthlyReport = (month, year) => {
    const endpoint = `http://localhost:5205/api/Report/monthly?month=${month}&year=${year}`;
    fetchReport(endpoint);
  };

  const handleTopSellingReport = (count) => {
    const endpoint = `http://localhost:5205/api/Report/top-selling?count=${count}`;
    fetchReport(endpoint);
  };

  if (!hasRole("Admin") && !hasRole("AdvancedUser")) {
    return <div>You do not have permission to view this page.</div>;
  }

  return (
    <div>
      <h2 className="text-2xl font-bold mb-4">Manage Reports</h2>

      {/* Buttons to Generate Reports */}
      <div className="mb-4 space-y-4">
        <button
          className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-700"
          onClick={handleDailyReport}
        >
          Generate Daily Report
        </button>

        <button
          className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-700"
          onClick={() => handleMonthlyReport(11, 2024)}
        >
          Generate Monthly Report (Nov 2024)
        </button>

        <button
          className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-700"
          onClick={() => handleTopSellingReport(2)}
        >
          Generate Top Selling Products Report (Top 2)
        </button>
      </div>

      {/* Display Report Data */}
      <div className="mt-4">
        {error && <p className="text-red-500">Error: {error}</p>}
        {reportData ? (
          <pre className="bg-gray-100 p-4 rounded overflow-x-auto">
            {JSON.stringify(reportData, null, 2)}
          </pre>
        ) : (
          <p>No report data available. </p>
        )}
      </div>
    </div>
  );
}

export default ManageReports;
