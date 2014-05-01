#Project NURL


##Spécifications

Nurl est un utilitaire en ligne de commande qui permet de voir, télécharger ou afficher des informations à partir d'une url.
L'executable sera nommé `nurl.exe`. Il pourra être lancé avec une seule commande à la fois et plusieurs options (comme décrit dans les exemples ci-dessous).


Voici l'ensemble des options disponibles dans l'application:

1. Affiche dans la console le contenu du fichier sité à l'url `abc`:

	nurl.exe get -url "http://abc" 


2. Sauvegarde le contenu de l'url `http://abc` dans le fichier `c:\abc.json`:

	nurl.exe get -url "http://abc" -save "c:\abc.json"


3. Teste le temps de chargement du ficher à l'url `http://abc` 5 fois et affiche les 5 temps

	nurl.exe test -url "http://abc" -times 5 

4. Teste le temps de chargement du fichier à l'url `http://abc` et affiche la moyenne du temps de chargement

	nurl.exe test -url "http://abc" -times 5 -avg


##Tâches

1. Créez un répertoire `Projects\nurl` dans votre repository projet github.

1. Transcrivez l'exemple complet sous forme de specifications dans un fichier `specs.txt`. Soit par exemple pour les 2 premières options:


		Feature:  GET
			In order : to see the content of a web page
			as a : shell fan
			I want to download a web page

		Scenario: show the content of a page
			Given : I entered a fake url option `-url "http://fake"`
			And : any other option
			When : I press enter
			Then : the result should be `<h1>hello</h1>`

		Scenario: save the content of a page
			Given : I entered a fake url option `-url "http://fake"`
			And : I enter the option `-save` with the value 'fake.txt'
			When : I press enter
			Then : a file called `fake.txt` should be created with the content `<h1>hello</h1>`


1. Implémentez votre moteur de traitement en le validant pour chaque fonctionalité par des tests en suivant les scénarios des spécifications. Par exemple:

		[Test]
		public void Should_show_the_content_of_a_page()
		{
			//given
			var command = null //votre implémentation

			//when
			var result = command.Show(url); //exemple d'implémentation

			//then

			Assert.That(result, Is.EqualTo("<h1>hello</h1>"))
		}
		
Cet exemple représente la transcription sous forme de code de votre scenario de test, mais vous devrez -très certainement- avoir des tests unitaires intermédiaires.

Faites un commit pour chaque Feature quand test/implémentation sont passés au vert.

1. Faites des recherches sur la meilleure manière d'effectuer le traitement des arguments et prennez la librairie qui vous convient (ou passez vous d'une librairie et faites le à la main)

1. Implémentez votre application en prennant en compte les arguments de la ligne de commande et branchez le tout avec votre moteur applicatif

1. Regardez/recherchez comment télécharger du contenu web et implémentez cela dans votre moteur.


### Notes:

* Pensez à découpler au maximum pour pouvoir tout tester
* Ne soyez pas choqués par avoir l'impression de coder cela dans le sens inverse auquel vous êtes habitués.
* testez finalement avec une vrai url mais uniquement à la fin
* pas de commit, pas de note de participation	

### Notes 2:

* pensez à bien commencer par implémenter votre coeur applicatif, la partie du code qui va effectivement aller faire une requete n'arrive qu'à la fin, de même que la partie qui gère les inputs.
* l'historique de vos commits sera regargé pour voir comment vous avez construit votre code.

_**Note**_: exemple d'url de test avec du vrai contenu : `http://api.openweathermap.org/data/2.5/weather?q=paris&units=metric`
