//
// Project #06
// 
// SQL, C# and ADO.NET program to retrieve Netflix movie data.
//
// <<Harsh Devprakash Gupta>>
// 

using System;
using System.Data.SqlClient;
using System.Data;

namespace workspace
{
  class Program
  {
    
    //
    // Main:
    //
    static void Main(string[] args)
    {
      string connectionInfo = String.Format(@"
Server=tcp:jhummel2.database.windows.net,1433;Initial Catalog=Netflix;
Persist Security Info=False;User ID=student;Password=cs341!uic;
MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;
Connection Timeout=30;
");
      String input = "hello"; // initializing the string
      String sql, sql2; // the two sql queries used
      SqlConnection db = null; // initializing the SqlConnection
      
      try{ // implementing try and catch
      
              while(true){ // loop until user enters blank line

              System.Console.Write("movie> ");
              input = System.Console.ReadLine(); // read in the input
              if(string.IsNullOrEmpty(input)){ // if enter then exit the program
                      Environment.Exit(0); // exit the environment
                      //return;
              }
              input = input.Replace("'","''"); // handling special cases where the input has " ' "


              db = new SqlConnection(connectionInfo); // start the database connection
              db.Open(); // open the database 
              //sql = string.Format(@"SELECT * FROM Movies WHERE MovieName LIKE '%{0}%' ORDER BY MovieName ASC;", input);
              //sql = string.Format(@"SELECT MovieName, MovieYear, COUNT(ReviewDate) AS TotalReviews, AVG(Rating) AS Average FROM Movies INNER JOIN Reviews ON Movies.MovieID = Reviews.MovieID WHERE MovieName LIKE '%{0}%' ORDER BY MovieName ASC;", input);
              sql = string.Format(@"SELECT Movies.MovieName, Movies.MovieYear, Movies.MovieID, COUNT(Reviews.ReviewDate) AS TotalReviews, AVG(CAST(Reviews.Rating as Float)) AS AverageRating, sum(case when Reviews.Rating = 5 then 1 else 0 end) AS Rating5, sum(case when Reviews.Rating = 4 then 1 else 0 end) AS Rating4, sum(case when Reviews.Rating = 3 then 1 else 0 end) AS Rating3, sum(case when Reviews.Rating = 2 then 1 else 0 end) AS Rating2, sum(case when Reviews.Rating = 1 then 1 else 0 end) AS Rating1  FROM Movies LEFT JOIN Reviews ON Movies.MovieID = Reviews.MovieID WHERE Movies.MovieName LIKE '%{0}%' GROUP BY Movies.MovieName, Movies.MovieYear, Movies.MovieID ORDER BY Movies.MovieName ASC;
        ", input); // sql query to get all the data except the Rank
              sql2 = string.Format(@"SELECT Movies.MovieName, Movies.MovieID AS MovieID2, AVG(CAST(Reviews.Rating as Float)) AS AverageRating FROM Movies LEFT JOIN Reviews ON Movies.MovieID = Reviews.MovieID GROUP BY Movies.MovieName, Movies.MovieYear, Movies.MovieID ORDER BY AverageRating DESC, Movies.MovieName ASC;
        "); // sql query to get the rank

              // Command 2
              SqlCommand cmd2 = new SqlCommand();
              cmd2.Connection = db;
              SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);
              DataSet ds2 = new DataSet();
              cmd2.CommandText = sql2;
              adapter2.Fill(ds2);
              var rows2 = ds2.Tables[0].Rows; // table for command 2

              // Command 1      
              SqlCommand cmd = new SqlCommand();
              cmd.Connection = db;
              SqlDataAdapter adapter = new SqlDataAdapter(cmd);
              DataSet ds = new DataSet();

              cmd.CommandText = sql;
              adapter.Fill(ds);

              var rows = ds.Tables[0].Rows; // table for command 1
              //System.Console.WriteLine(rows.Count);
              //System.Console.WriteLine("Movie Name");
              if(rows.Count == 0){
                      System.Console.WriteLine("**none found");
              }

              foreach (DataRow row in rows){ // iterate through each dataset and print the data

                      int index = 0;  
                      string rank = "";
                      string movieName = System.Convert.ToString(row["MovieName"]);
                      string movieYear = System.Convert.ToString(row["MovieYear"]);
                      string movieID = System.Convert.ToString(row["MovieID"]);
                      foreach (DataRow row2 in rows2){ // iterate throug the second dataset to find the rank
                              string movieID2 = System.Convert.ToString(row2["MovieID2"]);
                              index++;

                              if(string.Equals(movieID2, movieID) == true){
                                      rank = System.Convert.ToString(index);
                              }


                      }
                      string totalReviews = System.Convert.ToString(row["TotalReviews"]);
                      string averageRating = System.Convert.ToString(row["AverageRating"]);
                      string rating5 = System.Convert.ToString(row["Rating5"]);
                      string rating4 = System.Convert.ToString(row["Rating4"]);
                      string rating3 = System.Convert.ToString(row["Rating3"]);
                      string rating2 = System.Convert.ToString(row["Rating2"]);
                      string rating1 = System.Convert.ToString(row["Rating1"]);
                      System.Console.WriteLine("'{0}', released {1}", movieName, movieYear);
                      if (!string.IsNullOrEmpty(averageRating)){ // If any movies does no have any rankings at all then print no ratings
                              System.Console.WriteLine(" Avg rating: {0} across {1} reviews [5,4,3,2,1: {2},{3},{4},{5},{6}] " , averageRating, totalReviews, rating5, rating4, rating3, rating2, rating1);

                      }
                      else{
                              System.Console.WriteLine(" Avg rating: <<no reviews>>");
                      }

                      System.Console.WriteLine(" Ranked {0} out of {1}", rank, index); // print the ranks

              }


              //db.Close();      

              }
      }
      catch (Exception ex){
              System.Console.WriteLine("Error:{0}", ex.Message); // handling any exception occured
      }
      finally{
              // Sucess or failure
              if (db != null && db.State != ConnectionState.Closed)
                      db.Close(); // make sure to close the database
      }
         
    }//Main
    
  }//class
}//namespace
