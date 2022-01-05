using System;

namespace LeGrandRestaurant
{
    public class Serveur
    {
        public decimal ChiffreAffaires { get; private set; }

        public void PrendreCommande(decimal montantCommande)
        {
            ChiffreAffaires += montantCommande;
        }

        public String TransmetCommande()
        {
            throw new NotImplementedException();
        }
    }
}
