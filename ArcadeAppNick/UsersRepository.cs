using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using ArcadeAppNick.Models;
using SQLiteNetExtensions.Extensions;

namespace ArcadeAppNick;

public class UserRepository
{
    string dbPath;
    private SQLiteConnection conn;

    public UserRepository(string db)
    {
        dbPath = db;
    }

    private void Init()
    {
        //check if connection successful
        if (conn != null)
            return;
        //set connection
        conn = new SQLiteConnection(dbPath);
        //generate table (database)
        conn.CreateTable<Users>();
        conn.CreateTable<Cards>();
    }

    public void AddUser(string username, string password)
    {
        int result = 0;
        Init();

        if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
        {
            //inserts a new user into the database with a username and password
            result = conn.Insert(new Users { Username = username, Password = password });
        }
    }

    public Users GetUser(string username)
    {
        Init();
        //searches the databse for a entry with a matching username
        return conn.Table<Users>().Where(u => u.Username == username).FirstOrDefault();
    }

    public void AddCard(string name, string image, int attack, int hitpoint, string rarity)
    {
        int result = 0;
        Init();

        result = conn.Insert(new Cards { Name = name, Image = image, Attack = attack, Hitpoint = hitpoint, Rarity = rarity });
    }

    public Cards GetCard(string name)
    {
        Init();

        return conn.Table<Cards>().Where(u => u.Name == name).FirstOrDefault();
    }


    public void RemoveCard()
    {
        int result = 0;
        Init();

        result = conn.DeleteAll<Cards>();
    }

    public void UpdateUserAnimalCard(string username, string animalname, int value)
    {
        Init();

        if(username != null)
        {
            Users user = conn.Table<Users>().Where(u => u.Username == username).FirstOrDefault();
        
            if(user != null)
            {
                var column = typeof(Users).GetProperty(animalname);
                if(column != null && column.PropertyType == typeof(int))
                {
                    //set new value
                    column.SetValue(user, value);
                    //update user data
                    conn.Update(user);
                }
            }
        
        }
    }
}
