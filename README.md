# Blazor Product Store

## Project Overview
A Blazor Server application demonstrating a 3-tier architecture, ADO.NET for data access, Web API integration, and Google OAuth 2.0 authentication. This is a sample project for managing products and categories.

## Technologies Used
- **Framework:** ASP.NET Core 8, Blazor Server
- **Database:** Microsoft SQL Server
- **Data Access:** ADO.NET (SqlConnection, SqlCommand)
- **API:** ASP.NET Core Web API
- **Authentication:** ASP.NET Core Identity with Google OAuth 2.0
- **UI:** Bootstrap 5

## Setup Instructions
1. **Clone the repository:** `git clone https://github.com/your-username/your-repo.git`
2. **Database Setup:**
    - Open SQL Server Management Studio.
    - Run the SQL script located in `/Database/setup.sql` to create the `BlazorProductStoreDB` database and tables.
3. **Configure Connection String:**
    - In the `BlazorProductStore` project, open `appsettings.json`.
    - Update the `DefaultConnection` string with your SQL Server details.
4. **Configure Google Auth:**
    - In the `BlazorProductStore` project, right-click and select `Manage User Secrets`.
    - Add your Google Client ID and Client Secret:
      ```json
      {
        "Authentication:Google:ClientId": "YOUR_CLIENT_ID",
        "Authentication:Google:ClientSecret": "YOUR_CLIENT_SECRET"
      }
      ```
5. **Run the Application:**
    - Open the solution in Visual Studio 2022.
    - Right-click the solution and configure multiple startup projects (set both `BlazorProductStore` and `BlazorProductStore.API` to Start).
    - Press F5 or the "Start" button to run the application.
