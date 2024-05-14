using OutingRepository.Repository;
using System;
using System.Globalization;

namespace OutingRepository.Console;

public class ProgramUI
{
    private readonly OutingRepo _repository;
    private readonly Random _random = new Random();

    public ProgramUI()
    {
        _repository = new OutingRepo();
        InitializeSampleData();
    }

    private void InitializeSampleData()
    {
        // Define total costs for each event type for the year
        Dictionary<Outing.EventType, decimal> yearlyCosts = new Dictionary<Outing.EventType, decimal>
        {
            { Outing.EventType.Golf, 2000.00m },
            { Outing.EventType.Bowling, 1500.00m },
            { Outing.EventType.AmusementPark, 3000.00m },
            { Outing.EventType.Concert, 5000.00m }
        };

        // Approximate number of outings per type (assuming monthly outings)
        int eventsPerType = 12;

        foreach (var eventType in yearlyCosts.Keys)
        {
            decimal totalCostPerType = yearlyCosts[eventType];
            decimal averageCostPerEvent = totalCostPerType / eventsPerType;

            for (int i = 0; i < eventsPerType; i++)
            {
                // Randomize number of attendees (20 to 100)
                int attendees = _random.Next(20, 101);
                
                decimal costPerPerson = Math.Round(averageCostPerEvent / attendees, 2);

                
                DateTime eventDate = new DateTime(DateTime.Now.Year, _random.Next(1, 13), _random.Next(1, 29));

                _repository.AddOuting(new Outing(eventType, attendees, eventDate, costPerPerson));
            }
        }
    }

    public void Run()
    {
        bool keepRunning = true;
        while (keepRunning)
        {
            DisplayAllOutings();  // Display all outings every time the menu is shown.

            System.Console.WriteLine("\n---------------------< Komodo Outings Program >----------------------\n" +
                "1. Add Outing\n" +
                "2. Display Total Cost for All Outings\n" +
                "3. Display Total Costs by Outing Type\n" +
                "4. Exit");

            string input = System.Console.ReadLine();
            switch (input)
            {
                case "1":
                    AddOuting();
                    break;
                case "2":
                    DisplayTotalCostForAllOutings();
                    break;
                case "3":
                    DisplayTotalCostsByOutingType();
                    break;
                case "4":
                    keepRunning = false;
                    System.Console.WriteLine("Exiting...");
                    break;
                default:
                    System.Console.WriteLine("Please enter a valid number.");
                    break;
            }

            System.Console.WriteLine("Please press any key to continue...");
            System.Console.ReadKey();
            System.Console.Clear();
        }
    }

    private void AddOuting()
    {
        System.Console.WriteLine("Enter the type of event (Golf, Bowling, AmusementPark, Concert):");
        var eventType = (Outing.EventType)Enum.Parse(typeof(Outing.EventType), System.Console.ReadLine(), true);

        System.Console.WriteLine("Enter the number of people:");
        int numberOfPeople = int.Parse(System.Console.ReadLine());

        System.Console.WriteLine("Enter the date of the event (yyyy-mm-dd):");
        DateTime date = DateTime.ParseExact(System.Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture);

        System.Console.WriteLine("Enter the cost per person:");
        decimal costPerPerson = decimal.Parse(System.Console.ReadLine());

        Outing newOuting = new Outing(eventType, numberOfPeople, date, costPerPerson);
        _repository.AddOuting(newOuting);
        System.Console.WriteLine("Outing added successfully.");
    }

    private void DisplayAllOutings()
    {
        System.Console.WriteLine("Current List of Outings:");
        var outings = _repository.GetAllOutings();
        if (outings.Count == 0)
        {
            System.Console.WriteLine("No outings have been added yet.");
        }
        else
        {
            foreach (var outing in outings)
            {
                System.Console.WriteLine($"Type: {outing.Type}, Date: {outing.Date.ToShortDateString()}, People: {outing.NumberOfPeople}, Total Cost: {outing.TotalCost:C}");
            }
        }
    }

    private void DisplayTotalCostForAllOutings()
    {
        decimal totalCost = _repository.GetTotalCostForAllOutings();
        System.Console.WriteLine($"Total cost for all outings: {totalCost:C}");
    }

    private void DisplayTotalCostsByOutingType()
    {
        foreach (Outing.EventType type in Enum.GetValues(typeof(Outing.EventType)))
        {
            decimal costByType = _repository.GetTotalCostByEventType(type);
            System.Console.WriteLine($"Total cost for {type}: {costByType:C}");
        }
    }
}