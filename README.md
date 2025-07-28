# BookManagementSystem

A complete C#/.NET console application for managing a library book list with full CRUD operations, search functionality, and JSON data persistence.


## Features

- Add new books to the library inventory
- Remove books by Book ID
- Update book details (title, author, availability)
- Search books by title or author
- List all available books
- JSON data persistence (auto-saves on changes)
- Unit tested with xUnit and Moq

  ## Prerequisites

  Before running the application, ensure you have:
- .NET 8 SDK
- Visual Studio 2022 or Visual Studio Code
- Git

  ## Installation

  ### Clone the Repo:
- git clone https://github.com/rinaejustin963/BookManagementSystem.git
- cd BookManagementSystem

  ### Restore NuGet packages:
  - dotnet restore

  ### Build the solution:
  - dotnet build
 
  ### Running the Application:
  - dotnet run --project BookManagementSystem
 
  ### Testing:
  This project includes unit tests:

  - dotnet test
 
  ### To run specific tests:
  - dotnet test --filter "FullyQualifiedName~AddBook"
 
  ### To list all tests:
  - dotnet test --list-tests
 
  ### Preview:
  - <img width="610" height="510" alt="Screenshot 2025-07-28 002402" src="https://github.com/user-attachments/assets/73daff2f-52cb-4354-beb1-e61b354d65d6" />
  - <img width="410" height="224" alt="Screenshot 2025-07-28 002203" src="https://github.com/user-attachments/assets/d8b006ca-3c46-4f45-8f77-41012985f364" />
  - <img width="760" height="290" alt="image" src="https://github.com/user-attachments/assets/d4a426a4-fa01-49ab-8e6b-28cb094d4d60" />


