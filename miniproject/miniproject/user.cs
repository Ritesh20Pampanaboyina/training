﻿using System;

public static class UserFunctions
{
    public static void BookTickets()
    {
        Console.Write("Enter your username: ");
        string username = Console.ReadLine();

        Console.Write("Enter your password: ");
        string password = Console.ReadLine();

        int userID = DatabaseHelper.GetUserID(username, password);
        if (userID == 0)
        {
            Console.WriteLine("User not found. Please provide additional details to register:");

            Console.Write("Enter your full name: ");
            string fullName = Console.ReadLine();

            Console.Write("Enter your email: ");
            string email = Console.ReadLine();

            Console.Write("Enter your phone number: ");
            string phoneNumber = Console.ReadLine();

            DatabaseHelper.AddUser(username, password, fullName, email, phoneNumber, "User");
            userID = DatabaseHelper.GetUserID(username, password);

            Console.WriteLine("User registered successfully.");
        }

        DatabaseHelper.ListTrains();

        Console.Write("Enter TrainID to book tickets: ");
        int trainID = int.Parse(Console.ReadLine());

        Console.Write("Enter number of seats to book (max 5): ");
        int seats = int.Parse(Console.ReadLine());

        if (seats > 5)
        {
            Console.WriteLine("Cannot book more than 5 tickets at a time.");
            return;
        }

        int availableSeats = DatabaseHelper.GetAvailableSeats(trainID);

        if (availableSeats < seats)
        {
            Console.WriteLine("Not enough seats available.");
            return;
        }

        DatabaseHelper.ConfirmBooking(trainID, userID, seats);
        DatabaseHelper.UpdateSeatAvailability(trainID, seats);

        Console.WriteLine("Booking confirmed!");
    }

    public static void CancelTickets()
    {
        Console.Write("Enter your username: ");
        string username = Console.ReadLine();

        Console.Write("Enter your password: ");
        string password = Console.ReadLine();

        int userID = DatabaseHelper.GetUserID(username, password);

        if (userID == 0)
        {
            Console.WriteLine("User not found.");
            return;
        }

        Console.Write("Enter BookingID to cancel: ");
        int bookingID = int.Parse(Console.ReadLine());

        using (SqlConnection conn = DatabaseHelper.GetConnection())
        {
            conn.Open();

            SqlCommand getBookingCmd = new SqlCommand("SELECT TrainID, NumberOfSeats FROM Booking WHERE BookingID = @BookingID AND Status = 'Confirmed' AND UserID = @UserID", conn);
            getBookingCmd.Parameters.AddWithValue("@BookingID", bookingID);
            getBookingCmd.Parameters.AddWithValue("@UserID", userID);
            SqlDataReader reader = getBookingCmd.ExecuteReader();

            if (!reader.Read())
            {
                Console.WriteLine("Booking not found or already cancelled.");
                return;
            }

            int trainID = (int)reader["TrainID"];
            int numberOfSeats = (int)reader["NumberOfSeats"];
            reader.Close();

            DatabaseHelper.CancelBooking(bookingID, trainID, numberOfSeats);

            Console.WriteLine("Booking cancelled successfully.");
        }
    }
}
