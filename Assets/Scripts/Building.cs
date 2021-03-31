using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Building
{
    public DateTime date;
    public string name;
    public List<string> roles;
    public List<string> emails;
    public List<Commit> commits = new List<Commit>();
    public List<Change> changes = new List<Change>();
    public List<File> files = new List<File>();
    public List<Ticket> tickets = new List<Ticket>();
    public List<(string action, string file, string date)> commitedFiles = new List<(string, string, string)>();
    public bool show = true;

    public Building(Author author, DateTime date)
    {
        this.name = author.name;
        this.date = date;
        this.roles = author.roles;
        this.commits = author.commits.Where(commit => commit.created.Value == date).ToList();
        this.changes = author.changes.Where(change => change.created.Value == date).ToList();
        this.files = author.files.Where(files => files.created.Value == date).ToList();
        this.tickets = author.tickets.Where(tickets => tickets.created == date).ToList();
        this.commitedFiles = author.commitedFiles.Where(file => file.date == date.ToString("dd.MM.yyyy")).ToList();

        if (commits.Count == 0 && changes.Count == 0 && files.Count == 0)
        {
            show = false;
        }
    }
}
