﻿using System.ComponentModel.DataAnnotations;

namespace WSConvertisseur.Models
{
    /// <summary>
    /// Classe Devise, représentant un objet devise avec un id, un nom, un taux
    /// </summary>
    public class Devise
    {
        private int id;

        /// <summary>
        /// Obtient ou définit l'identifiant de la devise.
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string? nomDevise;

        /// <summary>
        /// Obtient ou définit le nom de la devise.
        /// </summary>
        [Required]
        public string? NomDevise
        {
            get { return nomDevise; }
            set { nomDevise = value; }
        }

        private double taux;

        /// <summary>
        /// Obtient ou définit le taux de la devise.
        /// </summary>
        public double Taux
        {
            get { return taux; }
            set { taux = value; }
        }

        public Devise(int id, string nomDevise, double taux)
        {
            this.id = id;
            this.nomDevise = nomDevise;
            this.taux = taux;
        }

        public Devise()
        {

        }

        public override bool Equals(object? obj)
        {
            return obj is Devise devise &&
                   id == devise.id &&
                   Id == devise.Id &&
                   nomDevise == devise.nomDevise &&
                   NomDevise == devise.NomDevise &&
                   taux == devise.taux &&
                   Taux == devise.Taux;
        }
    }
}
