![image](https://github.com/user-attachments/assets/8200ac2c-4184-4b57-8167-e26c89ffc561)

Application that loads and transforms a CSV dataset containing information of 17,000 FIFA players into a relational database. It uses a Python ETL script (with Pandas) and a .NET REST API with endpoints to filter players and then displays the results in a web interface for easy browsing and exploration.

### Backend
- .NET SDK: `8.0`
- Language: `C#`
- Framework: `ASP.NET Core Web API`
- Database: `SQL Server 2022`
  - Version: `16.0.1135.2`
  - Execute `DBQuery.sql` file to create the database in SQL Server

#### ETL script (Data/etl.py) requires pandas to create the `players_cleaned.json` file.
- Python: `3.9+` (must be installed and available in PATH)
- Python dependencies:
  - `pandas`

### Frontend (Angular)
- Angular CLI: `19.1.3`
- Node.js: `v22.13.1`
```bash
npm install
npm start
