﻿using System;
using System.Collections.Generic;
using AutomateBase;

public class Command 
{
    public Guid Id { get; private set; }
    public char ProcessedLetter { get; private set; }
    public Ticket PoppedTicket { get; private set; }
    public Node Node { get; private set; }
    public List<Ticket> PushedTickets { get; private set; }
    public int Index { get; private set; }

    public Command(char processedWord, char poppedTicketLetter, string pushedTicketLetters, Node node, int index)
    {
        Id = Guid.NewGuid();
        PushedTickets = new List<Ticket>();
        ProcessedLetter = processedWord;
        PoppedTicket = new Ticket(poppedTicketLetter);
        Node = node;
        Index = index;
        foreach (var letter in pushedTicketLetters)
        {
            PushedTickets.Add(new Ticket(letter));
        }
    }

    public bool IsCommandForTicket(Ticket ticket = null, char? ticketLetter = null)
    {
        if (ticket != null)
        {
            return PoppedTicket?.Letter == ticket.Letter;
        }

        if (ticketLetter.HasValue)
        {
            return PoppedTicket?.IsLetter(ticketLetter.Value) == true;
        }

        return false;
    }
}
