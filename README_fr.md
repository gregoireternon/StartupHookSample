# Injection de code au runtime dans un projet dotnet core

## Intro
Javaiste confirmé, je travaille principalement sur des projets dotnet depuis 2015; Java est régulièrement considéré comme un vieux langage vieillissant, mais cet ecosystème gère pourtant un nombre considérable de fonctionalités qui ne sont pas prises en charge par ses "concurrents habituels".
En particulier, cela fait très longtemps que **Java permet l'injection de code au runtime**; pour concrétiser cela, il suffit de rajouter le code requis dans le CLASSPATH de l'application.
L'exemple typique de cette possibilité est l'usage d'un driver JDBC qui implémente une interface référencée dans le projet, mais qui n'est pas embarqué dans le package applicatif, mais fourni par le contexte d'exécution. Cela permet, dans ce cas, de ne pas se soucier du type de base de donnée sur lequel s'exécutera l'application, donc **réduire le couplage**
C'est particulièrement puissant car cela permet d'adapter le comportement d'un programme à son environnement d'exécution, sans avoir à le modifier.
![Component on different platforms](runtime-inj-01.png)

Une telle chose n'était pas possible en .NET Framework (à moins de permettre explicitement et programmatiquement au propgramme de charger une assembly externe.... avec tout ce que cela implique en terme de qualité de code, robustesse, ...): toutes les dépendances devaient être linkées dans la solution, lors du build; Vous aviez donc à préparer un package par plateforme cible, dès que le comportement devait être différent.
Bien sur, des conteournements existent, tels que l'usage de stratégies (pattern), mais dans ce cas, vous aviez à embarquer toutes les stratégies dans le même package.

Microsoft rattrape bien son retard grace à la famille dotnet core (dotnet 5, 6, ...): avec ce nouveau framework, dont le runtime présente de nombreuses similitudes avec la JVM, il est maintenant possible d'injecter des librairies au runtime;
Cela peut se faire au moins de 2 manières différentes:
- Grace au [runtime store](https://docs.microsoft.com/fr-fr/dotnet/core/deploying/runtime-store); sa manipulation est toutefois hasardeuse
- Grace au [Startup hook](https://github.com/dotnet/runtime/blob/main/docs/design/features/host-startup-hook.md)


Note: Vous pourez voir ce qui m'a conduit à privilégier le startup hook au runtime store [ici](https://github.com/dotnet/runtime/issues/61103)

## Comment utiliser ce projet
Just launch Launch.ps1 pour lancer le build ET l'exécution du programme.
Le programme contient:
- Une lib qui contient une interface partagée (IVegetable)
- Un projet dll StartupHookPJ: il s'agit d'une dll qui contient le code pour s'exécuter en startup hook (classe StartupHook), ainsi que des implémentations aditionnelles de IVegetable
- Un projet dont l'unique but est de lister les IVegetable qui sont dans son assembly (et qui ne contient que Turnip - Navet); notez bien que ce projet **ne référence pas** StartupHookPJ

L'execution retourne un truc du genre:
```
CommonLib Startup hook init
CommonLib Startup hook init
This project's goal is to list all available vegetables
turnip is a vegetable
cauliflower is a vegetable
Cucumber is a vegetable
leek is a vegetable
```
Comme vous pouvez le voir, l'itération renvoi des éléments qui ne sont pas dans le projet, mais injectés dans le programme du fait de la présence de la référence de la dll du startup hook dans la variable d'environnement DOTNET_STARTUP_HOOKS.
