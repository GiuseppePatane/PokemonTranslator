# PokemonTranslator API
This API returns the Shakespearean translation of a Pokémon description.
For doing this, call in the first place the [pokemon api](https://pokeapi.co/) and it takes from the descriptions List the first element that starts or contains the name of the Pokémon searched. If the descriptions list doesn't contain any element with these conditions the API will take the first element in the descriptions list.
The description taken  will then be sent to the [Shakespearean translation  API](https://funtranslations.com/api/shakespeare)  that will return the Shakespearean version of the Pokémon description. I'm using the free version of this API, so it's limited to 5 request per hour.



## Project structure 

![image](https://user-images.githubusercontent.com/13527363/104906023-739a1e00-5983-11eb-9493-038ac5805c11.png)



## Run local with the DotNet CLI

1. Clone or download this repository to local machine.

    ` git clone https://github.com/GiuseppePatane/PokemonTranslator.git` 

2. Install [.NET 5 SDK for your platform](https://www.microsoft.com/net/core#windowscmd) if didn't install yet.

3. go to the project folder:` cd PokemonTranslator` 

4.  run `dotnet restore PokemonTranslator.sln` 

5.  Optional run  `dotnet test`  for run all the solution tests.   **This could end the numbers of requests per hour of the Shakespearean translation API** 

6. run `dotnet run --project src/PokemonTranslator.Api/PokemonTranslator.Api.csproj` 

7. For test the api `curl -X GET "http://localhost:5000/pokemon/{pokemonName}" -H  "accept: application/json"`

8. For see the Swagger go to http://localhost:5000/swagger/index.html

## Run as docker container:

1. Clone or download this repository to local machine.

    ` git clone https://github.com/GiuseppePatane/PokemonTranslator.git` 
   
2. Install [Docker for your platform](https://www.docker.com/get-started) if didn't install yet.

3. install [Docker-Compose](https://docs.docker.com/compose/install/)  

4. ` cd PokemonTranslator` 

5. Optional run  `dotnet test`  for run all the solution tests.   **This could end the numbers of requests per hour of the Shakespearean translation API** 

6. run  ` docker-compose up` 

7. For test the api `curl -X GET "http://localhost:5000/pokemon/{pokemonName}" -H  "accept: application/json"`

8. For see the Swagger go to http://localhost:5000/swagger/index.html


