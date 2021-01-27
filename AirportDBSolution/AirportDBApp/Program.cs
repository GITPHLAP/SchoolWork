using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportDBApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (AirportDBEntities ctx = new AirportDBEntities())
            {
                //Delete everything in tables
                ctx.Database.ExecuteSqlCommand(
                    "DELETE FROM Flights"
                    );
                ctx.Database.ExecuteSqlCommand(
                    "DELETE FROM Route"
                    );
                ctx.Database.ExecuteSqlCommand(
                    "DELETE FROM Airplane"
                    );
                ctx.Database.ExecuteSqlCommand(
                    "DELETE FROM Airlines"
                    );
                ctx.Database.ExecuteSqlCommand(
                    "DELETE FROM Airport"
                    );
                ctx.SaveChanges(); //save changes - it save what the airplane i just had made



                Airline sas = new Airline() //create a new airplane 
                {
                    ICAO = "SAS",
                    Name = "Scandinavia Airlines"
                };

                Airline vir = new Airline() //create a new airplane 
                {
                    ICAO = "VIR",
                    Name = "Virgin Atlantic"
                };

                Airline cca = new Airline() //create a new airplane 
                {
                    ICAO = "CCA",
                    Name = "Air china"
                };

                Airline ual = new Airline() //create a new airplane 
                {
                    ICAO = "UAL",
                    Name = "United Airlines"
                };

                ctx.Airlines.Add(sas); //add the airplane to airplane table in database
                ctx.Airlines.Add(vir); //add the airplane to airplane table in database
                ctx.Airlines.Add(cca); //add the airplane to airplane table in database
                ctx.Airlines.Add(ual); //add the airplane to airplane table in database

                ctx.SaveChanges(); //save changes - it save what the airplane i just had made


                Airplane airplane = new Airplane() //create a new airplane 
                {
                    No = "SK161",
                    Type = "CRJ9",
                    Airline_ICAO = sas
                };

                Airplane airplane2 = new Airplane() //create a new airplane 
                {
                    No = "VS250",
                    Type = "B789",
                    Airline_ICAO = vir
                };

                Airplane airplane3 = new Airplane() //create a new airplane 
                {
                    No = "UA2785",
                    Type = "B772",
                    Airline_ICAO = ual
                };

                Airplane airplane4 = new Airplane() //create a new airplane 
                {
                    No = "CA568",
                    Type = "A359",
                    Airline_ICAO = cca
                };

                ctx.Airplanes.Add(airplane); //add the airplane to airplane table in database
                ctx.Airplanes.Add(airplane2); //add the airplane to airplane table in database
                ctx.Airplanes.Add(airplane3); //add the airplane to airplane table in database
                ctx.Airplanes.Add(airplane4); //add the airplane to airplane table in database

                ctx.SaveChanges(); //save changes - it save what the airplane i just had made


                Airport airport1 = new Airport() //create a new airplane 
                {
                    IATA = "CPH",
                    Name = "Copenhagen Airport",
                    City = "Copenhagen",
                    Country = "Denmark"
                };

                Airport airport2 = new Airport() //create a new airplane 
                {
                    IATA = "MIA",
                    Name = "Miami International Airport",
                    City = "Miami",
                    Country = "United States"
                };

                Airport airport3 = new Airport() //create a new airplane 
                {
                    IATA = "IAD",
                    Name = "Wasinghton Airport",
                    City = "Wasington, D.C ",
                    Country = "United States"
                };

                Airport airport4 = new Airport() //create a new airplane 
                {
                    IATA = "SKP",
                    Name = "Skopje Airport"
                };

                Airport airport5 = new Airport() //create a new airplane 
                {
                    IATA = "MAD",
                    Name = "Madrid Airport",
                    City = "Madrid",
                    Country = "Espanio"
                };

                ctx.Airports.Add(airport1); //add the airplane to airplane table in database
                ctx.Airports.Add(airport2); //add the airplane to airplane table in database
                ctx.Airports.Add(airport3); //add the airplane to airplane table in database
                ctx.Airports.Add(airport4); //add the airplane to airplane table in database
                ctx.Airports.Add(airport5); //add the airplane to airplane table in database



                ctx.SaveChanges(); //save changes - it save what the airplane i just had made


                Route route = new Route() //create a new airplane 
                {
                    Airport_Departure = airport1,
                    Airport_Arrival = airport2,
                    Airline_ICAO = sas
                };

                Route route2 = new Route() //create a new airplane 
                {
                    Airport_Departure = airport1,
                    Airport_Arrival = airport4,
                    Airline_ICAO = vir
                };

                Route route3 = new Route() //create a new airplane 
                {
                    Airport_Departure = airport3,
                    Airport_Arrival = airport5,
                    Airline_ICAO = cca
                };

                Route route4 = new Route() //create a new airplane 
                {
                    Airport_Departure = airport2,
                    Airport_Arrival = airport1,
                    Airline_ICAO = sas
                };

                Route route5 = new Route() //create a new airplane 
                {
                    Airport_Departure = airport5,
                    Airport_Arrival = airport1,
                    Airline_ICAO = ual
                };

                ctx.Routes.Add(route); //add the airplane to airplane table in database
                ctx.Routes.Add(route2); //add the airplane to airplane table in database
                ctx.Routes.Add(route3); //add the airplane to airplane table in database
                ctx.Routes.Add(route4); //add the airplane to airplane table in database
                ctx.Routes.Add(route5); //add the airplane to airplane table in database
                ctx.SaveChanges(); //save changes - it save what the airplane i just had made


                Flight flight = new Flight() //create a new airplane 
                {
                    Airplane_No = airplane,
                    Airport_Departure = airport1,
                    Airport_Arrival = airport2,
                    Airline_ICAO = sas,
                    DepartureTime = DateTime.Now,
                    ArrivalTime = DateTime.Now.AddHours(15)
                };

                Flight flight2 = new Flight() //create a new airplane 
                {
                    Airplane_No = airplane2,
                    Airport_Departure = airport1,
                    Airport_Arrival = airport4,
                    Airline_ICAO = vir,
                    DepartureTime = DateTime.Now.AddHours(10),
                    ArrivalTime = DateTime.Now.AddHours(7)
                };

                Flight flight3 = new Flight() //create a new airplane 
                {
                    Airplane_No = airplane3,
                    Airport_Departure = airport3,
                    Airport_Arrival = airport5,
                    Airline_ICAO = cca,
                    DepartureTime = DateTime.Now.AddHours(1),
                    ArrivalTime = DateTime.Now.AddHours(2)
                };

                Flight flight4 = new Flight() //create a new airplane 
                {
                    Airplane_No = airplane2,
                    Airport_Departure = airport2,
                    Airport_Arrival = airport1,
                    Airline_ICAO = sas,
                    DepartureTime = DateTime.Now,
                    ArrivalTime = DateTime.Now.AddHours(4)
                };

                Flight flight5 = new Flight() //create a new airplane 
                {
                    Airplane_No = airplane4,
                    Airport_Departure = airport5,
                    Airport_Arrival = airport1,
                    Airline_ICAO = ual,
                    DepartureTime = DateTime.Now.AddHours(3),
                    ArrivalTime = DateTime.Now.AddHours(18)
                };

                ctx.Flights.Add(flight); //add the airplane to airplane table in database
                ctx.Flights.Add(flight2); //add the airplane to airplane table in database
                ctx.Flights.Add(flight3); //add the airplane to airplane table in database
                ctx.Flights.Add(flight4); //add the airplane to airplane table in database
                ctx.Flights.Add(flight5); //add the airplane to airplane table in database
                ctx.SaveChanges(); //save changes - it save what the airplane i just had made






                var query = ctx.Airlines; // query to get airlines from table

                foreach (var item in query)
                {
                    Console.WriteLine(item.ICAO);
                }





                Console.ReadLine();

            }



        }
    }
}
