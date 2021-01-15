# PokemonTranslator
This API returns the shekirian translation of a pokemon description.
For doing that call in the first place the [pokemon api](https://pokeapi.co/) and take from the description List the first element that starts with the name of the pokemon searched.
if the description list doesn't contain any element with this condition the API will take the first element that contains the pokemon name.




### Run local with the DotNet CLI
1. Clone or download this repository to local machine.

    ` git clone https://github.com/GiuseppePatane/PokemonTranslator.git` 

2. Install [.NET Core SDK for your platform](https://www.microsoft.com/net/core#windowscmd) if didn't install yet.

3. ` cd PokemonTranslator` 

4.  run `dotnet restore PokemonTranslator.sln` 

5. run `dotnet run --project src/PokemonTranslator.Api/PokemonTranslator.Api.csproj` 

6. For test the api `curl -X GET "http://localhost:5000/pokemon/{pokemonName}" -H  "accept: application/json"`

7. For see the Swagger go with a browser on http://localhost:5000/swagger/index.html

### Run as docker container:

1. Clone or download this repository to local machine.

    ` git clone https://github.com/GiuseppePatane/PokemonTranslator.git` 
   
2. Install [Docker for your platform](https://www.docker.com/) if didn't install yet.

3. install [Docker-Compose](https://docs.docker.com/compose/install/)  

4. ` cd PokemonTranslator` 

5. run  ` docker-compose up` 

6. For test the api `curl -X GET "http://localhost:5000/pokemon/{pokemonName}" -H  "accept: application/json"`

7. For see the Swagger go with a browser on http://localhost:5000/swagger/index.html


