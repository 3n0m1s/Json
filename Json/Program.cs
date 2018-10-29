using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

//Particolare istanziamento ad oggetti per json con datamember e datacontract

namespace Json
{
    [DataContract]
    internal class Person
    {
        [DataMember]
        internal string id;

        [DataMember]
        internal string name;

        [DataMember]
        internal int age;

        [DataMember]
        internal Lavoro Lavoro;
    }

    [DataContract]
    public class Lavoro
    {
        [DataMember]
        internal bool lavora { get; set; }
        [DataMember]
        internal string Indirizzo { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {           
            Person p = new Person();
            Lavoro lav = new Lavoro();

            p.name = "Simone";
            p.age = 35;
            lav.lavora = false;
            p.Lavoro = lav;

            MemoryStream stream1 = new MemoryStream();

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Person));
            ser.WriteObject(stream1, p);

            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);

            Console.WriteLine("JSON: "+sr.ReadToEnd());

            stream1.Position = 0;
            Person p2 = (Person)ser.ReadObject(stream1);

            Console.WriteLine($"Deserializzato: name={p2.name}, age={p2.age}, Lavoro={p2.Lavoro.lavora}, indirizzo={p2.Lavoro.Indirizzo}");
            Console.ReadKey();
        }

    }
}
