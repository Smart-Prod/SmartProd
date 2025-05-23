﻿using MongoDB.Driver;

namespace SmartProd.Models
{
    public class ContextMongodb
    {
        public static string? ConnectionString { get; set; }
        public static string? Database { get; set; }
        public static bool IsSSL { get; set; }
        private IMongoDatabase _database { get; }


        public ContextMongodb()
        {
            try
            {
                MongoClientSettings settings = MongoClientSettings.
                    FromUrl(new MongoUrl(ConnectionString));

                if (IsSSL) 
                {
                    settings.SslSettings = new SslSettings 
                    {
                        EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12
                    };
                }

                var mongoCliente = new MongoClient(settings);
                _database = mongoCliente.GetDatabase(Database);
            }
            catch(Exception)
            {
                throw new Exception("Não foi possivel conectar");
            }
            
        }

        public IMongoCollection<Usuario> Usuario
        {
            get 
            {
                return _database.GetCollection<Usuario>("Usuario");
            }
        }
        public IMongoCollection<Produto> Produto
        {
            get
            {
                return _database.GetCollection<Produto>("Produto");
            }
        }

        public IMongoCollection<Cliente> Cliente
        {
            get
            {
                return _database.GetCollection<Cliente>("Cliente");
            }
        }

        public IMongoCollection<Fornecedor> Fornecedor 
        {
            get
            {
                return _database.GetCollection<Fornecedor>("Fornecedor");
            }
        }

        public IMongoCollection<Funcionario> Funcionario
        {
            get
            {
                return _database.GetCollection<Funcionario>("Funcionario");
            }
        }

        public IMongoCollection<Estoque> Estoque 
        {
            get
            {
                return _database.GetCollection<Estoque>("Estoque");
            }
        }

        public IMongoCollection<Entrada> Entrada
        {
            get
            {
                return _database.GetCollection<Entrada>("Entrada");
            }
        }

        public IMongoCollection<Saida> Saida
        {
            get
            {
                return _database.GetCollection<Saida>("Saida");
            }
        }

        public IMongoCollection<Departamento> Departamento
        {
            get
            {
                return _database.GetCollection<Departamento>("Departamento");
            }
        }
    }
}
