using MongoDB.Driver;

namespace mvc_MongoDB.Models
    //para criar a conexão
{
    public class ContextMongoDB
    {
        //configurtação
        public static string ConnectionString { get; set; }
        public static string DataBaseName { get; set; }
        public static bool IsSSL { get; set; }
        private IMongoDatabase _database { get; }

        //construtor
        public ContextMongoDB()
        {
            //pegar configurações do appSettings
            //para conectar ao banco
            try { 
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));
                if (IsSSL) {//se for verdadeiro que vai usar SSL
                    settings.SslSettings = new SslSettings 
                    //gera uma instancia configuração ssl
                    {
                        EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 
                        //habilitando protocolo para receber =tls 12
                    };
                }
                var MongoClient = new MongoClient(settings);
                // instancia um client mongo  passando as configurações 
                _database = MongoClient.GetDatabase(DataBaseName);
            } 
            catch (Exception)
            {
                throw new Exception("não foi possivel conectar!");
            }

        }

        public IMongoCollection<Usuario> Usuario {
            get 
            {
                return _database.GetCollection<Usuario>("Usuario");//"nome" da model, no caso a tabela que está no banco
            }
        }
    }

}
