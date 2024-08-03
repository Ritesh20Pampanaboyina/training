using System;
using System.Data.SqlClient;

public static class DatabaseHelper
{
    private static string connectionString = "Server=YOUR_SERVER_NAME;Database=RailwayReservationDB;Trusted_Connection=True;";

    public static SqlConnection GetConnection()
    {
        return new SqlConnection(connectionString);
    }

    public static void ListTrains()
    {
        using (SqlConnection conn = GetConnection())
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Train", conn);
            SqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("Train List:");
            while (reader.Read())
            {
                Console.WriteLine($"TrainID: {reader["TrainID"]}, TrainNo: {reader["TrainNo"]}, TrainName: {reader["TrainName"]}, From: {reader["FromStation"]}, To: {reader["ToStation"]}, Date: {reader["Date"]}, Price: {reader["Price"]}, Seats Available: {reader["NoOfSeats"]}, Status: {reader["Status"]}");
            }
        }
    }

    public static void AddTrain(string trainNo, string trainName, string trainFrom, string trainTo, DateTime date, decimal price, string status, int noOfSeats, string classes)
    {
        using (SqlConnection conn = GetConnection())
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Train (TrainNo, TrainName, FromStation, ToStation, Date, Price, Status, NoOfSeats, Classes) VALUES (@TrainNo, @TrainName, @FromStation, @ToStation, @Date, @Price, @Status, @NoOfSeats, @Classes)", conn);
            cmd.Parameters.AddWithValue("@TrainNo", trainNo);
            cmd.Parameters.AddWithValue("@TrainName", trainName);
            cmd.Parameters.AddWithValue("@FromStation", trainFrom);
            cmd.Parameters.AddWithValue("@ToStation", trainTo);
            cmd.Parameters.AddWithValue("@Date", date);
            cmd.Parameters.AddWithValue("@Price", price);
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@NoOfSeats", noOfSeats);
            cmd.Parameters.AddWithValue("@Classes", classes);
            cmd.ExecuteNonQuery();
        }
    }

    public static void UpdateTrain(int trainID, string trainNo, string trainName, string trainFrom, string trainTo, DateTime date, decimal price, string status, int noOfSeats, string classes)
    {
        using (SqlConnection conn = GetConnection())
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Train SET TrainNo = @TrainNo, TrainName = @TrainName, FromStation = @FromStation, ToStation = @ToStation, Date = @Date, Price = @Price, Status = @Status, NoOfSeats = @NoOfSeats, Classes = @Classes WHERE TrainID = @TrainID", conn);
            cmd.Parameters.AddWithValue("@TrainNo", trainNo);
            cmd.Parameters.AddWithValue("@TrainName", trainName);
            cmd.Parameters.AddWithValue("@FromStation", trainFrom);
            cmd.Parameters.AddWithValue("@ToStation", trainTo);
            cmd.Parameters.AddWithValue("@Date", date);
            cmd.Parameters.AddWithValue("@Price", price);
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@NoOfSeats", noOfSeats);
            cmd.Parameters.AddWithValue("@Classes", classes);
            cmd.Parameters.AddWithValue("@TrainID", trainID);
            cmd.ExecuteNonQuery();
        }
    }

    public static int GetAvailableSeats(int trainID)
    {
        using (SqlConnection conn = GetConnection())
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT NoOfSeats FROM Train WHERE TrainID = @TrainID", conn);
            cmd.Parameters.AddWithValue("@TrainID", trainID);
            object result = cmd.ExecuteScalar();
            return result != null ? (int)result : 0;
        }
    }

    public static void ConfirmBooking(int trainID, int userID, int numberOfSeats)
    {
        using (SqlConnection conn = GetConnection())
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Booking (TrainID, UserID, NumberOfSeats, Status) VALUES (@TrainID, @UserID, @NumberOfSeats, 'Confirmed')", conn);
            cmd.Parameters.AddWithValue("@TrainID", trainID);
            cmd.Parameters.AddWithValue("@UserID", userID);
            cmd.Parameters.AddWithValue("@NumberOfSeats", numberOfSeats);
            cmd.ExecuteNonQuery();
        }
    }

    public static void UpdateSeatAvailability(int trainID, int bookedSeats)
    {
        using (SqlConnection conn = GetConnection())
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Train SET NoOfSeats = NoOfSeats - @BookedSeats WHERE TrainID = @TrainID", conn);
            cmd.Parameters.AddWithValue("@BookedSeats", bookedSeats);
            cmd.Parameters.AddWithValue("@TrainID", trainID);
            cmd.ExecuteNonQuery();
        }
    }

    public static void CancelBooking(int bookingID, int trainID, int numberOfSeats)
    {
        using (SqlConnection conn = GetConnection())
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Booking SET Status = 'Cancelled' WHERE BookingID = @BookingID", conn);
            cmd.Parameters.AddWithValue("@BookingID", bookingID);
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("UPDATE Train SET NoOfSeats = NoOfSeats + @NumberOfSeats WHERE TrainID = @TrainID", conn);
            cmd.Parameters.AddWithValue("@NumberOfSeats", numberOfSeats);
            cmd.Parameters.AddWithValue("@TrainID", trainID);
            cmd.ExecuteNonQuery();
        }
    }

    public static void AddUser(string username, string password, string fullName, string email, string phoneNumber, string userRole)
    {
        using (SqlConnection conn = GetConnection())
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO [User] (UserName, Password, FullName, Email, PhoneNumber, UserRole) VALUES (@UserName, @Password, @FullName, @Email, @PhoneNumber, @UserRole)", conn);
            cmd.Parameters.AddWithValue("@UserName", username);
            cmd.Parameters.AddWithValue("@Password", password);
            cmd.Parameters.AddWithValue("@FullName", fullName);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
            cmd.Parameters.AddWithValue("@UserRole", userRole);
            cmd.ExecuteNonQuery();
        }
    }

    public static int GetUserID(string username, string password)
    {
        using (SqlConnection conn = GetConnection())
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT UserID FROM [User] WHERE UserName = @UserName AND Password = @Password", conn);
            cmd.Parameters.AddWithValue("@UserName", username);
            cmd.Parameters.AddWithValue("@Password", password);
            object result = cmd.ExecuteScalar();
            return result != null ? (int)result : 0;
        }
    }
}
