# Pokemon Basics

The idea behind this project was to create foundation for my future Pokemon related projects

## Main goal

The main goal of the project was to create a database of entities related to Pokemons that could be useful for my future project

There is over 1000 pokemons. Some of them have regional forms, multiple evolution lines, Gmax or Mega variants... Sounds complicated? Wait till I get to pokedexes, games, damage relations between types and exceptions from that combined with pokemon abilities or moves

I wanted to create my own API which would store all of the necessary data and serve it just the way I need it without any complex analysis required

## Purpose of each project in the solution

Each project had a different purpose and they were split based on their responsibilities

- <b>Models</b>: Classes representing tables in my database marked with interface IModel
- <b>Data Access</b>: Contains DbContext for EntityFramework Core. Maintains history of migrations. Used to keep database schema in sync with my code
- <b>Logger</b>: My own logging class (wanted to try it)
- <b>Tests</b>: Tests for <b>ExternalApiCrawler</b> and <b>PokemonAPI</b> project
- <b>ExternalApiCrawler</b>: will be covered in a [seperate section](#external-api-crawler)
- <b>PokemonAPI</b>: API serving fetched and stored data in a desired manner for my other projects e.g. [PokeWaekness](https://github.com/PepeKwapien/PokeWeakness)

## External API Crawler

There is a lot of resources that serve information about Pokemon. The most popular is [PokeApi](https://pokeapi.co/)

The problem with this API is that in order to get some of the information I need I would reuire to do my own research how to extract the essential data.
This API serves a huge amount of information but some of it might be troublesome to use the way they are represented (I am looking at you evolution chains).

Having said that this API is a RestfulAPI which allows me to get to one endpoint and get all of the data in its subpaths. That's why I created this crawler to get all of the information I need, analyze it, clean it, map and put it in the database

Not going into a lof of details project consists of 4 main components:
- DTOs which are just classes with properties I want to retrieve from the endpoints. These classes are marked with IDto interface
- Requesters which send requests to the corresponding ednpoint and perform logic of going through the restful API. Requesters retrieve IDto collections
- Mappers which take IDto collections (most of the time more than one collection) and perform a set of actions to make IModel collection out of them (n IDto collections -> 1 IModel collection) which is then saved to the database. Mappers have to work in a correct order otherwise the data saved to the database will generate errors. Mappers can be chained using fluent API method and started so that they execute in correct order
- Orchestrator which assures that everything happens in a correct order. First requesters need to get collections of IDtos, then orchestrator sets the collections mappers will be working on and lastly it creates a correct chain of mappers and starts them. The whole process lasts approximately 10 minutes depending on your computer and network connection

<b>Be warned</b>:
In its peak this process can take up to 2GB of your memory

After the program.cs stops being executed the database is filled with objects related to pokemons


## PokemonAPI

It's a simple API that I develop when I need it for side projects or learning.

To run it simply provide connection string to the database you have filled previously (using Data Access and External API Crawler), and run the app using either dotner, or docker.

When using docker provide DB_TYPE and DB_CONNECTION_STRING envs
