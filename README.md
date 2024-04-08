# Pokedex API
A fun project in the form of a REST API that returns Pokemon information. It allows users to retrieve basic Pokemon information and get translated descriptions of Pokemon using existing public APIs.

### API Endpoints

1. **Basic Pokemon Information**: Returns standard Pokemon description and additional information.
    - Endpoint: `https://localhost:7273/pokemon/<pokemon name>`

2. **Translated Pokemon Description**: Returns translated Pokemon description and other basic information. If the Pokemon’s habitat is cave or it’s a legendary Pokemon, it will apply the Yoda translation, otherwise it will apply the Shakespeare translation.
    - Endpoint: `https://localhost:7273/pokemon/translated/<pokemon name>`

## How to Run

To run the project, follow these steps:

1. **Clone the Repository**: 
    ```bash
    git clone https://github.com/u-shahzad/Pokedex.git
    ```

2. **Navigate to the Project Directory**:
    ```bash
    cd Pokedex
    ```

3. **Run the Application**:
    ```bash
    dotnet run
    ```

4. **Access the API**:
    - Use a tool like [HTTPie](https://httpie.io/) or [Postman](https://www.postman.com/) to make HTTP requests to the API endpoints as described above.
    - You can also access the API using the Swagger UI Page at `https://localhost:7273/swagger/index.html` if you run your project with Visual Studio 2022.

## Instructions to build the Docker image and run it

1. **Build the Docker Image**: Open a terminal, navigate to the directory containing the Dockerfile and project files, and run the following command:
    ```bash
    docker build --rm -t pokedex .
    ```
    
2. **Run the Docker Container**: After successfully building the Docker image, you can run it using the following command:
   ```bash
    docker run --rm -p 5000:5000 -p 5001:5001 -e ASPNETCORE_HTTP_PORT=https://+:5001 -e ASPNETCORE_URLS=https://+:5000 pokedex
    ```
   
4. **Access the API**: Once the container is running, you can access the API by sending HTTP requests to `http://localhost:5000`
5. **Stop and Remove the Container (Optional)**: If you want to stop and remove the container, you can use the following commands:
   ```bash
    docker stop pokedex
    docker rm pokedex
    ```

## NuGet Packages
| Package Name  | Version |
| ------------- | ------------- |
| System.Net.Http.Json  | 13.0.3  |
| Newtonsoft.Json  | 8.0.0  |

## Suggestions for Production Environment

- **Caching Mechanism**: Implement a caching mechanism to reduce the number of external API calls and improve performance. Since Pokémon data doesn't change frequently, caching responses from the PokéAPI can significantly reduce latency and API usage costs.
- **Authentication and Authorization**: If the API is intended for restricted access, implement authentication and authorization mechanisms to control access to endpoints based on user roles and permissions.

## Additional Notes

- Ensure that you have the [.NET SDK](https://dotnet.microsoft.com/download) installed on your system.
- The project uses external APIs (PokéAPI and FunTranslations API) for fetching Pokemon data and translating descriptions, so ensure that you have internet access.
- If you encounter any issues or have questions, feel free to [open an issue](https://github.com/u-shahzad/Pokedex/issues) on the GitHub repository.
