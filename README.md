# WebStore

WebStore is a full-stack e-commerce application that allows users to manage products, orders, and reports. It includes functionalities for administrators, advanced users, and regular users, with roles-based access control.

## Features

### Admin Features
- **Manage Products**: Add, edit, delete, and apply discounts to products.
- **Manage Orders**: View and update the status of orders.
- **Manage Users**: Assign roles to users (e.g., Admin, AdvancedUser, SimpleUser).
- **Generate Reports**: View daily, monthly, and top-selling product reports.

### Advanced User Features
- **View Orders**: Access and view a list of all orders.
- **Generate Reports**: Access and generate sales reports (daily/monthly/top-selling).

### User Features
- **Place Orders**: Browse products and place orders.
- **View Orders**: View order history (limited to their own orders).

## Technologies Used

### Frontend
- **React**: Frontend framework.
- **Tailwind CSS**: For responsive and modern styling.

### Backend
- **ASP.NET Core**: Backend API development.
- **Entity Framework Core**: For database interactions.
- **SQL Server**: Database management.

### Other Tools
- **Swagger**: API documentation and testing.
- **Role-Based Access Control**: Using JWT (JSON Web Token) for secure authentication.

## Installation

### Prerequisites
- **Node.js**: Required for running the frontend.
- **Visual Studio**: For running the backend.
- **PostgreSQL**: Database for the project.

### Setup
1. **Clone the Repository**
   ```bash
   git clone https://github.com/b3jda/WebStore.git
   cd WebStore

