# TooliRent API

> TooliRent är ett RESTful API för uthyrning av verktyg.  
> Systemet gör det möjligt för kunder att hyra och returnera verktyg, medan administratörer kan hantera kunder, kategorier, användare och lagersaldo.  
> Byggt i **ASP.NET Core (.NET 8)** med **Entity Framework Core** och **Clean Architecture**.

---

## Project Overview

###  Architecture Overview

TooliRent är uppdelat i flera lager för tydlig struktur och enkel vidareutveckling:

#### **TooliRent (API Layer)**
Hantera HTTP-anrop och routing.

**Controllers:**
- `AuthController.cs` – Hanterar autentisering och rollhantering
- `CategoryController.cs` – CRUD-operationer för verktygskategorier
- `CustomerController.cs` – CRUD-operationer för kunder
- `RentalController.cs` – Hantering av uthyrning och återlämning
- `ToolController.cs` – CRUD-operationer för verktyg

**Repositories:**
- Implementerar repository pattern för dataåtkomst (`ToolRepository`, `RentalRepository`, etc.)

**Data:**
- `TooliRentDbContext.cs` – EF Core databas-kontext
- `SeedData.cs` – Initierar databasen med exempeldata

---

#### **TooliRent.Library (Domain & DTO Layer)**
Innehåller kärnmodeller och Data Transfer Objects.

- **Models:**  
  `Tool`, `Category`, `Customer`, `Rental`, `RentalDetail`, `User`
- **DTOs:**  
  `ToolDto`, `CategoryDto`, `CustomerDto`, `RentalDto`, `ReturnRentalDto`, `AuthDto`

---

#### **TooliRent.Tests (Testing Layer)**
Innehåller enhetstester och integrationstester.

- **Unit Tests:** Testar controllers med mockade repositories  
- **Repository Tests:** Körs mot en InMemory-databas  
- **Integration Tests:** Verifierar hela flöden via Postman

---

### Tech Stack

| Teknologi | Beskrivning |
|------------|-------------|
| ASP.NET Core (.NET 8) | Backend API |
| Entity Framework Core | ORM och databasåtkomst |
| PostgreSQL / MSSQL | Databas |
| MSTest | Enhetstester |
| Swagger | API-dokumentation |
| Postman | Integrationstester |

---

## 📡 API Endpoints

| Metod | Endpoint | Beskrivning |
|--------|-----------|-------------|
| **Auth** | | |
| `POST` | `/api/Auth/Register` | Registrera en ny användare |
| `POST` | `/api/Auth/Login` | Logga in och hämta JWT-token |
| `GET` | `/api/Auth/GetAllUsers` | Hämta alla användare (Admin) |
| **Categories** | | |
| `GET` | `/api/Category/GetAllCategory` | Hämta alla kategorier |
| `POST` | `/api/Category/AddCategory` | Skapa ny kategori |
| `PUT` | `/api/Category/UpdateCategory` | Uppdatera kategori |
| `DELETE` | `/api/Category/DeleteCategory/{id}` | Ta bort kategori |
| **Tools** | | |
| `GET` | `/api/Tool/GetAllTools` | Hämta alla verktyg |
| `GET` | `/api/Tool/GetToolById/{id}` | Hämta specifikt verktyg |
| `POST` | `/api/Tool/AddTool` | Lägg till nytt verktyg |
| `PUT` | `/api/Tool/UpdateTool` | Uppdatera verktyg |
| `DELETE` | `/api/Tool/DeleteTool/{id}` | Ta bort verktyg |
| **Customers** | | |
| `GET` | `/api/Customer/GetAllCustomer` | Hämta alla kunder |
| `GET` | `/api/Customer/GetCustomerById/{id}` | Hämta kund via ID |
| `POST` | `/api/Customer/AddCustomer` | Lägg till kund |
| `PUT` | `/api/Customer/UpdateCustomer` | Uppdatera kund |
| `DELETE` | `/api/Customer/DeleteCustomer/{id}` | Ta bort kund |
| **Rentals** | | |
| `POST` | `/api/Rental/CreateRental` | Skapa ny uthyrning |
| `GET` | `/api/Rental/GetAllRentals` | Hämta alla uthyrningar |
| `GET` | `/api/Rental/GetRentalById/{id}` | Hämta specifik uthyrning |
| `PATCH` | `/api/Rental/{id}/return` | Returnera en uthyrning (Admin) |

---

## 📦 Example Requests & Responses

