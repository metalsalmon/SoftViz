using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerLineManager : MonoBehaviour
{
    Ticket ticket;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setTicket(Ticket ticket)
    {
        this.ticket = ticket;
    }

    public Ticket getTicket()
    {
        return ticket;
    }
}
