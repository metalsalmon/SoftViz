using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island
{
    public GameObject islandInstance;
    public DateTime? DateFrom;
    public DateTime? DateTo;
    public List<DateTime> dates;

    public Island(GameObject islandInstance, DateTime? DateFrom, DateTime? DateTo)
    {
        dates = new List<DateTime>();
        this.DateFrom = DateFrom;
        this.DateTo = DateTo;
        this.islandInstance = islandInstance;
    }

    public void AddDate(DateTime date)
    {
        dates.Add(date);
    }
}
