using Assets.Scripts.AutomateBase;
using System;
using System.Collections.Generic;

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
        foreach (char letter in pushedTicketLetters)
        {
            PushedTickets.Add(new Ticket(letter));
        }
    }

    public bool IsCommandForTicket(Ticket ticket = null, char? ticketLetter = null)
    {
        if (ticket != null)
        {
            return PoppedTicket?.Equals(ticket) == true;
        }

        if (ticketLetter.HasValue)
        {
            return PoppedTicket?.IsLetter(ticketLetter.Value) == true;
        }

        return false;
    }
}
