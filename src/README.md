# Extra exercise - put destination data in csv and consume it with a type provider

Type provider is one powerfull features that did not fit in the original workshop content, but here is an extra exercise that you can play with.  

* Start from `done`
* Add the nuget package `FSharp.Data`
* Create a folder in `FSharpIntro`
* Add three csv files in that folder
    * `destinations.csv` - format: `id,name`
    * `prices.csv` - format: `fromId(int),toId,economy(float),business(float),first(float)`
    * `distances.csv` - format: `fromId(int),toId,km(float)`
* Fill the files with the data from the exercises
* Change the code to use `CsvProvider` instead of hard coded lists in the `Data.fs` file.
* Play around

The code in this branch contains a working solution with the csv type provider.

## More advanced

If you have even more type I suggest to add an extra database to the project and use the `SqlClient` type provider to interact with it.