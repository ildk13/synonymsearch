# Synonym Search Tool

This is a full-stack web application built as a synonym search tool. 
The project consists of a backend built with **C# (.NET)** and a frontend built with **React (TypeScript)**. 
It allows users to add synonyms for a word and search for synonyms in both directions (i.e., a word's synonym can be used to find the original word). 
The synonym search also implements a transitive rule, meaning if `A` is a synonym of `B`, and `B` is a synonym of `C`, then `C` is automatically considered a synonym of `A`.

## Features
- Add a word and its synonyms.
- Search for synonyms of a word.
- Implements the transitive rule of synonyms.
- User-friendly frontend using React and Material-UI.
- Backend built with .NET Core API.
- Dockerized for easy deployment.

## Technologies
- **Frontend**: React (TypeScript), Material-UI
- **Backend**: .NET Core Web API (C#)
- **State Management**: Zustand (React store)
- **Database**: In-memory storage (no persistence)
- **Testing**: xUnit, Moq, FluentAssertions
- **Containerization**: Docker

## Prerequisites
Before running the project, make sure you have the following installed:

- **Node.js**: v20 or later
- **.NET SDK**: v8 or later
- **Docker** (for containerization)
  
## Installation and Setup
### Backend
1. Clone the repository:
   ```bash
   git clone https://github.com/ildk13/synonymsearch.git
   cd synonymsearch/SynonymSearchAPI
   ```
2. Install dependencies:
   ```bash
   dotnet restore
   ```
3. Run the backend API:
   ```bash
   npm start
   ```

The backend API will be running on <mark>http://localhost:5089.</mark>

### Frontend
1. Navigate to the frontend directory:
   ```bash
   cd synonymsearch/SynonymSearch/synonymsearchapp
   ```
2. Install dependencies:
   ```bash
   npm install
   ```
3. Run the frontend app:
   ```bash
   npm start
   ```

The frontend will run on <mark>http://localhost:3000.</mark>

## API Endpoints
### Add Synonym
* Endpoint: /api/synonym/add
* Method: POST
* Payload:
  ```json
  {
    "word": "clean",
    "synonyms": ["wash", "tidy"]
  }
  ```
 * Description: Adds new synonyms for a word.
 ### Search Synonym
 * **Endpoint:** <mark>/api/synonym</mark>
 * **Method:** <mark>GET</mark>
 * **Parameters:** <mark>word</mark>
 * **Example:** <mark>/api/synonym?word=clean</mark>
 * **Description:** Returns the synonyms of the specified word.

 ## Docker Setup
 ### Dockerfile Configuration
 #### Backend <mark>Dockerfile</mark>
 In the <mark>backend</mark> directory, the following <mark>Dockerfile</mark> is used:
 ```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY *.csproj ./
RUN dotnet restore

COPY . .

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

EXPOSE 80

ENTRYPOINT [ "dotnet", "SynonymSearchAPI.dll" ]
```

#### Frontend <mark>Dockerfile</mark>
In the <mark>frontend</mark> directory, the following <mark>Dockerfile</mark> is used:
 ```dockerfile
FROM node:20.18.0

WORKDIR /app

COPY package*.json ./

RUN npm install

COPY . .

RUN npm run build

EXPOSE 3000

CMD [ "npm", "start" ]
```

## Docker Compose Setup
A <mark>docker-compose.yml</mark> file is used to manage both the frontend and backend services.
Here’s the configuration:
```yaml
version: '3.8'

services:
  api:
    build: 
      context: ./SynonymSearchAPI
      dockerfile: Dockerfile
    ports:
      - "5089:8080"
    networks:
      - synonym-network
    environment:
      - ASPNETCORE_ENVIROMENT=Development
    
  react-app:
    build: 
      context: ./SynonymSearch/synonymsearchapp
      dockerfile: Dockerfile
    ports:
      - "3000:3000"
    networks:
      - synonym-network

networks:
  synonym-network:
    driver: bridge
```

## Running the Application with Docker Compose
### 1. Build and start the containers using Docker Compose:

```bash
docker-compose up --build
```
This command will build both the frontend and backend images and start the services.
The backend will be available on <mark>http://localhost:5089</mark>, and the frontend will be accessible on <mark>http://localhost:3000</mark>.
### 1. Build and start the containers using Docker Compose:

```bash
docker-compose down
```

## Running Tests
To run the backend tests, navigate to the <mark>test</mark> project and run:

```bash
cd SynonymSearch/SynonymSearchAPI.Tests
dotnet test
```

This will execute the unit tests written using xUnit, Moq, and FluentAssertions.

### Troubleshooting
* **CORS Issues:** If you encounter CORS issues when making API calls from the frontend, ensure that the backend is correctly configured to allow requests from http://localhost:3000. The CORS configuration can be added in the Program.cs file.
* **Port Conflicts:** If the default ports (5000 for backend, 3000 for frontend) are in use, you can modify the ports in the docker-compose.yml file.

### Future Enhancements
* **Persistent Storage:** Implement a database (e.g., SQLite, PostgreSQL) for storing synonyms permanently.
* **Authentication:** Add user authentication and roles (admin, user) for managing synonyms.
* **Performance Improvements:** Optimize API response times by using more efficient data structures.