### 🔑 Register User
```http
POST /api/Auth/Register

````
[GET] /api/Guest/GetAllGuests
````

````
[GET] /api/Category/GetAllCategory
````
[
  {
    "id": "24a23575-4f55-4af0-98e8-9bf4eba2906f",
    "userName": "marvar",
    "email": "marvar@toolirent.com",
    "roles": [
      "Admin"
    ]
  },
  {
    "id": "a4c5b12c-f747-4e40-b1d9-16136ef93f00",
    "userName": "fibiebibi",
    "email": "fibie@user.com",
    "roles": [
      "User"
    ]
  },
  {
    "id": "cdc1d2e3-e75f-42a9-a55f-a81ebf809b1d",
    "userName": "marfar",
    "email": "marfar@something.com",
    "roles": [
      "User"
    ]
  },
  {
    "id": "d417d33c-2190-4cfb-b2ad-204efb70a944",
    "userName": "admin",
    "email": "admin@toolirent.com",
    "roles": [
      "Admin"
    ]
  }
]

</details>
````
[GET] /api/Category/GetAllCategory
````
[
  {
    "categoryId": 1,
    "name": "Power Tools",
    "description": "Electric and battery powered tools for construction and DIY projects"
  },
  {
    "categoryId": 2,
    "name": "Hand Tools",
    "description": "Manual tools and basic equipment"
  },
  {
    "categoryId": 3,
    "name": "Garden Tools",
    "description": "Outdoor and gardening equipment for lawn and landscape maintenance"
  },
  {
    "categoryId": 4,
    "name": "Construction Tools",
    "description": "Heavy duty construction and renovation equipment"
  },
  {
    "categoryId": 5,
    "name": "Cleaning Equipment",
    "description": "Professional cleaning tools and pressure washing equipment"
  }
]
````
</details>

<details close>
<summary>See available rooms</summary>
<br>
  
````
[GET] /api/Tool/GetAllTools
````
**Example Response**
````
[
{
    "toolId": 1,
    "name": "Cordless Drill 18V",
    "description": "Professional 18V cordless drill with two batteries and charger",
    "quantityInStock": 0,
    "pricePerDay": 15.99,
    "isAvailable": false,
    "categoryId": 1,
    "categoryName": "Power Tools"
  },
  {
    "toolId": 2,
    "name": "Circular Saw 7.25\"",
    "description": "Heavy duty circular saw with carbide blade",
    "quantityInStock": 0,
    "pricePerDay": 22.5,
    "isAvailable": false,
    "categoryId": 1,
    "categoryName": "Power Tools"
  },
  {
    "toolId": 3,
    "name": "Angle Grinder 4.5\"",
    "description": "Compact angle grinder for cutting and grinding",
    "quantityInStock": 4,
    "pricePerDay": 18.99,
    "isAvailable": true,
    "categoryId": 1,
    "categoryName": "Power Tools"
  },
  {
    "toolId": 4,
    "name": "Jigsaw Variable Speed",
    "description": "Variable speed jigsaw with orbital action",
    "quantityInStock": 3,
    "pricePerDay": 16.75,
    "isAvailable": true,
    "categoryId": 1,
    "categoryName": "Power Tools"
  },
  {
    "toolId": 5,
    "name": "Impact Driver",
    "description": "High torque impact driver with quick-change chuck",
    "quantityInStock": 5,
    "pricePerDay": 19.99,
    "isAvailable": true,
    "categoryId": 1,
    "categoryName": "Power Tools"
  },
  {
    "toolId": 6,
    "name": "Orbital Sander",
    "description": "Random orbital sander with dust collection",
    "quantityInStock": 2,
    "pricePerDay": 24.99,
    "isAvailable": true,
    "categoryId": 1,
    "categoryName": "Power Tools"
  },
  {
    "toolId": 7,
    "name": "Professional Hammer Set",
    "description": "3-piece hammer set: claw, ball peen, and sledge",
    "quantityInStock": 6,
    "pricePerDay": 8.5,
    "isAvailable": true,
    "categoryId": 2,
    "categoryName": "Hand Tools"
  },
  {
    "toolId": 8,
    "name": "Socket Wrench Set",
    "description": "42-piece metric and imperial socket set with ratchets",
    "quantityInStock": 3,
    "pricePerDay": 12.99,
    "isAvailable": true,
    "categoryId": 2,
    "categoryName": "Hand Tools"
  },
  {
    "toolId": 9,
    "name": "Precision Screwdriver Set",
    "description": "20-piece precision screwdriver set with magnetic tips",
    "quantityInStock": 8,
    "pricePerDay": 6.75,
    "isAvailable": true,
    "categoryId": 2,
    "categoryName": "Hand Tools"
  },
  {
    "toolId": 10,
    "name": "Professional Tool Box",
    "description": "Large tool box with multiple compartments and wheels",
    "quantityInStock": 1,
    "pricePerDay": 14.5,
    "isAvailable": true,
    "categoryId": 2,
    "categoryName": "Hand Tools"
  },
  {
    "toolId": 11,
    "name": "Adjustable Wrench Set",
    "description": "4-piece adjustable wrench set (6\", 8\", 10\", 12\")",
    "quantityInStock": 4,
    "pricePerDay": 9.99,
    "isAvailable": true,
    "categoryId": 2,
    "categoryName": "Hand Tools"
  },
  {
    "toolId": 12,
    "name": "Self-Propelled Lawn Mower",
    "description": "21-inch self-propelled gas lawn mower with mulching capability",
    "quantityInStock": 2,
    "pricePerDay": 35.99,
    "isAvailable": true,
    "categoryId": 3,
    "categoryName": "Garden Tools"
  },
  {
    "toolId": 13,
    "name": "Gas Leaf Blower",
    "description": "Powerful gas-powered leaf blower with variable speed",
    "quantityInStock": 3,
    "pricePerDay": 24.99,
    "isAvailable": true,
    "categoryId": 3,
    "categoryName": "Garden Tools"
  },
]
````
</details>

<details close>
<summary>Add breakfast to a booking</summary>
<br>
  
````
[GET] /api/Customer/GetAllCustomer
````
**Example Request URL**
````
[
  {
    "customerId": 1,
    "firstName": "Marmar",
    "lastName": "Varvar",
    "birthDate": "1998-10-29",
    "email": "marvar@something.com",
    "phoneNumber": "+9757647474",
    "address": "Somewhere in space 42"
  },
  {
    "customerId": 2,
    "firstName": "Margo",
    "lastName": "Fargo",
    "birthDate": "2005-10-29",
    "email": "marfar@something.com",
    "phoneNumber": "+987953846",
    "address": "Somewhere over the mountains 11"
  }
]
````
**Example Response**
````
{
}
````

</details>

<details close>
<summary>Cancel a booking</summary>
<br>
  
````
[PUT] /api/Guest/CancelBooking
````
**Example Request URL**
````

````

**Example Response**
````
{
    "message": "Booking is cancelled"
}

````
</details>

<details close>
<summary>Add a guest</summary>
<br>
  
````
[POST] /api/Guest/CreateGuest
````
**Example Request URL**
````

````

**Example Response**
````
{
  "id": 23,
  "firstName": "Test",
  "lastName": "Testsson",
  "email": "test@testmail.com",
  "phone": "555-444333",
  "bookings": null
}
````
</details>


<details close>
<summary>Delete a guest</summary>
<br>
  
````
[DEL] /api/Guest/DeleteGuest
````
**Example Request URL**
````
https://localhost:7047/api/Guest/DeleteGuest?guestId=5
````

**Example Response**
````
No response body
````
</details>

<details close>
<summary>See all future bookings</summary>
<br>
  
````
[GET] /api/Management/GetAllFutureBookings
````

**Request URL**
````
https://localhost:7047/api/Management/GetAllFutureBookings
````

**Example Response**
````
[
    {
        "id": 3,
        "guestName": "Larsson, Tom",
        "roomNames": [
            "103"
        ],
        "bookedFromDate": "2025-06-11",
        "bookedToDate": "2025-06-13",
        "numberOfAdults": 1,
        "numberOfChildren": 0Get All Customer 
    }
]

````
</details>

<details close>
<summary>See all future bookings grouped by week</summary>
<br>
  
````
[GET] /api/Management/GetBookingsGroupedByWeek
````
**Request URL**
````
https://localhost:7047/api/Management/GetBookingsGroupedByWeek
````

**Example Response**
````
[
    {
        "year": 2025,
        "week": 25,
        "bookings": [
            {
                "id": 4,
                "guestName": "Helgesson, Lisa",
                "roomNames": [
                    "106"
                ],
                "bookedFromDate": "2025-06-20",
                "bookedToDate": "2025-06-25",
                "numberOfAdults": 1,
                "numberOfChildren": 0
            },
            {
                "id": 7,
                "guestName": "Johansson, Oscar",
                "roomNames": [
                    "109"
                ],
                "bookedFromDate": "2025-06-16",
                "bookedToDate": "2025-06-18",
                "numberOfAdults": 2,
                "numberOfChildren": 0
            }
        ]
    }
]
````
</details>

<details close>
<summary>See all future bookings grouped by month</summary>
<br>
  
````
[GET] /api//
````
**Request URL**
````
https://localhost:7047/api//
````

**Example Response**
````
[

]
   

````
</details>

<details close>
<summary>Get a booking by id</summary>
<br>
  
````
[GET] /api/Management/GetBookingById
````
**Example Request URL**
````
https://localhost:7047/api/Management/GetBookingById?id=10
````

**Example Response**
````


````
</details>

<details close>
<summary>See the total price for a booking</summary>
<br>
  
````
[GET] /api/Management/GetTotalPrice?BookingId=3
````
**Example Request URL**
````
https://localhost:7047/api/Management/GetTotalPrice?BookingId=3
````

**Example Response**
````

````
</details>

<details close>
<summary>See available rooms by date and number of guests</summary>
<br>
  
````
[GET] /api/Management/GetAvailableRoomByDateAndGuests
````


**Example Response**
````
 

````
</details>


<details close>
<summary>See the booking history</summary>
<br>
  
````
[GET] /api/Management/GetBookingHistory
````


**Example Response**
````
[
 
]

````
</details>

<details close>
<summary>Update a booking</summary>
<br>
  
````
[PUT] /api/Management/UpdateBooking
````

**Example Response**
````

````
</details>

<details close>
<summary>Create a new booking</summary>
<br>
  
````
[POST] /api/Management/CreateBooking
````


**Example Response**
````

````
</details>

<details close>
<summary>Delete a booking</summary>
<br>
  
````
[DEL] /api/Management/DeleteBooking
````


**Example Response**
````
No response body
````
</details>
</details>

<details close>
<summary>Test strategy</summary>
  
---
  
### Unit Tests for Controller
To test our controllers, we used Moq to mock dependencies from the repository classes. We could simulate various scenarios and ensure that the appropriate HTTP responses were returned.

---
### Unit Tests for Repositories
For repository testing we used an in-memory database (InMemoryDatabase from Entity Framework). This allowed us to verify data database communication without depending on a real database.

---
### Integration Tests
For integration testing we used Postman by sending real HTTP requests to the API. This helped us verify that all endpoints worked as expected, including correct status codes, response bodies, and error handling. We used Postman to test full flows, such as creating bookings, finding available rooms, and cancelling bookings.

---
### Test Results
A few bugs were detected during testing. One example is that breakfast costs were included in the total booking amount, even when the customer had chosen to book without breakfast. These bugs were corrected during the testing phase.

We do not have full test coverage for all methods, mainly due to time constraints. Several new methods were added toward the end of the project, and we did not have enough time to implement tests for all of them. However, all members of the project group contributed to writing both controller and repository tests, and we prioritized testing our initial methods.


</details>

]
````
</details>

<details close>
<summary>something here </summary>
<br>
  
````
[GET] /api/Guest/GetAvailableRooms
````
**Example Request URL**
````

````

**Example Response**
````

````
</details>

<details close>
<summary>Add </summary>
<br>
  
````
[PUT] /api/Guest/AddBreakfast
````
**Example Request URL**
````

````
**Example Response**
````

````

</details>

<details close>
<summary>  </summary>
<br>
  
````
[PUT] /api/Guest/CancelBooking
````
**Example Request URL**
````
https://localhost:7047/api/Guest/CancelBooking?bookingId=10
````

**Example Response**
````
{
    "message": "Booking is cancelled"
}

````
</details>

<details close>
<summary>Add </summary>
<br>
  
````
[POST] /api/
````
**Example Request URL**
````

````

**Example Response**
````

````
</details>


<details close>
<summary>Delete </summary>
<br>
  
````
[DEL] /api/
````
**Example Request URL**
````

````

**Example Response**
````
No response body
````
</details>

<details close>
<summary>  </summary>
<br>
  
````
[GET] /api/
````

**Request URL**
````

````

**Example Response**
````


````
</details>

<details close>
<summary>  </summary>
<br>
  
````
[GET] /api/
````
**Request URL**
````

````

**Example Response**
````

````
</details>

<details close>
<summary>  </summary>
<br>
  
````
[GET] /api/
````
**Request URL**
````

````

**Example Response**
````


````
</details>


