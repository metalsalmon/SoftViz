using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PowerLine 
{
    public string name;
    public Ticket ticket;
    public bool show = true;

    public PowerLine(Ticket ticket)
    {
        this.name = ticket.name;
        this.ticket = ticket;


    }
}
