Mediatek
Application de gestion du personnel d'une médiathèque 

Dans ce projet d'application simplifiée de fin de première année d'étude initialement prévu en MVC - winform - mariaDB, j'ai pu choisir d'intégrer des technologies différentes afin de me former sur des frameworks plus réalistes et de tenter d'intégrer certains concepts fondamentaux de la programmation.

- Utilisation :
L'unique manager de la médiathèque (qui ne peut pas être modifié ni supprimé) doit rentrer ses identifiants : mail et mot de passe, afin de se connecter. 
Il accède ensuite a la page de gestion du personnel ou il peut ajouter une personne ou sélectionner en sélectionner une affiché a l'écran afin de le modifier, le supprimer ou gérer ses absences.
Lorsque le manager choisi de gérer les absences d'un employé, il va sur le portail de gestion des absences de l'employé en question et peut ajouter une absence ou en sélectionner une afin de la modifier ou de la supprimer. 
Les actions possibles sont gérées par des boutons qui sont grisés lorsque les conditions d'accès ne sont pas remplies (par exemple : sélection d'un staff ou d'une absence) et chaque action d'enregistrement, suppression ou modification demande la confirmation du manager pour être exécutée.

- Fonctionnalités : 

    • **Connexion manager** : permet à l’unique manager de se connecter avec son mail et son mot de passe.




    • **Gestion des collaborateur** : le manager peut ajouter ou supprimer des collaborateurs et mettre à jour leurs informations. 




    • **Gestion des absences** : le manager peut ajouter, supprimer ou mettre à jour les absences des collaborateurs.




    • **Sécurité** : hachage du mot de passe avec SHA256 + sel afin de limiter les attaques par rainbow tables. Utilisation du secret manager afin de ne pas avoir le mot de passe en dur dans l’application lors de son initialisation dans le code. Utilisation d’un passwordBox afin que le mot de passe ne sois pas visible et soit crypté au sein de l’application.


- Commentaire :

J’ai choisi la norme ?? ce qui permet une meilleure lisibilité, je n’ai commenté que ce qui me semblais pertinent ou qui vise à mieux comprendre certains concepts nouveaux pour moi.


- Concepts de programmation

    • **Architecture N-Tier / Design pattern MVVM** Cela me semblait pertinent pour séparer clairement la logique métier et la représentation graphique, permettant un développement modulaire et faiblement couplé ce qui facilite les modifications. C’était également l’occasion d’essayer une variante de l’architecture N-Tier.



    • **WPF** Ce framework est particulièrement adapté au design pattern MVVM. Le dataBinding facilite la communication entre le viewModel et la view est autonome, les préoccupation sont clairement séparées. Cela m’a également permis d’étudier ce framework.

    • **Projet SVE.Mediatek : App**  Réalise les dataBinding et les injections de dépendance nécessaire à l’affichage de view par les viewModels 

    • **Injection de dépendance** J’ai utiliser la bibliothèque Microsoft.Extensions.DependencyInjection et son « conteneur » IserviceProvider afin de faciliter l’inversion du contrôle et la gestion des tests d’intégration dans la class App. Cela m’a aussi permis d’expérimenter ce concept important de la programmation.

    • ** Système de gestion de base de données** J’ai utilisé SQL Serveur management studio qui est une valeur forte de l’environnement .NET

    • **ORM** J’ai choisi Entity Framework qui est tout à fait adapté à un environnement .NET afin de me familiariser avec cette outil 
      
    • **Tests d’intégation** Je teste les méthodes CRUD développées dans le projet DAL


