# PokemonTranslator
This API returns the Shakespearean translation of a Pokémon description.
For doing this, call in the first place the [pokemon api](https://pokeapi.co/) and takes from the description List the first element that starts with the name of the Pokémon searched. if the descriptions list doesn't contain any element with this condition the API will take the first element that contains the Pokémon name.
The description taken  will then be sent to the [Shakespearean translation  API](https://funtranslations.com/api/shakespeare)  that will return the Shakespearean version of the Pokémon description.

### Run local with the DotNet CLI
1. Clone or download this repository to local machine.

    ` git clone https://github.com/GiuseppePatane/PokemonTranslator.git` 

2. Install [.NET 5 SDK for your platform](https://www.microsoft.com/net/core#windowscmd) if didn't install yet.

3. go to the project folder:` cd PokemonTranslator` 

4.  run `dotnet restore PokemonTranslator.sln` 

5.  Optional run  `dotnet test`  for run all the solution tests.   **This will end the numbers of requests per hour of the Shakespearean translation API** 

6. run `dotnet run --project src/PokemonTranslator.Api/PokemonTranslator.Api.csproj` 

7. For test the api `curl -X GET "http://localhost:5000/pokemon/{pokemonName}" -H  "accept: application/json"`

8. For see the Swagger go with a browser on http://localhost:5000/swagger/index.html

### Run as docker container:

1. Clone or download this repository to local machine.

    ` git clone https://github.com/GiuseppePatane/PokemonTranslator.git` 
   
2. Install [Docker for your platform](https://www.docker.com/) if didn't install yet.

3. install [Docker-Compose](https://docs.docker.com/compose/install/)  

4. ` cd PokemonTranslator` 

5. Optional run  `dotnet test`  for run all the solution tests.   **This will end the numbers of requests per hour of the Shakespearean translation API** 

6. run  ` docker-compose up` 

7. For test the api `curl -X GET "http://localhost:5000/pokemon/{pokemonName}" -H  "accept: application/json"`

8. For see the Swagger go with a browser on http://localhost:5000/swagger/index.html


