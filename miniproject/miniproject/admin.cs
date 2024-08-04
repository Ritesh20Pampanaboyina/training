﻿using System;

public static class admin
{
    public static void AddNewTrain()
    {
        Console.Write("Enter Train No: ");
        string trainNo = Console.ReadLine();
        Console.Write("Enter Train Name: ");
        string trainName = Console.ReadLine();
        Console.Write("Enter From: ");
        string trainFrom = Console.ReadLine();
        Console.Write("Enter To: ");
        string trainTo = Console.ReadLine();
        Console.Write("Enter Date (YYYY-MM-DD): ");
        DateTime date = DateTime.Parse(Console.ReadLine());
        Console.Write("Enter Price: ");
        decimal price = decimal.Parse(Console.ReadLine());
        Console.Write("Enter Status (Active/Inactive): ");
        string status = Console.ReadLine();
        Console.Write("Enter Number of Seats: ");
        int noOfSeats = int.Parse(Console.ReadLine());
        Console.Write("Enter Classes (e.g., 1AC,2AC,3AC,SL): ");
        string classes = Console.ReadLine();

        Train.AddTrain(trainNo, trainName, trainFrom, trainTo, date, price, status, noOfSeats, classes);
        Console.WriteLine("New train added successfully.");
    }

    public static void UpdateTrain()
    {
        Console.Write("Enter TrainID to update: ");
        int trainID = int.Parse(Console.ReadLine());

        Console.Write("Enter new Train No: ");
        string trainNo = Console.ReadLine();
        Console.Write("Enter new Train Name: ");
        string trainName = Console.ReadLine();
        Console.Write("Enter new From: ");
        string trainFrom = Console.ReadLine();
        Console.Write("Enter new To: ");
        string trainTo = Console.ReadLine();
        Console.Write("Enter new Date (YYYY-MM-DD): ");
        DateTime date = DateTime.Parse(Console.ReadLine());
        Console.Write("Enter new Price: ");
        decimal price = decimal.Parse(Console.ReadLine());
        Console.Write("Enter new Status (Active/Inactive): ");
        string status = Console.ReadLine();
        Console.Write("Enter new Number of Seats: ");
        int noOfSeats = int.Parse(Console.ReadLine());
        Console.Write("Enter new Classes (e.g., 1AC,2AC,3AC,SL): ");
        string classes = Console.ReadLine();

        DatabaseHelper.UpdateTrain(trainID, trainNo, trainName, trainFrom, trainTo, date, price, status, noOfSeats, classes);
        Console.WriteLine("Train updated successfully.");
    }
}