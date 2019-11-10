﻿using Flunt.Validations;
using PR.Shared.Entities;
using System.Collections.Generic;

namespace PR.Domain.Entities
{
    public class Responsible : Entity
    {
        public Responsible()
        {
            Constructions = new List<Construction>();
        }
        public Responsible(string name, string crea, string email, string phone)
        {
            new Contract()
                .IsNullOrEmpty(name, "Name", "O Nome é campo obrigatório")
                .IsNullOrEmpty(crea, "CREA", "O CREA é campo obrigatório")
                .IsEmail(email, "Email", "Email invalido")
                .IsNullOrEmpty(phone, "Phone", "A Telefone é campo obrigatório");
            Name = name;
            CREA = crea;
            Email = email;
            Phone = phone;
        }

        public List<Construction> Constructions { get; set; }
        public string Name { get; set; }
        public string CREA { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public void Update(string name, string email, string phone)
        {
            Name = name;
            Email = email;
            Phone = phone;

            AddNotifications(new Contract()
                .IsNullOrEmpty(name, "Name", "O Nome é campo obrigatório")
                .IsEmail(email, "Email", "Email invalido")
                .IsNullOrEmpty(phone, "Phone", "A Telefone é campo obrigatório")
                );
        }
    }
}
