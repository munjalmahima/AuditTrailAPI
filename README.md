# AuditTrailAPI

AuditTrailAPI is a .NET-based Web API that provides an audit logging mechanism to track changes (Create, Update, Delete) in objects over time. It's designed to help developers implement an audit trail feature in their applications with minimal effort.

---

## 🛠️ Tech Stack

- **.NET 8**
- **C#**
- **ASP.NET Core Web API**
- **FluentValidation**
- **Newtonsoft.Json**

---

## 📁 Project Structure

- `AuditTrailAPI` : API Controllers and Startup Configuration  
- `AuditTrailService` : Core business logic for audit tracking  
- `AuditTrailRepository` : In-memory repository implementation  
- `AuditTrailShared` : Models, enums, and shared utilities

---

## ✅ Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)  
- Visual Studio or VS Code

---

## ▶️ Steps to Run

```bash
# Clone the repository
git clone https://github.com/munjalmahima/AuditTrailAPI.git

# Navigate to the project
cd AuditTrailAPI

# Restore dependencies
dotnet restore

# Run the API
dotnet run --project AuditTrailAPI
```

---

##  API Endpoint

### POST `/log`

Logs changes made to an object.

#### Request Body Example:

```json
{
  "before": {
    "Name": "Mahima",
    "Age": 25
  },
  "after": {
    "Name": "Mahima Munjal",
    "Age": 26
  },
  "action": "Updated",
  "userId": "user123",
  "entityName": "User"
}
```

> **Action Codes:**  
> 1 - Created, 2 - Updated, 3 - Deleted

#### 📤 Response Example:

```json
{
  "entityName": "User",
  "userId": "user123",
  "action": "Updated",
  "timestamp": "2025-06-04T12:09:04.2935926Z",
  "changes": [
    {
      "propertyName": "Name",
      "oldValue": "Mahima",
      "newValue": "Mahima Munjal"
    },
    {
      "propertyName": "Age",
      "oldValue": "25",
      "newValue": "26"
    }
  ]
}
```
