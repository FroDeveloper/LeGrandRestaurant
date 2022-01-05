using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace LeGrandRestaurant.Test
{
    public class ChiffreAffaireTest
    {
        [Fact(DisplayName = "Étant donné un nouveau serveur " +
                            "Quand je calcule son chiffre d'affaire " +
                            "Alors il est de zéro")]
        public void Serveur_Initial()
        {
            // Étant donné un nouveau serveur
            var serveur = new Serveur();

            // Quand je calcule son chiffre d'affaire
            var chiffreAffaires = serveur.ChiffreAffaires;

            // Alors il est de zéro
            Assert.Equal(0, chiffreAffaires);
        }

        [Fact(DisplayName = "Étant donné un nouveau serveur " +
                            "Quand il prend une commande " +
                            "Alors son chiffre d'affaires est le montant de cette commande")]
        public void Serveur_Commande()
        {
            // Étant donné un nouveau serveur
            var serveur = new Serveur();

            // Quand il prend une commande
            var montantCommande = new decimal(67.8);
            serveur.PrendreCommande(montantCommande);

            // Alors son chiffre d'affaires est le montant de cette commande
            var chiffreAffaires = serveur.ChiffreAffaires;
            Assert.Equal(montantCommande, chiffreAffaires);
        }

        [Fact(DisplayName = "Étant donné un serveur ayant déjà une commande " +
                            "Quand il prend une autre commande " +
                            "Alors son chiffre d'affaires est l'addition des deux")]
        public void Serveur_2_Commandes()
        {
            // Étant donné un serveur ayant déjà une commande
            var serveur = new Serveur();
            var montantPremièreCommande = new decimal(67.8);
            serveur.PrendreCommande(montantPremièreCommande);

            // Quand il prend une autre commande
            var montantSecondeCommande = new decimal(178);
            serveur.PrendreCommande(montantSecondeCommande);

            // Alors son chiffre d'affaires est l'addition des deux
            Assert.Equal(
                montantPremièreCommande + montantSecondeCommande,
                serveur.ChiffreAffaires
            );
        }

        [Fact(DisplayName = "Étant donné un restaurant ayant 2 serveurs " +
                            "Quand chacun prend une commande " +
                            "Alors le chiffre d'affaire du restaurant est la somme des deux")]
        public void Restaurant()
        {
            // Étant donné un restaurant ayant 2 serveurs
            var serveurs = new[] { new Serveur(), new Serveur() };
            var restaurant = new Restaurant(serveurs);

            // Quand chacun prend une commande
            var montantCommande = new decimal(67.8);
            foreach (var serveur in serveurs)
                serveur.PrendreCommande(montantCommande);

            // Alors le chiffre d'affaire du restaurant est la somme des deux
            Assert.Equal(
                montantCommande * 2,
                restaurant.ChiffreAffaires
            );
        }

        [Theory(DisplayName = "Étant donné une franchise ayant X restaurants " +
                            "Quand Y serveurs dans chaque restaurant prennent une commande " +
                            "Alors le chiffre d'affaires de la franchise est la somme de ces commandes")]
        [InlineData((int) 0, (int)0)]
        [InlineData((int) 1, (int) 0)]
        [InlineData((int) 2, (int)0)]
        [InlineData((int) 10000, (int)0)]
        [InlineData((int)1, (int)1)]
        [InlineData((int)2, (int)1)]
        [InlineData((int)10000, (int)1)]
        [InlineData((int)1, (int)2)]
        [InlineData((int)2, (int)2)]
        [InlineData((int)10000, (int)2)]
        [InlineData((int)1, (int)100)]
        [InlineData((int)2, (int)100)]
        [InlineData((int)10000, (int)100)]
        public void Franchise(int nombreRestaurants, int nombreServeursParRestaurant)
        {
            // Étant donné une franchise ayant X restaurants
            var serveurs = Enumerable.Range(0, nombreRestaurants)
                .Select(_ => 
                    Enumerable.Range(0, nombreServeursParRestaurant)
                        .Select(_ => new Serveur())
                        .ToArray())
                .ToArray();

            var restaurants = Enumerable.Range(0, nombreRestaurants)
                .Select(numéro => new Restaurant(serveurs[numéro]))
                .ToArray();

            var commandes = Enumerable.Range(0, nombreRestaurants)
                .Select(numéroRestaurant =>
                    Enumerable.Range(0, nombreServeursParRestaurant)
                        .Select(numéroCommande => new decimal(1.5) * numéroCommande * numéroRestaurant)
                        .ToArray())
                .ToArray();

            var franchise = new Franchise(restaurants);

            // Quand Y serveurs par restaurant prennent une commande
            for (var noRestaurant = 0; noRestaurant < nombreRestaurants; noRestaurant++)
            {
                var serveursDuRestaurant = serveurs[noRestaurant];
                var commandesDuRestaurant = commandes[noRestaurant];

                for (var noServeur = 0; noServeur < nombreServeursParRestaurant; noServeur++)
                {
                    var serveur = serveursDuRestaurant[noServeur];
                    var commande = commandesDuRestaurant[noServeur];

                    serveur.PrendreCommande(commande);
                }
            }

            // Alors le chiffre d'affaires de la franchise est la somme de ces commandes
            var somme = commandes.Sum(groupe => groupe.Sum());
            Assert.Equal(somme, franchise.ChiffreAffaires);
        }

    }
}
