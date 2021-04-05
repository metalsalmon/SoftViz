using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Building
{
    public int id;
    public DateTime dateFrom;
    public DateTime dateTo;
    public string name;
    public List<string> roles;
    public List<string> emails;
    public List<Commit> commits = new List<Commit>();
    public List<Change> changes = new List<Change>();
    public List<File> files = new List<File>();
    public List<Ticket> tickets = new List<Ticket>();
    public List<(string action, string file, DateTime date)> commitedFiles = new List<(string, string, DateTime)>();
    public bool show = true;

    public Building(Author author, DateTime dateFrom, DateTime dateTo, bool showCommits, bool showChanges, bool showCommitedFiles)
    {
        this.id = author.id;
        this.name = author.name;
        this.dateFrom = dateFrom;
        this.dateTo = dateTo;
        this.roles = author.roles;
        if (showCommits) this.commits = author.commits.Where(commit => commit.created.Value >= dateFrom && commit.created.Value <= dateTo).ToList();
        if (showChanges) this.changes = author.changes.Where(change => change.created.Value >= dateFrom && change.created.Value <= dateTo).ToList();
        //this.files = author.files.Where(files => files.created.Value >= dateFrom && files.created.Value <= dateTo).ToList();
        this.tickets = author.tickets.Where(tickets => tickets.created.Value >= dateFrom && tickets.created.Value <= dateTo).ToList();
        if(showCommitedFiles) this.commitedFiles = author.commitedFiles.Where(file => file.date >= dateFrom && file.date <= dateTo).ToList();

        if (commits.Count == 0 && changes.Count == 0 && files.Count == 0)
        {
            show = false;
        }
    }


}
