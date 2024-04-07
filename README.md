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

## NuGet Packages
| Package Name  | Version |
| ------------- | ------------- |
| System.Net.Http.Json  | 13.0.3  |
| Newtonsoft.Json  | 8.0.0  |

## Additional Notes

- Ensure that you have the [.NET SDK](https://dotnet.microsoft.com/download) installed on your system.
- The project uses external APIs (PokéAPI and FunTranslations API) for fetching Pokemon data and translating descriptions, so ensure that you have internet access.
- If you encounter any issues or have questions, feel free to [open an issue](https://github.com/u-shahzad/Pokedex/issues) on the GitHub repository.
