# Code injection at runtime in a dotnet core project

## Intro
Java is the first "object oriented" language I have learned; From 2015, I work mainly on dotnet projects; Java is regularly considered as an old dying language; But I constantly keep in mind things that are usual in this language, but are not possible in other languages.
In particular, in this article: Java enables people to inject dependencies at runtime, for example using CLASSPATH; 
The most well-known example is the usage of JDBC drivers: you can build a project planning to use database persistence, without having to choose at building time which Database (Oracle, Sybase, SQL Server, ...) you will use at runtime: Convenient JDBC driver will be provider by environment. This is really powerful because you can adapt your program's behavior regarding its hosting platform.
