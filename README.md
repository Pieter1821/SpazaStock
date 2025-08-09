# SpazaStock

A complete inventory management system for South African spaza shops.

## Features
- Mobile-first web app for spaza shops
- Track inventory, record sales, and get SMS business insights
- ASP.NET Web API backend (.NET 8, EF Core, SQL Server)
- React 18 + TypeScript frontend (planned)
- SMS integration (planned)
- Demo data import from RapidAPI Grocery API

## Backend Setup
1. Clone the repo
2. Configure your SQL Server connection in `appsettings.json`
3. Run migrations:
   ```bash
   dotnet ef database update
   ```
4. Start the backend:
   ```bash
   dotnet run
   ```

## API Endpoints
- `POST /api/auth/register` — Register user
- `POST /api/auth/login` — Login
- `GET /api/products` — List products
- `POST /api/products` — Add product
- `POST /api/products/import-demo` — Import demo products

## Demo Import
- Uses RapidAPI Grocery API (Amazon/Walmart)
- Converts USD prices to ZAR
- Applies spaza markup

## License
MIT
