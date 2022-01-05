using Xunit;

namespace LeGrandRestaurant.Test
{
    public class CuisineTest
    {
        [Fact(DisplayName = "Étant donné qu'une commande est transmise par un serveur" +
                            "Quand la cuisine la reçoit" +
                            "Alors elle l'accepte")]
        public void Preparation_Plat()
        {
            // Étant donné une commande transmis par un serveur
            var serveur = new Serveur();
            var commande = serveur.TransmetCommande();

            var cuisine = new Cuisine();
            //cuisine.RecoitCommande(commande);
        }
    }
}
