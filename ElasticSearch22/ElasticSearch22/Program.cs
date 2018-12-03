using Nest;
using System;
using System.Collections.Generic;

namespace ElasticSearch22
{
    class Program 
    {
        static void Main(string[] args)
        {
            Program checkp = new Program();
            dynamic c=checkp.Connect();
            checkp.Connect();
            checkp.CreateMapping(c);
            checkp.Check(c);
        }
        public dynamic Connect() {
            var node = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(
                node
            //defaultIndex: "my-application"
            );
            var client = new ElasticClient(settings);
            return client;
        }
        public  void Check(ElasticClient client)
        {
            var person = new Person
            {
                Id = "1",
                Firstname = "Martijn",
                Lastname = "Laarman"
            };
            var index = client.Index<Person>(person, i => i
                            .Index("myindex2")
                            .Type("person")
                            .Refresh(Elasticsearch.Net.Refresh.True));
        }

        public void CreateMapping(ElasticClient client)
        {
            client.CreateIndex("myindex2", c => c
                .Mappings(ms => ms
                    .Map<Person>(m => m.AutoMap())
                )
            );
        }
    }



    public class Person
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
