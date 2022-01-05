using System.Linq;

namespace LeGrandRestaurant
{
    public class Restaurant
    {
        private readonly Serveur[] _serveurs;

        public Restaurant(params Serveur[] serveurs)
        {
            _serveurs = serveurs;
        }

        public decimal ChiffreAffaires 
            => _serveurs.Sum(serveur => serveur.ChiffreAffaires);
    }
}
