using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island
{
    public short index;
    public float positionX;
    public GameObject islandInstance;
    public DateTime? DateFrom;
    public DateTime? DateTo;
    public List<DateTime> dates;
    public bool show = false;

    public Island(short index, DateTime? DateFrom, DateTime? DateTo)
    {
        dates = new List<DateTime>();
        this.index = index;
        this.DateFrom = DateFrom;
        this.DateTo = DateTo;
    }

    public void AddDate(DateTime date)
    {
        dates.Add(date);
    }
}
