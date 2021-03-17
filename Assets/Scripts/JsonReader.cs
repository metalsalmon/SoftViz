using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using SimpleJSON;
using System;

public class Author
{
    public int id;
    public string name;
    public List<string> roles;
    public List<string> emails;
    public Author(int id, string name, string[] roles , string[] emails)
    {
        this.id = id;
        this.name = name;
        this.roles = new List<string>(roles);
        this.emails = new List<string>(emails);
    }
}

public class Ticket
{
    public int id;
    public string name;
    public string description;
    public DateTime? created;
    public int number;
    public string assignee;
    public string type;
    public string priority;
    public DateTime? start;
    public DateTime? due;
    public double estimate;
    public double spent;
    public double progress;
    public string status;
    public string statusClass;

    public Ticket(int id, string name, string description, DateTime? created, int number, string assignee, string type, string priority, DateTime? start, DateTime? due, double estimate, double spent, double progress, string status, string statusClass)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.created = created;
        this.number = number;
        this.assignee = assignee;
        this.type = type;
        this.priority = priority;
        this.start = start;      
        this.due = due;
        this.estimate = estimate;
        this.spent = spent;
        this.progress = progress;
        this.status = status;
        this.statusClass = statusClass;
    }
}

public class File
{
    public int id;
    public string name;
    public string author;
    public DateTime? created;
    public string url;
    public int size;
    public File(int id, string name, string author, DateTime? created, string url, int size)
    {
        this.id = id;
        this.name = name;
        this.author = author;
        this.created = created;
        this.url = url;
        this.size = size;
    }
}

public class Change
{
    public int id;
    public string comment;
    public string author;
    public DateTime? created;
    public DateTime? commited;
    public string changes;

    public Change(int id, string comment, string author, DateTime? created, DateTime? commited, string changes)
    {
        this.id = id;
        this.comment = comment;
        this.author = author;
        this.created = created;
        this.commited = commited;
        this.changes = changes;
    }
}

public class Commit
{
    public int id;
    public string name;
    public string message;
    public string author;
    public DateTime? created;
    public string changes;
    public List<string> branches;
    public Commit(int id, string name, string message, string author, DateTime? created, string changes, string[] branches)
    {
        this.id = id;
        this.name = name;
        this.message = message;
        this.author = author;
        this.created = created;
        this.changes = changes;
        this.branches = new List<string>(branches);
    }
}

public class JsonReader
{
    public List<Author> authors = new List<Author>();
    public List<Commit> commits = new List<Commit>();
    public List<Change> changes = new List<Change>();
    public List<File> files = new List<File>();
    public List<Ticket> tickets = new List<Ticket>();
    public List<string> dates = new List<string>();


    public void LoadData(string datasetName)
    {
        TextAsset asset = Resources.Load("data/"+datasetName) as TextAsset;
        var data = JSON.Parse(asset.text);

        foreach (var vertex in data["vertices"].Values)
        {
            switch (vertex["archetype"].AsInt)
            {
                case 0:
                    authors.Add(new Author(vertex["id"], vertex["attributes"]["1"], ToStringArray(vertex["attributes"]["33"].AsArray), ToStringArray(vertex["attributes"]["6"].AsArray)));
                    break ;
                case 1:
                    tickets.Add(new Ticket(vertex["id"], vertex["attributes"]["1"], vertex["attributes"]["2"], ParseDate(vertex["attributes"]["9"]), vertex["attributes"]["11"].AsInt,
                        vertex["attributes"]["12"][0], vertex["attributes"]["13"][0], vertex["attributes"]["14"][0], ParseDate(vertex["attributes"]["16"]), ParseDate(vertex["attributes"]["17"]),
                        vertex["attributes"]["21"].AsDouble, vertex["attributes"]["22"].AsDouble, vertex["attributes"]["23"].AsDouble, vertex["attributes"]["19"][0], vertex["attributes"]["40"][0]));
                    break;
                case 2:
                    files.Add(new File(vertex["id"], vertex["attributes"]["1"], vertex["attributes"]["8"][0], ParseDate(vertex["attributes"]["9"]), vertex["attributes"]["10"], vertex["attributes"]["31"].AsInt));
                    break;
                case 4:
                    changes.Add(new Change(vertex["id"], vertex["attributes"]["3"], vertex["attributes"]["8"][0], ParseDate(vertex["attributes"]["9"]), ParseDate(vertex["attributes"]["26"]), vertex["attributes"]["27"]));
                    break;
                case 5:
                    commits.Add(new Commit(vertex["id"], vertex["attributes"]["1"], vertex["attributes"]["4"], vertex["attributes"]["8"][0], ParseDate(vertex["attributes"]["26"]), vertex["attributes"]["27"], ToStringArray(vertex["attributes"]["28"].AsArray)));
                    break;
            }
        }
        GetDates();
    }

    public static string[] ToStringArray(JSONArray arrayJson)
    {
        string[] array = new string[arrayJson.Count];
        int index = 0;
        foreach (JSONNode node in arrayJson)
        {
            array[index] = (string)node.ToString();
            index += 1;
        }
        return array;
    }

    public DateTime? ParseDate(string date)
    {
        if (!string.IsNullOrWhiteSpace(date))
        {
            return DateTime.Parse(date).Date;
        }
        else
        {
            return null;
        }
    }

    public void GetDates()
    {
       List<DateTime> dateList = new List<DateTime>();

        foreach (var commit in commits)
        {
            dateList.Add(commit.created.Value.Date);
        }
        dateList.Sort();
        dateList = dateList.Distinct().ToList();

        foreach (var item in dateList)
        {
        dates.Add(item.ToString("dd.MM.yyyy"));
        }
    }
}